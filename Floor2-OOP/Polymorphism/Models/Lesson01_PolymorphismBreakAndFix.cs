namespace Floor2_OOP.Polymorphism.Models;

/// <summary>
/// PILLAR 3: POLYMORPHISM - One shape, many forms
/// ================================================
/// 
/// Polymorphism solves: Tight coupling and if/else chains
/// Instead of services checking "what type are you?" constantly,
/// define a SHAPE (parent) and let many FORMS (children) implement it
/// Runtime decides which form runs based on actual object
/// </summary>

// ============================================================================
// THE PROBLEM: No Polymorphism - If/Else Chains Everywhere
// ============================================================================

/// <summary>
/// ❌ WRONG: NotificationService with separate methods for each type
/// </summary>
public class NotificationServiceNoBridge
{
    public void SendEmail(string message)
    {
        Console.WriteLine($"[EMAIL] {message}");
    }

    public void SendSms(string message)
    {
        Console.WriteLine($"[SMS] {message}");
    }

    public void SendPush(string message)
    {
        Console.WriteLine($"[PUSH] {message}");
    }
}

/// <summary>
/// ❌ WRONG: BookService coupled to notification types
/// Every new notification type requires changes to BookService
/// </summary>
public class BookServiceNoBridge
{
    private readonly NotificationServiceNoBridge _notificationService;

    public BookServiceNoBridge(NotificationServiceNoBridge notificationService)
    {
        _notificationService = notificationService;
    }

    public void BorrowBook(int bookId, string notificationType)
    {
        Console.WriteLine($"Book {bookId} borrowed");

        // 💀 if/else chain - tightly coupled to notification types
        // Tomorrow: add Slack? Come back here and add another else if
        if (notificationType == "email")
            _notificationService.SendEmail($"Book {bookId} borrowed");

        else if (notificationType == "sms")
            _notificationService.SendSms($"Book {bookId} borrowed");

        else if (notificationType == "push")
            _notificationService.SendPush($"Book {bookId} borrowed");

        // next month: add WhatsApp
        // this method keeps growing
        // the service keeps changing
        // other services (MemberService, ReviewService) need same changes
    }
}

/// <summary>
/// WHAT GOES WRONG:
/// 
/// - If/else chains in every method that sends notifications
/// - New notification type added → must update BookService ❌
///                                 must update MemberService ❌
///                                 must update ReviewService ❌
///                                 forget one service? Silent bugs 💀
/// - BookService knows WAY too much about notification internals
/// - Tight coupling = hard to test, hard to extend, hard to maintain
/// </summary>

// ============================================================================
// THE FIX: Polymorphism - Define Shape, Let Forms Implement
// ============================================================================

/// <summary>
/// ✅ RIGHT: ONE parent shape with virtual Send()
/// Defines what all notifiers MUST have
/// 
/// virtual = parent allows children to override this
/// </summary>
public class BaseNotifierTeaching
{
    // virtual = I'm allowing children to replace this behavior
    public virtual void Send(string message)
    {
        Console.WriteLine($"[BASE] {message}");
    }
}

/// <summary>
/// ✅ EmailNotifier - one form
/// override = officially replacing parent's version
/// </summary>
public class EmailNotifierTeaching : BaseNotifierTeaching
{
    public override void Send(string message)
    {
        Console.WriteLine($"[EMAIL] Sending email: {message}");
    }
}

/// <summary>
/// ✅ SmsNotifier - another form
/// Same parent, different implementation
/// </summary>
public class SmsNotifierTeaching : BaseNotifierTeaching
{
    public override void Send(string message)
    {
        Console.WriteLine($"[SMS] Sending SMS: {message}");
    }
}

/// <summary>
/// ✅ PushNotifier - yet another form
/// This is where "many forms" comes from
/// </summary>
public class PushNotifierTeaching : BaseNotifierTeaching
{
    public override void Send(string message)
    {
        Console.WriteLine($"[PUSH] Sending push: {message}");
    }
}

/// <summary>
/// ✅ BookService WITH polymorphism - knows only the SHAPE
/// 
/// Compare this to BookServiceNoBridge above:
/// - No if/else chains checking notification type ✅
/// - Only knows BaseNotifier (the shape), not Email/Sms/Push ✅
/// - Same method signature - no parameters for type selection ✅
/// - Tomorrow add Slack? BookService never changes ✅
/// </summary>
public class BookServiceWithBridge
{
    private readonly BaseNotifierTeaching _notifier;  // 👈 SHAPE not specific type

