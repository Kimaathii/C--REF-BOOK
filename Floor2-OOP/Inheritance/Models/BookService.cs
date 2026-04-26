namespace Floor2_OOP.Inheritance.Models;

/// <summary>
/// BookService - inherits from BaseService
/// Gets _activityLog, _serviceName, LogActivity, PrintAllLogs automatically
/// Overrides LogActivity to add custom behavior
/// </summary>
public class BookService : BaseService  // 👈 inherits from BaseService
{
    private readonly List<Book> _books;

    public BookService()
        : base("BookService")  // : base calls parent constructor
    {
        _books = new List<Book>();
    }

    /// <summary>
    /// Override parent's LogActivity - BookService logs differently
    /// Calls base.LogActivity first, then adds extra behavior
    /// </summary>
    public override void LogActivity(string message)
    {
        // Call parent's version first
        base.LogActivity(message);

        // Then add extra behavior - show book count
        Console.WriteLine($"    → Books in system: {_books.Count}");
    }

    public void AddBook(string title, string author)
    {
        var book = new Book(title, author);
        _books.Add(book);
        LogActivity($"Book added: {book.Title}");  // inherited + overridden
    }

    public void BorrowBook(int bookId)
    {
        var book = FindBook(bookId);
        book.Borrow();  // encapsulation - Book controls its own state
        LogActivity($"Book borrowed: {book.Title}");
    }

    public void ReturnBook(int bookId)
    {
        var book = FindBook(bookId);
        book.Return();
        LogActivity($"Book returned: {book.Title}");
    }

    public void PrintAllBooks()
    {
        Console.WriteLine("\n--- All Books ---");

        if (_books.Count == 0)
        {
            Console.WriteLine("  No books available");
            return;
        }

        foreach (var book in _books)
            Console.WriteLine($"  {book}");

        Console.WriteLine("----------------\n");
    }

    private Book FindBook(int bookId)
    {
        var book = _books.FirstOrDefault(b => b.Id == bookId);

        if (book == null)
            throw new InvalidOperationException($"Book with id {bookId} not found");

        return book;
    }
}
