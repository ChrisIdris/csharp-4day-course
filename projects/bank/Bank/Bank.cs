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
        throw new NotImplementedException("TODO: assign Name, initialise accounts = new List<Account>(), and start nextAccountNumber at 1000");
    }

    public int AccountCount
    {
        get { throw new NotImplementedException("TODO: return accounts.Count"); }
    }

    // Sum of every account's balance at this moment. Computed, not stored.
    public decimal TotalAssets
    {
        get { throw new NotImplementedException("TODO: iterate accounts and sum each one's Balance"); }
    }

    public IReadOnlyList<Account> Accounts
    {
        get { throw new NotImplementedException("TODO: return accounts.AsReadOnly()"); }
    }

    public Account OpenAccount(string holder, decimal startingBalance)
    {
        throw new NotImplementedException("TODO: format the next account number as $\"ACC-{nextAccountNumber}\", increment the counter, construct an Account, add it to the accounts list, and return it");
    }

    // Returns null when no account matches.
    public Account? FindAccount(string accountNumber)
    {
        throw new NotImplementedException("TODO: iterate accounts and return the one whose AccountNumber matches; otherwise return null");
    }

    // Returns true if an account was found and removed, false otherwise.
    public bool CloseAccount(string accountNumber)
    {
        throw new NotImplementedException("TODO: find the matching account and remove it from the list; return true if removed, false if not found");
    }
}
