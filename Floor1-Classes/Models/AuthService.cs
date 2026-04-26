namespace Floor1_Classes.Models;

/// <summary>
/// Option 3: Fully Private, Never Exposed
/// =======================================
/// Best for: Sensitive internal data (passwords, tokens, secrets)
/// Pattern: Private field, NO property getter, only internal use
/// 
/// The outside world has NO idea what's stored here.
/// This class is a black box for security-critical operations.
/// </summary>
public class AuthService
{
    // FULLY PRIVATE — nobody outside this class sees this
    private string _passwordHash;
    private DateTime _lastPasswordChangeAt;

    // Constructor initializes but nothing is exposed
    public AuthService()
    {
        _lastPasswordChangeAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Register a password. Hash it immediately and store only the hash.
    /// The original password is NEVER stored anywhere.
    /// </summary>
    public void Register(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be empty");

        if (password.Length < 8)
            throw new ArgumentException("Password must be at least 8 characters");

        if (!HasUpperCase(password))
            throw new ArgumentException("Password must contain uppercase letters");

        // Hash it and store ONLY the hash
        _passwordHash = HashPassword(password);
        _lastPasswordChangeAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Verify a provided password against the stored hash.
    /// Returns true/false — the hash is never exposed.
    /// </summary>
    public bool VerifyPassword(string providedPassword)
    {
        if (string.IsNullOrEmpty(_passwordHash))
            throw new InvalidOperationException("No password registered yet");

        if (string.IsNullOrEmpty(providedPassword))
            return false;

        string providedHash = HashPassword(providedPassword);
        return providedHash == _passwordHash;
    }

    /// <summary>
    /// Check if password was changed recently.
    /// Returns time since change, hash stays private.
    /// </summary>
    public TimeSpan TimeSinceLastPasswordChange()
    {
        return DateTime.UtcNow - _lastPasswordChangeAt;
    }

    /// <summary>
    /// Check if password needs to be refreshed (older than 90 days).
    /// Business logic lives here, not outside code.
    /// </summary>
    public bool ShouldChangePassword()
    {
        return TimeSinceLastPasswordChange().TotalDays > 90;
    }

    // ============= PRIVATE HELPERS — NEVER EXPOSED =============

    private string HashPassword(string password)
    {
        // Simplified hashing for demo (use bcrypt/PBKDF2 in real apps!)
        return Convert.ToBase64String(
            System.Text.Encoding.UTF8.GetBytes(password + "salt")
        );
    }

    private bool HasUpperCase(string text)
    {
        return text.Any(char.IsUpper);
    }

    // INTENTIONALLY NO properties like GetHash() or PasswordHash getter
    // The hash is OUR secret. You don't need it. You only call Verify().
}
