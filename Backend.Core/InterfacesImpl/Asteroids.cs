using Backend.Constant;
using Backend.Core.Interfaces;
using Backend.DTO.APIResponse;
using Backend.DTO.NASAResponse;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Backend.Core.InterfacesImpl
{
    public class Asteroids : IAsteroids
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetNasaNeoUrl()
        {
            string ret = string.Empty;

            DateTime startDate = DateTime.UtcNow;
            DateTime endDate = startDate.AddDays(7);

            ret = $"https://api.nasa.gov/neo/rest/v1/feed?start_date={startDate:yyyy-MM-dd}&end_date={endDate:yyyy-MM-dd}&api_key={Constants.NASA.API_KEY}";

            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nasaResponseDTO"></param>
        /// <param name="planet"></param>
        /// <returns></returns>
        public List<APIResponseDTO> DataTreatment(NasaResponseDTO nasaResponseDTO, string planet)
        {
            List<APIResponseDTO> ret = new List<APIResponseDTO>();

            if (nasaResponseDTO.near_earth_objects!= null && nasaResponseDTO.near_earth_objects.Values.SelectMany(sm => sm).Any())
            {
                ret = nasaResponseDTO.near_earth_objects.Values.SelectMany(sm => sm).AsEnumerable()
                                                               .Where(w => w.is_potentially_hazardous_asteroid)
                                                               .Where(w2 => w2.close_approach_data[0].orbiting_body?.Equals(planet, StringComparison.CurrentCultureIgnoreCase) == true)
                                                               .OrderByDescending(obd => (obd.estimated_diameter.kilometers.estimated_diameter_max + obd.estimated_diameter.kilometers.estimated_diameter_min) / 2)
                                                               .GroupBy(gb => gb.neo_reference_id)
                                                               .Take(3)
                                                               .Select(s => s.FirstOrDefault().MaptoDTO())
                                                               .ToList();
            }

            return ret;
        }
    }
}
