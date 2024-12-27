using core_service.domain;
using core_service.domain.models;
using core_service.domain.models.valueobjects;
using core_service.infrastructure.repository.postgresql.repositories.@base;
using testing_repositories.@base;
using static NUnit.Framework.Assert;

namespace testing_repositories;

public class TestOperationRepositoryByOneModel : BaseOperationRep
{
    [Test]
    public async Task AddOperation()
    {
        // Arrange
        if(_context.Set<Operation>().Count() > 0)
            _context.Set<Operation>().RemoveRange(_context.Set<Operation>());
        
        var resBanks = _context.Set<DebetBankAccount>().ToList();
        
        var operations = AllOperations(resBanks);
        var operation = operations[0];
            

        // Act
        await _rep.Add(operation);
        var res = await _rep.Save();
        if(res.IsError)
            Fail();

        var result = await _rep.GetOne(operation.Id);
        if(result.IsError)
            Fail();
        
        // Assert
        IsNotNull(result.Value);
        AreEqual(operation, result.Value);
    }

    [Test]
    public async Task UpdateOperation()
    {
        // Arrange
        _context.Set<Operation>().RemoveRange(_context.Set<Operation>());
        
        var resBanks = _context.Set<DebetBankAccount>().ToList();
        
        var operations = AllOperations(resBanks);
        var operation = operations[0];
        
        // Act
        await _rep.Add(operation);
        var res = await _rep.Save();
        if(res.IsError)
            Fail();
        
        operation.Name = Name.Create("New Name");
        var resUpdate = operation.ChangeAmount(UDecimal.Parse(40000));
        if(resUpdate.IsError)
            Fail();
        
        await _rep.Update(operation);
        res = await _rep.Save();
        if(res.IsError)
            Fail();
        
        var result = await _rep.GetOne(operation.Id);
        if(result.IsError)
            Fail();
        
        // Assert
        AreEqual(operation, result.Value);
    }

    [Test]
    public async Task DeleteOperation()
    {
        // Arrange
        _context.Set<Operation>().RemoveRange(_context.Set<Operation>());
        
        var resBanks = _context.Set<DebetBankAccount>().ToList();
        
        var operations = AllOperations(resBanks);
        var operation = operations[0];
        
        // Act
        await _rep.Add(operation);
        var res = await _rep.Save();
        if(res.IsError)
            Fail();
        
        await _rep.Delete(operation.Id);
        res = await _rep.Save();
        if(res.IsError)
            Fail();
        
        var result = await _rep.GetOne(operation.Id);
        if(result.IsSuccess)
            Fail();
        
        // Assert
        IsNull(result.Value);
    }

    [Test]
    public async Task GetOperationById()
    {
        // Arrange
        _context.Set<Operation>().RemoveRange(_context.Set<Operation>());
        
        var resBanks = _context.Set<DebetBankAccount>().ToList();
        
        var operations = AllOperations(resBanks);
        var operation = operations[0];
        
        // Act
        await _rep.Add(operation);
        var res = await _rep.Save();
        if(res.IsError)
            Fail();
        
        var result = await _rep.GetOne(operation.Id);
        if(result.IsError)
            Fail();
        
        // Assert
        AreEqual(operation, result.Value);
    }

    [Test]
    public async Task GetOperationByFilter()
    {
        // Arrange
        _context.Set<Operation>().RemoveRange(_context.Set<Operation>());
        
        var resBanks = _context.Set<DebetBankAccount>().ToList();
        
        var operations = AllOperations(resBanks);
        var operation = operations[0];
        
        // Act
        await _rep.Add(operation);
        var res = await _rep.Save();
        if(res.IsError)
            Fail();
        
        var result = await _rep.GetOne(o => o.Name == operation.Name);
        if(result.IsError)
            Fail();
        
        // Assert
        AreEqual(operation, result.Value);
    }
}