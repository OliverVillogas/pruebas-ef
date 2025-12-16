using GLS.Platform.u202323562.Contexts.Assignments.Domain.Model.Aggregates;
using GLS.Platform.u202323562.Contexts.Shared.Infrastructure.Persistence.Strategies;
using GLS.Platform.u202323562.Contexts.Tracking.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace GLS.Platform.u202323562.Contexts.Shared.Infrastructure.Persistence.Configuration;

public class GLSContext(DbContextOptions<GLSContext> options) : DbContext(options)
{
    public DbSet<DataRecord> DataRecords { get; set; }
    public DbSet<Device> Devices { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfigurationsFromAssembly(typeof(GLSContext).Assembly);
        
        builder.ConfigureSnakeCaseNames();
        
        SeedData(builder);
    }
    
    private static void SeedData(ModelBuilder builder)
    {
        builder.Entity<Device>().HasData(
            new
            {
                Id = 1,
                MissionId = 301,
                PreferredThrust = 750.0m,
                CreatedDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = 0
            },
            new
            {
                Id = 2,
                MissionId = 302,
                PreferredThrust = 820.5m,
                CreatedDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = 0
            },
            new
            {
                Id = 3,
                MissionId = 303,
                PreferredThrust = 910.0m,
                CreatedDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = 0
            },
            new
            {
                Id = 4,
                MissionId = 304,
                PreferredThrust = 880.5m,
                CreatedDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = 0
            }
        );
        
        builder.Entity<Device>()
            .OwnsOne(d => d.MacAddress)
            .HasData(
                new { DeviceId = 1, Value = "A1:B2:C3:D4:E5:F6" },
                new { DeviceId = 2, Value = "F6:E5:D4:C3:B2:A1" },
                new { DeviceId = 3, Value = "12:34:56:78:9A:BC" },
                new { DeviceId = 4, Value = "BC:9A:78:56:34:12" }
            );
    }
}