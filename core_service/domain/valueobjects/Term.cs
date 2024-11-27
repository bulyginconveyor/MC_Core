using core_service.domain.@base;
using core_service.domain.valueobjects.enums;

namespace core_service.domain.valueobjects;

public class Term : Entity, IDbModel
{
    public UnitTerm Unit { get; init; }
    public uint CountUnits { get; init; }

    private Term(UnitTerm unit, uint countUnits)
    {
        Unit = unit;
        CountUnits = countUnits;
    }
    private Term(){}

    public static Term Create(UnitTerm unit, uint countUnits)
    {
        if(!IsValid(unit, countUnits))
            throw new ArgumentException("Invalid parameters");
        
        return new Term(unit, countUnits);
    }

    private static bool IsValid(UnitTerm unit, uint countUnits)
    {
        if (countUnits <= 0)
            return false;

        return true;
    }

    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; }
    public DateTime? DeletedAt { get; }
}