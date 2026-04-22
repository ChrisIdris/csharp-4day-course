namespace Fundamentals.Lessons;

// Theme: Structs — defining your own simple data types, C#-style.
//
// Closest analogues from your background:
//   Python    → @dataclass, namedtuple, or a plain class holding a few fields
//   JavaScript → a plain object literal: { firstName: "Ada", lastName: "Lovelace" }
//
// BUT there's one huge C#-specific twist you'll hit in Lesson F:
// structs are VALUE TYPES — they get COPIED on assignment and when passed
// to methods. This means that a deep copy of the object gets made
// rather than a reference. So changing a value, will update the copy's value
// and the original struct will remain unchanged.
// Python dataclasses and JS objects are reference-like; a C#
// struct is not. This is the single most important thing in this lesson.
//
// This file contains the TEACHING EXAMPLES only. Your work lives in
// Exercises/Structs.cs.

// ─────────────────────────────────────────────────────────────
// LESSON B, C, D live INSIDE the struct definition itself
// ─────────────────────────────────────────────────────────────
// The Student struct is the "subject" of this lesson — we'll instantiate
// it and manipulate it from the static demo class below. Read top to bottom.
public struct Student
{
  // LESSON B: PROPERTIES — `{ get; set; }` is C#'s shorthand for
  // "a field that reads and writes through a getter and setter".
  // To a caller, `student.FirstName` looks exactly like a field access
  // (same syntax as a plain attribute in Python or a plain property in JS),
  // but under the hood C# generates a hidden backing field plus getter/setter
  // methods. You can later add logic (validation, logging) without breaking
  // any callers — this is why C# code uses properties almost everywhere
  // instead of plain public fields.
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public int Age { get; set; }

  // LESSON C: CONSTRUCTOR — a special method with the SAME name as the type,
  // no return type. Runs when a caller does `new Student(...)`.
  // Its job: take the incoming arguments and assign them to the properties
  // so the new instance starts life in a valid state.
  public Student(string firstName, string lastName, int age)
  {
    FirstName = firstName;
    LastName = lastName;
    Age = age;
  }

  // LESSON D: INSTANCE METHODS — methods that operate on THIS struct's data.
  // Inside the method, `FirstName` / `LastName` / `Age` refer to this
  // instance's values (you could also write `this.FirstName` for clarity;
  // both are legal).
  public string FullName()
  {
    // e.g. new Student("Ada", "Lovelace", 36).FullName() == "Ada Lovelace"
    return $"{FirstName} {LastName}";
  }

  public bool IsAdult()
  {
    // e.g. Age = 36 → true; Age = 12 → false
    return Age >= 18;
  }
}

public static class Structs
{
  // ─────────────────────────────────────────────────────────────
  // LESSON A: What IS a struct?
  // ─────────────────────────────────────────────────────────────
  // A struct is a lightweight, custom-made TYPE. You name it, you decide
  // what data it holds (its fields / properties), and you decide what
  // operations it supports (its methods).
  //
  // Mental model:
  //   • Python equivalent:  @dataclass or collections.namedtuple
  //   • JS equivalent:      a plain object { firstName, lastName, age }
  //
  // A struct is best for SMALL, simple data holders — coordinates, money,
  // date ranges, colours. For anything bigger or richer, use a `class`
  // (the next lesson). The syntactic difference is literally one keyword
  // (`struct` vs `class`) — but the behavioural difference is massive
  // (see Lesson F).

  // ─────────────────────────────────────────────────────────────
  // LESSON E: Instantiating and using a struct
  // ─────────────────────────────────────────────────────────────
  // `new Student(...)` invokes the constructor and hands you back an
  // instance. From there, property access and method calls work the
  // way they do in Python/JS.

  public static Student MakeAda()
  {
    // Call the constructor to build a fresh Student.
    return new Student("Ada", "Lovelace", 36);
  }

  public static string AdaFullName()
  {
    // e.g. returns "Ada Lovelace"
    Student ada = MakeAda();
    return ada.FullName(); // method call on the instance
  }

  public static bool AdaIsAdult()
  {
    // e.g. returns true
    Student ada = MakeAda();
    return ada.IsAdult();
  }

  public static Student WithSetter()
  {
    // Properties declared with `{ get; set; }` can be reassigned.
    // Here we mutate Age after construction.
    Student ada = MakeAda();
    ada.Age = 37; // allowed because Age has a setter
    return ada;
  }

  // ═════════════════════════════════════════════════════════════
  // LESSON F: THE BIG ONE — STRUCTS ARE VALUE TYPES
  // ═════════════════════════════════════════════════════════════
  //
  // This is the one thing about structs you absolutely must internalise.
  //
  //   ► When you ASSIGN a struct to another variable, you get a COPY.
  //   ► When you PASS a struct to a method, the method gets a COPY.
  //
  // Changes to the copy do NOT affect the original. This is the opposite
  // of what Python and JavaScript programmers expect, because in those
  // languages every non-primitive is a reference — two variables holding
  // "the same object" really do point to one object in memory, and
  // mutating one is visible through the other. Not so in C# structs.
  //
  // Think of a struct like an `int`: when you write `int b = a; b = 99;`
  // nobody expects `a` to change. Structs work the same way.
  //
  // Below are two demonstrations (gotcha) plus the standard fixes.

  // ── Gotcha 1: assignment copies ────────────────────────────────
  // s2 = s1 gives us an independent copy. Mutating s2 leaves s1 alone.
  public static (Student original, Student copy) AssignmentCopiesStruct()
  {
    // e.g. original.Age == 36, copy.Age == 99
    Student s1 = new Student("Ada", "Lovelace", 36);
    Student s2 = s1;    // COPY — s2 is a wholly separate Student
    s2.Age = 99;        // mutates only s2
    return (s1, s2);    // s1.Age is still 36
  }

  // ── Gotcha 2: passing to a method copies ───────────────────────
  // The method receives a copy. Mutations don't leak back to the caller.
  // This one trips up literally everyone coming from Python/JS.
  public static void BirthdayBy10_Gotcha(Student student)
  {
    student.Age += 10;  // modifies the local copy — caller won't see it
  }

  public static int CallerAgeAfterBirthdayGotcha()
  {
    // e.g. returns 36 — NOT 46, even though we "added 10" to Ada's age
    Student ada = new Student("Ada", "Lovelace", 36);
    BirthdayBy10_Gotcha(ada);
    return ada.Age;
  }

  // ── Fix: return the modified struct and reassign ───────────────
  // The idiomatic way with value types is: don't try to mutate in place
  // across a method boundary. Build a new value and hand it back.
  public static Student BirthdayBy10_Fixed(Student student)
  {
    student.Age += 10;  // mutate our own local copy...
    return student;     // ...then return it
  }

  public static int CallerAgeAfterBirthdayFixed()
  {
    // e.g. returns 46 — caller captures the returned (modified) copy
    Student ada = new Student("Ada", "Lovelace", 36);
    ada = BirthdayBy10_Fixed(ada); // reassign with the returned value
    return ada.Age;
  }
}
