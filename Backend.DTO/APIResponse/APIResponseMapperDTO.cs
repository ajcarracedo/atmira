using Backend.DTO.NASAResponse;
using System.Linq;

namespace Backend.DTO.APIResponse
{
    public static class APIResponseMapperDTO
    {
        public static APIResponseDTO MaptoDTO(this Date d)
        {
            return new APIResponseDTO()
            {
                nombre = d.name,
                diametro = (d.estimated_diameter.kilometers.estimated_diameter_max + d.estimated_diameter.kilometers.estimated_diameter_min) / 2,
                velocidad = d.close_approach_data.ElementAt(0).relative_velocity.kilometers_per_hour,
                fecha = d.close_approach_data.ElementAt(0).close_approach_date,
                planeta = d.close_approach_data.ElementAt(0).orbiting_body
            };
        }
    }
}
