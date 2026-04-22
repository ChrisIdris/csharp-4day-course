using System.Globalization;
using Lessons = Fundamentals.Lessons;
using Exercises = Fundamentals.Exercises;

// Pin culture so format specifiers like :C render £ rather than the host's local currency.
CultureInfo.CurrentCulture = new CultureInfo("en-GB");

Console.WriteLine("=== NUMBERS — examples ===\n");

Console.WriteLine("-- Lesson A: integers --");
Console.WriteLine($"Double(21) = {Lessons.Numbers.Double(21)}");

Console.WriteLine("\n-- Lesson B: variables --");
Console.WriteLine($"CountUpToFive() = {Lessons.Numbers.CountUpToFive()}");

Console.WriteLine("\n-- Lesson C: doubles --");
Console.WriteLine($"CircleArea(2.0) = {Lessons.Numbers.CircleArea(2.0)}");

Console.WriteLine("\n-- Lesson D: integer division truncates --");
Console.WriteLine($"DivideIntegers(7, 2) = {Lessons.Numbers.DivideIntegers(7, 2)}   ← fractional part lost!");

Console.WriteLine("\n-- Lesson E: casting --");
Console.WriteLine($"IntToDouble(5)         = {Lessons.Numbers.IntToDouble(5)}");
Console.WriteLine($"DoubleToInt(3.9)       = {Lessons.Numbers.DoubleToInt(3.9)}   ← truncated, not rounded");
Console.WriteLine($"DivideWithCasting(7,2) = {Lessons.Numbers.DivideWithCasting(7, 2)}  ← fixed!");

Console.WriteLine("\n=== NUMBERS — your exercises ===");
Console.WriteLine("Implement each method in Exercises/Numbers.cs, then uncomment below.\n");

// Console.WriteLine($"Add(3, 4)                 = {Exercises.Numbers.Add(3, 4)}");
// Console.WriteLine($"RectangleArea(3, 4)       = {Exercises.Numbers.RectangleArea(3, 4)}");
// Console.WriteLine($"CelsiusToFahrenheit(100)  = {Exercises.Numbers.CelsiusToFahrenheit(100)}");


Console.WriteLine("\n\n=== STRINGS — examples ===\n");

Console.WriteLine("-- Lesson A: char vs string --");
Console.WriteLine($"FirstLetter(\"hello\")           = '{Lessons.Strings.FirstLetter("hello")}'   ← a char, not a string");
Console.WriteLine($"AddCharsAsNumbers('a', 'b')    = {Lessons.Strings.AddCharsAsNumbers('a', 'b')}   ← char + char is int arithmetic!");
Console.WriteLine($"AddStringsAsText(\"a\", \"b\")     = \"{Lessons.Strings.AddStringsAsText("a", "b")}\"  ← string + string concatenates");

Console.WriteLine("\n-- Lesson B: three quote styles --");
Console.WriteLine($"RegularLiteral()  →");
Console.WriteLine(Lessons.Strings.RegularLiteral());
Console.WriteLine($"\nWindowsPath_RegularString()  = {Lessons.Strings.WindowsPath_RegularString()}   ← each \\ had to be written as \\\\");
Console.WriteLine($"WindowsPath_VerbatimString() = {Lessons.Strings.WindowsPath_VerbatimString()}   ← @\"...\" — backslashes are literal");
Console.WriteLine($"VerbatimWithQuotes()         = {Lessons.Strings.VerbatimWithQuotes()}   ← inside @\"...\" use \"\" to get a literal quote");
Console.WriteLine($"\nRawLiteral() →");
Console.WriteLine(Lessons.Strings.RawLiteral());
Console.WriteLine($"\nInterpolated(\"Dogezen\", 42) → {Lessons.Strings.Interpolated("Dogezen", 42)}");

Console.WriteLine("\n-- Lesson C: immutability --");
Console.WriteLine($"ImmutabilityGotcha(\"hello\")  = \"{Lessons.Strings.ImmutabilityGotcha("hello")}\"  ← ToUpper() result was discarded");
Console.WriteLine($"ImmutabilityFixed(\"hello\")   = \"{Lessons.Strings.ImmutabilityFixed("hello")}\"  ← result captured with =");

