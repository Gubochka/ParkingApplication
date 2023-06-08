using Microsoft.EntityFrameworkCore;
using ParkingApplication.DAL.Context;
using ParkingApplication.DAL.Entities;
using ParkingApplication.DAL.Repositories.Interfaces;

namespace ParkingApplication.DAL.Repositories.Classes;

public class AdminRepository : BaseRepository<Admin>, IAdminRepository
{
    public AdminRepository(DataBaseContext context) : base(context)
    {
    }

    public async Task<Admin> GetAdminByEmail(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Email == email);
    }
}