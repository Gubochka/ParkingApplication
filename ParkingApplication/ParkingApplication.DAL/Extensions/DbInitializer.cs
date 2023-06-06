using ParkingApplication.DAL.Context;

namespace ParkingApplication.DAL.Extensions;

public class DbInitializer
{
    public static void Initialize(DataBaseContext context)
    {
        context.Database.EnsureCreated();
    }
}