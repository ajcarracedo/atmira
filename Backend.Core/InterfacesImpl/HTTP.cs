using Backend.Core.Interfaces;
using Backend.DTO.NASAResponse;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Backend.Core.InterfacesImpl
{
    public class Http : IHttp
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<NasaResponseDTO> Request(string url)
        {
            NasaResponseDTO ret = new NasaResponseDTO();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    ret = JsonConvert.DeserializeObject<NasaResponseDTO>(response.Content.ReadAsStringAsync().Result);
                }
            }

            return ret;
        }
    }
}
