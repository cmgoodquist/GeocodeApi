using IntegrationTests.TestHelpers;
using System.Linq;
using Xunit;

namespace IntegrationTests.Geocode.DrivenDependencies
{
    internal class GeocodeAddressTheoryData
    {
        public static TheoryData<string, string, string, string> ValidAddressData()
        {
            var data = new TheoryData<string, string, string, string>();
            var random = new RandomValueGenerator();
            var validZipCodeValues = new[] { random.String(5), random.String(9), string.Empty, null, "  " };
            foreach (var zipCode in validZipCodeValues)
                data.Add(random.String(), random.String(), random.String(2), zipCode);

            return data;
        }

        public static TheoryData<string, string, string, string> InvalidAddressData()
        {
            var data = new TheoryData<string, string, string, string>();
            var random = new RandomValueGenerator();
            var validInputs = new[] { random.String(), random.String(), random.String(2), random.String(6) };
            var invalidInputs = new[] { null, string.Empty, "  " };
            //Test every bad value for every relevant input.
            foreach (var invalidInput in invalidInputs)
            {
                for (var i = 0; i < 3; i++)
                {
                    var invalidInputList = validInputs.ToList();
                    invalidInputList[i] = invalidInput;
                    data.Add(invalidInputList[0], invalidInputList[1], invalidInputList[2], invalidInputList[3]);
                }
            }
            //Test additional (length) constraint on state code input.
            data.Add(random.String(), random.String(), random.String(), random.String());
            //Test additional (length) constraint on zip code input.
            data.Add(random.String(), random.String(), random.String(2), random.String());
            return data;
        }
    }
}
