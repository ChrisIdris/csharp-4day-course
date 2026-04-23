# Bank Simulator — Capstone Project

A small object-oriented bank simulation. You will design and implement:

- A **`Bank`** that owns a portfolio of accounts and can open/close/look them up.
- An **`Account`** that holds a transaction history and computes its balance from it.
- A **`Transaction`** value record (a `struct`) that captures one movement of money.
- A **`TransactionType`** enum distinguishing credits from debits.

Everything you need to build is captured by the tests in `Bank.Tests/`. Your job
is to make them all green.

---

## How this project is organised

```
projects/bank/
├── Bank.slnx                 ← solution file (open this in Visual Studio)
├── global.json               ← pins .NET 10
├── Bank/
│   ├── Bank.csproj           ← console app
│   ├── Program.cs            ← demo scaffolding — extend as you like
│   ├── TransactionType.cs
│   ├── Transaction.cs        ← struct
│   ├── Account.cs            ← class
│   └── Bank.cs               ← class
└── Bank.Tests/
    ├── Bank.Tests.csproj     ← xUnit tests
    ├── TransactionTests.cs
    ├── AccountTests.cs
    └── BankTests.cs
```

The `Bank.csproj` root namespace is **`BankApp`** so there's no clash between the
project name and the `Bank` class. Every source file starts with
`namespace BankApp;`.

---

## Running it

From `projects/bank/`:

```sh
dotnet test      # runs every test — initially red, go green by implementing
dotnet run --project Bank   # runs Program.cs (the demo)
```

Or open `Bank.slnx` in Visual Studio and use Test Explorer / F5 as usual.

---

## Core spec

Read the type skeletons in `Bank/*.cs` alongside the rules below. The tests
enforce everything here — if a rule is listed, there is a test for it.

### `TransactionType` (enum)

Two values: `Credit` (money in) and `Debit` (money out).

### `Transaction` (struct — immutable)

A single movement of money. **Must be a `struct` — not a class.** Fields (all
read-only, all set via the constructor):

| Field         | Type              | Notes                                     |
|---------------|-------------------|-------------------------------------------|
| `Type`        | `TransactionType` |                                           |
| `Amount`      | `decimal`         | Always positive — the `Type` decides sign |
| `Timestamp`   | `DateTime`        | Set to `DateTime.UtcNow` in the ctor      |
| `Description` | `string`          | e.g. `"Opening deposit"`, `"Withdrawal"`  |

Constructor: `Transaction(TransactionType type, decimal amount, string description)`

Rules:

- Throw `ArgumentException` if `amount <= 0`.

### `Account` (class)

One customer's account. Owns a **private** list of transactions and exposes
a read-only view.

Properties:

| Property           | Type                            | Notes                           |
|--------------------|---------------------------------|---------------------------------|
| `AccountNumber`    | `string`                        | Assigned by the `Bank`          |
| `Holder`           | `string`                        |                                 |
| `Balance`          | `decimal`                       | **Computed** from transactions  |
| `TransactionCount` | `int`                           |                                 |
| `Transactions`     | `IReadOnlyList<Transaction>`    | Outside code cannot Add/Remove  |

Constructor: `Account(string accountNumber, string holder, decimal startingBalance)`

Rules:

- Throw `ArgumentException` if `startingBalance < 0`.
- If `startingBalance > 0`, record an opening `Credit` transaction with
  description `"Opening deposit"`. If it's exactly `0`, start with an empty
  history.
- `Balance` must be derived every time — there is no stored `_balance` field.

Methods:

| Method                                 | Behaviour                                                                                                  |
|----------------------------------------|------------------------------------------------------------------------------------------------------------|
| `void Deposit(decimal amount)`         | Appends a `Credit` transaction with description `"Deposit"`. Throws `ArgumentException` if `amount <= 0`. |
| `void Withdraw(decimal amount)`        | Appends a `Debit` transaction with description `"Withdrawal"`. Throws `ArgumentException` if `amount <= 0`. Throws `InvalidOperationException` if `amount > Balance`. Must not record a transaction on failure. |
| `string Statement()`                   | Returns a multi-line human-readable statement. See the DateTime lesson below.                             |
| `List<Transaction> FindTransactions(string search)` | Returns transactions whose `Description` contains `search` (case-insensitive), sorted oldest-first by `Timestamp`. |

