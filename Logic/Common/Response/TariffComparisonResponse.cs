using Logic.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Common.Response
{
    public class TariffComparisonResponse
    {
        public List<TariffCosts> AllTariffCostDetails { get; set; }
        public string OurSuggestion { get; set; }
    }
}
