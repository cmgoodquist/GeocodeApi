using Microsoft.AspNetCore.Mvc;
using System;

namespace GeocodeApi.Geocode.DrivingDependencies
{
    [ApiController]
    [Route("api/v1/geocode")]
    public class GeocodeController : ControllerBase
    {
        private readonly IGeocodeService _service;

        public GeocodeController(IGeocodeService service) => _service = service;

        [HttpGet]
        public IActionResult GetGeocode()
        {
            throw new NotImplementedException();
        }
    }
}
