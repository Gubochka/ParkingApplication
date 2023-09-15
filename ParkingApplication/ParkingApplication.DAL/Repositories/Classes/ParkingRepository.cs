using System.Data.Entity;
using ParkingApplication.DAL.Context;
using ParkingApplication.DAL.Entities;
using ParkingApplication.DAL.Repositories.Interfaces;

namespace ParkingApplication.DAL.Repositories.Classes;

public class ParkingRepository : BaseRepository<Parking>, IParkingRepository
{
    public ParkingRepository(DataBaseContext context) : base(context)
    {
    }

    public IQueryable<Parking>? GetAllCarsOnFloor(int floor)
    {
        return GetAll().Where(x => x.FloorNumber == floor);
    }

    public async Task<int> FindCarOnParking(int floor, int slot)
    {
        var result = await DbSet.FirstOrDefaultAsync(x => x.FloorNumber == floor && x.SlotNumber == slot);
        return result.Id;
    }

    public async Task DeleteCarFromParking(int carId, int floor, int slot)
    {
        var item = await DbSet.FirstOrDefaultAsync(x => x.CarId == carId && x.FloorNumber == floor && x.SlotNumber == slot);
        Context.Remove(item);
        await Context.SaveChangesAsync();
    }
}