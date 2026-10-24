using InsureAI.Models;

namespace InsureAI.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly List<Vehicle> _vehicles;

        public VehicleService()
        {
            // In-memory sample data
            _vehicles = new List<Vehicle>
            {
                new Vehicle { RegistrationNumber = "ABC123", Make = "Toyota", Model = "Corolla", Year = 2018, InsuredDeclaredValue = 15000m },
                new Vehicle { RegistrationNumber = "XYZ789", Make = "Ford", Model = "Focus", Year = 2020, InsuredDeclaredValue = 18000m }
            };
        }

        // Returns matching vehicle or null when not found. Comparison is case-insensitive.
        public Vehicle? GetByRegistrationNumber(string registrationNumber)
        {
            if (string.IsNullOrWhiteSpace(registrationNumber))
                return null;

            return _vehicles.FirstOrDefault(v => string.Equals(v.RegistrationNumber, registrationNumber, StringComparison.OrdinalIgnoreCase));
        }
    }
}
