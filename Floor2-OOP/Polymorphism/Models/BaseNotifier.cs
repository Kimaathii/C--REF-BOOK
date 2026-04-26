namespace Floor2_OOP.Polymorphism.Models;

/// <summary>
/// BaseNotifier - parent class defining the SHAPE
/// 
/// virtual = parent allows children to override this method
/// All notifiers must have a Send(message) method
/// The actual behavior depends on which form (EmailNotifier, SmsNotifier, etc)
/// </summary>
public class BaseNotifier
{
    public virtual void Send(string message)
    {
        Console.WriteLine($"[BASE NOTIFIER] {message}");
    }
}
