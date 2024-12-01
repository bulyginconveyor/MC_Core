using static NUnit.Framework.Assert;

namespace testing_valueobjects;

[Parallelizable(ParallelScope.All)]
public class TermTest
{
    [Test]
    [TestCase(UnitTerm.Day, 120u)]
    [TestCase(UnitTerm.Week, 2u)]
    [TestCase(UnitTerm.Month, 6u)]
    [TestCase(UnitTerm.Year, 30u)]
    public void CreateWithValidData(UnitTerm unit, uint countDays)
    {
        // Arrange
        
        // Act
        var term = Term.Create(unit, countDays);
        
        // Assert
        Pass();
    }

    [Test]
    [TestCase(UnitTerm.Day, 0u)]
    [TestCase(UnitTerm.Week, 0u)]
    [TestCase(UnitTerm.Month, 0u)]
    [TestCase(UnitTerm.Year, 0u)]
    public void CreateWithInvalidData(UnitTerm unit, uint countDays)
    {
        // Arrange
        
        // Act
        try
        {
            var term = Term.Create(unit, countDays);
        }
        catch (ArgumentException ex)
        {
            Pass();
        }
        
        // Assert
        Fail();
    }

    [Test]
    public void NextDateWithDays5StartDate12_02_2022()
    {
        // Arrange
        var unit = UnitTerm.Day;
        var countDays = 5u;
        
        var startDate = new DateTime(2022, 2, 12);
        var expectedDate = new DateTime(2022, 2, 17);
        
        var term = Term.Create(unit, countDays);
        
        // Act
        var res = term.EndDate(startDate);

        if (res.IsError)
            Fail(res.ErrorMessage);
        
        // Assert
        AreEqual(expectedDate, res.Value);
    }

    [Test]
    public void NextDateWithWeeks2StartDate12_02_2022()
    {
        // Arrange
        var unit = UnitTerm.Week;
        var countDays = 2u;
        
        var startDate = new DateTime(2022, 2, 12);
        var expectedDate = new DateTime(2022, 2, 26);
        
        var term = Term.Create(unit, countDays);
        
        // Act
        var res = term.EndDate(startDate);

        if (res.IsError)
            Fail(res.ErrorMessage);
        
        // Assert
        AreEqual(expectedDate, res.Value);
    }
    
    [Test]
    public void NextDateWithMonth6StartDate12_02_2022()
    {
        // Arrange
        var unit = UnitTerm.Month;
        var countDays = 6u;
        
        var startDate = new DateTime(2022, 2, 12);
        var expectedDate = new DateTime(2022, 8, 12);
        
        var term = Term.Create(unit, countDays);
        
        // Act
        var res = term.EndDate(startDate);

        if (res.IsError)
            Fail(res.ErrorMessage);
        
        // Assert
        AreEqual(expectedDate, res.Value);
    }
    
    [Test]
    public void NextDateWithYears30StartDate12_02_2022()
    {
        // Arrange
        var unit = UnitTerm.Year;
        var countDays = 30u;
        
        var startDate = new DateTime(2022, 2, 12);
        var expectedDate = new DateTime(2052, 2, 12);
        
        var term = Term.Create(unit, countDays);
        
        // Act
        var res = term.EndDate(startDate);

        if (res.IsError)
            Fail(res.ErrorMessage);
        
        // Assert
        AreEqual(expectedDate, res.Value);
    }
}