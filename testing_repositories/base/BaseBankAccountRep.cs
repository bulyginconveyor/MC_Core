using core_service.domain;
using core_service.domain.models;
using core_service.domain.models.enums;
using core_service.domain.models.valueobjects;
using core_service.infrastructure.repository.interfaces;
using core_service.infrastructure.repository.postgresql.repositories.@base;
using core_service.services.GuidGenerator;
using Renci.SshNet.Messages.Authentication;
using testing_repositories;

namespace testing_repositories.@ase;

public class BaseBankAccountRep : BaseTest
{
    protected IDbRepository<ActiveBankAccount> _rep;
    
    [SetUp]
    public void SetUp()
    {
        _rep = new BaseBankAccountRepository<ActiveBankAccount>(_context);
    }
    
    protected ActiveBankAccount OneActiveBankAccount()
    {
        Random random = new Random();
        return  AllActiveBankAccounts()[random.Next(0, 3)];
    }
    protected List<ActiveBankAccount> AllActiveBankAccounts()
    {
        var photoUrl = PhotoUrl.Empty;
        var isoCode = IsoCode.Create("RUB");
        var name = Name.Create("Российский рубль");
        var rub = Currency.Create(isoCode, name, photoUrl);

        var houseActive = new Active(UDecimal.Parse(1000000), DateTime.UtcNow, TypeActiveBankAccount.Realty);
        var house = new ActiveBankAccount(GuidGenerator.GenerateByBytes(), "Дом", "#FFF", rub, houseActive);
        
        var autoActive = new Active(UDecimal.Parse(1750000), DateTime.UtcNow, TypeActiveBankAccount.Transport);
        var auto = new ActiveBankAccount(GuidGenerator.GenerateByBytes(), "Автомобиль", "#F0F", rub, autoActive);
        
        var carActive = new Active(UDecimal.Parse(750000), DateTime.UtcNow, TypeActiveBankAccount.Transport);
        var car = new ActiveBankAccount(GuidGenerator.GenerateByBytes(), "Машина", "#F00", rub, carActive);
        
        var iphoneActive = new Active(UDecimal.Parse(82500), DateTime.UtcNow, TypeActiveBankAccount.ValuableThing);
        var iphone = new ActiveBankAccount(GuidGenerator.GenerateByBytes(), "iPhone", "#00F", rub, iphoneActive);
        
        return [house, auto, car, iphone];
    }
}