using GLS.Platform.u202323562.Contexts.Assignments.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GLS.Platform.u202323562.Contexts.Assignments.Infrastructure.Persistence.Configuration;

public class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder.ToTable("devices");

        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).ValueGeneratedOnAdd();

        builder.Property(d => d.CreatedDate).IsRequired();
        builder.Property(d => d.UpdatedDate).IsRequired(false);

        builder.Property(d => d.IsDeleted)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(d => d.MissionId).IsRequired();

        builder.Property(d => d.PreferredThrust)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.OwnsOne(d => d.MacAddress, mac =>
        {
            mac.Property(m => m.Value)
                .HasColumnName("mac_address")
                .HasMaxLength(17)
                .IsRequired();
            mac.HasIndex(m => m.Value).IsUnique();
        });

        builder.HasIndex(d => d.MissionId);
    }
}