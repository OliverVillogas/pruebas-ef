using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace GLS.Platform.u202323562.Contexts.Shared.Infrastructure.Persistence.Strategies;

public static class SnakeCaseNamingStrategy
{
    public static void ConfigureSnakeCaseNames(this ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entity.GetTableName();
            if (tableName != null)
            {
                entity.SetTableName(ToSnakeCase(tableName));
            }
            
            foreach (var property in entity.GetProperties())
            {
                var columnName = property.GetColumnName();
                if (columnName != null)
                {
                    property.SetColumnName(ToSnakeCase(columnName));
                }
            }
            
            foreach (var key in entity.GetKeys())
            {
                var keyName = key.GetName();
                if (keyName != null)
                {
                    key.SetName(ToSnakeCase(keyName));
                }
            }
            
            foreach (var foreignKey in entity.GetForeignKeys())
            {
                var foreignKeyName = foreignKey.GetConstraintName();
                if (foreignKeyName != null)
                {
                    foreignKey.SetConstraintName(ToSnakeCase(foreignKeyName));
                }
            }
            
            foreach (var index in entity.GetIndexes())
            {
                var indexName = index.GetDatabaseName();
                if (indexName != null)
                {
                    index.SetDatabaseName(ToSnakeCase(indexName));
                }
            }
        }
    }

    private static string ToSnakeCase(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input;
        
        var result = Regex.Replace(input, "(?<!^)([A-Z])", "_$1");

        return result.ToLowerInvariant();
    }
}