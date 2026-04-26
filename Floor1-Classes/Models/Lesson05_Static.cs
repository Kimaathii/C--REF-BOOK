namespace Floor1_Classes.Models;

/// <summary>
/// LESSON: static keyword - Class-level vs Instance-level Members
/// ================================================================
/// 
/// Regular member: Each object gets its own copy
/// Static member:  ONE copy shared across the entire class
/// 
/// No object needed to access static members
/// </summary>

// ============================================================================
// PART 1: STATIC METHODS — No object needed
// ============================================================================

/// <summary>
/// Helper utility with static methods
/// No need to create an object - methods belong to the class itself
/// </summary>
public static class PasswordHelper
{
    /// <summary>
    /// Simple hashing (use bcrypt in real apps)
    /// </summary>
    public static string Hash(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentNullException(nameof(password));

        // Simplified for demo
        return Convert.ToBase64String(
            System.Text.Encoding.UTF8.GetBytes(password + "salt")
        );
    }

    /// <summary>
    /// Verify password against hash
    /// </summary>
    public static bool Verify(string password, string hash)
    {
        if (string.IsNullOrWhiteSpace(password))
            return false;

        string passwordHash = Hash(password);
        return passwordHash == hash;
    }

    /// <summary>
    /// Check password strength
    /// </summary>
    public static bool IsStrong(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return false;

        return password.Length >= 8
            && password.Any(char.IsUpper)
            && password.Any(char.IsDigit)
            && password.Any(c => !char.IsLetterOrDigit(c));  // special char
    }
}

/// <summary>
/// Usage: PasswordHelper.Hash(password)
/// No new PasswordHelper() needed - methods belong to the class
/// </summary>

// ============================================================================
// PART 2: STATIC CONSTANTS — Shared configuration values
// ============================================================================

/// <summary>
/// Application-level constants
/// </summary>
public static class AppConstants
{
    public const string AppName = "Library System";
    public const string AppVersion = "1.0.0";
    public const int MaxLoginAttempts = 5;
    public const int PasswordMinLength = 8;
    public const int SessionTimeoutMinutes = 30;
}

/// <summary>
/// Role definitions - one source of truth
/// </summary>
public static class UserRoles
{
    public const string Admin = "Admin";
    public const string Librarian = "Librarian";
    public const string Member = "Member";
    public const string Guest = "Guest";
}

/// <summary>
/// Usage:
/// if (user.Role == UserRoles.Admin) { ... }
/// Console.WriteLine(AppConstants.MaxLoginAttempts);
/// </summary>

// ============================================================================
// PART 3: INSTANCE vs STATIC - Mixed in one class
// ============================================================================

/// <summary>
/// Book class with both instance members and static members
/// Instance: belongs to each book object
/// Static: shared across all books in the application
/// </summary>
public class Book
{
    // ========== STATIC MEMBERS (belong to the CLASS) ==========

    /// Static counter - tracks total books created
    private static int _totalBooksCreated = 0;

    /// Static property - can read total, can't write from outside
    public static int TotalBooksCreated
    {
        get => _totalBooksCreated;
    }

    /// Static method - gets the latest book ID
    private static int _nextBookId = 1000;

    public static int GetNextId()
    {
        return _nextBookId++;
    }

    // ========== INSTANCE MEMBERS (belong to each book) ==========

    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Author { get; private set; }
    public bool IsAvailable { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Book(string title, string author)
    {
        Id = GetNextId();  // Call static method
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Author = author ?? throw new ArgumentNullException(nameof(author));
        IsAvailable = true;
        CreatedAt = DateTime.UtcNow;

        _totalBooksCreated++;  // Increment static counter
    }

    public void Borrow()
    {
        if (!IsAvailable)
            throw new InvalidOperationException("Book is not available");

        IsAvailable = false;
    }

    public void Return()
    {
        IsAvailable = true;
    }
}

/// <summary>
/// MEMORY MODEL - Shows the critical difference
/// 
/// Instance members:
///   book1.Title = "Clean Code"        → belongs to book1 only
///   book2.Title = "Clean Architecture" → belongs to book2 only
///   (Each object has its own Title)
/// 
/// Static member:
///   Book.TotalBooksCreated = 3  → ONE value, shared by entire class
///                                  all books see the same value
///                                  changes in one place affect everywhere
/// </summary>

// ============================================================================
// PART 4: STATIC CLASS - Everything must be static
// ============================================================================

/// <summary>
/// Logger utility - static class means NO objects can be created
/// Cannot do: var logger = new Logger();  (COMPILER ERROR)
/// Must do:   Logger.Log("message");      (CORRECT)
/// </summary>
public static class Logger
{
    private static List<string> _logs = new();

