namespace BankApp;

// The top-level simulation: one Bank holds many Accounts, generates account
// numbers, and exposes summary info across the whole portfolio.
//
// Account numbers are auto-generated in the form "ACC-1000", "ACC-1001", ...
// The next-number counter lives as an instance field on the Bank (one counter
// per Bank instance). Simple, private, no `static` required.
public class Bank
{
    private List<Account> accounts;
    private int nextAccountNumber;

    public string Name { get; }

    public Bank(string name)
    {
        this.Name = name;
        this.accounts = new List<Account>();
        this.nextAccountNumber = 1000;
        throw new NotImplementedException("TODO: assign Name, initialise accounts = new List<Account>(), and start nextAccountNumber at 1000");
    }

    public int AccountCount
    {
        get { return accounts.Count; }
    }

    // Sum of every account's balance at this moment. Computed, not stored.
    public decimal TotalAssets
    {
        get
        {
            decimal total = 0;
            foreach (var account in accounts)
            {
                total += account.Balance;
            }
            return total;
        }
    }

    public IReadOnlyList<Account> Accounts
    {
        get { return accounts.AsReadOnly(); }
    }

    public Account OpenAccount(string holder, decimal startingBalance)
    {
        string accountNumber = $"ACC-{nextAccountNumber}";
        nextAccountNumber++;
        var account = new Account(accountNumber, holder, startingBalance);
        accounts.Add(account);
        return account;
    }

    // Returns null when no account matches.
    public Account? FindAccount(string accountNumber)
    {
        foreach (var account in accounts)
        {
            if (account.AccountNumber == accountNumber)
            {
                return account;
            }
        }
        return null;
    }

    // Returns true if an account was found and removed, false otherwise.
    public bool CloseAccount(string accountNumber)
    {

        throw new NotImplementedException("TODO: find the matching account and remove it from the list; return true if removed, false if not found");
    }
}