Console.WriteLine("\n-- Lesson D: comparison --");
Console.WriteLine($"SameExact(\"Hello\", \"hello\")           = {Lessons.Strings.SameExact("Hello", "hello")}   ← case-sensitive");
Console.WriteLine($"SameIgnoringCase(\"Hello\", \"hello\")    = {Lessons.Strings.SameIgnoringCase("Hello", "hello")}    ← case-insensitive");
Console.WriteLine($"HasContent(\"   \")                     = {Lessons.Strings.HasContent("   ")}   ← whitespace doesn't count");
Console.WriteLine($"HasContent(\"hi\")                      = {Lessons.Strings.HasContent("hi")}");

Console.WriteLine("\n-- Lesson E: parsing --");
Console.WriteLine($"ParseOrCrash(\"42\")       = {Lessons.Strings.ParseOrCrash("42")}");
try { Lessons.Strings.ParseOrCrash("abc"); }
catch (FormatException) { Console.WriteLine($"ParseOrCrash(\"abc\")     threw FormatException  ← bad input crashes"); }
Console.WriteLine($"TryParseSafely(\"42\")    = {(Lessons.Strings.TryParseSafely("42", out int good) ? $"true, value = {good}" : "false")}");
Console.WriteLine($"TryParseSafely(\"abc\")   = {(Lessons.Strings.TryParseSafely("abc", out int _) ? "true" : "false")}  ← no exception");

Console.WriteLine("\n-- Lesson F: format specifiers --");
Console.WriteLine($"PriceAsCurrency(19.99m)  = {Lessons.Strings.PriceAsCurrency(19.99m)}");
Console.WriteLine($"RoundedToTwo(3.14159)    = {Lessons.Strings.RoundedToTwo(3.14159)}");
Console.WriteLine($"PaddedInteger(42)        = {Lessons.Strings.PaddedInteger(42)}");

Console.WriteLine("\n=== STRINGS — your exercises ===");
Console.WriteLine("Implement each method in Exercises/Strings.cs, then uncomment below.\n");

// Console.WriteLine($"Shout(\"hello\")              = \"{Exercises.Strings.Shout("hello")}\"");
// Console.WriteLine($"CountVowels(\"Hello World\")  = {Exercises.Strings.CountVowels("Hello World")}");
// Console.WriteLine($"IsPalindrome(\"Racecar\")     = {Exercises.Strings.IsPalindrome("Racecar")}");
// Console.WriteLine($"FormatPrice(19.99m)         = \"{Exercises.Strings.FormatPrice(19.99m)}\"");
// Console.WriteLine($"SafeParseInt(\"abc\")         = {Exercises.Strings.SafeParseInt("abc")}");


Console.WriteLine("\n\n=== STRINGS (advanced — tackle these last) ===\n");

Console.WriteLine("-- Lesson G: StringBuilder --");
string[] parts = ["one", "two", "three", "four"];
Console.WriteLine($"JoinWithConcatenation(parts)  = \"{Lessons.StringsAdvanced.JoinWithConcatenation(parts)}\"   ← allocates each iter");
Console.WriteLine($"JoinWithStringBuilder(parts)  = \"{Lessons.StringsAdvanced.JoinWithStringBuilder(parts)}\"   ← one buffer, one final string");
Console.WriteLine($"JoinWithStringJoin(parts)     = \"{Lessons.StringsAdvanced.JoinWithStringJoin(parts)}\"   ← simplest when you just need to join");

Console.WriteLine("\n=== STRINGS (advanced) — your exercise ===");
Console.WriteLine("Implement ParseCsvRow in Exercises/StringsAdvanced.cs, then uncomment below.\n");

// Console.WriteLine($"ParseCsvRow(\"one,two,three\")              = [{string.Join(" | ", Exercises.StringsAdvanced.ParseCsvRow("one,two,three"))}]");
// Console.WriteLine($"ParseCsvRow(\"\\\"Hello, world\\\",foo,bar\")  = [{string.Join(" | ", Exercises.StringsAdvanced.ParseCsvRow("\"Hello, world\",foo,bar"))}]");
// Console.WriteLine($"ParseCsvRow(\"a,,b\")                       = [{string.Join(" | ", Exercises.StringsAdvanced.ParseCsvRow("a,,b"))}]");
