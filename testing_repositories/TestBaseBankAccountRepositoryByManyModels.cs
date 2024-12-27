using core_service.domain;
using core_service.domain.models;
using core_service.infrastructure.repository.enums;
using testing_repositories.ase;
using static NUnit.Framework.Assert;

namespace testing_repositories;

public class TestBaseBankAccountRepositoryByManyModels : BaseBankAccountRep
{
    [Test]
    public async Task GetAllActiveBankAccounts()
    {
        // Arrange
        _context.Set<ActiveBankAccount>().RemoveRange(_context.Set<ActiveBankAccount>());
        _context.Set<BankAccount>().RemoveRange(_context.Set<BankAccount>());
        
        var accounts = AllActiveBankAccounts();
        
        await _rep.AddRange(accounts);
        await _rep.Save();
        
        // Act
        var result = await _rep.GetAll();

        // Assert
        AreEqual(accounts.Count, result.Value.Count());
    }
    
    [Test]
    public async Task GetAllActiveBankAccountsByFilter()
    {
        // Arrange
        _context.Set<ActiveBankAccount>().RemoveRange(_context.Set<ActiveBankAccount>());
        _context.Set<BankAccount>().RemoveRange(_context.Set<BankAccount>());
        
        var accounts = AllActiveBankAccounts();
        
        await _rep.AddRange(accounts);
        await _rep.Save();
        
        // Act
        var result = await _rep.GetAll(ba => ba.Name.Value == "Дом", Tracking.No);

        // Assert
        AreEqual(1, result.Value.Count());
    }
}