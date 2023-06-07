using ParkingApplication.Mapping;

namespace ParkingApplication.Extensions;

public static class AddMapperExtension
{
    public static void AddMapper(this IServiceCollection service)
    {
        service.AddAutoMapper(typeof(AppMapperProfile));
    }
}