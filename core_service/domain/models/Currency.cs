using core_service.domain.models.@base;
using core_service.domain.models.valueobjects;

namespace core_service.domain.models;

public class Currency : Entity, IDbModel
{
    public IsoCode IsoCode { get; private set; }
    public Name FullName { get; private set; }
    public PhotoUrl ImageUrl { get; private set; }

    private Currency(Guid id, IsoCode isoCode, Name fullName, PhotoUrl imageUrl)
    {
        Id = id;
        IsoCode = isoCode;
        FullName = fullName;
        ImageUrl = imageUrl;
    }
    private Currency(){}

    public static Currency Create(IsoCode isoCode, Name fullName, PhotoUrl imageUrl) => new Currency(Guid.NewGuid(), isoCode, fullName, imageUrl);
    public static Currency Create(Guid id, IsoCode isoCode, Name fullName, PhotoUrl imageUrl) => new Currency(id, isoCode, fullName, imageUrl);
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; }
    
    
    public void ChangeName(Name fullName) => FullName = fullName;
}
