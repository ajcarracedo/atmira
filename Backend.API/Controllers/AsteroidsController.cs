using Backend.Constant;
using Backend.Core.Injections;
using Backend.DTO.APIResponse;
using Backend.DTO.NASAResponse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
                    string url = CoreKernel.retrieve().getAsteroids().GetNasaNeoUrl();

                    //The request is made
                    NasaResponseDTO nasaResponseDTO = await CoreKernel.retrieve().getAsteroids().NasaRequest(url);

                    //The required data is searched
                    List<APIResponseDTO> apiResponseDTO = CoreKernel.retrieve().getAsteroids().DataTreatment(nasaResponseDTO, planet);
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