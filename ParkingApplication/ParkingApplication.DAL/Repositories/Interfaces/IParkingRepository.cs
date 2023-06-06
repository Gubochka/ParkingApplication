using ParkingApplication.DAL.Entities;

namespace ParkingApplication.DAL.Repositories.Interfaces;

public interface IParkingRepository : IRepository<Parking>
{
    IQueryable<Parking>? GetAllCarsOnFloor(int floor);
    Task<int> FindCarOnParking(int floor, int slot);
    Task DeleteCarFromParking(int carId, int floor, int slot);
}