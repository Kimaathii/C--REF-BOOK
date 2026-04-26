namespace Floor2_OOP.Polymorphism.Models;

/// <summary>
/// ReviewService - third service using polymorphic notifier
/// </summary>
public class ReviewService
{
    private readonly BaseNotifier _notifier;

    public ReviewService(BaseNotifier notifier)
    {
        _notifier = notifier;
    }

    public void AddReview(string reviewText, int rating)
    {
        Console.WriteLine($"Review added: {rating} stars - {reviewText}");
        _notifier.Send($"Review: {rating}⭐ - {reviewText}");
    }
}