### `Bank` (class)

The top-level container.

Properties:

| Property        | Type                       | Notes                        |
|-----------------|----------------------------|------------------------------|
| `Name`          | `string`                   |                              |
| `AccountCount`  | `int`                      |                              |
| `TotalAssets`   | `decimal`                  | Sum of every account balance |
| `Accounts`      | `IReadOnlyList<Account>`   |                              |

Constructor: `Bank(string name)`

Methods:

| Method                                                    | Behaviour                                                                  |
|-----------------------------------------------------------|----------------------------------------------------------------------------|
| `Account OpenAccount(string holder, decimal startingBalance)` | Generates the next account number (`ACC-1000`, `ACC-1001`, ...), creates the Account, stores it, returns it. |
| `Account? FindAccount(string accountNumber)`              | Returns the matching account, or `null` if not found.                      |
| `bool CloseAccount(string accountNumber)`                 | Removes the account and returns `true`, or returns `false` if not found.  |

The account-number counter lives on each `Bank` instance (a private `int`),
**not** a static field — two `Bank` objects each number their accounts
independently, starting at `1000`.

---

## Working through it

This is a red-to-green project. The stub version of the code throws
`NotImplementedException` in every method — so **every test fails at first.**

1. Open `Bank.slnx` in Visual Studio.
2. Open **Test Explorer** (Test → Test Explorer). Run all tests. Everything should be red.
3. Pick a small test, e.g. `TransactionTests.Constructor_AssignsAllProperties`.
4. Read the test to understand what it expects.
5. Implement just enough code in the matching source file (`Transaction.cs`) to
   make that one test pass. Run again. Green?
6. Move on to the next test.

Suggested order:

1. `TransactionTests` — smallest surface, good warm-up.
2. `AccountTests` — construction, `Balance`, `Deposit`, `Withdraw`.
3. `AccountTests` — `Statement` and `FindTransactions`.
4. `BankTests` — tying the whole thing together.

When everything is green, run `Program.cs` to see the bank working end-to-end,
then extend the demo (or pick an extension below).

---

## `decimal` — mini-lesson

Every amount in this project is a `decimal` — never `double` or `float`. This
is the standard C# rule for **money**, and there is a specific reason why.

**Why not `double`?** Binary floating-point types (`double`, `float`) can't
represent many everyday decimal fractions exactly. `0.1 + 0.2` in `double`
equals `0.30000000000000004`, not `0.3`. That's fine for physics and graphics,
fatal for a balance sheet — rounding drift adds up over many transactions, and
`balance == 0m` tests start behaving unpredictably.

`decimal` is a 128-bit base-10 number. `0.1m + 0.2m` is **exactly** `0.3m`, so
arithmetic on money is predictable and `==` comparisons work the way you expect.

**Essential reading (Microsoft Learn):**

