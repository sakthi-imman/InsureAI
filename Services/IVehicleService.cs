using InsureAI.Models;

namespace InsureAI.Services
{
    public interface IVehicleService
    {
        Vehicle? GetByRegistrationNumber(string registrationNumber);
    }
}
