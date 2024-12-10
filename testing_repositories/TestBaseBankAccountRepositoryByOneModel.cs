using core_service.domain.valueobjects;
using core_service.infrastructure.repository.enums;
using core_service.infrastructure.repository.postgresql.repositories.@base;
using testing_repositories.ase;
using static NUnit.Framework.Assert;

namespace testing_repositories;

public class TestBaseBankAccountRepositoryByOneModel : BaseBankAccountRep
{
    [Test]
    public async Task AddActiveBankAccount()
    {
        // Arrange
        var bankAccount = OneActiveBankAccount();
        
        // Act
        var resAdd = await _rep.Add(bankAccount);
        if (resAdd.IsError)
            Fail(resAdd.ErrorMessage);

        await _rep.Save();
        
        var resGet = await _rep.GetOne(bankAccount.Id);
        if (resGet.IsError)
            Fail(resGet.ErrorMessage);

        // Assert
        Multiple(async () =>
        {
            NotNull(resGet.Value);
            AreEqual(bankAccount.Id, resGet.Value.Id);
            AreEqual(bankAccount.Color, resGet.Value.Color);
            AreEqual(bankAccount.Name, resGet.Value.Name);
            AreEqual(bankAccount.Balance, resGet.Value.Balance);
            AreEqual(bankAccount.Type, resGet.Value.Type);
            AreEqual(bankAccount.Currency, resGet.Value.Currency);
            AreEqual(bankAccount.TypeActive, resGet.Value.TypeActive);
            AreEqual(bankAccount.BuyDate, resGet.Value.BuyDate);
            AreEqual(bankAccount.BuyPrice, resGet.Value.BuyPrice);
            AreEqual(bankAccount.PhotoUrl, resGet.Value.PhotoUrl);
        });
    }

    [Test]
    public async Task UpdateActiveBankAccount()
    {
        // Arrange
        var bankAccount = OneActiveBankAccount();
        
        await _rep.Add(bankAccount);
        await _rep.Save();
        
        // Act
        bankAccount.Name = Name.Create("New Name");
        var resUpdate = await _rep.Update(bankAccount);
        if(resUpdate.IsError)
            Fail(resUpdate.ErrorMessage);
        
        var resSave = await _rep.Save();
        if (resSave.IsError)
            Fail(resSave.ErrorMessage);
        
        var resGet = await _rep.GetOne(bankAccount.Id);
        if(resGet.IsError)
            Fail(resGet.ErrorMessage);
        
        // Assert
        Multiple(async () =>
        {
            NotNull(resGet.Value);
            AreEqual(bankAccount.Id, resGet.Value.Id);
            AreEqual(bankAccount.Color, resGet.Value.Color);
            AreEqual(bankAccount.Name, resGet.Value.Name);
            AreEqual(bankAccount.Balance, resGet.Value.Balance);
            AreEqual(bankAccount.Type, resGet.Value.Type);
            AreEqual(bankAccount.Currency, resGet.Value.Currency);
            AreEqual(bankAccount.TypeActive, resGet.Value.TypeActive);
            AreEqual(bankAccount.BuyDate, resGet.Value.BuyDate);
            AreEqual(bankAccount.BuyPrice, resGet.Value.BuyPrice);
            AreEqual(bankAccount.PhotoUrl, resGet.Value.PhotoUrl);
        });
    }

    [Test]
    public async Task DeleteActiveBankAccountByObject()
    {
        // Arrange
        var bankAccount = OneActiveBankAccount();
        
        await _rep.Add(bankAccount);
        await _rep.Save();
        
        // Act
        var resDelete = await _rep.Delete(bankAccount);
        if(resDelete.IsError)
            Fail(resDelete.ErrorMessage);
        
        await _rep.Save();
        
        var resGet = await _rep.GetOne(bankAccount.Id);
        if(resGet.IsSuccess)
            Fail("ActiveBankAccount was not deleted!");
        
        // Assert
        Null(resGet.Value);
    }
    [Test]
    public async Task DeleteActiveBankAccountById()
    {
        // Arrange
        var bankAccount = OneActiveBankAccount();
        
        await _rep.Add(bankAccount);
        await _rep.Save();
        
        // Act
        var resDelete = await _rep.Delete(bankAccount.Id);
        if(resDelete.IsError)
            Fail(resDelete.ErrorMessage);
        
        await _rep.Save();
        
        var resGet = await _rep.GetOne(bankAccount.Id);
        if(resGet.IsSuccess)
            Fail("ActiveBankAccount was not deleted!");
        
        // Assert
        Null(resGet.Value);
    }

    [Test]
    public async Task GetOneActiveBankAccountById()
    {
        // Arrange
        var currency = OneActiveBankAccount();
        await _rep.Add(currency);
        await _rep.Save();
        
        // Act
        var resGet = await _rep.GetOne(currency.Id);
        if(resGet.IsError)
            Fail(resGet.ErrorMessage);
        
        // Assert
        NotNull(resGet.Value);
    }
    [Test]
    public async Task GetOneActiveBankAccountByFilter()
    {
        // Arrange
        var bankAccount = OneActiveBankAccount();
        await _rep.Add(bankAccount);
        await _rep.Save();
        
        // Act
        var resGet = await _rep.GetOne(c => c.Name == bankAccount.Name, Tracking.No);
        if(resGet.IsError)
            Fail(resGet.ErrorMessage);
        
        // Assert
        NotNull(resGet.Value);
    }
    
    //TODO: Добавить тест метода LoadData() - лень сейчас делать, сделаю позже
}