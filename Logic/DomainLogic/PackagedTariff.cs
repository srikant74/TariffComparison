using Logic.Common.Constants;
using Logic.Interfaces;
using Logic.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DomainLogic
{
    public class PackagedTariff : ITariff<PackagedTariff>
    {
        public TariffCosts calculateAnnualCost(int consumedElectricity)
        {
            try
            {
                TariffCosts tariffCostDetails = new TariffCosts
                {
                    TariffName = PackagedTariffConstants.TariffName
                };
                if (consumedElectricity <= PackagedTariffConstants.BasicConsumptionLimit)
                {
                    tariffCostDetails.AnnualCost = PackagedTariffConstants.BasicConsumptionCost;
                }
                else
                {
                    decimal extraConsumedPower = consumedElectricity - PackagedTariffConstants.BasicConsumptionLimit;
                    decimal extraConsumptionCost = (decimal)extraConsumedPower * PackagedTariffConstants.ExtraConsumptionCostPerkWh / (decimal)100;
                    tariffCostDetails.AnnualCost = Convert.ToDecimal(PackagedTariffConstants.BasicConsumptionCost + extraConsumptionCost);
                }
                return tariffCostDetails;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Details: {ex.Message}");
                return new TariffCosts();
            }
        }
    }
}
