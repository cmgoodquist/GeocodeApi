using GeocodeApi.Geocode.DrivingDependencies;
using IntegrationTests.TestHelpers;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Geocode.DrivingDependencies
{
    public class PostToGeocodeShould
    {
        private readonly RandomValueGenerator _random;

        public PostToGeocodeShould() => _random = new RandomValueGenerator();

        [Fact]
        public async Task ReturnBadRequest_WhenRequestToExternalApiFails()
        {
            //Arrange
            var expectedResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest
            };
            using (var server = new StubTestServer(expectedResponse))
            {
                var client = server.CreateClient();
                var geocodeRequest = BuildRandomGeocodeRequest();

                //Act
                var response = await client.PostAsJsonAsync("api/v1/geocode", geocodeRequest);

                //Assert
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task ReturnExpectedContent_WhenRequestToExternalApiSucceeds()
        {
            //Arrange
            var expectedContent = _random.String();
            var expectedResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(expectedContent)
            };
            using (var server = new StubTestServer(expectedResponse))
            {
                var client = server.CreateClient();
                var geocodeRequest = BuildRandomGeocodeRequest();

                //Act
                var response = await client.PostAsJsonAsync("api/v1/geocode", geocodeRequest);
                response.EnsureSuccessStatusCode();
                var actualContent = await response.Content.ReadAsStringAsync();

                //Assert
                Assert.Equal(expectedContent, actualContent);
            }
        }

        private GeocodeRequest BuildRandomGeocodeRequest() => new GeocodeRequest()
        {
            Street = _random.String(),
            City = _random.String(),
            StateCode = _random.String(2),
            ZipCode = _random.String()
        };

    }
}
