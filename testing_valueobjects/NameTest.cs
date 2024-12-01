using static NUnit.Framework.Assert;

namespace testing_valueobjects;

[Parallelizable(ParallelScope.All)]
public class NameTest
{
    [Test]
    public void ValidData()
    {
        // Arrange
        var str = "name";

        // Act
        var name = Name.Create(str);

        // Assert
        Pass();
    }

    [Test]
    [TestCase("")]
    [TestCase(null)] 
    [TestCase("      ")] 
    public void InvalidData(string str)
    {
        // Arrange

        // Act
        try
        {
            var name = Name.Create(str);
        }
        catch (ArgumentException ex)
        {
            Pass();
        }

        // Assert
        Fail();
    }
}