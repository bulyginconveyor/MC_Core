using core_service.domain.models.valueobjects;
using static NUnit.Framework.Assert;

namespace testing_valueobjects;

[Parallelizable(ParallelScope.All)]
public class IsoCodeTest
{
    [Test]
    [TestCase("RUB")]
    [TestCase("usd")]
    [TestCase("cNY")]
    [TestCase("EuR")]
    [TestCase("JPy")]
    public void ValidCurrencyIsoCode(string isoCode)
    {
        // Arrange

        // Act 
        var ic = IsoCode.Create(isoCode);

        // Assert
        Pass();
    }

    [Test]
    [TestCase("rublie")]
    [TestCase("руб")]
    [TestCase("153")]
    public void InvalidCuurencyIsoCode(string isoCode)
    {
        // Arrange

        // Act 
        try
        {
            var ic = IsoCode.Create(isoCode);
        }
        catch (ArgumentException ex)
        {
            Pass();
        }

        // Assert
        Fail();
    }
}