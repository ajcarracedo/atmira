using Backend.DTO.APIResponse;
using Backend.DTO.NASAResponse;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Core.Interfaces
{
    public interface IAsteroids
    {
        string GetNasaNeoUrl(DateTime startDate, DateTime endDate);

        List<APIResponseDTO> TopThreeWithMaximunDiameterPotentiallyHazardous(NasaResponseDTO nasaResponseDTO, string planet);
    }
}
