using BankApp;

// Demo scaffolding — this is here so you can see the domain working end-to-end
// when you press F5. Students: extend this with your own scenarios once the
// tests are green.
//
// Note the `m` suffix on every amount literal — that marks it as a `decimal`.
// For the full rundown on why money uses `decimal` (and not `double` / `float`),
// how to format amounts, and how to round them, see the
// "`decimal` — mini-lesson" section in README.md.

Bank bank = new Bank("Acme Savings");

Account ada = bank.OpenAccount("Ada Lovelace", 1250.75m);
Account alan = bank.OpenAccount("Alan Turing", 3410.00m);

// A week of Ada's account activity — all amounts in decimals.
ada.Deposit(89.99m);    // payday top-up
ada.Withdraw(12.50m);   // lunch
ada.Withdraw(47.33m);   // groceries
ada.Deposit(500m);      // refund

// Alan's activity, including a big bill.
alan.Withdraw(1299.99m);
alan.Deposit(42.42m);
alan.Withdraw(2000m);

// Try to overdraw — the account throws InvalidOperationException,
// we catch it so the program keeps running.
try
{
    alan.Withdraw(10_000m);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Blocked withdrawal on {alan.AccountNumber}: {ex.Message}");
    Console.WriteLine();
}

Console.WriteLine($"Bank: {bank.Name}");
Console.WriteLine($"Accounts open: {bank.AccountCount}");
Console.WriteLine($"Total assets: {bank.TotalAssets:N2}");
Console.WriteLine();

Console.WriteLine(ada.Statement());
Console.WriteLine();
Console.WriteLine(alan.Statement());

// Show FindTransactions — look up every "deposit" on Ada's account.
Console.WriteLine();
Console.WriteLine($"Ada's deposits:");
foreach (Transaction t in ada.FindTransactions("deposit"))
{
    Console.WriteLine($"  {t.Timestamp:yyyy-MM-dd HH:mm}   {t.Amount,10:N2}   {t.Description}");
}

// TODO (students): extend the demo. Ideas —
//   • Try more edge cases — zero amounts, negative amounts — and catch the
//     ArgumentException each one throws.
//   • Close an account and print the updated bank totals.
//   • Add a third customer with their own sequence of transactions.
//   • Implement one of the Extensions from the README (Transfer, Overdraft…).
