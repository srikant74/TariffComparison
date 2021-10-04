using Logic.Common.Constants;
using Logic.Common.Response;
using Logic.Interfaces;
using Logic.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DomainLogic
{
    public class TariffComparisonLogic : ITariffComparisonLogic
    {
        private readonly ITariff<BasicElectricityTariff> _basicElectricityTariff;
        private readonly ITariff<PackagedTariff> _packagedTariff;

        public TariffComparisonLogic(ITariff<BasicElectricityTariff> basicElectricityTariff, ITariff<PackagedTariff> packagedTariff)
        {
            _basicElectricityTariff = basicElectricityTariff;
            _packagedTariff = packagedTariff;
        }
        public async Task<TariffComparisonResponse> GetTariffComparison(int consumpton)
        {
            try
            {
                List<TariffCosts> aggregatedCosts = new List<TariffCosts>();
                TariffCosts basicTariffCost = null, packagesTariffCost = null;
                TariffComparisonResponse tariffComparisonResponse = null;

                Parallel.Invoke
                (
                     () => basicTariffCost = _basicElectricityTariff.calculateAnnualCost(consumpton),
                     () => packagesTariffCost = _packagedTariff.calculateAnnualCost(consumpton)
                );

                aggregatedCosts.Add(basicTariffCost);
                aggregatedCosts.Add(packagesTariffCost);

                string responseMessage = string.Empty;

                if (basicTariffCost.AnnualCost > packagesTariffCost.AnnualCost)
                {
                    decimal costSaved = basicTariffCost.AnnualCost - packagesTariffCost.AnnualCost;
                    responseMessage = CommonConstants.IfYourTariff + consumpton + CommonConstants.Choosing + PackagedTariffConstants.TariffName +
                        CommonConstants.WillBeBetter + consumpton + CommonConstants.YouCanSave + costSaved + CommonConstants.ByChoosing + PackagedTariffConstants.TariffName;
                }
                else
                {
                    decimal costSaved = packagesTariffCost.AnnualCost - basicTariffCost.AnnualCost;
                    responseMessage = CommonConstants.IfYourTariff + consumpton + CommonConstants.Choosing + BasicElectricityTariffConstants.TariffName +
                       CommonConstants.WillBeBetter + consumpton + CommonConstants.YouCanSave + costSaved + CommonConstants.ByChoosing + BasicElectricityTariffConstants.TariffName;
                }

                await Task.Run(() =>
                {
                    tariffComparisonResponse = new TariffComparisonResponse
                    {
                        AllTariffCostDetails = aggregatedCosts.OrderBy(s => s.AnnualCost).ToList(),
                        OurSuggestion = responseMessage
                    };
                });

                return tariffComparisonResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Details: {ex.Message}");
                return new TariffComparisonResponse();
            }

        }

    }
}
