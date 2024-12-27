using core_service.domain.models.valueobjects;
using core_service.domain.models.valueobjects.enums;
using static NUnit.Framework.Assert;

namespace testing_valueobjects;

[Parallelizable(ParallelScope.All)]
public class PeriodTest
{
    [Test]
    [TestCase(TypePeriod.Month, (ushort)28)]
    [TestCase(TypePeriod.UnitDay, (ushort)1)]
    [TestCase(TypePeriod.UnitWeek, (ushort)2)]
    [TestCase(TypePeriod.UnitMonth, (ushort)4)]
    [TestCase(TypePeriod.UnitYear, (ushort)3)]
    public void CreateWithValidData(TypePeriod type, ushort value)
    {
        // Arrange
        
        // Act
        var period = Period.Create(type, value);
        
        // Assert
        Pass();
    }

    [Test]
    [TestCase(TypePeriod.Month, (ushort)29)]
    [TestCase(TypePeriod.UnitDay, (ushort)0)]
    public void CreateWithInvalidData(TypePeriod type, ushort value)
    {
        // Arrange
        
        // Act
        try
        {
            Period.Create(type, value);
        }
        catch (ArgumentException ex)
        {
            Pass();
        }
        
        // Assert
        Fail();
    }

    
    // Tests method NextDate(DateOnly startDate)
    
    [Test]
    public void NextDateWith18MonthStartDate12_02_2022()
    {
        // Arrange
        var startDate = new DateOnly(2022, 2, 12); // 12/02/2022
        var period = Period.Create(TypePeriod.Month, 18);
        
        // Act
        var nextDate = period.NextDate(startDate);
        
        // Assert
        var expectedDate = new DateOnly(2022, 3, 18); // 18/03/2022
        AreEqual(expectedDate, nextDate);
    }
    
    [Test]
    public void NextDateWith3DaysStartDate12_02_2022()
    {
        // Arrange
        var startDate = new DateOnly(2022, 2, 12); // 12/02/2022
        var period = Period.Create(TypePeriod.UnitDay, 3);
        
        // Act
        var nextDate = period.NextDate(startDate);
        
        // Assert
        var expectedDate = new DateOnly(2022, 2, 15); // 15/02/2022
        AreEqual(expectedDate, nextDate);
    }
    
    [Test]
    public void NextDateWith2WeekStartDate12_02_2022()
    {
        // Arrange
        var startDate = new DateOnly(2022, 2, 12); // 12/02/2022
        var period = Period.Create(TypePeriod.UnitWeek, 2);
        
        // Act
        var nextDate = period.NextDate(startDate);
        
        // Assert
        var expectedDate = new DateOnly(2022, 2, 26); // 26/02/2022
        AreEqual(expectedDate, nextDate);
    }

    [Test]
    public void NextDateWith3MonthStartDate12_02_2022()
    {
        // Arrange
        var startDate = new DateOnly(2022, 2, 12); // 12/02/2022
        var period = Period.Create(TypePeriod.UnitMonth, 3);
        
        // Act
        var nextDate = period.NextDate(startDate);
        
        // Assert
        var expectedDate = new DateOnly(2022, 5, 12); // 12/05/2022
        AreEqual(expectedDate, nextDate);
    }

    [Test]
    public void NextDateWith5YearStartDate12_02_2022()
    {
        // Arrange
        var startDate = new DateOnly(2022, 2, 12); // 12/02/2022
        var period = Period.Create(TypePeriod.UnitYear, 5);
         
        // Act
        var nextDate = period.NextDate(startDate);
         
        // Assert
        var expectedDate = new DateOnly(2027, 2, 12); // 12/05/2022
        AreEqual(expectedDate, nextDate);
    }
}