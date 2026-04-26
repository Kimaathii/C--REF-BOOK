namespace Floor2_OOP.Polymorphism.Models;

/// <summary>
/// SmsNotifier - another form of Notifier
/// Same shape as EmailNotifier (both inherit from BaseNotifier)
/// But completely different behavior
/// </summary>
public class SmsNotifier : BaseNotifier
{
    public override void Send(string message)
    {
        Console.WriteLine($"[SMS] Sending SMS: {message}");
    }
}
