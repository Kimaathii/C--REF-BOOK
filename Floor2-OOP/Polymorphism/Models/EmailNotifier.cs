namespace Floor2_OOP.Polymorphism.Models;

/// <summary>
/// EmailNotifier - one form of Notifier
/// override = officially replace parent's Send with my own implementation
/// </summary>
public class EmailNotifier : BaseNotifier
{
    public override void Send(string message)
    {
        Console.WriteLine($"[EMAIL] Sending email: {message}");
    }
}
