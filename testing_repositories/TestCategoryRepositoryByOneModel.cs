using core_service.domain;
using core_service.domain.valueobjects;
using core_service.infrastructure.repository.enums;
using core_service.services.GuidGenerator;
using testing_repositories.@base;
using static NUnit.Framework.Assert;

namespace testing_repositories;

public class TestCategoryRepositoryByOneModel : BaseCategoryRep
{
    [Test]
    public async Task LoadDataCategory()
    {
        // Arrange
        var all = AllCategories();
        await _rep.AddRange(all);
        await _rep.Save();

        var expected = all.First();

        // Act
        var actual = await _rep.GetOne(expected.Id, Tracking.No);
        if(actual.IsError)
            Fail();
        
        actual = await _rep.LoadData(actual.Value!);
        if(actual.IsError)
            Fail();

        // Assert
        AreEqual(expected, actual.Value);
    }

    [Test]
    public async Task UpdateCategoryChangeNameAndAddSubCategory()
    {
        // Arrange
        var all = AllCategories();
        await _rep.AddRange(all);
        await _rep.Save();

        var expected = all.First();
        
        // Act
        expected.Name = Name.Create("New Name");

        Name name = Name.Create("New subcategory");
        Color color = Color.Parse("#F0F00F");
        Category newSubCategory = new(GuidGenerator.GenerateByBytes(), name, color);
        
        expected.AddSubCategory(newSubCategory);
        
        var resUpdate = await _rep.Update(expected);
        if(resUpdate.IsError)
            Fail();
        await _rep.Save();

        var actual = await _rep.GetOne(expected.Id, Tracking.No);
        actual = await _rep.LoadData(actual.Value!);
        if(actual.IsError)
            Fail();
        
        // Assert
        AreEqual(expected, actual.Value);
    }
    
}