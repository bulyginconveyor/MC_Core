using core_service.domain;
using core_service.domain.models;
using core_service.domain.models.valueobjects;
using core_service.infrastructure.repository.interfaces;
using core_service.infrastructure.repository.postgresql.repositories;
using core_service.infrastructure.repository.postgresql.repositories.@base;
using core_service.services.GuidGenerator;
using testing_repositories;

namespace testing_repositories.@base;

public class BaseOperationRep : BaseTest
{
    protected IDbRepository<Operation> _rep;

    [SetUp]
    public void Setup()
    {
        _rep = new OperationRepository(_context);
        
        var bankAccountRep = new BaseBankAccountRepository<DebetBankAccount>(_context);
        var accounts = GetBankAccounts();
        bankAccountRep.AddRange(accounts);
        bankAccountRep.Save();
    }

    public List<Operation> AllOperations(IEnumerable<DebetBankAccount> accounts)
    {
        if (accounts.Count() < 2)
            throw new ArgumentException();
        
        DebetBankAccount debet1 = accounts.First(d => d.Name.Value == "Дебетовый счет №1");
        DebetBankAccount debet2 = accounts.First(d => d.Name.Value == "Дебетовый счет №2");

        // создание категории "Зарплата"
        Name salaryName = Name.Create("Зарплата");
        Color salaryColor = Color.Parse("#0FF");
        Category salary = new Category(GuidGenerator.GenerateByBytes(), salaryName, salaryColor);
        
        // создание категории "Продукты"
        Name foodName = Name.Create("Продукты");
        Color foodColor = Color.Parse("#0F0");
        Category food = new Category(GuidGenerator.GenerateByBytes(), foodName, foodColor);
        
        // создание операции "Получение зарплаты"
        Name salaryOperationName = Name.Create("Получение зарплаты");
        DateOnly salaryOperationDate = DateOnly.FromDateTime(DateTime.Now);
        UDecimal salaryOperationAmount = UDecimal.Parse(43357.42);
        Operation salaryOperation = new Operation(salaryOperationName, salaryOperationDate, salaryOperationAmount, null,
            null, debet1, salary);
        
        // создание операции "Покупка продуктов"
        Name foodOperationName = Name.Create("Покупка продуктов");
        DateOnly foodOperationDate = DateOnly.FromDateTime(DateTime.Now);
        UDecimal foodOperationAmount = UDecimal.Parse(125.76);
        Operation foodOperation = new Operation(foodOperationName, foodOperationDate, foodOperationAmount, null,
            debet2, null, food);
        
        // создание операции "Перевод между счетами"
        Name transferOperationName = Name.Create("Перевод между счетами");
        DateOnly transferOperationDate = DateOnly.FromDateTime(DateTime.Now);
        UDecimal transferOperationAmount = UDecimal.Parse(10000);
        Operation transferOperation = new Operation(transferOperationName, transferOperationDate, transferOperationAmount,
            null, debet1, debet2, null);
        
        return new List<Operation> {salaryOperation, foodOperation, transferOperation};
    }

    public IEnumerable<DebetBankAccount> GetBankAccounts()
    {
        // создание валюты "Российский рубль"
        IsoCode rubIsoCode = IsoCode.Create("RUB");
        Name rubName = Name.Create("Российский рубль");
        PhotoUrl empty = PhotoUrl.Empty;
        Currency rub = Currency.Create(rubIsoCode, rubName, empty);
        
        // создание дебетового счета №1 с балансом 10000 рублей
        DebetBankAccount debet1 = new DebetBankAccount(
            GuidGenerator.GenerateByBytes(), 
            "Дебетовый счет №1",
            "#F0F",
            rub, 
            10000);
        
        // создание дебетового счета №1 с балансом 127.54 рублей
        DebetBankAccount debet2 = new DebetBankAccount(
            GuidGenerator.GenerateByBytes(), 
            "Дебетовый счет №2",
            "#0FF",
            rub, 
            127.54M);
        
        return new List<DebetBankAccount> { debet1, debet2 };
    }
}