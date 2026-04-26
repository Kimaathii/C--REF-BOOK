namespace Floor1_Classes.Models;

/// <summary>
/// Option 1: Full Private Field + Public Method
/// ============================================
/// Best for: Sensitive data that needs validation
/// Pattern: Private field with controlled gateway methods
/// 
/// The class OWNS the data and controls ALL access.
/// Nothing gets in or out without passing through our validation.
/// </summary>
public class User
{
    private string _email;

    // Constructor can also validate
    public User(string email)
    {
        UpdateEmail(email);
    }

    /// <summary>
    /// The ONLY way to set email. It's a gateway with a gatekeeper.
    /// </summary>
    public void UpdateEmail(string newEmail)
    {
        if (string.IsNullOrWhiteSpace(newEmail))
            throw new ArgumentException("Email cannot be empty");

        if (!newEmail.Contains("@"))
            throw new ArgumentException("Invalid email format — must contain @");

        if (newEmail.Length > 254)
            throw new ArgumentException("Email too long");

        _email = newEmail.Trim().ToLowerInvariant();
    }

    /// <summary>
    /// Controlled reading. We decide what information leaves this class.
    /// </summary>
    public string GetEmail()
    {
        return _email;
    }

    /// <summary>
    /// Another method that uses the private field internally.
    /// Outside code never needs to know about _email directly.
    /// </summary>
    public bool IsValidEmail()
    {
        return !string.IsNullOrEmpty(_email) && _email.Contains("@");
    }
}
