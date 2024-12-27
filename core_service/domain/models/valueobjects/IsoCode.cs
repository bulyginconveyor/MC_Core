using System.Text.RegularExpressions;

namespace core_service.domain.models.valueobjects;

public record IsoCode
{
    public string Value { get; init; }
    
    private IsoCode(string value)
    {
        this.Value = value;
    }
    public static IsoCode Create(string isoCode)
    {
        string isoCodeUp = isoCode.ToUpper();
        
        if(!IsoCodeIsValid(isoCodeUp))
            throw new ArgumentException($"Invalid IsoCode: {isoCode}");
        return new IsoCode(isoCodeUp);
    }
    private static bool IsoCodeIsValid(string isoCode) => 
        isoCode.Length == 3 && Regex.IsMatch(isoCode, "^[A-Z]{3}$"); //ISO 4217   
}