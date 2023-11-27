using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingApplication.DAL.Entities;

namespace ParkingApplication.DAL.EntityConfigurations;

public class ParkingConfiguration : IEntityTypeConfiguration<Parking>
{
    public void Configure(EntityTypeBuilder<Parking> builder)
    {
        builder.HasIndex(x => x.Id);
        builder.Property(x => x.ParkingTemplateId);
        builder.Property(x => x.FloorNumber);
        builder.Property(x => x.SlotNumber);
        builder.Property(x => x.StandsUntil);
        builder.Property(x => x.Price);
    }
}