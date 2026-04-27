using BankApp;

namespace BankApp.Tests;

public class AccountTests
{
    // ── Construction ───────────────────────────────────────────────

    [Fact]
    public void Constructor_AssignsNumberAndHolder()
    {
        Account a = new Account("ACC-1000", "Ada Lovelace", 100m);
        Assert.Equal("ACC-1000", a.AccountNumber);
        Assert.Equal("Ada Lovelace", a.Holder);
    }

    [Fact]
    public void Constructor_StartingBalanceCreatesOpeningCreditTransaction()
    {
        Account a = new Account("ACC-1000", "Ada", 250m);
        Assert.Equal(1, a.TransactionCount);
        Assert.Equal(TransactionType.Credit, a.Transactions[0].Type);
        Assert.Equal(250m, a.Transactions[0].Amount);
        Assert.Equal("Opening deposit", a.Transactions[0].Description);
    }

    [Fact]
    public void Constructor_ZeroStartingBalanceRecordsNoOpeningTransaction()
    {
        Account a = new Account("ACC-1000", "Ada", 0m);
        Assert.Equal(0, a.TransactionCount);
        Assert.Equal(0m, a.Balance);
    }

    [Fact]
    public void Constructor_ThrowsOnNegativeStartingBalance()
    {
        Assert.Throws<ArgumentException>(
            () => new Account("ACC-1000", "Ada", -1m));
    }

    // ── Balance ────────────────────────────────────────────────────

    [Fact]
    public void Balance_MatchesStartingDeposit()
    {
        Account a = new Account("ACC-1000", "Ada", 100m);
        Assert.Equal(100m, a.Balance);
    }

    [Fact]
    public void Balance_ReflectsCreditsAndDebits()
    {
        Account a = new Account("ACC-1000", "Ada", 100m);
        a.Deposit(50m);
        a.Withdraw(30m);
        a.Deposit(10m);
        Assert.Equal(130m, a.Balance);
    }

    // ── Deposit ────────────────────────────────────────────────────

