using GeocodeApi.Geocode.DrivenDependencies;
using IntegrationTests.TestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Geocode.DrivenDependencies
{
    public class GeocodeAddressShould
    {
        public static IEnumerable<object[]> ValidAddressData()
        {
            var random = new RandomValueGenerator();
            var validZipCodeValues = new[] { random.String(), string.Empty, null, "  " };
            foreach(var zipCode in validZipCodeValues)
                yield return new[] { random.String(), random.String(), random.String(2), zipCode };
        }

        public static IEnumerable<object[]> InvalidAddressData()
        {
            var random = new RandomValueGenerator();
            var validInputs = new [] { random.String(), random.String(), random.String(2), random.String() };
            var invalidInputs = new[] { null, string.Empty, "  " };
            foreach(var invalidInput in invalidInputs)
            {
                for(var i = 0; i < 3; i++)
                {
                    var invalidInputList = validInputs.ToList();
                    invalidInputList[i] = invalidInput;
                    yield return invalidInputList.ToArray();
                }

            }
            yield return new[] { random.String(), random.String(), random.String(), random.String() };
        }

        [Theory]
        [MemberData(nameof(ValidAddressData))]
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
        [MemberData(nameof(InvalidAddressData))]
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
