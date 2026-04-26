namespace Floor2_OOP.Polymorphism.Models;

/// <summary>
/// MemberService - another service that uses polymorphic notifier
/// Shows how multiple services work with same shape
/// </summary>
public class MemberService
{
    private readonly BaseNotifier _notifier;

    public MemberService(BaseNotifier notifier)
    {
        _notifier = notifier;
    }

    public void RegisterMember(string memberName)
    {
        Console.WriteLine($"Member '{memberName}' registered");
        _notifier.Send($"New member: {memberName}");
    }
}
