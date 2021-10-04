using Logic.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
   public interface ITariffComparisonLogic
    {
        Task<TariffComparisonResponse> GetTariffComparison(int consumpton);
    }
}
