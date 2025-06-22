using core_service.domain.models.@base;
using core_service.domain.models.valueobjects;

namespace core_service.domain.models;

public class Currency : Entity, IDbModel
{
    public IsoCode IsoCode { get; private set; }
    public Name FullName { get; private set; }
    public CurrencySimbol Simbol { get; private set; }

    private Currency(Guid id, IsoCode isoCode, Name fullName, CurrencySimbol simbol)
    {
        Id = id;
        IsoCode = isoCode;
        FullName = fullName;
        Simbol = simbol;
    }
    private Currency(){}

    public static Currency Create(IsoCode isoCode, Name fullName, CurrencySimbol simbol) => new Currency(Guid.NewGuid(), isoCode, fullName, simbol);
    public static Currency Create(Guid id, IsoCode isoCode, Name fullName, CurrencySimbol simbol) => new Currency(id, isoCode, fullName, simbol);
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; }
}
