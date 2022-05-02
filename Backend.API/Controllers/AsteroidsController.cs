using Backend.Constant;
using Backend.Core.Injections;
using Backend.DTO.APIResponse;
using Backend.DTO.NASAResponse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AsteroidsController : ControllerBase
    {
        #region Configuration
        private readonly ILogger<AsteroidsController> _logger;

        public AsteroidsController(ILogger<AsteroidsController> logger)
        {
            _logger = logger;
        }
        #endregion

        [HttpGet]
        public async Task<ActionResult<string>> Get(string planet)
        {
            string ret = string.Empty;

            try
            {
                //The input parameter is validated
                if (!String.IsNullOrEmpty(planet) && !String.IsNullOrWhiteSpace(planet) && Constants.Parameters.VALID_PLANETS.Contains(planet, StringComparer.CurrentCultureIgnoreCase))
                {
                    //The URL for the external request is created
                    DateTime startDate = DateTime.UtcNow;
                    DateTime endDate = startDate.AddDays(7);
                    string url = CoreKernel.Retrieve().GetAsteroids().GetNasaNeoUrl(startDate, endDate);

                    //The request is made
                    JObject nasaResponseJObject = await CoreKernel.Retrieve().GetHttp().Request(url);
                    NasaResponseDTO nasaResponseDTO = nasaResponseJObject.ToObject<NasaResponseDTO>();

                    //The required data is searched
                    List<APIResponseDTO> apiResponseDTO = CoreKernel.Retrieve().GetAsteroids().TopThreeWithMaximunDiameterPotentiallyHazardous(nasaResponseDTO, planet);
                    ret = JsonConvert.SerializeObject(apiResponseDTO);
                }
                else
                {
                    _logger.LogError(Constants.ResponseMessages.INVALID_PARAM, planet);
                    ret = Constants.ResponseMessages.INVALID_PARAM;
                }
            }
            catch (Exception exc)
            {
                ret = $"Internal Server error: {exc.Message}.";
                _logger.LogCritical($"Internal Server error: {exc.Message}.", planet);

                if (exc.InnerException != null)
                {
                    _logger.LogCritical($"Internal Server error: {exc.InnerException.Message}.", planet);
                }
            }

            //The result is returned
            return ret;
        }
    }
}