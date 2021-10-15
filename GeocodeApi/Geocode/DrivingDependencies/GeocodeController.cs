using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GeocodeApi.Geocode.DrivingDependencies
{
    [ApiController]
    [Route("api/v1/geocode")]
    public class GeocodeController : ControllerBase
    {
        private readonly IGeocodeService _service;

        public GeocodeController(IGeocodeService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> GeocodeAddress(GeocodeRequest geocodeRequest)
        {
            var response = await _service.GeocodeAddress(geocodeRequest.Street, geocodeRequest.City, geocodeRequest.StateCode, geocodeRequest.ZipCode);
            if (!response.IsSuccessStatusCode)
                return BadRequest(response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }
    }
}
