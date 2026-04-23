namespace BankApp;

// A single movement of money against an Account.
//
// Transactions are a STRUCT — they're small, immutable records of "what
// happened, when, and why". Once created, none of their fields change.
// An account's transaction history is just a list of these.
//
// Fields:
//   Type        Credit or Debit (see TransactionType)
//   Amount      Always POSITIVE. The Type decides whether it adds or subtracts.
//   Timestamp   When the transaction was recorded (UTC).
//   Description Human-readable label, e.g. "Opening deposit", "Withdrawal".
//
// Why positive-only `Amount` and a separate `Type`? It keeps validation
// simple ("amount must be > 0") and makes the transaction record read
// clearly when printed.
public struct Transaction
{
    public TransactionType Type { get; }
    public decimal Amount { get; }
    public DateTime Timestamp { get; }
    public string Description { get; }

    public Transaction(TransactionType type, decimal amount, string description)
    {
        throw new NotImplementedException("TODO: throw ArgumentException if amount <= 0, then assign Type, Amount, Description, and set Timestamp = DateTime.UtcNow");
    }
}
