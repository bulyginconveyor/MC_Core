using core_service.domain.@base;
using core_service.domain.valueobjects;

namespace core_service.domain;

public class Currency : Entity, IDbModel
{
    public IsoCode IsoCode { get; private set; }
    public Name FullName { get; private set; }
    public PhotoUrl ImageUrl { get; private set; }

    private Currency(IsoCode isoCode, Name fullName, PhotoUrl imageUrl)
    {
        IsoCode = isoCode;
        FullName = fullName;
        ImageUrl = imageUrl;
    }
    private Currency(Guid id, IsoCode isoCode, Name fullName, PhotoUrl imageUrl)
    {
        Id = id;
        IsoCode = isoCode;
        FullName = fullName;
        ImageUrl = imageUrl;
    }
    private Currency(){}

    public static Currency Create(IsoCode isoCode, Name fullName, PhotoUrl imageUrl) => new Currency(isoCode, fullName, imageUrl);
    public static Currency Create(Guid id, IsoCode isoCode, Name fullName, PhotoUrl imageUrl) => new Currency(id, isoCode, fullName, imageUrl);
    
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; }
    
    
    public void ChangeName(Name fullName) => FullName = fullName;
}
