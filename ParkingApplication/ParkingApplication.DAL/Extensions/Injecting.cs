using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParkingApplication.DAL.Context;
using ParkingApplication.DAL.Repositories.Classes;
using ParkingApplication.DAL.Repositories.Interfaces;

namespace ParkingApplication.DAL.Extensions;

public static class Injecting
{
    public static void Inject(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataBaseContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Sql")));

        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<IOwnerRepository, OwnerRepository>();
        services.AddScoped<IParkingTemplateRepository, ParkingTemplateRepository>();
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<IParkingRepository, ParkingRepository>();
        services.AddScoped<IPasswordHashRepository, PasswordHashRepository>();
    }
}
