using GeocodeApi.Geocode.DrivenDependencies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    public class GeocodeServiceTests
    {
        [Fact]
        public async Task GeocodeAddress_ReturnsGeocodeResponse_WhenCalledWithValidData()
        {
            //Arrange
            var street = "street";
            var city = "city";
            var state = "state";
            var zip = "zip";
            var service = new GeocodeService();

            //Act
            var response = await service.GeocodeAddress(street, city, state, zip);

            //Assert
            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
