using core_service.domain;
using core_service.domain.models;
using core_service.domain.models.valueobjects;
using core_service.infrastructure.repository.interfaces;
using core_service.infrastructure.repository.postgresql.repositories;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using testing_repositories.@base;
using static NUnit.Framework.Assert;

namespace testing_repositories;

public class TestBaseRepositoryByOneModel : BaseRep
{
    [Test]
    public async Task AddCurrency()
    {
        // Arrange
        Currency currency = OneCurrency();
        
        // Act
        var resAdd = await _rep.Add(currency);
        if (resAdd.IsError)
            Fail(resAdd.ErrorMessage);

        await _rep.Save();
        
        var resGet = await _rep.GetOne(currency.Id);
        if (resGet.IsError)
            Fail(resGet.ErrorMessage);

        // Assert
        Multiple(async () =>
        {
            NotNull(resGet.Value);
            AreEqual(currency.Id, resGet.Value.Id);
            AreEqual(currency.IsoCode, resGet.Value.IsoCode);
            AreEqual(currency.FullName, resGet.Value.FullName);
            AreEqual(currency.ImageUrl, resGet.Value.ImageUrl);
        });
    }

    [Test]
    public async Task UpdateCurrency()
    {
        // Arrange
        var currency = OneCurrency();
        
        await _rep.Add(currency);
        await _rep.Save();
        
        // Act
        currency.ChangeName(Name.Create("New Name"));
        var resUpdate = await _rep.Update(currency);
        if(resUpdate.IsError)
            Fail(resUpdate.ErrorMessage);
        
        var resSave = await _rep.Save();
        if (resSave.IsError)
            Fail(resSave.ErrorMessage);
        
        var resGet = await _rep.GetOne(currency.Id);
        if(resGet.IsError)
            Fail(resGet.ErrorMessage);
        
        // Assert
        Multiple(async () =>
        {
            NotNull(resGet.Value);
            AreEqual(currency.FullName, resGet.Value.FullName);
            AreEqual(currency.IsoCode, resGet.Value.IsoCode);
            AreEqual(currency.ImageUrl, resGet.Value.ImageUrl);
        });
    }

    [Test]
    public async Task DeleteCurrencyByObject()
    {
        // Arrange
        var currency = OneCurrency();
        
        await _rep.Add(currency);
        await _rep.Save();
        
        // Act
        var resDelete = await _rep.Delete(currency);
        if(resDelete.IsError)
            Fail(resDelete.ErrorMessage);
        
        await _rep.Save();
        
        var resGet = await _rep.GetOne(currency.Id);
        if(resGet.IsSuccess)
            Fail("Currency was not deleted!");
        
        // Assert
        Null(resGet.Value);
    }
    [Test]
    public async Task DeleteCurrencyById()
    {
        // Arrange
        var currency = OneCurrency();
        
        await _rep.Add(currency);
        await _rep.Save();
        
        // Act
        var resDelete = await _rep.Delete(currency.Id);
        if(resDelete.IsError)
            Fail(resDelete.ErrorMessage);
        
        await _rep.Save();
        
        var resGet = await _rep.GetOne(currency.Id);
        if(resGet.IsSuccess)
            Fail("Currency was not deleted!");
        
        // Assert
        Null(resGet.Value);
    }

    [Test]
    public async Task GetOneCurrencyById()
    {
        // Arrange
        var currency = OneCurrency();
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
    public async Task GetOneCurrencyByFilter()
    {
        // Arrange
        var currency = OneCurrency();
        await _rep.Add(currency);
        await _rep.Save();
        
        // Act
        var resGet = await _rep.GetOne(c => c.IsoCode == currency.IsoCode);
        if(resGet.IsError)
            Fail(resGet.ErrorMessage);
        
        // Assert
        NotNull(resGet.Value);
    }
    
    
}
