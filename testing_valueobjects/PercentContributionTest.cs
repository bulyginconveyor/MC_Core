using System.Text;
using static NUnit.Framework.Assert;

namespace testing_valueobjects;

[Parallelizable(ParallelScope.All)]
public class PercentContributionTest
{
    [Test]
    public void ValidData()
    {
        // Arrange
        ushort countDays = 30;
        var percent = UDecimal.Parse(14.4);

        // Act
        var percentContribution = PercentContribution.Create(percent, countDays);

        // Assert
        Pass();
    }

    [Test]
    [TestCase( 0, (ushort)0)]
    [TestCase( 0, (ushort)1)]
    [TestCase( 1.4, (ushort)0)]
    public void InvalidData(decimal percent, ushort countDays)
    {
        // Arrange
        var prc = UDecimal.Parse(percent);

        // Act
        try
        {
            var percentContribution = PercentContribution.Create(prc, countDays);
        }
        catch (ArgumentException ex)
        {
            Pass();
        }
        
        // Assert
        Fail();
    }
}