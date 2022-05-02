using Backend.DTO.NASAResponse;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Core.Interfaces
{
    public interface IHttp
    {
        Task<JObject> Request(string url);
    }
}
