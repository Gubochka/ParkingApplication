using ParkingApplication.DAL.Context;
using ParkingApplication.DAL.Entities;
using ParkingApplication.DAL.Repositories.Interfaces;

namespace ParkingApplication.DAL.Repositories.Classes;

public class OwnerRepository : BaseRepository<Owner>, IOwnerRepository
{
    public OwnerRepository(DataBaseContext context) : base(context)
    {
    }
}