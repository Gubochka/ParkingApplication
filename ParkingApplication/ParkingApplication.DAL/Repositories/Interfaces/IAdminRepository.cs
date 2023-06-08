using ParkingApplication.DAL.Entities;

namespace ParkingApplication.DAL.Repositories.Interfaces;

public interface IAdminRepository : IRepository<Admin>
{
    Task<Admin> GetAdminByEmail(string email);
}