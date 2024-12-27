using core_service.domain.models.valueobjects;
using static NUnit.Framework.Assert;

namespace testing_valueobjects;

[Parallelizable(ParallelScope.All)]
[TestFixture]
public class ColorTest
{
    [Test]
    [TestCase("#FFF")]
    [TestCase("#F1ad23")]
    [TestCase("#f1ad")]
    [TestCase("#f1ad23FF")]
    public void CreateColorWithValidData(string color)
    {
        // Arrange

        // Act
        var clr = Color.Parse(color);

        // Assert
        Pass();
    }

    [Test]
    [TestCase("")]
    [TestCase(null)]
    [TestCase("F1234")]
    [TestCase("#F09090923")]
    [TestCase("#rrggbb")]
    public void CreateColorWithInvalidData(string color)
    {
        // Arrange
        
        // Act
        try
        {
            var clr = Color.Parse(color);
        }
        catch (ArgumentException ex)
        {
            Pass();
        }
        
        // Assert
        Fail();
    }
    
}