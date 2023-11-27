using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingApplication.DAL.Entities;

namespace ParkingApplication.DAL.EntityConfigurations;

public class AdminConfiguration : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.HasIndex(x => x.Id);
        builder.Property(x => x.Email);
        builder.Property(x => x.Password);
        builder.Property(x => x.ParkingTemplateId);
        builder.Property(x => x.IsSuperAdmin);

        builder
            .HasOne(x => x.ParkingTemplate)
            .WithMany(x => x.Admins)
            .HasForeignKey(x => x.ParkingTemplateId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}