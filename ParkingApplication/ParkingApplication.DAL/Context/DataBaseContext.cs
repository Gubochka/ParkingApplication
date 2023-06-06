using Microsoft.EntityFrameworkCore;
using ParkingApplication.DAL.Entities;
using ParkingApplication.DAL.EntityConfigurations;

namespace ParkingApplication.DAL.Context;

public class DataBaseContext : DbContext
{
    
    public virtual DbSet<Owner> Owners { get; set; }
    public virtual DbSet<Car> Cars { get; set; }
    public virtual DbSet<Parking> Parking { get; set; }
    
    public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new OwnerConfiguration());
        builder.ApplyConfiguration(new CarConfiguration());
        builder.ApplyConfiguration(new ParkingConfiguration());

        base.OnModelCreating(builder);
    }
}