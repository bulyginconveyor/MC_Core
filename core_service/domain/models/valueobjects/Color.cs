using System.Text.RegularExpressions;
using core_service.services.Result;

namespace core_service.domain.models.valueobjects;

public partial record Color
{
    public string Value { get; init; }

    private Color(string value) => this.Value = value;

    public static Color Parse(string color)
    {
        var resultColorValidation = ColorIsValid(color);
        if(resultColorValidation.IsError)
            throw new ArgumentException(resultColorValidation.ErrorMessage);
        
        return new Color(color);
    }

    public virtual bool Equals(Color? other) => Value == other?.Value;
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static Result ColorIsValid(string color)
    {
        if(color is null)
            return Result.Error("Color is null!");
        
        var valid = color.Length is 4 or 5 or 7 or 9;

        if (!valid)
            return Result.Error("Invalid length color: {color}! RGB = #RGB or #RRGGBB, RGBA = #RGBA or #RRGGBBAA");
        
        valid = ColorRgbRgbaHex().IsMatch(color);
        
        return !valid
            ? Result.Error("Invalid color by regex: {color}! RGB = #RGB or #RRGGBB, RGBA = #RGBA or #RRGGBBAA")
            : Result.Success();
    }

    [GeneratedRegex(@"^#([0-9A-Fa-f]{3,4}){1,2}$", RegexOptions.Singleline)]
    private static partial Regex ColorRgbRgbaHex();
}