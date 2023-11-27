using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingApplication.DAL.Entities;

namespace ParkingApplication.DAL.EntityConfigurations;

public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
{
    public void Configure(EntityTypeBuilder<Owner> builder)
    {
        builder.HasIndex(x => x.Id);
        builder.Property(x => x.FullName);
        builder.Property(x => x.PhoneNumber);

        builder
            .HasMany(x => x.Cars)
            .WithOne(x => x.Owner)
            .HasForeignKey(x => x.OwnerId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}