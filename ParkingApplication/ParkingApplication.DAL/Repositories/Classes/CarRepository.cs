using ParkingApplication.DAL.Context;
using ParkingApplication.DAL.Entities;
using ParkingApplication.DAL.Repositories.Interfaces;

namespace ParkingApplication.DAL.Repositories.Classes;

public class CarRepository : BaseRepository<Car>, ICarRepository
{
    public CarRepository(DataBaseContext context) : base(context)
    {
    }
}