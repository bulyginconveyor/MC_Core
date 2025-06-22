using System.Runtime.InteropServices.JavaScript;
using core_service.domain.models.valueobjects;
using static NUnit.Framework.Assert;

namespace testing_valueobjects;

[Parallelizable(ParallelScope.All)]
public class DateRangeTest
{
    [Test]
    public void TestStartDateEarlierEndDate()
    {
        // Arrange
        var dateStart = new DateOnly(2020, 1, 1);
        var dateEnd = new DateOnly(2021, 1, 1);

        // Act
        var dateRange = DateRange.Create(dateStart, dateEnd);

        // Assert
        Pass();
    }

    [Test]
    public void TestEndDateEarlierStartDate()
    {
        // Arrange
        var dateStart = new DateOnly(2021, 1, 1);
        var dateEnd = new DateOnly(2020, 1, 1);
        
        // Act
        try
        {
            var dateRange = DateRange.Create(dateStart, dateEnd);
        }
        catch (ArgumentException ex)
        {
            Pass();
        }

        // Assert
        Fail();
    }
}
