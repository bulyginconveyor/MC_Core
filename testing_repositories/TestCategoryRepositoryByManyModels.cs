using core_service.domain;
using core_service.domain.models;
using core_service.infrastructure.repository.enums;
using testing_repositories.@base;
using static NUnit.Framework.Assert;

namespace testing_repositories;

public class TestCategoryRepositoryByManyModels : BaseCategoryRep
{
    [Test]
    public async Task GetAllCategoriesWithTracking()
    {
        // Arrange
        _context.Set<Category>().RemoveRange(_context.Set<Category>());
        await _context.SaveChangesAsync();

        var list = AllCategories();
        
        await _rep.AddRange(list!);
        await _rep.Save();

        // Act
        var result = await _rep.GetAll();
        if (result.IsError)
            Fail(result.ErrorMessage);

        // Assert
        AreEqual(list.Count(), result.Value.Count());
    }

    [Test]
    public async Task GetAllCategoriesWithNoTracking()
    {
        // Arrange
        _context.Set<Category>().RemoveRange(_context.Set<Category>());
        await _context.SaveChangesAsync();

        var list = AllCategories();
        
        await _rep.AddRange(list);
        await _rep.Save();

        // Act
        var result = await _rep.GetAll(Tracking.No);
        if (result.IsError)
            Fail(result.ErrorMessage);

        // Assert
        Multiple(() =>
        {
            AreEqual(list.Count(), result.Value.Count());
            AreEqual(list.First().SubCategories.Count(), result.Value.First().SubCategories.Count());
            AreEqual(list.Last().SubCategories.Count(), result.Value.Last().SubCategories.Count());
        });
    }
}