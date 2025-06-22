namespace core_service.domain.models.valueobjects;

public record DateRange
{
    public DateOnly StartDate { get; init; }
    public DateOnly? EndDate { get; init; }

    private DateRange(DateOnly startDate, DateOnly? endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    public static DateRange Create(DateOnly startDate, DateOnly? endDate)
    {
        if(!IsValid(startDate, endDate))
            throw new ArgumentException("EndDate must be earlier than StartDate");
        
        return new DateRange(startDate, endDate);
    }

    private static bool IsValid(DateOnly startDate, DateOnly? endDate)
    {
        if(startDate > endDate)
            return false;
        
        return true;
    }
}