- [`decimal` type — language reference](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types#the-decimal-type)
- [Floating-point numeric types — overview & comparison](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types)
- [`decimal.Round` method](https://learn.microsoft.com/en-us/dotnet/api/system.decimal.round)
- [Standard numeric format strings](https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings) — for `:N2`, `:F2`, `:C`, `:P`, `:D`

**Things to skim from the docs:**

- **Literal suffix.** A bare `100` is an `int`; a bare `100.0` is a `double`.
  For a `decimal` literal you must append `m` (or `M`): `100m`, `0.01m`,
  `1234.56m`. Forget the suffix and you'll get a compile error when assigning
  to a `decimal` parameter — that's the compiler saving you from a silent
  conversion.
- **No implicit conversion from `double`.** `decimal d = 1.5;` won't compile
  because `1.5` is a `double`. `decimal d = 1.5m;` does. This is deliberate —
  C# won't let you accidentally launder a lossy `double` into a `decimal`.
- **Formatting for display.** Inside interpolation:
  - `$"{amount:N2}"` → `"1,234.56"` — number with thousands separator and 2 dp.
  - `$"{amount:F2}"` → `"1234.56"` — fixed-point, 2 dp, no thousands separator.
  - `$"{amount:C}"`  → `"£1,234.56"` — culture's currency symbol (locale dependent).
  - `$"{amount:C0}"` → `"£1,235"` — currency with 0 decimal places.
- **Rounding.** Use `decimal.Round(value, digits)`, **not** `Math.Round` — the
  latter is for `double`. `decimal.Round(1.235m, 2)` returns `1.24m`.
  The docs also show you how to pick a rounding mode (banker's rounding,
  away-from-zero) via `MidpointRounding`.
- **Equality is safe.** `0.1m + 0.2m == 0.3m` is `true` in C#. Keep that in
  the back of your mind — it's the whole reason this type exists.

**Practical takeaway for this project:** every balance, amount, and total in
the spec is `decimal`. When you write literals, add the `m` suffix; when you
display an amount, reach for `:N2` or `:C` via the standard numeric format
strings; when you round, use `decimal.Round`.

---

## DateTime / Timestamps — mini-lesson

`Statement()` prints a table. Each row should show a timestamp, a type
(`CREDIT` / `DEBIT`), an amount, and a description. C# has rich built-in support
for dates and times — you should be reading the official docs to learn how to
format timestamps, because no one memorises format strings by heart.

**Essential reading (Microsoft Learn):**

- [`DateTime` struct — type overview](https://learn.microsoft.com/en-us/dotnet/api/system.datetime)
- [Standard date and time format strings](https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings)
- [Custom date and time format strings](https://learn.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings)
- [Choose between `DateTime`, `DateTimeOffset`, `TimeSpan`, and `TimeZoneInfo`](https://learn.microsoft.com/en-us/dotnet/standard/datetime/choosing-between-datetime) — quick context

**Things to skim from the docs:**

- `DateTime.Now` vs `DateTime.UtcNow` — the Transaction constructor uses
  `UtcNow` so all timestamps are in the same time zone (UTC) regardless of
  where the code runs. Think about what that means when you display them.
- `ToString(string format)` on a `DateTime` — e.g.
  `t.Timestamp.ToString("yyyy-MM-dd HH:mm")` produces `2026-04-22 14:30`.
  Skim the *custom format strings* page to see the tokens (`yyyy`, `MM`, `dd`,
  `HH`, `mm`, `ss`, `ffff`, ...).
- String interpolation format specifiers — `$"{t.Timestamp:yyyy-MM-dd HH:mm}"`
  is the compact version of the same thing.
- Column alignment — `$"{text,-20}"` left-aligns in a 20-char column;
  `$"{n,10:N2}"` right-aligns in 10 chars and formats with 2 decimals.

**Your challenge:** design the format of your `Statement()` output using the
docs above. The tests are deliberately loose about exact layout — they check
that the required *content* appears (account number, holder name, balance,
`CREDIT` and `DEBIT` labels, transaction descriptions), so you have room to
make it pretty.

---

## Extensions (stretch work — no tests provided)

Finish the core spec first. Then pick any of these and write the code **plus
the tests** yourself. Good practice for the TDD cadence you saw in the
red-to-green phase.

The list is rough-ordered easiest → hardest.

### 1. Transfers

Add `void Transfer(string fromAccountNumber, string toAccountNumber, decimal amount)`
to `Bank`. It should:

- Look up both accounts; throw `InvalidOperationException` if either is missing.
- Debit the source account and credit the destination account for the same amount.
- Record transactions with descriptions like `"Transfer to ACC-1001"` (on the
  source) and `"Transfer from ACC-1000"` (on the destination) so the statement
  tells the full story.
- If the source account has insufficient funds, do **nothing** — neither
  account should be changed. (Think about order of operations.)

### 2. Overdrafts

Let each account opt into an overdraft limit (e.g. `decimal OverdraftLimit`).
Change `Withdraw` so the rule becomes `amount > Balance + OverdraftLimit` before
throwing `InvalidOperationException`.

Questions to design around:

- How does the overdraft limit get set? Constructor parameter? A setter?
- What does `TotalAssets` report when some accounts are in the red?
- Should the statement call out negative balances differently?

### 3. Interest and fees

Add `void ApplyInterest(decimal rate)` on `Bank` that credits every account by
`balance * rate` with a transaction description like `"Interest 0.5%"`. Also
support a monthly fee on accounts below some threshold.

Think about decimals and rounding — `decimal.Round` is your friend, not
`Math.Round`.

### 4. Statement by date range

Add an overload: `string Statement(DateTime from, DateTime to)` on `Account`.
The existing no-arg `Statement()` keeps its current behaviour; the new one
includes only transactions whose `Timestamp` falls in `[from, to]` (decide and
document whether both ends are inclusive).

Exercises **method overloading** and **DateTime comparison**. Skim the
DateTime mini-lesson above if you've forgotten the format tokens.

### 5. Categorised transactions

Introduce a `TransactionCategory` enum (`Food`, `Rent`, `Salary`, `Transfer`,
`Interest`, `Fees`, `Other`). Add a `Category` field to `Transaction` (you'll
extend the constructor). Then add a `FindTransactions` overload:

```csharp
List<Transaction> FindTransactions(TransactionCategory category)
```

Exercises **enums**, **method overloading**, and a small migration — every
existing `new Transaction(...)` call now needs a category.

### 6. Custom exception type

Define `InsufficientFundsException : Exception` carrying two public
properties: `decimal RequestedAmount` and `decimal AvailableBalance`. Replace
the raw `InvalidOperationException` in `Account.Withdraw` with this typed
exception.

Exercises **custom exceptions** and **inheritance from `Exception`**. Callers
can now `catch (InsufficientFundsException ex)` and read
`ex.AvailableBalance` directly, instead of sniffing at an exception message.

### 7. Generic ledger

Extract the private list of transactions inside `Account` into a small
generic class of your own:

```csharp
public class Ledger<T>
{
    // Add, Count, iterate, filter — whatever Account needs of it.
}
```

Then change `Account` to use `Ledger<Transaction>` in place of its current
`List<Transaction>` field. The public `Account` API must NOT change — every
existing test stays green.

Exercises **writing** a generic (not just consuming `List<T>`). Think about
which operations genuinely belong on a generic ledger versus being
transaction-specific.

### 8. Account type hierarchy

Introduce two subclasses of `Account`:

- **`SavingsAccount`** — earns interest. Give `Account` a `virtual void
  ApplyInterest(decimal rate)` that does nothing (or is abstract, your call);
  `SavingsAccount` overrides it to credit `Balance * rate` as a `Credit`
  transaction. Overdrafts are NOT allowed — the base `Withdraw` works as-is.
- **`CurrentAccount`** — has an `OverdraftLimit`. Overrides `Withdraw` to let
  the balance go negative up to that limit. Does nothing for interest.

Then split `Bank.OpenAccount` into `OpenSavingsAccount(...)` and
`OpenCurrentAccount(...)`, each returning the appropriate subclass.

Exercises **inheritance**, **`virtual` / `override`**, and **polymorphism**
in the students' own code — `Bank.TotalAssets` keeps iterating `Account`
and summing `Balance`, even though each element might now be a Savings or
Current.

Design questions to think about:

- Should the base `Account` itself be `abstract` once the subclasses exist?
- What's the right default `ApplyInterest` on the base class — no-op, or
  throw? Why?
- Does `Transfer` (Extension §1) need to know which kind of account it's
  moving money between?

### 9. Exporters — one abstract base, many formats (advanced)

Build a small exporter framework that turns an `Account` into some external
format. This is the OOP capstone: one abstract base class, three concrete
subclasses, polymorphism, and real file I/O.

Start with the shape:

```csharp
public abstract class Exporter
{
    public abstract string Export(Account account);

    public void ExportToFile(Account account, string filePath)
    {
        string content = Export(account);
        // write `content` to `filePath` on disk — see File I/O docs below
    }
}
```

Then build three concrete exporters.

#### 9a. `CsvExporter : Exporter`

Outputs comma-separated values — one header row, one row per transaction:

```
Timestamp,Type,Amount,Description
2026-04-22T14:30:00Z,Credit,1250.75,Opening deposit
2026-04-22T14:35:00Z,Credit,89.99,Deposit
2026-04-22T14:40:00Z,Debit,12.50,Withdrawal
```

Questions to think about:

- How do you handle descriptions that contain commas or quotes? (This is
  exactly the CSV parsing problem they met in the strings advanced exercise,
  now in reverse.)
- Should account-holder metadata live in the file? CSV has no header region,
  so most tools put it in a separate "info" file, or as `# comment` lines
  prefixed with `#`.

#### 9b. `MarkdownTableExporter : Exporter`

Outputs a GitHub-flavoured markdown table:

```markdown
# Statement — ACC-1000 — Ada Lovelace
**Balance:** 1,780.91

| Timestamp        | Type   |   Amount | Description     |
|------------------|--------|---------:|-----------------|
| 2026-04-22 14:30 | CREDIT |  1250.75 | Opening deposit |
| 2026-04-22 14:35 | CREDIT |    89.99 | Deposit         |
| 2026-04-22 14:40 | DEBIT  |    12.50 | Withdrawal      |
```

Exercise: **column-width alignment**. You'll need to compute each column's
widest value before rendering rows — otherwise the table stays readable as
plain text even though markdown tolerates ragged pipes.

#### 9c. `ExcelExporter : Exporter` — super advanced

Produce a real `.xlsx` file. The .NET standard library doesn't ship Excel
support out of the box — you'll need to add a NuGet package.

Your task:

1. **Research.** Look up **ClosedXML** (Apache-licensed, friendly API) or
   the **Open XML SDK** (official Microsoft, lower-level and harder). Pick
   one. Reading a library's README and sample code IS the exercise here.
2. **Add the package** to your `Bank.csproj`:
   `dotnet add package ClosedXML`
   (or whichever you chose — the command is the same shape).
3. **Build the workbook** inside `ExcelExporter`: a worksheet named
   `"Statement"`, a header row, one row per transaction, and account
   metadata in a second sheet or at the top.
4. **Save it to disk.** Writing a binary `.xlsx` doesn't fit the
   `Export(Account) → string` contract — so you'll need to either:
   - **Override `ExportToFile`** in `ExcelExporter` and do the workbook-save
     directly, while `Export` returns something like a base64 blob or
     throws `NotSupportedException`; **or**
   - **Refactor the base class.** Change `Exporter` to have an abstract
     `void ExportToFile(Account account, string filePath)` and let each
     subclass own its own serialisation.

   Option 2 is the cleaner answer once you realise "string" doesn't
   generalise to binary formats — and refactoring the base class in response
   to a subclass that doesn't fit is one of the real lessons of working
   with inheritance.

Stretch: format the amount column as currency (`£#,##0.00`), freeze the
header row, auto-size columns. ClosedXML docs cover all of these.

#### File I/O — essential reading

Writing a string to a text file is a two-liner in .NET, but skim the docs
so you know what's available:

- [File and stream I/O overview](https://learn.microsoft.com/en-us/dotnet/standard/io/) —
  the high-level map.
- [`System.IO.File`](https://learn.microsoft.com/en-us/dotnet/api/system.io.file) —
  `File.WriteAllText(path, content)` is the one-call path for text formats.
- [How to read from and write to a newly created data file](https://learn.microsoft.com/en-us/dotnet/standard/io/how-to-read-and-write-to-a-newly-created-data-file) —
  a walkthrough tutorial.
- [`System.IO.StreamWriter`](https://learn.microsoft.com/en-us/dotnet/api/system.io.streamwriter) —
  the streaming option; useful for large files and pairs with `using`.
- [`System.IO.Path`](https://learn.microsoft.com/en-us/dotnet/api/system.io.path) —
  combining paths safely across Windows / Linux / macOS.

`using` statements (automatic cleanup) show up the moment you open a stream
manually. Treat them as "do exactly this for now" for CSV and markdown —
for Excel, the ClosedXML API takes care of disposal for you. If you're
curious, read
[`IDisposable` and `using` statements](https://learn.microsoft.com/en-us/dotnet/standard/garbage-collection/using-objects).
