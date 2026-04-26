namespace Floor1_Classes.Models;

/// <summary>
/// LESSON: this keyword - Disambiguation & Constructor Chaining
/// ==============================================================
/// 
/// Part 1: This removes naming collisions
/// Part 2: This chains constructors to eliminate duplication
/// </summary>

// ============================================================================
// PART 1: DISAMBIGUATION — When parameter names collide with property names
// ============================================================================

/// <summary>
/// ❌ WRONG: Parameter names collide with property names
/// </summary>
public class BookWrong
{
    public string Title { get; private set; }
    public string Author { get; private set; }

    // Problem: parameter names same as property names
    public BookWrong(string Title, string Author)
    {
        Title = Title;    // 💀 assigns parameter to itself, property never set
        Author = Author;  // 💀 same disaster
    }
}

/// <summary>
/// ✅ OPTION A: Use underscore convention (what we prefer)
/// </summary>
public class BookWithUnderscore
{
    private string _title;
    private string _author;

    public string Title
    {
        get => _title;
        private set => _title = value;
    }

    public string Author
    {
        get => _author;
        private set => _author = value;
    }

    // Parameters lowercase, fields have _, collision impossible
    public BookWithUnderscore(string title, string author)
    {
        // Clear which is which
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Author = author ?? throw new ArgumentNullException(nameof(author));
    }
}

/// <summary>
/// ✅ OPTION B: Use this keyword when parameter names match property names
/// </summary>
public class BookWithThis
{
    // PascalCase properties (C# convention)
    public string Title { get; private set; }
    public string Author { get; private set; }

    // Can name parameters to match properties
    public BookWithThis(string Title, string Author)
    {
        // Use this. to explicitly mean "this object's property"
        this.Title = Title ?? throw new ArgumentNullException(nameof(Title));
        this.Author = Author ?? throw new ArgumentNullException(nameof(Author));
    }
}

// ============================================================================
// PART 2: CONSTRUCTOR CHAINING — Eliminate duplicated constructor code
// ============================================================================

/// <summary>
/// ❌ WRONG: Duplicated code in multiple constructors
/// Every time you add a property, you update BOTH constructors = bug magnet
/// </summary>
public class LibraryBookWrong
{
    public string Title { get; set; }
    public string Author { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime CreatedAt { get; set; }

    // Full constructor - does the real work
    public LibraryBookWrong(string title, string author, bool isAvailable)
    {
        Title = title;
        Author = author;
        IsAvailable = isAvailable;
        CreatedAt = DateTime.UtcNow;
    }

    // Convenience constructor - DUPLICATES all the logic
    public LibraryBookWrong(string title, string author)
    {
        Title = title;                    // 👈 duplicated
        Author = author;                  // 👈 duplicated
        IsAvailable = true;               // 👈 duplicated
        CreatedAt = DateTime.UtcNow;      // 👈 duplicated
        // If you add new property, you update BOTH. Silent bugs guaranteed.
    }
}

/// <summary>
/// ✅ RIGHT: Constructor chaining with this
/// One master constructor does the real work
/// Other constructors chain to it, providing defaults
/// </summary>
public class LibraryBook
{
    public string Title { get; set; }
    public string Author { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Genre { get; set; }

    /// <summary>
    /// MASTER constructor - this is where all the work happens
    /// </summary>
    public LibraryBook(string title, string author, bool isAvailable, string genre)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Author = author ?? throw new ArgumentNullException(nameof(author));
        IsAvailable = isAvailable;
        CreatedAt = DateTime.UtcNow;
        Genre = genre ?? "Unknown";
    }

    /// <summary>
    /// CONVENIENCE constructor - chains to master with defaults
    /// : this(...) means "before my body runs, run THAT constructor first"
    /// </summary>
    public LibraryBook(string title, string author)
        : this(title, author, true, "Unknown")
    {
        // Body is empty - master constructor handles everything
    }

    /// <summary>
    /// Another convenience constructor
    /// </summary>
    public LibraryBook(string title, string author, string genre)
        : this(title, author, true, genre)
    {
    }
}

/// <summary>
/// Practical example: User registration with different constructors
/// </summary>
public class UserWithConstructorChaining
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public bool IsEmailVerified { get; set; }
    public DateTime CreatedAt { get; set; }

    /// MASTER: Full control
    public UserWithConstructorChaining(int id, string email, string username, bool isEmailVerified)
    {
        if (id < 0)  // Allow 0 for unassigned new users
            throw new ArgumentException("Id must be non-negative");
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException(nameof(email));
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentNullException(nameof(username));

        Id = id;
        Email = email;
        Username = username;
        IsEmailVerified = isEmailVerified;
        CreatedAt = DateTime.UtcNow;
    }

    /// CONVENIENCE: New user registration (not verified yet)
    public UserWithConstructorChaining(string email, string username)
        : this(0, email, username, false)
    {
    }

    /// CONVENIENCE: Admin creating a pre-verified user
    public UserWithConstructorChaining(int id, string email, string username)
        : this(id, email, username, true)
    {
    }
}
