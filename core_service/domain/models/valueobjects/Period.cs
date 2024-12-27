using core_service.domain.models.@base;
using core_service.domain.models.valueobjects.enums;
using core_service.services.Result;

namespace core_service.domain.models.valueobjects;

public class Period : Entity, IDbModel, IByUserModel
{
    public TypePeriod TypePeriod { get; }
    public ushort Value { get; }

    private Period(TypePeriod typePeriod, ushort value)
    {
        this.TypePeriod = typePeriod;
        this.Value = value;
    }
    private Period(){}

    private Period(Guid? id, TypePeriod typePeriod, ushort value)
    {
        this.Id = id ?? Guid.Empty;
        this.TypePeriod = typePeriod;
        this.Value = value;
    }

    public static Period Create(TypePeriod typePeriod, ushort value)
    {
        var res = Period.PeriodIsValid(typePeriod, value);
        if(res.IsError)
            throw new ArgumentException(res.ErrorMessage);
        
        return new Period(typePeriod, value);
    }
    public static Period Create(Guid? id, TypePeriod typePeriod, ushort value)
    {
        var res = Period.PeriodIsValid(typePeriod, value);
        if(res.IsError)
            throw new ArgumentException(res.ErrorMessage);
        
        return new Period(id, typePeriod, value);
    }
    private static Result PeriodIsValid(TypePeriod typePeriod, ushort value)
    {
        if (typePeriod == TypePeriod.Month && value > 28)
            return Result.Error("Period is 'Day of month' can use value less than 29: value is " + value);

        if (value == 0)
            return Result.Error("Value can't be zero: value is " + value);

        return Result.Success();
    }

    public DateOnly NextDate(DateOnly startDate)
    {
        var end = new DateOnly(startDate.Year, startDate.Month, startDate.Day);

        switch (TypePeriod)
        {
            case TypePeriod.Month:
                end = startDate.AddMonths(1);
                end = new DateOnly(end.Year, end.Month, Value);
                break;
            case TypePeriod.UnitDay:
                end = startDate.AddDays(Value);
                break;
            case TypePeriod.UnitWeek:
                end = startDate.AddDays(Value * 7);
                break;
            case TypePeriod.UnitMonth:
                end = startDate.AddMonths(Value);
                break;
            case TypePeriod.UnitYear:
                end = startDate.AddYears(Value);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        return end;
    }

    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; }
    public Guid UserId { get; set; }
}