    public BookServiceWithBridge(BaseNotifierTeaching notifier)
    {
        _notifier = notifier;
    }

    public void BorrowBook(int bookId)
    {
        Console.WriteLine($"Book {bookId} borrowed");
        _notifier.Send($"Book {bookId} borrowed");  // 👈 ONE line. Always.
                                                     // No if/else, no type checking
    }
}

/// <summary>
/// Usage difference - see the elegance:
/// 
/// WITHOUT polymorphism:
///   var service = new BookServiceNoBridge(notificationService);
///   service.BorrowBook(1, "email");  // must specify TYPE
///   service.BorrowBook(1, "sms");    // must specify TYPE
///
/// WITH polymorphism:
///   var emailService = new BookServiceWithBridge(new EmailNotifierTeaching());
///   emailService.BorrowBook(1);      // type is ALREADY in the object
///
///   var smsService = new BookServiceWithBridge(new SmsNotifierTeaching());
///   smsService.BorrowBook(1);        // different form, same code
/// </summary>

// ============================================================================
// THE BREAKTHROUGH: List of Different Forms (All Same Shape)
// ============================================================================

/// <summary>
/// This is where polymorphism gets TRULY powerful
/// A list holding different forms all implementing same shape
/// </summary>
public class PolymorphismRevealed
{
    public static void DemonstrateListPolymorphism()
    {
        // a list of BaseNotifier references
        // but they can hold different actual types
        var notifiers = new List<BaseNotifierTeaching>
        {
            new EmailNotifierTeaching(),
            new SmsNotifierTeaching(),
            new PushNotifierTeaching()
        };

        // one loop - calling same method on each
        // but each runs its OWN implementation
        foreach (var notifier in notifiers)
        {
            notifier.Send("Book borrowed!");
        }

        // 💡 OUTPUT:
        // [EMAIL] Sending email: Book borrowed!
        // [SMS] Sending SMS: Book borrowed!
        // [PUSH] Sending push: Book borrowed!

        // Same method. Same loop. Three completely different outputs.
        // That's polymorphism.
    }
}

/// <summary>
/// BREAKING RULE 1: Remove virtual from parent
/// What happens?
/// </summary>
public class BaseNotifierWithoutVirtual  // 💀 virtual removed
{
    public void Send(string message)  // no virtual
    {
        Console.WriteLine($"[BASE] {message}");
    }
}

public class EmailNotifierTryingToOverride : BaseNotifierWithoutVirtual
{
    public new void Send(string message)  // 'new' = hiding (since parent not virtual)
    {
        Console.WriteLine($"[EMAIL] {message}");
    }
}

// Run this: BaseNotifierWithoutVirtual notifier = new EmailNotifierTryingToOverride();
//          notifier.Send("test");
// Result: [BASE] test  ← Child's version NEVER runs because parent didn't allow override

/// <summary>
/// BREAKING RULE 2: Remove override from child
/// What happens?
/// </summary>
public class EmailNotifierWithoutOverride : BaseNotifierTeaching
{
    public new void Send(string message)  // 'new' = hiding (not overriding)
    {
        Console.WriteLine($"[EMAIL] {message}");
    }
}

// Run this: BaseNotifierTeaching notifier = new EmailNotifierWithoutOverride();
//          notifier.Send("test");
// Result: [BASE] test  ← Parent version runs. Child's hidden method is ignored.

/// <summary>
/// BREAKING RULE 3: Type assignment rules
/// </summary>
// ✅ This works - child assigned to parent variable (polymorphism can happen)
// BaseNotifierTeaching notifier = new EmailNotifierTeaching();

// ❌ This fails - try to assign parent to child variable restricted to child type
// EmailNotifierTeaching notifier = new BaseNotifierTeaching();  // 💥 compiler error
// You can't assign parent to child because parent might not have all child's methods

/// <summary>
/// KEY INSIGHT - Why This Matters
/// 
/// The power is in the type mismatch:
/// - Variable type: BaseNotifier (the interface/contract)
/// - Actual object: EmailNotifier (the implementation)
/// - C# checks what's ACTUALLY in memory at runtime
/// - Runs the right version without you knowing which one it is
/// 
/// This is why it's called RUNTIME polymorphism
/// Compile time: C# compiles against the shape you declared
/// Runtime: C# runs the actual form's version
/// </summary>
