using GeocodeApi.Geocode.DrivingDependencies;
using IntegrationTests.TestHelpers;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    public class GeocodeControllerTests
    {
        [Fact]
        public async Task GeocodeAddress_ReturnsBadRequest_WhenRequestFails()
        {
            //Arrange
            var expectedResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest
            };
            using (var server = new StubTestServer(expectedResponse))
            {
                var client = server.CreateClient();
                var geocodeRequest = new GeocodeRequest()
                {
                    Street = "street",
                    City = "city",
                    StateCode = "st",
                    ZipCode = "zip"
                };

                //Act
                var response = await client.PostAsJsonAsync("api/v1/geocode", geocodeRequest);

                //Assert
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task GeocodeAddress_ReturnsExpectedContent_WhenRequestSucceeds()
        {
            //Arrange
            var expectedContent = "arbitrary expectation";
            var expectedResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(expectedContent)
            };
            using (var server = new StubTestServer(expectedResponse))
            {
                var client = server.CreateClient();
                var geocodeRequest = new GeocodeRequest()
                {
                    Street = "street",
                    City = "city",
                    StateCode = "st",
                    ZipCode = "zip"
                };

                //Act
                var response = await client.PostAsJsonAsync("api/v1/geocode", geocodeRequest);
                response.EnsureSuccessStatusCode();
                var actualContent = await response.Content.ReadAsStringAsync();

                //Assert
                Assert.Equal(expectedContent, actualContent);
            }
        }

    }
}
