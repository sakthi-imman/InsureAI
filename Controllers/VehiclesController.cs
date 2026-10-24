using Microsoft.AspNetCore.Mvc;
using InsureAI.Models;
using InsureAI.Services;

namespace InsureAI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        // GET api/vehicles/{registrationNumber}
        [HttpGet("{registrationNumber}")]
        public ActionResult<Vehicle> GetByRegistrationNumber(string registrationNumber)
        {
            var vehicle = _vehicleService.GetByRegistrationNumber(registrationNumber);
            if (vehicle is null)
                return NotFound();

            return Ok(vehicle);
        }
    }
}
