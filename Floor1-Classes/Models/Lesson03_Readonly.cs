namespace Floor1_Classes.Models;

/// <summary>
/// LESSON: readonly Keyword
/// ========================
/// readonly means: "Set once in the constructor. Never again. Ever."
/// The compiler ENFORCES this. You cannot accidentally reassign it.
/// </summary>

public class ReadonlyDemo
{
    // ============================================================
    // ❌ THE PROBLEM: Field can be accidentally reassigned
    // ============================================================
    public class DatabaseConnectionBROKEN
    {
        private string _connectionString;

        public DatabaseConnectionBROKEN(string connectionString)
        {
            _connectionString = connectionString;  // set in constructor
        }

        public void ExecuteQuery(string query)
        {
            // 💀 ANY method can accidentally wipe it
            _connectionString = null;  // compiles fine, runtime CRASHES
            // Now every method after this breaks
        }

        public void AnotherMethod()
        {
            // 💀 Or accidentally overwrite it
            _connectionString = "wrong://connection";
            // Now all queries go to the wrong database
        }
    }

    // ============================================================
    // ✅ SOLUTION: readonly makes it immutable after construction
    // ============================================================
    public class DatabaseConnectionFIXED
    {
        private readonly string _connectionString;

        public DatabaseConnectionFIXED(string connectionString)
        {
            _connectionString = connectionString;  // ✅ only legal assignment
        }

        public void ExecuteQuery(string query)
        {
            // 💥 COMPILER ERROR if you try:
            // _connectionString = null;
            // Cannot assign to readonly field
            
            // The compiler just saved you from a runtime crash
        }

        public void AnotherMethod()
        {
            // 💥 COMPILER ERROR if you try:
            // _connectionString = "wrong://connection";
            // Cannot assign to readonly field
            
            // You are GUARANTEED this field is what was set in the constructor
        }
    }

    // ============================================================
    // REAL WORLD: Multi-dependency Service
    // ============================================================
    public class AuthServicePROPER
    {
        private readonly string _connectionString;    // set once, never changes
        private readonly string _secretKey;           // set once, never changes
        private readonly int _maxLoginAttempts;       // set once, never changes
        private int _currentAttempts = 0;             // NOT readonly - changes during work

        public AuthServicePROPER(string connectionString, string secretKey, int maxLoginAttempts)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("Connection string required");

            if (string.IsNullOrEmpty(secretKey))
                throw new ArgumentException("Secret key required");

            if (maxLoginAttempts <= 0)
                throw new ArgumentException("Max attempts must be positive");

            // All readonly fields set EXACTLY ONCE
            _connectionString = connectionString;
            _secretKey = secretKey;
            _maxLoginAttempts = maxLoginAttempts;
        }

        public bool Login(string username, string password)
        {
            // These readonly fields are GUARANTEED to be what we set in the constructor
            // Every method in this class can use them with complete confidence
            if (_currentAttempts >= _maxLoginAttempts)
                throw new InvalidOperationException($"Max {_maxLoginAttempts} attempts exceeded");

            // Simulate checking password against connection string
            bool isValid = password.Length > 0;

            if (!isValid)
                _currentAttempts++;

            return isValid;
        }

        public void ResetAttempts()
        {
            _currentAttempts = 0;  // ✅ this CAN be reassigned - it's not readonly
        }
    }

    // ============================================================
    // KEY RULE
    // ============================================================
    // readonly field = can be assigned:
    //  ✅ In its declaration:  private readonly string _name = "John";
    //  ✅ In the constructor:  _name = "John";
    //  ❌ NOWHERE ELSE - compiler prevents it
    //
    // Use readonly for: dependencies, configuration, immutable state
    // Don't use for: working variables that change during execution
}
