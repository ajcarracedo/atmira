using Backend.Core.Interfaces;
using Backend.DTO.NASAResponse;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public async Task<JObject> Request(string url)
        {
            JObject ret = new JObject();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    ret = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    //todo:logger
                }
            }

            return ret;
        }
    }
}
