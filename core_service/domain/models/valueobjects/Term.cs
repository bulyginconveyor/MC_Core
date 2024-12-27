using core_service.domain.models.@base;
using core_service.domain.models.valueobjects.enums;
using core_service.services.Result;

namespace core_service.domain.models.valueobjects;

public class Term : Entity, IDbModel, IByUserModel
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
        var res = IsValid(unit, countUnits);
        
        if(res.IsError)
            throw new ArgumentException(res.ErrorMessage);
        
        return new Term(unit, countUnits);
    }

    private static Result IsValid(UnitTerm unit, uint countUnits)
    {
        if (countUnits <= 0)
            return Result.Error($"CountUnits must be greater than 0 (countUnits = {countUnits})");

        return Result.Success();
    }

    public Result<DateTime> EndDate(DateTime startDate)
    {
        DateTime? endDate = null;
        
        switch (Unit)
        {
            case UnitTerm.Day:
                endDate = startDate.AddDays(CountUnits);
                break;
            case UnitTerm.Week:
                endDate = startDate.AddDays(CountUnits * 7);
                break;
            case UnitTerm.Month:
                endDate = startDate.AddMonths((int)CountUnits);
                break;
            case UnitTerm.Year:
                endDate = startDate.AddYears((int)CountUnits);
                break;
        }
        
        return endDate is null ? 
            Result<DateTime>.Error(DateTime.Now, $"Unit {Unit} is not valid! Return null!")
            :
            Result<DateTime>.Success((DateTime)endDate);
    }
    public Result<DateOnly> EndDate(DateOnly startDate) {
        DateTime start = new DateTime(startDate.Year, startDate.Month, startDate.Day);
        
        var res = EndDate(start);

        return res.IsError
            ? Result<DateOnly>.Error(DateOnly.FromDateTime(res.Value), $"Unit {Unit} is not valid! Return now value!")
            : Result<DateOnly>.Success(DateOnly.FromDateTime(res.Value));
    }

    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; }
    public Guid UserId { get; set; }
}