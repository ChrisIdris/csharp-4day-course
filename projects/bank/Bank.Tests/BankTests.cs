using BankApp;

namespace BankApp.Tests;

public class BankTests
{
    [Fact]
    public void Constructor_AssignsNameAndStartsEmpty()
    {
        Bank b = new Bank("Acme Savings");
        Assert.Equal("Acme Savings", b.Name);
        Assert.Equal(0, b.AccountCount);
        Assert.Equal(0m, b.TotalAssets);
    }

    [Fact]
    public void OpenAccount_ReturnsAccountWithAutoAssignedNumber()
    {
        Bank b = new Bank("Acme");
        Account a = b.OpenAccount("Ada", 100m);
        Assert.Equal("ACC-1000", a.AccountNumber);
        Assert.Equal("Ada", a.Holder);
        Assert.Equal(100m, a.Balance);
    }

    [Fact]
    public void OpenAccount_IncrementsAccountNumbers()
    {
        Bank b = new Bank("Acme");
        Account a = b.OpenAccount("Ada", 100m);
        Account c = b.OpenAccount("Alan", 200m);
        Account d = b.OpenAccount("Grace", 300m);
        Assert.Equal("ACC-1000", a.AccountNumber);
        Assert.Equal("ACC-1001", c.AccountNumber);
        Assert.Equal("ACC-1002", d.AccountNumber);
    }

    [Fact]
    public void OpenAccount_StoresTheAccountInTheBank()
    {
        Bank b = new Bank("Acme");
        b.OpenAccount("Ada", 100m);
        b.OpenAccount("Alan", 200m);
        Assert.Equal(2, b.AccountCount);
    }

    [Fact]
    public void TotalAssets_SumsEveryAccountsBalance()
    {
        Bank b = new Bank("Acme");
        b.OpenAccount("Ada", 100m);
        b.OpenAccount("Alan", 250m);
        Account grace = b.OpenAccount("Grace", 500m);
        grace.Withdraw(50m);   // Grace now 450
        Assert.Equal(800m, b.TotalAssets); // 100 + 250 + 450
    }

    [Fact]
    public void FindAccount_ReturnsTheMatchingAccount()
    {
        Bank b = new Bank("Acme");
        Account a = b.OpenAccount("Ada", 100m);
        b.OpenAccount("Alan", 200m);
        Account? found = b.FindAccount(a.AccountNumber);
        Assert.NotNull(found);
        Assert.Same(a, found);
    }

    [Fact]
    public void FindAccount_ReturnsNullWhenUnknown()
    {
        Bank b = new Bank("Acme");
        b.OpenAccount("Ada", 100m);
        Assert.Null(b.FindAccount("ACC-9999"));
    }

    [Fact]
    public void CloseAccount_RemovesTheAccountAndReturnsTrue()
    {
        Bank b = new Bank("Acme");
        Account a = b.OpenAccount("Ada", 100m);
        b.OpenAccount("Alan", 200m);
        bool closed = b.CloseAccount(a.AccountNumber);
        Assert.True(closed);
        Assert.Equal(1, b.AccountCount);
        Assert.Null(b.FindAccount(a.AccountNumber));
    }

    [Fact]
    public void CloseAccount_ReturnsFalseWhenUnknown()
    {
        Bank b = new Bank("Acme");
        b.OpenAccount("Ada", 100m);
        Assert.False(b.CloseAccount("ACC-9999"));
        Assert.Equal(1, b.AccountCount);
    }

    [Fact]
    public void Accounts_ExposesReadOnlyView()
    {
        Bank b = new Bank("Acme");
        b.OpenAccount("Ada", 100m);
        Assert.IsAssignableFrom<IReadOnlyList<Account>>(b.Accounts);
    }

    [Fact]
    public void NextAccountNumber_IsInstanceScopedNotShared()
    {
        // Two separate banks each start their own numbering at 1000 —
        // the counter is per-Bank, not global.
        Bank one = new Bank("One");
        Bank two = new Bank("Two");
        Account a = one.OpenAccount("Ada", 100m);
        Account c = two.OpenAccount("Alan", 200m);
        Assert.Equal("ACC-1000", a.AccountNumber);
        Assert.Equal("ACC-1000", c.AccountNumber);
    }
}
