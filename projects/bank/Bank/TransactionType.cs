namespace BankApp;

// Two kinds of transaction against an account.
//   Credit → money INTO the account (starting deposit, deposits)
//   Debit  → money OUT of the account (withdrawals)
//
// Balance is computed by summing Credits and subtracting Debits.
public enum TransactionType
{
    Credit,
    Debit
}
