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
        throw new NotImplementedException("TODO: throw ArgumentException if startingBalance < 0. Assign AccountNumber and Holder. Initialise transactions = new List<Transaction>(). If startingBalance > 0, append an opening Credit transaction with description \"Opening deposit\".");
    }

    // Computed on every read — there's no stored balance field.
    public decimal Balance
    {
        get { throw new NotImplementedException("TODO: iterate transactions, add Credit amounts, subtract Debit amounts, return the running total"); }
    }

    public int TransactionCount
    {
        get { throw new NotImplementedException("TODO: return transactions.Count"); }
    }

    // Expose a read-only view — callers can enumerate but not Add/Remove.
    public IReadOnlyList<Transaction> Transactions
    {
        get { throw new NotImplementedException("TODO: return transactions.AsReadOnly()"); }
    }

    public void Deposit(decimal amount)
    {
        throw new NotImplementedException("TODO: throw ArgumentException if amount <= 0, then add a Credit transaction with description \"Deposit\"");
    }

    public void Withdraw(decimal amount)
    {
        throw new NotImplementedException("TODO: throw ArgumentException if amount <= 0, throw InvalidOperationException if amount > Balance, otherwise add a Debit transaction with description \"Withdrawal\". On failure the transaction must NOT be recorded.");
    }

    // Returns a printable multi-line bank statement. Format is deliberately
    // our own choice here — the tests only check that the required fields
    // appear in the output, so you're free to make it pretty.
    public string Statement()
    {
        throw new NotImplementedException("TODO: return a multi-line string — header with AccountNumber, Holder, and Balance; then a row per transaction showing timestamp, CREDIT/DEBIT, amount, and description. See the `decimal` and DateTime mini-lessons in README.md for formatting.");
    }

    // Case-insensitive substring match on Description.
    // Results are sorted oldest-first by Timestamp.
    public List<Transaction> FindTransactions(string search)
    {
        throw new NotImplementedException("TODO: return every transaction whose Description contains `search` (case-insensitive), sorted by Timestamp oldest-first");
    }
}
