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
    public class BasicElectricityTariff : ITariff<BasicElectricityTariff>
    {
        public TariffCosts calculateAnnualCost(int consumedElectricity)
        {
            try
            {
                decimal yearlyBaseCost = BasicElectricityTariffConstants.BaseCostPerMonth * 12;
                decimal yearlyConsumedCost = (decimal)(consumedElectricity * BasicElectricityTariffConstants.ConsumptionCostPerkWh) / (decimal)100;
                decimal totalCost = yearlyBaseCost + yearlyConsumedCost;
                return new TariffCosts
                {
                    TariffName = BasicElectricityTariffConstants.TariffName,
                    AnnualCost = totalCost
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Details: {ex.Message}");
                return new TariffCosts();
            }

        }
    }
}
