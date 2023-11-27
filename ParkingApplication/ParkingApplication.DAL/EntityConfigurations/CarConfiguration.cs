using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingApplication.DAL.Entities;

namespace ParkingApplication.DAL.EntityConfigurations;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.HasIndex(x => x.Id);
        builder.Property(x => x.OwnerId);
        builder.Property(x => x.CarName);
        builder.Property(x => x.CarNumber);

        builder
            .HasMany(x => x.Parking)
            .WithOne(x => x.Car)
            .HasForeignKey(x => x.CarId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}