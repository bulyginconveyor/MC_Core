using core_service.domain;
using core_service.domain.models;
using core_service.infrastructure.repository.postgresql.repositories.@base;
using testing_repositories.@base;
using static NUnit.Framework.Assert;

namespace testing_repositories;

public class TestOperationRepositoryByManyModels : BaseOperationRep
{
    [Test]
    public async Task GetOperations()
    {
        // Arrange
        _context.Set<Operation>().RemoveRange(_context.Set<Operation>());

        var res = _context.Set<DebetBankAccount>().ToList();
        
        var operations = AllOperations(res);
        await _rep.AddRange(operations); 
        await _rep.Save();

        // Act
        var resGet = await _rep.GetAll();
        if (resGet.IsError)
            Fail();
        
        // Assert
        Multiple(() =>
        {
            AreEqual(operations.Count, resGet.Value.Count());

            for (int i = 0; i < operations.Count; i++)
                AreEqual(operations[i], resGet.Value.ToList().FirstOrDefault(o => o.Name == operations[i].Name));
        });
    }

    [Test]
    public async Task GetOperationsByFilter()
    {
        // Arrange
        _context.Set<Operation>().RemoveRange(_context.Set<Operation>());
        
        var res = _context.Set<DebetBankAccount>().ToList();
        
        var operations = AllOperations(res);
        await _rep.AddRange(operations); 
        await _rep.Save();

        // Act
        var resGet = await _rep.GetAll(o => o.Name == operations[0].Name);
        if (resGet.IsError)
            Fail();
        
        // Assert
        Multiple(() =>
        {
            AreEqual(1, resGet.Value.Count());
            AreEqual(operations[0], resGet.Value.First());
        });
    }
}
