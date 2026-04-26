namespace Floor2_OOP.Inheritance.Models;

/// <summary>
/// Book model - demonstrates encapsulation
/// Data is protected, state changes only through methods
/// </summary>
public class Book
{
    // Encapsulated properties - nobody sets these directly
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Author { get; private set; }
    public bool IsAvailable { get; private set; }
    public DateTime AddedAt { get; private set; }

    // Static counter - shared across all books
    private static int _nextId = 1;

    public Book(string title, string author)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty", nameof(title));

        if (string.IsNullOrWhiteSpace(author))
            throw new ArgumentException("Author cannot be empty", nameof(author));

        Id = _nextId++;
        Title = title;
        Author = author;
        IsAvailable = true;
        AddedAt = DateTime.Now;
    }

    /// <summary>
    /// Controlled gateway - change state only through method
    /// </summary>
    public void Borrow()
    {
        if (!IsAvailable)
            throw new InvalidOperationException($"'{Title}' is already borrowed");

        IsAvailable = false;
    }

    /// <summary>
    /// Controlled gateway for returning
    /// </summary>
    public void Return()
    {
        if (IsAvailable)
            throw new InvalidOperationException($"'{Title}' was not borrowed");

        IsAvailable = true;
    }

    public override string ToString()
    {
        var status = IsAvailable ? "Available" : "Borrowed";
        return $"[{Id}] {Title} by {Author} — {status}";
    }
}
