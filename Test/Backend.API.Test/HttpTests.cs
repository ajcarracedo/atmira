using Backend.Constant;
using Backend.Core.Injections;
using Backend.Core.Interfaces;
using Backend.Core.InterfacesImpl;
using Backend.DTO.NASAResponse;
using Moq;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Backend.Test
{
    public class HttpTests
    {
        private readonly Http _sut;

        public HttpTests()
        {
            _sut = new Http();
        }

        [Fact]
        public async Task Request_ShouldReturnEmptyObject_WhenResponseIsNotSuccess()
        {
            //Arrange
            var mock = new Mock<IHttp>();

            //Act
            NasaResponseDTO ret = new NasaResponseDTO();
            mock.Setup(s => s.Request($"stuff")).Returns(Task.FromResult(ret));

            //Assert
            Assert.True(mock.Object.Request($"stuff").Result.element_count == 0);
            Assert.Null(mock.Object.Request($"stuff").Result.near_earth_objects);
            Assert.Null(mock.Object.Request($"stuff").Result.links);
        }
    }
}
