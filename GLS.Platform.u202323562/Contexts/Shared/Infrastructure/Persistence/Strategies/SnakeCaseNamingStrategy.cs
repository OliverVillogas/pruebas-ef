using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

// Asegúrate de tener este using

namespace GLS.Platform.u202323562.Contexts.Shared.Infrastructure.Persistence.Strategies;

public static class SnakeCaseNamingStrategy
{
    public static void ConfigureSnakeCaseNames(this ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            // 1. Renombrar Tablas
            var tableName = entity.GetTableName();
            if (tableName != null && !entity.IsOwned()) entity.SetTableName(ToSnakeCase(tableName));

            // 2. Renombrar Columnas
            foreach (var property in entity.GetProperties())
            {
                // --- LA CORRECCIÓN CLAVE ESTÁ AQUÍ ---
                // Si es una entidad Owned (como MacAddress) y es su Primary Key,
                // NO la renombres. Deja que EF use la columna del dueño ('id').
                if (entity.IsOwned() && property.IsPrimaryKey()) continue;
                // -------------------------------------

                var columnName = property.GetColumnName();
                if (columnName != null) property.SetColumnName(ToSnakeCase(columnName));
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