using ParkingApplication.DAL.Context;
using ParkingApplication.DAL.Entities;
using ParkingApplication.DAL.Repositories.Interfaces;

namespace ParkingApplication.DAL.Repositories.Classes;

public class AdminRepository : BaseRepository<Admin>, IAdminRepository
{
    public AdminRepository(DataBaseContext context) : base(context)
    {
    }
}