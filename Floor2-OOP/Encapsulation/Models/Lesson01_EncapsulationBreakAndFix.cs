namespace Floor2_OOP.Encapsulation.Models;

/// <summary>
/// PILLAR 1: ENCAPSULATION - Bundle data + behavior, hide internals
/// ==================================================================
/// 
/// Encapsulation = data and the methods that control it live together
///                 outside world can't corrupt the data directly
/// </summary>

// ============================================================================
// BREAK 1: ZERO ENCAPSULATION — Raw public data, no protection
// ============================================================================

/// <summary>
/// ❌ WRONG: No encapsulation whatsoever
/// Data is fully exposed. Anyone anywhere can corrupt it.
/// </summary>
public class BankAccountNoEncapsulation
{
    // 💀 Everything is public - zero protection
    public string Owner;
    public decimal Balance;
    public string AccountNumber;
}

/// <summary>
/// WHAT HAPPENS:
/// 
/// var account = new BankAccountNoEncapsulation();
/// account.Owner = "John";
/// account.Balance = 1000;
/// account.AccountNumber = "ACC-001";
/// 
/// // But also anywhere in your app:
/// account.Balance = -999999;     // 💀 negative balance - no check
/// account.Balance = 0;           // 💀 wiped silently
/// account.AccountNumber = null;  // 💀 corrupted
/// account.Owner = "";            // 💀 invalid
/// 
/// No protection. No rules. Silent data corruption.
/// In a banking app this is catastrophic. 💀
/// </summary>

// ============================================================================
// FIX 1: FULL ENCAPSULATION — Data hidden, behavior controls access
// ============================================================================

/// <summary>
/// ✅ RIGHT: Full encapsulation
/// Data is private. Only controlled gateways modify it.
/// Business rules are enforced at the point of mutation.
/// </summary>
public class BankAccount
{
    // 🔒 INTERNAL DATA — private, hidden from outside
    private decimal _balance;
    private readonly string _accountNumber;

    // 🌍 PUBLIC SURFACE — only what we allow to see
    public string Owner { get; private set; }
    public decimal Balance => _balance;  // read-only, no setter at all
    public string AccountNumber => _accountNumber;

    /// <summary>
    /// CONSTRUCTOR — only legal way to create a valid account
    /// </summary>
    public BankAccount(string owner, string accountNumber, decimal initialDeposit)
    {
        if (string.IsNullOrWhiteSpace(owner))
            throw new ArgumentException("Owner cannot be empty", nameof(owner));

        if (string.IsNullOrWhiteSpace(accountNumber))
            throw new ArgumentException("Account number cannot be empty", nameof(accountNumber));

        if (initialDeposit < 0)
            throw new ArgumentException("Initial deposit cannot be negative", nameof(initialDeposit));

        Owner = owner;
        _accountNumber = accountNumber;
        _balance = initialDeposit;
    }

    /// <summary>
    /// GATEWAY 1: Deposit — add money with validation
    /// </summary>
    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Deposit amount must be positive", nameof(amount));

        _balance += amount;
    }

    /// <summary>
    /// GATEWAY 2: Withdraw — remove money with validation
    /// </summary>
    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Withdrawal amount must be positive", nameof(amount));

        if (amount > _balance)
            throw new InvalidOperationException($"Insufficient funds. Balance: {_balance}, Requested: {amount}");

        _balance -= amount;
    }

    public override string ToString()
    {
        return $"Account {AccountNumber} ({Owner}): ${Balance:F2}";
    }
}

/// <summary>
/// ENCAPSULATION AT WORK:
/// 
/// var account = new BankAccount("John", "ACC-001", 1000);
/// 
/// // Try to corrupt it:
/// account._balance = -999999;     // 💥 COMPILER ERROR - private field
/// account.Balance = 500;          // 💥 COMPILER ERROR - no setter
/// account.Owner = "";             // 💥 COMPILER ERROR - no setter
/// 
/// // Only legal ways to modify:
/// account.Deposit(500);           // ✅ balance = 1500
/// account.Withdraw(200);          // ✅ balance = 1300
/// account.Withdraw(999999);       // 💥 throws - Insufficient funds
/// 
/// Console.WriteLine(account.Balance);  // ✅ 1300 - reading is fine
/// </summary>

// ============================================================================
// BREAK 2: LIBRARY BOOK — Zero Encapsulation
// ============================================================================

/// <summary>
/// ❌ WRONG: Book with no encapsulation
/// Anyone can put it in invalid states
/// </summary>
public class BookNoEncapsulation
{
    public int Id;
    public string Title;
    public string Author;
    public bool IsAvailable;
    public DateTime CreatedAt;
}

/// <summary>
/// WHAT GOES WRONG:
/// 
/// var book = new BookNoEncapsulation();
/// book.Title = "Clean Code";
/// book.IsAvailable = true;
/// 
/// // But then:
/// book.IsAvailable = false;  // borrowed
/// book.IsAvailable = false;  // set again (invalid state - already borrowed)
/// book.Title = null;         // corrupted title
/// book.Author = "";          // corrupted author
/// book.CreatedAt = new DateTime(1900, 1, 1);  // invalid date
/// 
/// Now your database has garbage data and your business logic is broken.
/// </summary>

// ============================================================================
// FIX 2: BOOK WITH ENCAPSULATION
// ============================================================================

/// <summary>
/// ✅ RIGHT: Book with encapsulation
/// Only valid state transitions allowed
/// Business rules enforced by the class itself
/// </summary>
public class Book
{
    // 🔒 INTERNAL DATA
    private bool _isAvailable;

