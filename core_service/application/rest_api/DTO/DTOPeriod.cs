using core_service.domain.models.valueobjects;
using core_service.domain.models.valueobjects.enums;

namespace core_service.application.rest_api.DTO;

public class DTOPeriod
{
    public Guid? Id { get; set; }
    public string Type { get; set; }
    public ushort Value { get; set; }
    
    public static DTOPeriod CreateLight(Period period)
    {
        if (period is null)
            return null;

        return new DTOPeriod
        {
            Id = period.Id,
            Type = period.TypePeriod.ToString(),
            Value = period.Value
        };
    }

    public static implicit operator Period(DTOPeriod period)
    {
        if (period is null)
            return null;
        
        TypePeriod type = (TypePeriod)Enum.Parse(typeof(TypePeriod), period.Type);
        return Period.Create(period.Id, type, period.Value);
    }
}