    [Fact]
    public void Deposit_AddsCreditTransaction()
    {
        Account a = new Account("ACC-1000", "Ada", 100m);
        a.Deposit(50m);
        Assert.Equal(2, a.TransactionCount);
        Assert.Equal(TransactionType.Credit, a.Transactions[1].Type);
        Assert.Equal(50m, a.Transactions[1].Amount);
        Assert.Equal(150m, a.Balance);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-25)]
    public void Deposit_ThrowsOnNonPositiveAmount(decimal badAmount)
    {
        Account a = new Account("ACC-1000", "Ada", 100m);
        Assert.Throws<ArgumentException>(() => a.Deposit(badAmount));
    }

    // ── Withdraw ───────────────────────────────────────────────────

    [Fact]
    public void Withdraw_AddsDebitTransaction()
    {
        Account a = new Account("ACC-1000", "Ada", 100m);
        a.Withdraw(40m);
        Assert.Equal(2, a.TransactionCount);
        Assert.Equal(TransactionType.Debit, a.Transactions[1].Type);
        Assert.Equal(40m, a.Transactions[1].Amount);
        Assert.Equal(60m, a.Balance);
    }

    [Fact]
    public void Withdraw_ThrowsOnInsufficientFunds()
    {
        Account a = new Account("ACC-1000", "Ada", 100m);
        Assert.Throws<InvalidOperationException>(() => a.Withdraw(500m));
    }

    [Fact]
    public void Withdraw_DoesNotRecordTransactionWhenItFails()
    {
        Account a = new Account("ACC-1000", "Ada", 100m);
        try { a.Withdraw(500m); } catch (InvalidOperationException) { }
        Assert.Equal(1, a.TransactionCount); // only the opening deposit
        Assert.Equal(100m, a.Balance);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-25)]
    public void Withdraw_ThrowsOnNonPositiveAmount(decimal badAmount)
    {
        Account a = new Account("ACC-1000", "Ada", 100m);
        Assert.Throws<ArgumentException>(() => a.Withdraw(badAmount));
    }

    // ── Transactions (read-only view) ──────────────────────────────

    [Fact]
    public void Transactions_IsReadOnlyView()
    {
        Account a = new Account("ACC-1000", "Ada", 100m);
        // The returned collection is IReadOnlyList — there is no Add method,
        // so callers cannot bypass Deposit / Withdraw to mutate history.
        Assert.IsAssignableFrom<IReadOnlyList<Transaction>>(a.Transactions);
    }

    // ── Transfer ────────────────────────────────────────────────────

    [Fact]

    public void Transfer_MovesFundsBetweenAccounts()
    {
        Account a1 = new Account("ACC-1000", "Ada", 200m);
        Account a2 = new Account("ACC-2000", "Bob", 50m);
        a1.Transfer(a2, 75m);
        Assert.Equal(125m, a1.Balance);
        Assert.Equal(125m, a2.Balance);
    }

    [Fact]
    public void Transfer_ThrowsOnInsufficientFunds()
    {
        Account a1 = new Account("ACC-1000", "Ada", 100m);
        Account a2 = new Account("ACC-2000", "Bob", 50m);
        Assert.Throws<InvalidOperationException>(() => a1.Transfer(a2, 150m));
    }

    [Fact]
    public void Transfer_ThrowsOnNullTargetAccount()
    {
        Account a1 = new Account("ACC-1000", "Ada", 100m);
        Assert.Throws<ArgumentNullException>(() => a1.Transfer(null, 50m));
    }

    [Fact]
    public void Transfer_ThrowsOnNonPositiveAmount()
    {
        Account a1 = new Account("ACC-1000", "Ada", 100m);
        Account a2 = new Account("ACC-2000", "Bob", 50m);
        Assert.Throws<ArgumentException>(() => a1.Transfer(a2, 0m));
        Assert.Throws<ArgumentException>(() => a1.Transfer(a2, -50m));
    }

    // ── Statement ──────────────────────────────────────────────────
    // Format is deliberately loose: we check that required fields appear,
    // not the exact layout. Students have freedom to design the output.

    [Fact]
    public void Statement_IncludesAccountNumberHolderAndBalance()
    {
        Account a = new Account("ACC-1000", "Ada Lovelace", 100m);
        string s = a.Statement();
        Assert.Contains("ACC-1000", s);
        Assert.Contains("Ada Lovelace", s);
        Assert.Contains("100", s); // balance rendered somewhere
    }

    [Fact]
    public void Statement_ListsEveryTransaction()
    {
        Account a = new Account("ACC-1000", "Ada", 100m);
        a.Deposit(50m);
        a.Withdraw(30m);
        string s = a.Statement();
        Assert.Contains("Opening deposit", s);
        Assert.Contains("Deposit", s);
        Assert.Contains("Withdrawal", s);
        Assert.Contains("CREDIT", s);
        Assert.Contains("DEBIT", s);
    }

    [Fact]
    public void Statement_ContainsMultipleLines()
    {
        Account a = new Account("ACC-1000", "Ada", 100m);
        string s = a.Statement();
        Assert.Contains("\n", s);
    }

    // ── FindTransactions ───────────────────────────────────────────

    [Fact]
    public void FindTransactions_ReturnsAllMatchesByDescription()
    {
        Account a = new Account("ACC-1000", "Ada", 100m);
        a.Deposit(50m);
        a.Withdraw(30m);
        a.Deposit(20m);
        // "Deposit" should match both "Opening deposit" (case-insensitive)
        // and the two "Deposit" entries.
        List<Transaction> matches = a.FindTransactions("deposit");
        Assert.Equal(3, matches.Count);
    }

    [Fact]
    public void FindTransactions_IsCaseInsensitive()
    {
        Account a = new Account("ACC-1000", "Ada", 100m);
        a.Deposit(50m);
        Assert.Equal(2, a.FindTransactions("DEPOSIT").Count);
        Assert.Equal(2, a.FindTransactions("Deposit").Count);
        Assert.Equal(2, a.FindTransactions("deposit").Count);
    }

    [Fact]
    public void FindTransactions_ReturnsEmptyListWhenNothingMatches()
    {
        Account a = new Account("ACC-1000", "Ada", 100m);
        a.Deposit(50m);
        List<Transaction> matches = a.FindTransactions("transfer");
        Assert.NotNull(matches);
        Assert.Empty(matches);
    }

    [Fact]
    public void FindTransactions_ReturnsResultsSortedByTimestampOldestFirst()
    {
        Account a = new Account("ACC-1000", "Ada", 100m);
        // Small sleeps ensure distinct timestamps so the sort is verifiable.
        System.Threading.Thread.Sleep(15);
        a.Deposit(50m);
        System.Threading.Thread.Sleep(15);
        a.Deposit(20m);

        List<Transaction> matches = a.FindTransactions("deposit");
        Assert.Equal(3, matches.Count);
        for (int i = 1; i < matches.Count; i++)
        {
            Assert.True(matches[i - 1].Timestamp <= matches[i].Timestamp);
        }
    }
}
