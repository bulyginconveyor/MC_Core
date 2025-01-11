using core_service.domain.models;
using core_service.domain.models.valueobjects;

namespace core_service.application.rest_api.DTO;

public class DTOCurrency()
{
    public Guid Id { get; set; }
    public string IsoCode { get; set; }
    public string FullName { get; set; }
    public string? PhotoUrl { get; set; }

    public static implicit operator Currency(DTOCurrency dto)
    {
        if (dto is null)
            return null;
        
        IsoCode isoCode = domain.models.valueobjects.IsoCode.Create(dto.IsoCode);
        Name fullName = Name.Create(dto.FullName);
        PhotoUrl photoUrl = dto.PhotoUrl is null ? domain.models.valueobjects.PhotoUrl.Empty : domain.models.valueobjects.PhotoUrl.Create(dto.PhotoUrl);
        
        return Currency.Create(dto.Id, isoCode, fullName, photoUrl);
    }

    public static implicit operator DTOCurrency(Currency currency)
    {
        if (currency is null)
            return null;

        return new DTOCurrency
        {
            Id = currency.Id,
            FullName = currency.FullName.Value,
            IsoCode = currency.IsoCode.Value,
            PhotoUrl = currency.ImageUrl.Url
        };
    }
}
