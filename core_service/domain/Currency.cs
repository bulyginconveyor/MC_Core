using core_service.domain.@base;
using core_service.domain.valueobjects;

namespace core_service.domain;

public class Currency : Entity, IDbModel
{
    public IsoCode IsoCode { get; init; } //ISO 4217
    public Name FullName { get; init; }
    public PhotoUrl ImageUrl { get; init; }

    private Currency(IsoCode isoCode, Name fullName, PhotoUrl imageUrl)
    {
        IsoCode = isoCode;
        FullName = fullName;
        ImageUrl = imageUrl;
    }
    private Currency(){}

    public static Currency Create(IsoCode isoCode, Name fullName, PhotoUrl imageUrl) => new Currency(isoCode, fullName, imageUrl);
    
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; }
    public DateTime? DeletedAt { get; }
}
