using static NUnit.Framework.Assert;

namespace testing_valueobjects;

[Parallelizable(ParallelScope.All)]
public class PhotoUrlTest
{
    [Test]
    [TestCase("https://www.google.com/images/branding/googlelogo/2x/googlelogo_color_272x92dp.png")]
    [TestCase("https://moneycuhsion.ru/картинки/иконка-банк-деньги-дёньги.jpg")]
    public void CreateWithValidData(string url)
    {
        // Arrange
        
        // Act
        var photoUrl = PhotoUrl.Create(url);
        
        // Assert
        AreEqual(url, photoUrl.Url);
    }

    [Test]
    [TestCase("")]
    [TestCase(null)]
    [TestCase(" ")]
    [TestCase("http:/google.com")]
    [TestCase("https://googlecom")]
    public void CreateWithInvalidData(string url)
    {
        // Arrange
        
        // Act
        try
        {
            var photoUrl = PhotoUrl.Create(url);
        }
        catch (ArgumentException ex)
        {
            Pass();
        }
        
        // Assert
        Fail();
    }
}