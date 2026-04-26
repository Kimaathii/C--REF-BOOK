namespace Floor2_OOP.Inheritance.Models;

/// <summary>
/// Member model - demonstrates encapsulation and constructor chaining
/// </summary>
public class Member
{
    // Encapsulated properties
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Role { get; private set; }

    private static int _nextId = 1;
    private static readonly string[] ValidRoles = { "Admin", "Member", "Moderator" };

    /// <summary>
    /// Convenience constructor - chains to master constructor with default role
    /// </summary>
    public Member(string name, string email)
        : this(name, email, "Member")  // : this chains to the master constructor below
    { }

    /// <summary>
    /// Master constructor - does all the real work
    /// </summary>
    public Member(string name, string email, string role)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));

        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            throw new ArgumentException("Invalid email format", nameof(email));

        if (!ValidRoles.Contains(role))
            throw new ArgumentException($"Invalid role: {role}. Must be: {string.Join(", ", ValidRoles)}", nameof(role));

        Id = _nextId++;
        Name = name;
        Email = email;
        Role = role;
    }

    /// <summary>
    /// Controlled gateway for email updates
    /// </summary>
    public void UpdateEmail(string newEmail)
    {
        if (string.IsNullOrWhiteSpace(newEmail) || !newEmail.Contains("@"))
            throw new ArgumentException("Invalid email format", nameof(newEmail));

        Email = newEmail;
    }

    /// <summary>
    /// Controlled gateway for role assignment
    /// </summary>
    public void AssignRole(string role)
    {
        if (!ValidRoles.Contains(role))
            throw new ArgumentException($"Invalid role: {role}. Must be: {string.Join(", ", ValidRoles)}", nameof(role));

        Role = role;
    }

    public override string ToString()
    {
        return $"[{Id}] {Name} ({Email}) — {Role}";
    }
}
