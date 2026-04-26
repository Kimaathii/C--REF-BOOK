namespace Floor2_OOP.Polymorphism.Models;

/// <summary>
/// TelegramNotifier - demonstrating adding new form to system
/// Created without changing any existing classes
/// </summary>
public class TelegramNotifier : BaseNotifier
{
    public override void Send(string message)
    {
        Console.WriteLine($"[TELEGRAM] Sent via Telegram: {message}");
    }
}
