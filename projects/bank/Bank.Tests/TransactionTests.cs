using BankApp;

namespace BankApp.Tests;

public class TransactionTests
{
    [Fact]
    public void Constructor_AssignsAllProperties()
    {
        Transaction t = new Transaction(TransactionType.Credit, 100m, "Opening deposit");
        Assert.Equal(TransactionType.Credit, t.Type);
        Assert.Equal(100m, t.Amount);
        Assert.Equal("Opening deposit", t.Description);
    }

    [Fact]
    public void Constructor_StampsTimestampCloseToNow()
    {
        DateTime before = DateTime.UtcNow;
        Transaction t = new Transaction(TransactionType.Credit, 1m, "x");
        DateTime after = DateTime.UtcNow;
        Assert.InRange(t.Timestamp, before, after);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100.50)]
    public void Constructor_ThrowsOnNonPositiveAmount(decimal badAmount)
    {
        Assert.Throws<ArgumentException>(
            () => new Transaction(TransactionType.Credit, badAmount, "x"));
    }

    [Fact]
    public void Transaction_IsAValueType()
    {
        // Sanity check: Transaction is a struct (value type).
        // Copying it and mutating the copy cannot affect the original,
        // and properties have no setters so the compiler also prevents
        // mutation outright.
        Assert.True(typeof(Transaction).IsValueType);
    }
}
