namespace Floor2_OOP.Polymorphism.Models;

/// <summary>
/// BookService - knows only the SHAPE (BaseNotifier)
/// Doesn't know about EmailNotifier, SmsNotifier, PushNotifier, SlackNotifier
/// 
/// This is the power of polymorphism:
/// - BookService never needs to change when new notifier types are added
/// - No if/else chains checking "what type are you?"
/// - Just call _notifier.Send() and the right thing happens
/// </summary>
public class BookService
{
    private readonly BaseNotifier _notifier;  // 👈 SHAPE not specific type

    public BookService(BaseNotifier notifier)
    {
        _notifier = notifier;
    }

    public void BorrowBook(int bookId)
    {
        Console.WriteLine($"Book {bookId} borrowed");
        _notifier.Send($"Book {bookId} has been borrowed");  // 👈 One line. Always.
    }

    public void ReturnBook(int bookId)
    {
        Console.WriteLine($"Book {bookId} returned");
        _notifier.Send($"Book {bookId} has been returned");  // 👈 Same shape
    }

    public void AddReview(int bookId, int rating)
    {
        Console.WriteLine($"Review added for book {bookId}: {rating} stars");
        _notifier.Send($"Book {bookId} reviewed with {rating} stars");  // 👈 No if/else
    }
}
