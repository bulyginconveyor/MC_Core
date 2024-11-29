using core_service.domain;
using core_service.domain.valueobjects;
using core_service.infrastructure.repository.enums;
using Microsoft.EntityFrameworkCore;
using Renci.SshNet.Messages.Authentication;
using testing_repositories.@base;
using static NUnit.Framework.Assert;

namespace testing_repositories;

public class TestManyCurrencies : BaseCurrency
{
    [Test]
    public async Task AddManyCurrencies()
    {
        // Arrange
        var currencies = AllCurrencies();

        // Act
        await _rep.AddRange(currencies);
        await _rep.Save();
        
        var resGet = await _rep.GetAll();
        if (resGet.IsError)
            Fail(resGet.ErrorMessage);
        
        // Assert
        AreEqual(currencies.Count, resGet.Value.Count());
    }
    
    [Test]
    public async Task UpdateManyCurrencies()
    {
        // Arrange
        await _context.Set<Currency>().ExecuteDeleteAsync();
        
        var currencies = AllCurrencies();
        
        await _rep.AddRange(currencies);
        await _rep.Save();

        Name name = Name.Create("New Name");
        foreach (var cur in currencies)
            cur.ChangeName(name);
        
        // Act
        await _rep.UpdateRange(currencies);
        
        var resSave = await _rep.Save();
        if(resSave.IsError)
            Fail(resSave.ErrorMessage);
        
        var resGet = await _rep.GetAll();
        if(resGet.IsError)
            Fail(resGet.ErrorMessage);
        
        // Assert
        foreach (var cur in resGet.Value)
            AreEqual(name, cur.FullName);
    }
    
    [Test]
    public async Task DeleteManyCurrencies()
    {
        // Arrange
        await _context.Set<Currency>().ExecuteDeleteAsync();
        
        var currencies = AllCurrencies();
        await _rep.AddRange(currencies);
        await _rep.Save();
        
        // Act
        await _rep.DeleteRange(currencies);
        
        var resGet = await _rep.GetAll();
        
        // Assert
        AreEqual(0, resGet.Value.Count());
    }

    [Test]
    public async Task GetAllCurrencies()
    {
        // Arrange
        var currencies = AllCurrencies();

        await _rep.AddRange(currencies);
        await _rep.Save();
        
        // Act
        var resGet = await _rep.GetAll();
        if (resGet.IsError)
            Fail(resGet.ErrorMessage);
        
        // Assert
        AreEqual(currencies.Count, resGet.Value.Count());
    }

    [Test]
    public async Task GetManyCurrenciesByFilter()
    {
        // Arrange
        await _context.Set<Currency>().ExecuteDeleteAsync();
        var currencies = AllCurrencies();

        await _rep.AddRange(currencies);
        await _rep.Save();
        
        // Act
        var resGet = await _rep.GetAll(Tracking.No, c => c.IsoCode == currencies[0].IsoCode);
        if (resGet.IsError)
            Fail(resGet.ErrorMessage);
        
        // Assert
        AreEqual(1, resGet.Value.Count());
    }
}