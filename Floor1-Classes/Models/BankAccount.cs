namespace Floor1_Classes.Models;

/// <summary>
/// REAL WORLD: BankAccount
/// =======================
/// Brings together ALL THREE CONCEPTS:
/// 1. Underscore convention (_accountNumber, _balance)
/// 2. Field vs Property (_balance field + Balance property)
/// 3. readonly (_accountNumber never changes after creation)
/// </summary>
public class BankAccount
{
    // readonly field + underscore = set once in constructor, never changes
    private readonly string _accountNumber;

    // Private field with underscore + controlled property = protection
    private decimal _balance;

    public decimal Balance
    {
        get => _balance;
        private set => _balance = value;  // only this class can set
    }

    // Immutable properties set at creation
    public string AccountHolder { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // State that changes over time
    public int TransactionCount { get; private set; } = 0;

    public BankAccount(string accountNumber, string accountHolder, decimal initialBalance)
    {
        // Validate inputs
        if (string.IsNullOrWhiteSpace(accountNumber))
            throw new ArgumentException("Account number required");

        if (string.IsNullOrWhiteSpace(accountHolder))
            throw new ArgumentException("Account holder required");

        if (initialBalance < 0)
            throw new ArgumentException("Initial balance cannot be negative");

        // Set readonly fields (only legal place)
        _accountNumber = accountNumber;

        // Set properties
        AccountHolder = accountHolder;
        CreatedAt = DateTime.UtcNow;
        Balance = initialBalance;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Deposit amount must be positive");

        _balance += amount;  // use private field
        TransactionCount++;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Withdrawal amount must be positive");

        if (_balance < amount)
            throw new InvalidOperationException(
                $"Insufficient funds. Balance: {_balance}, Requested: {amount}");

        _balance -= amount;
        TransactionCount++;
    }

    public void PrintStatement()
    {
        Console.WriteLine($"════════════════════════════════════════");
        Console.WriteLine($"Account: {_accountNumber}");          // readonly field
        Console.WriteLine($"Holder:  {AccountHolder}");           // property
        Console.WriteLine($"Created: {CreatedAt:G}");             // property
        Console.WriteLine($"────────────────────────────────────");
        Console.WriteLine($"Balance: ${_balance:F2}");            // private field via property
        Console.WriteLine($"Transactions: {TransactionCount}");   // property
        Console.WriteLine($"════════════════════════════════════════");
    }

    // ============================================================
    // WHAT CAN'T HAPPEN (Compiler prevents these)
    // ============================================================

    // ❌ account._accountNumber = "faked";  // readonly - compiler error
    // ❌ account.AccountHolder = "someone";  // no setter - compiler error
    // ❌ account._balance = 99999;            // private - compiler error
    // ❌ account.Balance = 99999;             // private setter - compiler error

    // ONLY THESE WORK:
    // ✅ account.Deposit(100);
    // ✅ account.Withdraw(50);
    // ✅ var balance = account.Balance;  // getter works
    // ✅ var number = account._accountNumber;  // NOPE - it's private!
}
