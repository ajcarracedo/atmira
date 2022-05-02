using Backend.DTO.NASAResponse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Core.Interfaces
{
    public interface IHttp
    {
        Task<NasaResponseDTO> Request(string url);
    }
}
