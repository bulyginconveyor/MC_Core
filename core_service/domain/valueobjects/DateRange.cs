namespace core_service.domain.valueobjects;

public record DateRange
{
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }

    private DateRange(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    public static DateRange Create(DateTime startDate, DateTime endDate)
    {
        if(!IsValid(startDate, endDate))
            throw new ArgumentException("EndDate must be earlier than StartDate");
        
        return new DateRange(startDate, endDate);
    }

    private static bool IsValid(DateTime startDate, DateTime endDate)
    {
        if(startDate > endDate)
            return false;
        
        return true;
    }
}