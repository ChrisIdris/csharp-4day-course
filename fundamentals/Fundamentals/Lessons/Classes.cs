namespace Fundamentals.Lessons;

// Theme: Classes — defining richer custom types that hold state + behaviour.
//
// Classes look VERY similar to structs — most of the syntax is identical
// (properties, constructors, methods). The one-word difference on the type
// declaration (`class` instead of `struct`) unlocks the one thing that
// matters more than anything else in this lesson:
//
// ═════════════════════════════════════════════════════════════
//   CLASSES ARE REFERENCE TYPES.
//   When you assign a class instance or pass one to a method,
//   both variables point to the SAME object. Mutations are shared.
//   This is the OPPOSITE of struct behaviour (Structs.cs, Lesson F).
// ═════════════════════════════════════════════════════════════
//
// This is how objects behave in Python and JavaScript already, so the
// mental model is familiar — but the contrast with structs is the
// C#-specific thing to internalise.
//
// NOTE: this lesson deliberately stays out of public/private/static
// territory — all members below are `public`. We'll cover visibility,
// overloads, and the full OOP picture in the next lesson (Classes2).
//
// This file contains the TEACHING EXAMPLES only. Your work lives in
// Exercises/Classes.cs.

// ─────────────────────────────────────────────────────────────
// The Course class — the subject of this lesson.
// A Course has a name and a list of enrolled Students.
// It exposes public methods to add/remove students and inspect the roster.
// ─────────────────────────────────────────────────────────────
public class Course
{
    // LESSON B: MEMBER VARIABLES (state)
    // Same property syntax as a struct — that part doesn't change.
    // `Name` is read/write. `Students` is read-only from the outside
    // (no `set`), but the list it refers to is still mutable — callers
    // can add/remove items through the returned reference. We'll use
    // our own Enroll/Remove methods for that.
    public string Name { get; set; }
    public List<Student> Students { get; }

    // LESSON C: CONSTRUCTOR
    // Same shape as a struct's constructor: same name as the type, no return
    // type. Its job is to put the new instance into a valid starting state —
    // here, setting the name and creating an empty list. Forgetting to
    // initialise `Students` would leave it as `null`, and every method
    // below would throw `NullReferenceException` on first use.
    public Course(string name)
    {
        Name = name;
        Students = new List<Student>();
    }

    // LESSON D: PUBLIC METHODS (behaviour)
    // Methods defined on the class operate on this instance's state.
    // `Students` here refers to THIS course's list.

    public void Enroll(Student student)
    {
        // Append to this course's roster. Mutates state.
        Students.Add(student);
    }

    public bool Remove(string firstName, string lastName)
    {
        // Find the first matching student and remove them.
        // Returns true if someone was removed, false otherwise.
        for (int i = 0; i < Students.Count; i++)
        {
            Student s = Students[i];
            if (s.FirstName == firstName && s.LastName == lastName)
            {
                Students.RemoveAt(i);
                return true;
            }
        }
        return false;
    }

    public int Count()
    {
        // How many students are currently enrolled.
        return Students.Count;
    }

    public string[] StudentNames()
    {
        // Snapshot of full names, useful for display.
        string[] names = new string[Students.Count];
        for (int i = 0; i < Students.Count; i++)
        {
            names[i] = Students[i].FullName(); // reuse Student.FullName() from the struct lesson
        }
        return names;
    }
}

public static class Classes
{
    // ─────────────────────────────────────────────────────────────
    // LESSON A: class vs struct — one keyword, two worlds
    // ─────────────────────────────────────────────────────────────
    // Syntactically, a class looks almost identical to a struct. You
    // declare properties, a constructor, methods, and you instantiate
    // with `new` — all the same. The differences that matter:
    //
    //   1) REFERENCE SEMANTICS (see Lesson E below — this is THE point).
    //   2) Classes are what you reach for by default for anything with
    //      behaviour and internal state. Structs are for small, simple
    //      "pieces of data".
    //   3) A class field / variable can be `null` (no instance yet);
    //      a struct field always has a real value.
    //
    // We'll exercise (1) on a Course because it holds a list of Students,
    // which makes the reference behaviour easy to see.

    // ─────────────────────────────────────────────────────────────
    // LESSON E-demo: instantiating and using a class
    // ─────────────────────────────────────────────────────────────

    public static Course MakePhysics101()
    {
        // `new Course(...)` calls the constructor — same syntax as structs.
        return new Course("Physics 101");
    }

    public static Course PhysicsWithTwoStudents()
    {
        Course course = MakePhysics101();
        course.Enroll(new Student("Ada", "Lovelace", 36));
        course.Enroll(new Student("Alan", "Turing", 24));
        return course;
    }

    public static int PhysicsCount()
    {
        // e.g. returns 2
        return PhysicsWithTwoStudents().Count();
    }

    public static string[] PhysicsNames()
    {
        // e.g. returns ["Ada Lovelace", "Alan Turing"]
        return PhysicsWithTwoStudents().StudentNames();
    }

    public static bool RemoveTuring()
    {
        // e.g. returns true, and the course afterwards has 1 student
        Course course = PhysicsWithTwoStudents();
        return course.Remove("Alan", "Turing");
    }

    // ═════════════════════════════════════════════════════════════
    // LESSON E: THE BIG ONE — CLASSES ARE REFERENCE TYPES
    // ═════════════════════════════════════════════════════════════
    //
    // Flip everything from Structs.cs Lesson F on its head.
    //
    //   ► When you ASSIGN a class instance to another variable, BOTH
    //     variables point to the SAME object. Mutating through one is
    //     visible through the other.
    //   ► When you PASS a class instance to a method, the method receives
    //     the SAME reference. Mutations made inside the method ARE visible
    //     to the caller.
    //
    // This matches the mental model you already have from Python and
    // JavaScript: objects are "shared by reference". The teaching moment
    // here is the CONTRAST with structs — same C#, same syntax, opposite
    // runtime behaviour, because one keyword changed (`struct` → `class`).

    // ── Reference-copy: assignment shares the object ───────────────
    // c2 = c1 does NOT copy — both names refer to the same Course.
    // Enrolling through c2 is visible through c1, because they're one object.
    public static (int c1Count, int c2Count) AssignmentSharesReference()
    {
        // e.g. c1Count == 1, c2Count == 1  (both "see" the one enrolled student)
        Course c1 = new Course("Physics 101");
        Course c2 = c1;  // SAME reference — NOT a copy
        c2.Enroll(new Student("Ada", "Lovelace", 36));
        return (c1.Count(), c2.Count());
    }

    // ── Reference-copy: passing to a method shares the object ──────
    // Notice the contrast with BirthdayBy10_Gotcha in Structs.cs:
    // there the caller saw NO change. Here the caller DOES see the change.
    public static void EnrollNewStudent(Course course, Student student)
    {
        course.Enroll(student);  // mutates the caller's course — visible outside
    }

    public static int CallerCountAfterEnroll()
    {
        // e.g. returns 1 — the caller's course was mutated by the method
        Course course = new Course("Physics 101");
        EnrollNewStudent(course, new Student("Ada", "Lovelace", 36));
        return course.Count();
    }
}
