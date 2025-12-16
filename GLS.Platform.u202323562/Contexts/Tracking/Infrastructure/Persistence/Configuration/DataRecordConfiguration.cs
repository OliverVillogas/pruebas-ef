using GLS.Platform.u202323562.Contexts.Tracking.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GLS.Platform.u202323562.Contexts.Tracking.Infrastructure.Persistence.Configuration;

public class DataRecordConfiguration : IEntityTypeConfiguration<DataRecord>
{
    public void Configure(EntityTypeBuilder<DataRecord> builder)
    {
        builder.ToTable("data_records");

        builder.HasKey(dr => dr.Id);
        builder.Property(dr => dr.Id).ValueGeneratedOnAdd();
        
        builder.Property(dr => dr.CreatedDate).IsRequired();
        builder.Property(dr => dr.UpdatedDate).IsRequired(false);
        
        builder.Property(dr => dr.IsDeleted)
            .IsRequired()
            .HasDefaultValue(0);
        
        builder.OwnsOne(dr => dr.DeviceMacAddress, mac =>
        {
            mac.Property(m => m.Value)
                .HasColumnName("device_mac_address")
                .HasMaxLength(17)
                .IsRequired();
            mac.HasIndex(m => m.Value);
        });
        
        builder.Property(dr => dr.OperationMode)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();
        
        builder.Property(dr => dr.TargetThrust)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(dr => dr.CurrentThrust)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
        
        builder.Property(dr => dr.EngineState)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();
        
        builder.Property(dr => dr.GeneratedAt).IsRequired();
        builder.HasIndex(dr => dr.GeneratedAt);
    }
}