using System.Text;

namespace BankApp;

// A bank account belonging to one holder. Keeps its own transaction history
// and computes its balance from that history (nothing else stores a balance).
//
// Key rules:
//   • The opening balance passed to the constructor is recorded as the FIRST
//     transaction (a Credit with description "Opening deposit") — UNLESS
//     startingBalance is exactly 0, in which case the history starts empty.
//   • Balance is derived: sum of Credits minus sum of Debits.
//   • Deposit and Withdraw both require a positive amount (ArgumentException
//     otherwise). Withdraw throws InvalidOperationException if the amount
//     would take the balance below zero (no overdrafts in the core spec).
//
// The `transactions` list is private so outside code can only change the
// account through Deposit / Withdraw — the balance invariant stays intact.
public class Account
{
    private List<Transaction> transactions;

    public string AccountNumber { get; }
    public string Holder { get; }

    public Account(string accountNumber, string holder, decimal startingBalance)
    {
        if (startingBalance < 0)
            throw new ArgumentOutOfRangeException(nameof(startingBalance));

        this.AccountNumber = accountNumber;
        this.Holder = holder;
        this.transactions = new List<Transaction>();
        if (startingBalance > 0)
        {
            this.transactions.Add(new Transaction(TransactionType.Credit, startingBalance, "Opening deposit"));
        }
    }

    // Computed on every read — there's no stored balance field.
    public decimal Balance
    {
        get
        {
            decimal balance = 0;
            foreach (var transaction in transactions)
            {
                if (transaction.Type == TransactionType.Credit)
                {
                    balance += transaction.Amount;
                }
                else if (transaction.Type == TransactionType.Debit)
                {
                    balance -= transaction.Amount;
                }
            }
            return balance;
        }
    }

    public int TransactionCount
    {
        get { return transactions.Count; }
    }

    // Expose a read-only view — callers can enumerate but not Add/Remove.
    public IReadOnlyList<Transaction> Transactions
    {
        get { return transactions.AsReadOnly(); }
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be positive", nameof(amount));
        }
        this.transactions.Add(new Transaction(TransactionType.Credit, amount, "Deposit"));
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be positive", nameof(amount));
        }
        if (amount > Balance)
        {
            throw new InvalidOperationException("Insufficient funds");
        }
        this.transactions.Add(new Transaction(TransactionType.Debit, amount, "Withdrawal"));
    }

    // Returns a printable multi-line bank statement. Format is deliberately
    // our own choice here — the tests only check that the required fields
    // appear in the output, so you're free to make it pretty.
    //TODO: return a multi-line string — header with AccountNumber, Holder, and Balance; then a row per transaction showing timestamp, CREDIT/DEBIT, amount, and description. See the `decimal` and DateTime mini-lessons in README.md for formatting.
    public string Statement()
    {
        var sb = new StringBuilder();

        sb.AppendLine($"Account Statement");
        sb.AppendLine($"Account Number: {AccountNumber}");
        sb.AppendLine($"Holder: {Holder}");
        sb.AppendLine($"Current Balance: {Balance:C}");
        sb.AppendLine();
        sb.AppendLine("Transactions:");
        sb.AppendLine("----------------------------------------");

        foreach (var transaction in transactions)
        {
            string type = transaction.Type == TransactionType.Credit ? "CREDIT" : "DEBIT";
            sb.AppendLine($"{transaction.Timestamp:yyyy-MM-dd HH:mm:ss} | {type,-6} | {transaction.Amount,10:C} | {transaction.Description}");
        }

        return sb.ToString();
    }

    // Case-insensitive substring match on Description.
    // Results are sorted oldest-first by Timestamp.
    public List<Transaction> FindTransactions(string search)
    {
        if (string.IsNullOrEmpty(search))
        {
            return new List<Transaction>();
        }

        return transactions
            .Where(t => t.Description.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0)
            .OrderBy(t => t.Timestamp)
            .ToList();
    }
}
