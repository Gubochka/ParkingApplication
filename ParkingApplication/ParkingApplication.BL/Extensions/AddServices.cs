using Microsoft.Extensions.DependencyInjection;
using ParkingApplication.BL.Services.Classes;
using ParkingApplication.BL.Services.Interfaces;

namespace ParkingApplication.BL.Extensions;

public static class AddServices
{
    public static void AddServiceInjection(this IServiceCollection services)
    {
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IOwnerService, OwnerService>();
        services.AddScoped<ICarService, CarService>();
        services.AddScoped<IParkingTemplateService, ParkingTemplateService>();
        services.AddScoped<IParkingService, ParkingService>();

        services.AddAutoMapper(typeof(AppMappingProfile));
    }
}