namespace Floor2_OOP.Polymorphism.Models;

/// <summary>
/// SlackNotifier - NEW form added later without touching anything else
/// This demonstrates the power of polymorphism:
/// Add new form → existing code adapts automatically
/// </summary>
public class SlackNotifier : BaseNotifier
{
    public override void Send(string message)
    {
        Console.WriteLine($"[SLACK] Posting to Slack: {message}");
    }
}
