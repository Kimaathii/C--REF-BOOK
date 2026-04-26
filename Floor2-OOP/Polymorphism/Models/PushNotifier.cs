namespace Floor2_OOP.Polymorphism.Models;

/// <summary>
/// PushNotifier - yet another form of Notifier
/// All three (Email, SMS, Push) are different behaviors of the same shape
/// </summary>
public class PushNotifier : BaseNotifier
{
    public override void Send(string message)
    {
        Console.WriteLine($"[PUSH] Sending push notification: {message}");
    }
}
