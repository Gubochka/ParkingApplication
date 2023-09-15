using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ParkingApplication.DAL.Entities;
using ParkingApplication.DAL.EntityConfigurations;
using ParkingApplication.DAL.Repositories.Interfaces;

namespace ParkingApplication.DAL.Context;

public class DataBaseContext : DbContext
{

    public virtual DbSet<Admin> Admins { get; set; } = default!;
    public virtual DbSet<Owner> Owners { get; set; } = default!;
    public virtual DbSet<Car> Cars { get; set; } = default!;
    public virtual DbSet<ParkingTemplate> ParkingTemplates { get; set; } = default!;
    public virtual DbSet<Parking> Parking { get; set; } = default!;

    private readonly IConfiguration _configuration;
    private readonly IPasswordHashRepository _passwordHashRepository;
    
    public DataBaseContext(DbContextOptions<DataBaseContext> options, IConfiguration configuration, IPasswordHashRepository passwordHashRepository) : base(options)
    {
        _configuration = configuration;
        _passwordHashRepository = passwordHashRepository;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AdminConfiguration());
        modelBuilder.ApplyConfiguration(new OwnerConfiguration());
        modelBuilder.ApplyConfiguration(new CarConfiguration());
        modelBuilder.ApplyConfiguration(new ParkingTemplateConfiguration());
        modelBuilder.ApplyConfiguration(new ParkingConfiguration());

        base.OnModelCreating(modelBuilder);
        RegSuperAdmin(modelBuilder);
    }

    private void RegSuperAdmin(ModelBuilder builder)
    {
        builder.Entity<Admin>()
            .HasData( new Admin {
                    Id = 1,
                    Email = _configuration["Secrets:SuperAdmin:Email"]!,
                    Password = _passwordHashRepository.HashPassword(_configuration["Secrets:SuperAdmin:Password"]!),
                    IsSuperAdmin = true,
                });
    }
}