namespace ParkingApplication.DAL.Repositories.Interfaces;

public interface IPasswordHashRepository
{
    string HashPassword(string password);

    bool VerifyPassword(string enteredPassword, string savedPasswordHash);
}