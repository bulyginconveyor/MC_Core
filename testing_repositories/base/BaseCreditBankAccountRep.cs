using System.Globalization;
using core_service.domain;
using core_service.domain.models;
using core_service.domain.models.enums;
using core_service.domain.models.valueobjects;
using core_service.domain.models.valueobjects.enums;
using core_service.infrastructure.repository.interfaces;
using core_service.infrastructure.repository.postgresql.repositories;
using core_service.infrastructure.repository.postgresql.repositories.@base;

namespace testing_repositories.@base;

public class BaseCreditBankAccountRep : BaseTest
{
    protected IDbRepository<CreditBankAccount> _rep;
    
    [SetUp]
    public void SetUp()
    {
        _rep = new CreditBankAccountRepository(_context);
    }

    protected CreditBankAccount  OneCreditBankAccount()
    {
        IsoCode rubIsoCode = IsoCode.Create("RUB");
        Name rubName = Name.Create("Российский рубль");
        Currency rub = Currency.Create(rubIsoCode, rubName, CurrencySimbol.Create("\u20bd"));
        Percent percent = Percent.Create(24.6); 
        Term term = Term.Create(UnitTerm.Week, 60u);
        DateOnly startDate = new DateOnly(2021, 1, 20);

        Active loanObjectActive = new Active(UDecimal.Parse(750000), startDate, TypeActiveBankAccount.Transport);
        ActiveBankAccount loanObject = new ActiveBankAccount(Guid.NewGuid(), "Toyota Mark II", "#FF0",
            rub, loanObjectActive, 750000);

        Loan loan = new Loan(UDecimal.Parse(750000), UDecimal.Parse(100000), percent, term, startDate,
            TypeCreditBankAccount.CarLoan, loanObject);
        
        CreditBankAccount credit = new CreditBankAccount(Guid.NewGuid(), "Автокредит", "#F00", rub, loan, 750000);
        credit.UserId = Guid.NewGuid();
        return credit;
    }
}
