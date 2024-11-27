using System.Text.RegularExpressions;

namespace core_service.domain.valueobjects;

public record IsoCode
{
    public string Value { get; init; }
    
    private IsoCode(string value)
    {
        this.Value = value;
    }
    public static IsoCode Create(string isoCode)
    {
        if(!IsoCodeIsValid(isoCode))
            throw new ArgumentException($"Invalid IsoCode: {isoCode}");
        return new IsoCode(isoCode);
    }
    private static bool IsoCodeIsValid(string isoCode) => isoCode.Length == 3 && Regex.IsMatch(isoCode, "^[A-Z]{3}$"); //ISO 4217   
}