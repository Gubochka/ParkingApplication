using ParkingApplication.BL.Models;

namespace ParkingApplication.BL.Services.Interfaces;

public interface IAuthService
{
    string GenerateToken(AdminModel admin);
    Task<string?> Login(AdminModel admin);
}