using System.Net.Http;
using System.Threading.Tasks;

namespace GeocodeApi.Geocode
{
    public interface IGeocodeService
    {
        Task<HttpResponseMessage> GeocodeAddress(string street, string city, string stateCode, string zipCode);
    }
}
