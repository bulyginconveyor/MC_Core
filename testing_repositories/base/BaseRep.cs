using core_service.domain;
using core_service.domain.models;
using core_service.domain.models.valueobjects;
using core_service.infrastructure.repository.interfaces;
using core_service.infrastructure.repository.postgresql.repositories;
using core_service.infrastructure.repository.postgresql.repositories.@base;
using testing_repositories;

namespace testing_repositories.@base;

public class BaseRep : BaseTest
{
    protected IDbRepository<Currency> _rep;

    [SetUp]
    public virtual void Setup()
    {
        _rep = new BaseRepository<Currency>(_context);
    }
    
    protected Currency OneCurrency()
    {
        Random random = new Random();
        return  AllCurrencies()[random.Next(0, 3)];
    }
    protected List<Currency> AllCurrencies()
    {
        CurrencySimbol simbol = CurrencySimbol.Empty;
        
        IsoCode icRub = IsoCode.Create("RUB");
        Name nRub = Name.Create("Российский рубль");
        
        IsoCode icUsd = IsoCode.Create("USD");
        Name nUsd = Name.Create("Американский доллар");
        
        IsoCode icEur = IsoCode.Create("EUR");
        Name nEur = Name.Create("Евро");
        
        IsoCode icCny = IsoCode.Create("CNY");
        Name nCny = Name.Create("Китайский юань");

        Currency rub = Currency.Create(icRub, nRub, simbol);
        Currency usd = Currency.Create(icUsd, nUsd, simbol);
        Currency eur = Currency.Create(icEur, nEur, simbol);
        Currency cny = Currency.Create(icCny, nCny, simbol);
        
        return new List<Currency>() { rub, usd, eur, cny };
    }
}
