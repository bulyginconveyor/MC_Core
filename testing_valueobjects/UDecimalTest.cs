using static NUnit.Framework.Assert;

namespace testing_valueobjects;

[Parallelizable(ParallelScope.All)]
public class UDecimalTest
{
    [Test]
    public void Parse0()
    {
        // Arrange
        var zero = 0;
        
        // Act
        var udecimal = UDecimal.Parse(zero);
        
        // Assert
        Pass();
    }

    [Test]
    public void ParsePositive()
    {
        // Arrange
        var positive = 10234.354;
        
        // Act
        var udecimal = UDecimal.Parse(positive);
        
        // Assert
        Pass();
    }

    [Test]
    public void ParseNegative()
    {
        // Arrange
        var negative = -10234.354;
        
        // Act
        try
        {
            var udecimal = UDecimal.Parse(negative);
        }
        catch (ArgumentException ex)
        {
            Pass();
        }
        
        // Assert
        Fail();
    }
}