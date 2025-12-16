using System.Text.RegularExpressions;

namespace GLS.Platform.u202323562.Contexts.Shared.Domain.Model.ValueObjects;

public record MacAddress
{
    private static readonly Regex MacAddressRegex = new(
        @"^([0-9A-Fa-f]{2}:){5}[0-9A-Fa-f]{2}$",
        RegexOptions.Compiled);

    public MacAddress(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("MAC Address cannot be empty");

        var trimmed = value.Trim().ToUpperInvariant();

        if (!MacAddressRegex.IsMatch(trimmed))
            throw new ArgumentException($"Invalid MAC Address format: {value}. Expected format: XX:XX:XX:XX:XX:XX");

        Value = trimmed;
    }

    public string Value { get; init; }

    public static implicit operator string(MacAddress macAddress)
    {
        return macAddress.Value;
    }

    public override string ToString()
    {
        return Value;
    }
}