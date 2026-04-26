namespace Floor2_OOP.Inheritance.Models;

/// <summary>
/// PILLAR 2: INHERITANCE - Reuse and extend parent class behavior
/// ==================================================================
/// 
/// Inheritance solves: Code duplication
/// Instead of copying the same fields/methods in every service class,
/// create a parent class with shared stuff - children inherit it for free
/// </summary>

// ============================================================================
// THE PROBLEM: Code Duplication Without Inheritance
// ============================================================================

/// <summary>
/// ❌ WRONG: Three services all manually declaring the same stuff
/// </summary>
public class AuthServiceNoInheritance
{
    private readonly string _dbConnection;
    private readonly List<string> _logs;

    public AuthServiceNoInheritance(string dbConnection)
    {
        _dbConnection = dbConnection;
        _logs = new List<string>();
    }

    // 💀 copied method
    public void LogActivity(string message)
    {
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");
    }

    public void Register(string email)
    {
        LogActivity($"Registering: {email}");
        // registration logic
    }
}

public class BookServiceNoInheritance
{
    private readonly string _dbConnection;  // 💀 copied
    private readonly List<string> _logs;     // 💀 copied

    public BookServiceNoInheritance(string dbConnection)  // 💀 duplicated constructor
    {
        _dbConnection = dbConnection;
        _logs = new List<string>();
    }

    // 💀 exact same method copied
    public void LogActivity(string message)
    {
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");
    }

    public void AddBook(string title)
    {
        LogActivity($"Adding book: {title}");
        // book logic
    }
}

public class ReviewServiceNoInheritance
{
    private readonly string _dbConnection;  // 💀 copied AGAIN
    private readonly List<string> _logs;     // 💀 copied AGAIN

    public ReviewServiceNoInheritance(string dbConnection)  // 💀 duplicated AGAIN
    {
        _dbConnection = dbConnection;
        _logs = new List<string>();
    }

    // 💀 copied AGAIN
    public void LogActivity(string message)
    {
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");
    }

    public void AddReview(string bookId)
    {
        LogActivity($"Adding review for book: {bookId}");
        // review logic
    }
}

/// <summary>
/// WHAT GOES WRONG:
/// 
/// LogActivity() needs to change → you update AuthService ✅
///                                  you update BookService ✅
///                                  you update ReviewService ✅
///                                  you FORGOT ShelfService 💀
///                                  you FORGOT RatingService 💀
/// 
/// Silent bugs. Multiple versions of the same method.
/// When database changes, you update one service and forget two others.
/// </summary>

// ============================================================================
// THE FIX: See BaseService.cs, BookService.cs, MemberService.cs
// ============================================================================
//
// ✅ INHERITANCE IN ACTION:
// - BaseService.cs contains the parent class (once)
// - BookService.cs and MemberService.cs inherit from BaseService
// - When LogActivity needs to change? Update BaseService ONE TIME ✅
// - All children automatically get the update without any duplication

// ============================================================================
// VIRTUAL & OVERRIDE — Children Customize Parent Behavior
// ============================================================================

/// <summary>
/// Parent with virtual method (allows override)
/// </summary>
public class BaseServiceWithVirtual
{
    protected readonly string _serviceName;

    public BaseServiceWithVirtual(string serviceName)
    {
        _serviceName = serviceName;
    }

    // virtual = you're ALLOWED to override this
    public virtual void LogActivity(string message)
    {
        Console.WriteLine($"[{_serviceName}] {message}");
    }
}

/// <summary>
/// Child 1 - keeps parent behavior as-is
/// </summary>
public class SimpleChildService : BaseServiceWithVirtual
{
    public SimpleChildService()
        : base("SimpleChild")
    { }

    public void DoWork()
    {
        LogActivity("doing work");  // uses parent's version
    }
}

/// <summary>
/// Child 2 - OVERRIDES parent behavior with its own version
/// </summary>
public class CustomChildService : BaseServiceWithVirtual
{
    public CustomChildService()
        : base("CustomChild")
    { }

    // override = I'm replacing parent's version with my own
    public override void LogActivity(string message)
    {
        // child's own implementation
        Console.WriteLine($"[CUSTOM] [{_serviceName}] {message}");
    }

    public void DoWork()
    {
        LogActivity("doing work");  // uses child's overridden version
    }
}

/// <summary>
/// Child 3 - extends parent behavior with base keyword
/// </summary>
public class ExtendedChildService : BaseServiceWithVirtual
{
    public ExtendedChildService()
        : base("ExtendedChild")
    { }

    // override but also use parent's version
    public override void LogActivity(string message)
    {
        base.LogActivity(message);  // ✅ run parent's version first

        // then add extra behavior
        Console.WriteLine("  [EXTRA: logged to database too]");
    }

    public void DoWork()
    {
        LogActivity("doing work");  // parent's version PLUS child's extra
    }
}

/// <summary>
/// USAGE:
/// 
/// var simple = new SimpleChildService();
/// simple.DoWork();
/// // Output: [SimpleChild] doing work
/// 
/// var custom = new CustomChildService();
/// custom.DoWork();
/// // Output: [CUSTOM] [CustomChild] doing work
/// 
/// var extended = new ExtendedChildService();
/// extended.DoWork();
/// // Output: [ExtendedChild] doing work
///           [EXTRA: logged to database too]
/// </summary>
