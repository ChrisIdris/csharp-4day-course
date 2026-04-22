# C# 4-Day Course — Authoring Guide

This repo holds teaching materials for a 4-day C# course. Follow the conventions
below when adding lessons, exercises, or examples so everything stays consistent
and students can navigate predictably.

---

## Student background

Students are already proficient programmers who know:

- **Python** (Django)
- **JavaScript**

So **don't waste their time re-teaching generic concepts** they already have:
variables, loops, functions, conditionals, basic strings, immutability as a
concept, template literals, etc. Teach what's **different or surprising in C#**.

Topics they **have not** covered and DO need full explanation:

- **Classes / object-oriented programming** (encapsulation, inheritance,
  interfaces, polymorphism)
- **Generics**
- **Static typing nuances** — value vs reference types, nullability,
  boxing/unboxing
- **Other C#-specific features** — LINQ, properties vs fields, pattern matching,
  `out`/`ref`, `using`/IDisposable, async/await

Frame new concepts against Python/JS equivalents where it helps
("like a JS template literal but…", "Python's f-string is the closest
analogue").

---

## File layout per theme

Every theme (Numbers, Strings, Collections, …) is split across four files (plus
two more if the theme has an advanced section):

```
Fundamentals/
├── Lessons/<Theme>.cs              ← teaching examples, inline comments
├── Exercises/<Theme>.cs            ← student exercises (throw NotImplementedException)
├── Lessons/<Theme>Advanced.cs      ← advanced teaching (optional)
└── Exercises/<Theme>Advanced.cs    ← advanced exercises (optional)

Fundamentals.Tests/
├── Lessons/<Theme>Tests.cs         ← guards the teaching examples
├── Exercises/<Theme>Tests.cs       ← validates student work
├── Lessons/<Theme>AdvancedTests.cs
└── Exercises/<Theme>AdvancedTests.cs
```

**Namespaces:** `Fundamentals.Lessons`, `Fundamentals.Exercises`,
`Fundamentals.Tests.Lessons`, `Fundamentals.Tests.Exercises`.

Class names are **the same** in Lessons and Exercises (both have a `Numbers`
class, for example) — disambiguated by namespace. In `Program.cs`, use
namespace aliases so call sites read clearly:

```csharp
using Lessons = Fundamentals.Lessons;
using Exercises = Fundamentals.Exercises;

Lessons.Numbers.DivideIntegers(7, 2);   // teaching example
Exercises.Numbers.Add(3, 4);            // student exercise
```

---

## Teaching pattern: "show the gotcha, then the fix"

Wherever a C# behaviour is surprising or different from Python/JS intuition,
structure the lesson as:

1. **`Method_ShowsProblem`** — produces the surprising / wrong result
   (e.g. `DivideIntegers(7, 2)` returns `3`, not `3.5`).
2. **`Method_Fixed`** — the correct idiom.
3. **`Method_WrongFix`** (optional but encouraged) — a common *wrong* attempt
   that doesn't work (e.g. casting AFTER the division). These are pedagogical
   gold — keep them in.

Each example method is small and self-contained so `Program.cs` output reads
like a narrated lesson.

---

## Lessons/\<Theme\>.cs conventions

- **Block-bodied** methods only (no expression-bodied `=>`). Every example
  method gets a short `// e.g.` comment showing input → output:

  ```csharp
  public static string Trimmed(string s)
  {
      // e.g. s = "  hi  "; s.Trim() == "hi"
      return s.Trim();
  }
  ```

- Organise with ASCII header bars marking each lesson:

  ```
  // ─────────────────────────────────────────────────────────────
  // LESSON D: Integer division truncates
  // ─────────────────────────────────────────────────────────────
  ```

- Lead each lesson section with a short prose explanation in comments before
  the first method.
- For surprising behaviour, give the method a **name that tells the story**
  (`ImmutabilityGotcha`, `ImmutabilityFixed`) — don't rely on comments alone.
- File header: a one-liner describing the theme + a pointer to the sibling
  Exercises file.

---

## Exercises/\<Theme\>.cs conventions

Every exercise method has this shape:

```csharp
// EXERCISE 2: CountVowels
// Return the number of vowels (a, e, i, o, u) in the text. Case-insensitive.
// Example: CountVowels("Hello World") → 3
// Hint: iterate with `foreach (char c in text) { ... }`; use char.ToLower(c).
public static int CountVowels(string text)
{
    throw new NotImplementedException("TODO: loop over each char, count vowels");
}
```

- Numbered `EXERCISE N: <Name>` header
- Concrete **Example** showing input → expected output
- **Hint** pointing at the relevant lesson or technique
- Body throws `NotImplementedException` with a one-line action hint
- File header reminds students the teaching material is in `Lessons/<Theme>.cs`

Exercises should be small wins that apply what the lesson taught — don't set
traps that require techniques outside the preceding lesson.

---

## Program.cs conventions

- Runs **all** lesson examples so students see narrated output for every
  teaching method.
- Lists **all** exercise calls as **commented-out lines** students uncomment
  once they've implemented each method.
- Clear section headers:

  ```
  === NUMBERS — examples ===
  === NUMBERS — your exercises ===
  === STRINGS (advanced — tackle these last) ===
  ```

- Pins culture at the top so format specifiers like `:C` render £ regardless
  of host locale:

  ```csharp
  CultureInfo.CurrentCulture = new CultureInfo("en-GB");
  ```

---

## Test conventions

- **Lesson tests are guards** — they verify the teaching examples produce
  their stated results. In the scaffolded state **all lesson tests pass**.
  They protect the teaching content from a student who "fixes" a deliberate
  gotcha method.
- **Exercise tests** validate student work. In the scaffolded state **all
  exercise tests fail** (because exercises throw `NotImplementedException`)
  and pass once the student implements correctly.
- Use `[Theory]` + `[InlineData]` for multi-case tests.
- Pin culture in a test-class constructor when any test depends on
  culture-sensitive output:

  ```csharp
  public StringsTests()
  {
      CultureInfo.CurrentCulture = new CultureInfo("en-GB");
  }
  ```

---

## Advanced material

When a theme has useful-but-deeper content (e.g. `StringBuilder` for Strings,
or `LINQ` aggregation for Collections), put it in a separate
`<Theme>Advanced.cs` — same Lessons/Exercises split, separate files, separate
tests.

This keeps the core lesson focused while giving faster students stretch
material. `Program.cs` renders the advanced section clearly marked with
"tackle these last".

---

## Technical conventions

- **Target framework:** `net10.0`
- **SDK pinning:** `global.json` uses
  `{ "version": "10.0.100", "rollForward": "latestFeature" }` so any .NET 10
  SDK (Mac, Windows, Linux) works. Never pin an exact patch version —
  Windows/Parallels/VS students will likely have a different patch installed.
- **Solution format:** `.slnx` (new XML format; works in VS 2022 17.10+).
- **Test framework:** xUnit.
- **Authoring environment:** course is authored on macOS but students
  typically build on Windows under Parallels with Visual Studio. Keep tooling
  portable — no macOS-only shell tricks in `.csproj` files, forward slashes
  in documentation paths.
- **.gitignore** at the repo root covers `bin/`, `obj/`, `.vs/`, `.idea/`,
  `.vscode/`, test results, and common OS junk. Don't commit build output.

---

## Adding a new theme — checklist

1. Create `Fundamentals/Lessons/<Theme>.cs` with lesson sections A, B, …
2. Create `Fundamentals/Exercises/<Theme>.cs` with numbered exercises.
3. Create `Fundamentals.Tests/Lessons/<Theme>Tests.cs` (guards) and
   `Fundamentals.Tests/Exercises/<Theme>Tests.cs` (exercise validations).
4. Append a `=== <THEME> — examples ===` + `=== <THEME> — your exercises ===`
   block to `Program.cs`, using `Lessons.<Theme>.*` and commented
   `Exercises.<Theme>.*` calls.
5. If an advanced angle exists: add `<Theme>Advanced.cs` in both Lessons and
   Exercises, matching tests, and an `=== <THEME> (advanced) ===` block in
   `Program.cs`.
6. `dotnet build` and `dotnet run` to verify the narrated output renders
   cleanly.
