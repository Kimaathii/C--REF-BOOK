namespace Floor2_OOP.Polymorphism.Models;

/// <summary>
/// BREAKING DEMONSTRATIONS - Classes for testing polymorphism rules
/// </summary>

/// <summary>
/// BrokenBaseNotifier - WITHOUT virtual keyword
/// Shows what happens when parent doesn't allow override
/// </summary>
public class BrokenBaseNotifier
{
    public void Send(string message)  // ❌ no virtual
    {
        Console.WriteLine($"[BROKEN BASE] {message}");
    }
}

/// <summary>
/// BrokenChildNotifier - tries to override without parent virtual
/// Child's version will be ignored when accessed through parent type
/// Uses 'new' keyword because parent doesn't allow override
/// </summary>
public class BrokenChildNotifier : BrokenBaseNotifier
{
    public new void Send(string message)  // 'new' = intentional hiding (not override)
    {
        Console.WriteLine($"[BROKEN CHILD] {message}");
    }
}

/// <summary>
/// GoodBaseNotifier - WITH virtual keyword (correct)
/// </summary>
public class GoodBaseNotifier
{
    public virtual void Send(string message)  // ✅ virtual
    {
        Console.WriteLine($"[GOOD BASE] {message}");
    }
}

/// <summary>
/// HiddenChildNotifier - no override keyword
/// This hides parent's method instead of overriding
/// Parent's version runs when accessed through parent type
/// Uses 'new' keyword to make the hiding intentional
/// </summary>
public class HiddenChildNotifier : GoodBaseNotifier
{
    public new void Send(string message)  // 'new' = hiding parent method (not polymorphism)
    {
        Console.WriteLine($"[HIDDEN CHILD] {message}");
    }
}
