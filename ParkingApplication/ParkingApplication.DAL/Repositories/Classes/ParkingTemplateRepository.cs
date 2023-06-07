using ParkingApplication.DAL.Context;
using ParkingApplication.DAL.Entities;
using ParkingApplication.DAL.Repositories.Interfaces;

namespace ParkingApplication.DAL.Repositories.Classes;

public class ParkingTemplateRepository : BaseRepository<ParkingTemplate>, IParkingTemplateRepository
{
    public ParkingTemplateRepository(DataBaseContext context) : base(context)
    {
    }
}