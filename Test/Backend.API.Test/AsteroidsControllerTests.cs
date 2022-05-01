using Backend.API.Controllers;
using Backend.Constant;
using Backend.DTO;
using Backend.DTO.APIResponse;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Backend.API.Test
{
    public class AsteroidsControllerTests
    {
        private readonly AsteroidsController _sut;
        private readonly Mock<ILogger<AsteroidsController>> _loggerMock = new Mock<ILogger<AsteroidsController>>();

        public AsteroidsControllerTests()
        {
            _sut = new AsteroidsController(_loggerMock.Object);
        }

        [Fact]
        public async Task Get_ShouldReturnThreeObjetsAtMost_WhenParamIsCorrect()
        {
            //Arrange


            //Act
            var ret = await _sut.Get("earth");
            List<APIResponseDTO> response = JsonConvert.DeserializeObject<List<APIResponseDTO>>(ret.Value);

            //Assert
            Assert.True(response.Count <= 3);
        }

        [Fact]
        public async Task Get_ShouldReturnErrorMessage_WhenParamIsNull()
        {
            //Arrange

            //Act
            var ret = await _sut.Get(null);

            //Assert
            Assert.Equal(ret.Value, ResponseMessages.INVALID_PARAM);
        }

        [Fact]
        public async Task Get_ShouldReturnErrorMessage_WhenParamIsNotAPlanet()
        {
            //Arrange

            //Act
            var ret = await _sut.Get("stuff");

            //Assert
            Assert.Equal(ret.Value, ResponseMessages.INVALID_PARAM);
        }

    }
}
