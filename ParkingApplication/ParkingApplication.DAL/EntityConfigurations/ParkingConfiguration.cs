using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingApplication.DAL.Entities;

namespace ParkingApplication.DAL.EntityConfigurations;

public class ParkingConfiguration : IEntityTypeConfiguration<Parking>
{
    public void Configure(EntityTypeBuilder<Parking> builder)
    {
        builder.HasIndex(x => x.Id);
        builder.Property(x => x.FloorNumber).HasColumnType("int");
        builder.Property(x => x.SlotNumber).HasColumnType("int");
        builder.Property(x => x.StandsUntil).HasColumnType("datetime");
        builder.Property(x => x.Price).HasColumnType("float");
    }
}