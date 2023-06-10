using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ParkingApplication.DAL.Entities;
using ParkingApplication.DAL.EntityConfigurations;

namespace ParkingApplication.DAL.Context;

public class DataBaseContext : DbContext
{
    
    public virtual DbSet<Admin> Admins { get; set; }
    public virtual DbSet<Owner> Owners { get; set; }
    public virtual DbSet<Car> Cars { get; set; }
    public virtual DbSet<ParkingTemplate> ParkingTemplates { get; set; }
    public virtual DbSet<Parking> Parking { get; set; }

    private readonly IConfiguration _configuration;
    
    public DataBaseContext(DbContextOptions<DataBaseContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new AdminConfiguration());
        builder.ApplyConfiguration(new OwnerConfiguration());
        builder.ApplyConfiguration(new CarConfiguration());
        builder.ApplyConfiguration(new ParkingTemplateConfiguration());
        builder.ApplyConfiguration(new ParkingConfiguration());

        base.OnModelCreating(builder);
        RegSuperAdmin(builder);
    }

    private void RegSuperAdmin(ModelBuilder builder)
    {
        builder.Entity<Admin>()
            .HasData( new Admin {
                    Id = 1,
                    Email = _configuration["Secrets:SuperAdmin:Email"],
                    Password = _configuration["Secrets:SuperAdmin:Password"],
                    IsSuperAdmin = true,
                });
    }
}