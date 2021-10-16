using GeocodeApi.Geocode.DrivenDependencies;
using IntegrationTests.TestHelpers;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Geocode.DrivenDependencies
{
    public class GeocodeAddressShould
    {
        [Theory]
        [MemberData(nameof(GeocodeAddressTheoryData.ValidAddressData), MemberType = typeof(GeocodeAddressTheoryData))]
        public async Task ReturnExpectedContent_WhenCalledWithValidAddress(string street, string city, string state, string zip)
        {
            //Arrange
            var expectedContent = nameof(ReturnExpectedContent_WhenCalledWithValidAddress);
            using (var client = SetUpStub(expectedContent))
            {
                var service = new GeocodeService(client);

                //Act
                var actualResponse = await service.GeocodeAddress(street, city, state, zip);
                var actualContent = await actualResponse.Content.ReadAsStringAsync();

                //Assert
                Assert.Equal(expectedContent, actualContent);
            }
        }

        [Theory]
        [MemberData(nameof(GeocodeAddressTheoryData.InvalidAddressData), MemberType = typeof(GeocodeAddressTheoryData))]
        public async Task ThrowInvalidAddressException_WhenCalledWithInvalidAddress(string street, string city, string state, string zip)
        {
            await Assert.ThrowsAsync<InvalidAddressException>(async () =>
            {
                //Arrange
                var expectedContent = nameof(ThrowInvalidAddressException_WhenCalledWithInvalidAddress);
                using (var client = SetUpStub(expectedContent))
                {
                    var service = new GeocodeService(client);

                    //Act
                    var actualResponse = await service.GeocodeAddress(street, city, state, zip);
                }
            });
        }

        private HttpClient SetUpStub(string expectedResponseContent)
        {
            var expectedResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(expectedResponseContent)
            };
            return new HttpClient(new StubHttpMessageHandler(expectedResponse))
            {
                BaseAddress = new Uri("https://baseAddress")
            };
        }

    }
}
