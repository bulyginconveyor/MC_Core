using static NUnit.Framework.Assert;

namespace testing_valueobjects;

[Parallelizable(ParallelScope.All)]
[TestFixture]
public class BalanceTest
{
    [Test]
    public void CreateBalanceWithZero()
    {
        // Arrange
        decimal balance = Decimal.Zero;
        
        // Act
        var balPositive = Balance.Create(false, balance);
        var balNegative = Balance.Create(true, balance);
        
        // Assert
        Pass();
    }

    [Test]
    public void CreateBalanceWithNegativeNumberAndMaybeNegative()
    {
        // Arrange
        decimal balance = -1;
        bool maybeNegative = true;
        
        // Act
        var bal = Balance.Create(maybeNegative, balance);
        
        // Assert
        Pass();
    }

    [Test]
    public void CreateBalanceWithNegativeNumberAndNotMaybeNegative()
    {
        // Arrange
        decimal balance = -1;
        bool maybeNegative = false;
        
        // Act
        try
        {
            var bal = Balance.Create(maybeNegative, balance);
        }
        catch (ArgumentException ex)
        {
            Pass();
        }

        // Assert
        Fail();
    }
}