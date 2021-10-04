using Logic.Common.Constants;
using Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TariffComparison.Controllers
{
    [Route("api/tariffComparison")]
    [ApiController]
    public class TariffComparisonController : ControllerBase
    {
        private readonly ITariffComparisonLogic _tariffComparisonLogic;
        public TariffComparisonController(ITariffComparisonLogic tariffComparisonLogic)
        {
            _tariffComparisonLogic = tariffComparisonLogic;

        }

        [HttpGet()]
        public async Task<IActionResult> GetTariffComparison(int consumption)
        {
            try
            {
                if (consumption >= 0)
                    return Ok(await _tariffComparisonLogic.GetTariffComparison(consumption));
                else
                    return BadRequest(CommonConstants.InvalidInput);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Details: {ex.Message}");
                throw;
            }
        }
    }
}
