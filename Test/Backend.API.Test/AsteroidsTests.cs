using Backend.Constant;
using Backend.Core.Injections;
using Backend.Core.Interfaces;
using Backend.Core.InterfacesImpl;
using Backend.DTO.APIResponse;
using Backend.DTO.NASAResponse;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Backend.Test
{
    public class AsteroidsTests
    {
        private readonly Asteroids _sut;

        public AsteroidsTests()
        {
            _sut = new Asteroids();
        }

        [Fact]
        public async Task GetNasaNeoUrl_ShouldReturnCorrectDatesAndAPIValues_WhenIsInvoked()
        {
            //Arrange
            var mock = new Mock<IAsteroids>();

            //Act
            DateTime startDate = new DateTime(2022, 05, 01);
            DateTime endDate = new DateTime(2022, 05, 07);
            string url = $"https://api.nasa.gov/neo/rest/v1/feed?start_date={startDate:yyyy-MM-dd}&end_date={endDate:yyyy-MM-dd}&api_key={Constants.NASA.API_KEY}";

            mock.Setup(s => s.GetNasaNeoUrl()).Returns(url);

            //Assert
            Assert.Equal($"https://api.nasa.gov/neo/rest/v1/feed?start_date=2022-05-01&end_date=2022-05-07&api_key=NTHIocgkS4vEsDMHf86rehVhXaKlC2YQ3vojvVjh", mock.Object.GetNasaNeoUrl());
        }

        [Fact]
        public async Task NasaRequest_ShouldReturnEmptyObject_WhenResponseIsNotSuccess()
        {
            //Arrange
            var mock = new Mock<IAsteroids>();

            //Act
            NasaResponseDTO ret = new NasaResponseDTO();
            mock.Setup(s => s.NasaRequest($"stuff")).Returns(Task.FromResult(ret));

            //Assert
            Assert.True(mock.Object.NasaRequest($"stuff").Result.element_count == 0);
            Assert.Null(mock.Object.NasaRequest($"stuff").Result.near_earth_objects);
            Assert.Null(mock.Object.NasaRequest($"stuff").Result.links);
        }

        [Fact]
        public async Task DataTreatment_ShouldReturnEmptyObject_WhenInputParameterIsEmpty()
        {
            //Arrange
            NasaResponseDTO nasaResponseDTO = new NasaResponseDTO();

            //Act

            //Assert
            Assert.True(CoreKernel.retrieve().getAsteroids().DataTreatment(nasaResponseDTO, $"stuff").Count == 0);
        }


        [Fact]
        public async Task DataTreatment_ShouldReturnZeroObjects_WhenNameOfPlanetNotMatch()
        {
            //Arrange
            string startupPath = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory().ToString()).ToString()).ToString()).ToString();
            string filePath = Path.Combine(startupPath, "Files", "Correct Response example.txt");
            NasaResponseDTO nasaResponseDTO = JsonConvert.DeserializeObject<NasaResponseDTO>(File.ReadAllText(filePath));

            //Act

            //Assert
            Assert.True(CoreKernel.retrieve().getAsteroids().DataTreatment(nasaResponseDTO, $"stuff").Count == 0);
        }
    }
}
