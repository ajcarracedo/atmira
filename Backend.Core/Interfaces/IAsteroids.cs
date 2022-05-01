using Backend.DTO.APIResponse;
using Backend.DTO.NASAResponse;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Core.Interfaces
{
    public interface IAsteroids
    {
        string GetNasaNeoUrl();

        Task<NasaResponseDTO> NasaRequest(string url);

        List<APIResponseDTO> DataTreatment(NasaResponseDTO nasaResponseDTO, string planet);
    }
}
