using Backend.Constant;
using Backend.DTO.APIResponse;
using Backend.DTO.NASAResponse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
                if (!String.IsNullOrEmpty(planet) && !String.IsNullOrWhiteSpace(planet) && Parameters.VALID_PLANETS.Contains(planet, StringComparer.CurrentCultureIgnoreCase))
                {
                    //The URL for the external request is created
                    string url = GetNasaNeoUrl();

                    //The request is made
                    NasaResponseDTO nasaResponseDTO = await NasaRequest(url);

                    //The required data is searched
                    List<APIResponseDTO> apiResponseDTO = DataTreatment(nasaResponseDTO, planet);
                    ret = JsonConvert.SerializeObject(apiResponseDTO);
                }
                else
                {
                    _logger.LogError(ResponseMessages.INVALID_PARAM, planet);
                    ret = ResponseMessages.INVALID_PARAM;
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

        private string GetNasaNeoUrl()
        {
            string ret = string.Empty;

            DateTime startDate = DateTime.UtcNow;
            DateTime endDate = startDate.AddDays(7);

            ret = $"https://api.nasa.gov/neo/rest/v1/feed?start_date={startDate:yyyy-MM-dd}&end_date={endDate:yyyy-MM-dd}&api_key={NASA.API_KEY}";

            return ret;
        }

        private async Task<NasaResponseDTO> NasaRequest(string url)
        {
            NasaResponseDTO ret = new NasaResponseDTO();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    ret = JsonConvert.DeserializeObject<NasaResponseDTO>(response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    ret = null;
                }
            }

            return ret;
        }

        private List<APIResponseDTO> DataTreatment(NasaResponseDTO nasaResponseDTO, string planet)
        {
            List<APIResponseDTO> ret = new List<APIResponseDTO>();

            ret = nasaResponseDTO.near_earth_objects.Values.SelectMany(sm => sm).AsEnumerable()
                                                      .Where(w => w.is_potentially_hazardous_asteroid)
                                                      .Where(w2 => w2.close_approach_data[0].orbiting_body?.Equals(planet, StringComparison.CurrentCultureIgnoreCase) == true)
                                                      .OrderByDescending(obd => (obd.estimated_diameter.kilometers.estimated_diameter_max + obd.estimated_diameter.kilometers.estimated_diameter_min) / 2)
                                                      .GroupBy(gb => gb.neo_reference_id)
                                                      .Take(3)
                                                      .Select(s => s.FirstOrDefault().MaptoDTO())
                                                      .ToList();

            return ret;
        }
    }
}