using GeocodeApi.Geocode.DrivenDependencies;
using IntegrationTests.TestHelpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    public class GeocodeServiceTests
    {
        public static IEnumerable<object[]> ValidAddressData()
        {
            yield return new[] { "street", "city", "state", "zip" };
            yield return new[] { "street", "city", "state", null };
            yield return new[] { "street", "city", "state", string.Empty };
        }

        public static IEnumerable<object[]> InvalidAddressData()
        {
            yield return new[] { null, "city", "state", "zip" };
            yield return new[] { "street", null, "state", "zip" };
            yield return new[] { "street", "city", null, "zip" };
        }

        [Theory]
        [MemberData(nameof(ValidAddressData))]
        public async Task GeocodeAddress_ReturnsExpectedContent_WhenCalledWithValidAddress(string street, string city, string state, string zip)
        {
            //Arrange
            var expectedContent = nameof(GeocodeAddress_ReturnsExpectedContent_WhenCalledWithValidAddress);
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
        [MemberData(nameof(InvalidAddressData))]
        public async Task GeocodeAddress_ThrowsInvalidAddressException_WhenCalledWithInvalidAddress(string street, string city, string state, string zip)
        {
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                //Arrange
                var expectedContent = nameof(GeocodeAddress_ThrowsInvalidAddressException_WhenCalledWithInvalidAddress);
                using(var client = SetUpStub(expectedContent))
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
