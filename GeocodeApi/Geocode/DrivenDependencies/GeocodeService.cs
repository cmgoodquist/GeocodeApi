using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GeocodeApi.Geocode.DrivenDependencies
{
    public class GeocodeService : IGeocodeService
    {
        public Task<HttpResponseMessage> GeocodeAddress(string street, string city, string stateCode, string zipCode)
        {
            throw new NotImplementedException();
        }
    }
}