    // 🌍 readonly properties
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Author { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // Read-only property for state
    public bool IsAvailable => _isAvailable;

    /// <summary>
    /// Constructor — the only way to create a book
    /// </summary>
    public Book(int id, string title, string author)
    {
        if (id <= 0)
            throw new ArgumentException("Id must be positive", nameof(id));

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty", nameof(title));

        if (string.IsNullOrWhiteSpace(author))
            throw new ArgumentException("Author cannot be empty", nameof(author));

        Id = id;
        Title = title;
        Author = author;
        _isAvailable = true;  // new books are always available
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// GATEWAY 1: Borrow — only if available
    /// </summary>
    public void Borrow()
    {
        if (!_isAvailable)
            throw new InvalidOperationException($"'{Title}' is already borrowed");

        _isAvailable = false;
    }

    /// <summary>
    /// GATEWAY 2: Return — only if borrowed
    /// </summary>
    public void Return()
    {
        if (_isAvailable)
            throw new InvalidOperationException($"'{Title}' was not borrowed");

        _isAvailable = true;
    }

    public override string ToString()
    {
        string status = _isAvailable ? "Available" : "Borrowed";
        return $"[{Id}] {Title} by {Author} ({status})";
    }
}

/// <summary>
/// ENCAPSULATION AT WORK:
/// 
/// var book = new Book(1, "Clean Code", "Robert Martin");
/// 
/// // Try to corrupt:
/// book._isAvailable = false;  // 💥 COMPILER ERROR - private
/// book.IsAvailable = false;   // 💥 COMPILER ERROR - no setter
/// book.Title = null;          // 💥 COMPILER ERROR - no setter
/// 
/// // Only legal state transitions:
/// book.Borrow();              // ✅ IsAvailable = false
/// book.Borrow();              // 💥 throws - already borrowed
/// book.Return();              // ✅ IsAvailable = true
/// book.Return();              // 💥 throws - was not borrowed
/// 
/// The book is now SELF-PROTECTING.
/// It won't allow invalid state transitions.
/// </summary>

// ============================================================================
// BREAK 3: USER REGISTRATION — Where Encapsulation Goes Wrong
// ============================================================================

/// <summary>
/// ❌ WRONG: User with no encapsulation
/// Status can be changed to invalid values
/// </summary>
public class UserNoEncapsulation
{
    public int Id;
    public string Email;
    public string Username;
    public string Status;  // "Active", "Suspended", "Deleted", or "lolwhatever"?
    public DateTime RegisteredAt;
}

/// <summary>
/// WHAT GOES WRONG:
/// 
/// var user = new UserNoEncapsulation();
/// user.Status = "Active";
/// user.Status = "Suspended";
/// user.Status = "lolwhatever";    // 💀 invalid status in database
/// user.Status = null;              // 💀 null status corrupts checks
/// 
/// Your app can't trust the Status value. It could be anything.
/// </summary>

// ============================================================================
// FIX 3: USER WITH ENCAPSULATION
// ============================================================================

/// <summary>
/// ✅ RIGHT: User with encapsulation
/// Status can only be set to valid values through methods
/// </summary>
public class User
{
    // 🔒 INTERNAL STATE
    private string _status;  // "Active", "Suspended", or "Deleted"

    // 🌍 readonly properties
    public int Id { get; private set; }
    public string Email { get; private set; }
    public string Username { get; private set; }
    public string Status => _status;
    public DateTime RegisteredAt { get; private set; }
    public DateTime? SuspendedAt { get; private set; }

    public User(int id, string email, string username)
    {
        if (id <= 0)
            throw new ArgumentException("Id must be positive", nameof(id));

        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            throw new ArgumentException("Invalid email", nameof(email));

        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username cannot be empty", nameof(username));

        Id = id;
        Email = email;
        Username = username;
        _status = "Active";
        RegisteredAt = DateTime.UtcNow;
    }

    /// <summary>
    /// GATEWAY 1: Suspend — move from Active to Suspended
    /// </summary>
    public void Suspend(string reason = "")
    {
        if (_status != "Active")
            throw new InvalidOperationException($"Cannot suspend a {_status} user");

        _status = "Suspended";
        SuspendedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// GATEWAY 2: Reactivate — move from Suspended back to Active
    /// </summary>
    public void Reactivate()
    {
        if (_status != "Suspended")
            throw new InvalidOperationException($"Cannot reactivate a {_status} user");

        _status = "Active";
        SuspendedAt = null;
    }

    /// <summary>
    /// GATEWAY 3: Delete — permanent, no going back
    /// </summary>
    public void Delete()
    {
        if (_status == "Deleted")
            throw new InvalidOperationException("User is already deleted");

        _status = "Deleted";
    }

    public override string ToString()
    {
        return $"{Username} ({Email}) - {Status}";
    }
}

/// <summary>
/// ENCAPSULATION IN ACTION:
/// 
/// var user = new User(1, "john@example.com", "johndoe");
/// 
/// // Try to corrupt:
/// user._status = "lolwhatever";  // 💥 COMPILER ERROR - private
/// user.Status = "Deleted";       // 💥 COMPILER ERROR - no setter
/// 
/// // Only valid transitions:
/// user.Suspend();                // ✅ Status = "Suspended"
/// user.Suspend();                // 💥 throws - already suspended
/// user.Reactivate();             // ✅ Status = "Active"
/// user.Delete();                 // ✅ Status = "Deleted"
/// user.Reactivate();             // 💥 throws - can't reactivate deleted user
/// 
/// Status is now GUARANTEED to be one of the three valid values.
/// No invalid states possible.
/// </summary>
