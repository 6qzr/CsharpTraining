# C# Learning
 
---
 
## 1. Displaying 3 Decimal Places: `F3` vs `N3`
 
Both format to 3 decimal places, but `N3` adds **thousand separators**.
 
| Format | ToString | String Interpolation | Output |
|--------|----------|----------------------|--------|
| `F3` | `(1234.5).ToString("F3")` | `$"{value:F3}"` | `1234.500` |
| `N3` | `(1234.5).ToString("N3")` | `$"{value:N3}"` | `1,234.500` |
 
**Rule of thumb:** Use `F3` for raw numbers (coordinates, measurements). Use `N3` for money or anything user-facing.
 
---
 
## 2. Reading a `char` in C#
 
Three common ways:
 
```csharp
// Option 1: Read a full line, grab first character
char c = Console.ReadLine()[0];
 
// Option 2: Read a single key press (no Enter needed)
char c = Console.ReadKey().KeyChar;
 
// Option 3: Cast from string input
char c = Convert.ToChar(Console.ReadLine());
```
 
**Rule of thumb:** Use `ReadKey()` for menus/games. Use `ReadLine()[0]` for simple console input.
 
---
 
## 3. `int.Parse` vs `Convert.ToInt32`
 
| | `int.Parse("123")` | `Convert.ToInt32("123")` |
|---|---|---|
| Input is `null` | Throws `ArgumentNullException` | Returns `0` |
| Input is invalid | Throws `FormatException` | Throws `FormatException` |
| Accepts non-strings | No | Yes (bool, double, etc.) |
 
```csharp
int.Parse(null);           // crashes
Convert.ToInt32(null);     // returns 0
 
Convert.ToInt32(3.9);      // returns 4 (rounds)
Convert.ToInt32(true);     // returns 1
```
 
**Rule of thumb:** Use `int.Parse` when you're sure the input is a valid string. Use `Convert.ToInt32` when the input might be null or a different type. For user input, prefer `int.TryParse` to avoid crashes entirely.
 
```csharp
if (int.TryParse(input, out int result))
    Console.WriteLine(result);
else
    Console.WriteLine("Invalid input");
```