    public static void Log(string message)
    {
        string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        string logEntry = $"[{timestamp}] {message}";
        _logs.Add(logEntry);
        Console.WriteLine(logEntry);
    }

    public static void PrintAllLogs()
    {
        Console.WriteLine("\n=== ALL LOGS ===");
        foreach (var log in _logs)
        {
            Console.WriteLine(log);
        }
    }

    public static int LogCount => _logs.Count;
}

/// <summary>
/// MATH EXTENSION - Static class adding methods to built-in types
/// This is an extension method - you'll see this A LOT in real code
/// </summary>
public static class MathExtensions
{
    /// <summary>
    /// Square a number
    /// The 'this' keyword attaches this to int type itself
    /// </summary>
    public static int Square(this int number)
    {
        return number * number;
    }

    /// <summary>
    /// Check if even
    /// </summary>
    public static bool IsEven(this int number)
    {
        return number % 2 == 0;
    }

    /// <summary>
    /// Check if odd
    /// </summary>
    public static bool IsOdd(this int number)
    {
        return number % 2 != 0;
    }
}

/// <summary>
/// USAGE - Extension methods feel like native to the type:
/// 
/// int number = 5;
/// int squared = number.Square();      // feels native
/// bool even = number.IsEven();        // feels native
/// bool odd = number.IsOdd();          // feels native
/// 
/// Without extension methods, you'd need:
/// int squared = MathExtensions.Square(5);  // awkward
/// </summary>

// ============================================================================
// REAL WORLD: Combining All Concepts
// ============================================================================

/// <summary>
/// Event management system showing:
/// - Instance members (per event)
/// - Static members (system level)
/// - Static methods and properties
/// </summary>
public class Event
{
    // ===== STATIC - System level =====
    private static int _totalEventsCreated = 0;
    private static DateTime _systemStartTime = DateTime.UtcNow;

    public static int TotalEvents => _totalEventsCreated;
    public static TimeSpan SystemUptime => DateTime.UtcNow - _systemStartTime;

    // ===== INSTANCE - What each event is =====
    public int Id { get; private set; }
    public string Name { get; private set; }
    public DateTime ScheduledTime { get; private set; }
    public int AttendeeCount { get; private set; }

    /// Constructor where we increment the static counter
    public Event(string name, DateTime scheduledTime)
    {
        Id = ++_totalEventsCreated;  // Get next ID and increment
        Name = name ?? throw new ArgumentNullException(nameof(name));
        ScheduledTime = scheduledTime;
        AttendeeCount = 0;
    }

    public void AddAttendee()
    {
        AttendeeCount++;
    }

    public override string ToString()
    {
        return $"Event #{Id}: {Name} at {ScheduledTime:G} ({AttendeeCount} attendees)";
    }
}

/// <summary>
/// USAGE:
/// var event1 = new Event("Conference", DateTime.UtcNow.AddDays(7));
/// var event2 = new Event("Meetup", DateTime.UtcNow.AddDays(14));
/// 
/// event1.AddAttendee();
/// event1.AddAttendee();
/// event2.AddAttendee();
/// 
/// Console.WriteLine(event1.AttendeeCount);  // 2 - event1's own count
/// Console.WriteLine(event2.AttendeeCount);  // 1 - event2's own count
/// Console.WriteLine(Event.TotalEvents);     // 2 - class-level total
/// </summary>
