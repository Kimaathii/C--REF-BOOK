using Floor2_OOP.Polymorphism.Models;

Console.WriteLine("═══════════════════════════════════════════════════════════════");
Console.WriteLine("FLOOR 2: PILLAR 3 — POLYMORPHISM");
Console.WriteLine("One shape · Many forms · Runtime dispatch");
Console.WriteLine("═══════════════════════════════════════════════════════════════\n");

// ============================================================================
// SECTION 1: Individual Notifiers - Each Form Different Behavior
// ============================================================================

Console.WriteLine("📌 SECTION 1: Individual Notifiers (Different Forms)\n");

// Same shape (BaseNotifier)
// Different actual objects (EmailNotifier, SmsNotifier, PushNotifier)
BaseNotifier emailNotifier = new EmailNotifier();
BaseNotifier smsNotifier = new SmsNotifier();
BaseNotifier pushNotifier = new PushNotifier();
BaseNotifier slackNotifier = new SlackNotifier();

Console.WriteLine("All the same shape (BaseNotifier) - watch different behaviors:\n");

emailNotifier.Send("Book borrowed");
smsNotifier.Send("Book borrowed");
pushNotifier.Send("Book borrowed");
slackNotifier.Send("Book borrowed");

Console.WriteLine("\n✅ INSIGHT: Same method call (.Send()) → Different outputs based on actual type\n");

// ============================================================================
// SECTION 2: The Mind-Blowing List Example
// ============================================================================

Console.WriteLine("═══════════════════════════════════════════════════════════════");
Console.WriteLine("📌 SECTION 2: List of Different Forms (This Will Blow Your Mind)\n");

// A list of BaseNotifier references
// In memory: EmailNotifier, SmsNotifier, PushNotifier, SlackNotifier
var notifiers = new List<BaseNotifier>
{
    new EmailNotifier(),
    new SmsNotifier(),
    new PushNotifier(),
    new SlackNotifier()
};

Console.WriteLine("One list. One loop. One method call (.Send()). Four different behaviors:\n");

foreach (var notifier in notifiers)
{
    notifier.Send("Book borrowed!");
}

Console.WriteLine("\n✅ POLYMORPHISM IN ACTION:");
Console.WriteLine("   - The loop doesn't know what types are in the list");
Console.WriteLine("   - It just calls Send() on each 'BaseNotifier'");
Console.WriteLine("   - Runtime decides which version runs based on actual object");
Console.WriteLine("   - Get 4 completely different outputs from 1 method call\n");

// ============================================================================
// SECTION 3: BookService - Polymorphism Solves Tight Coupling
// ============================================================================

Console.WriteLine("═══════════════════════════════════════════════════════════════");
Console.WriteLine("📌 SECTION 3: BookService Using Polymorphism\n");

Console.WriteLine("EmailNotifier version:");
var emailBookService = new BookService(new EmailNotifier());
emailBookService.BorrowBook(1);
emailBookService.ReturnBook(1);
emailBookService.AddReview(1, 5);

Console.WriteLine("\nSmsNotifier version (same BookService code, different notifier):");
var smsBookService = new BookService(new SmsNotifier());
smsBookService.BorrowBook(2);
smsBookService.ReturnBook(2);

Console.WriteLine("\nSlackNotifier version (same BookService code, different notifier):");
var slackBookService = new BookService(new SlackNotifier());
slackBookService.BorrowBook(3);

Console.WriteLine("\n✅ INSIGHT: BookService never changes");
Console.WriteLine("   - No if/else checking notification type");
Console.WriteLine("   - No knowledge of EmailNotifier, SmsNotifier, PushNotifier");
Console.WriteLine("   - Just knows BaseNotifier (the shape)");
Console.WriteLine("   - Tomorrow add WhatsAppNotifier? BookService never changes!\n");

// ============================================================================
// SECTION 4: Adding New Forms Without Touching Existing Code
// ============================================================================

Console.WriteLine("═══════════════════════════════════════════════════════════════");
Console.WriteLine("📌 SECTION 4: Extend System With New Notifier Type\n");

Console.WriteLine("New TelegramNotifier added - watch it work with existing BookService:");
var telegramBookService = new BookService(new TelegramNotifier());
telegramBookService.BorrowBook(4);

Console.WriteLine("\n✅ POLYMORPHISM POWER:");
Console.WriteLine("   - Created TelegramNotifier");
Console.WriteLine("   - Never touched BookService");
Console.WriteLine("   - BookService automatically works with TelegramNotifier");
Console.WriteLine("   - This is Open/Closed Principle: Open for extension, closed for modification\n");

// ============================================================================
// SECTION 5: Multiple Services Using Same Polymorphic Shape
// ============================================================================

Console.WriteLine("═══════════════════════════════════════════════════════════════");
Console.WriteLine("📌 SECTION 5: Multiple Services, One Polymorphic Shape\n");

Console.WriteLine("All three services use same notifier polymorphically:\n");

var emailNotif = new EmailNotifier();
var bookSvc = new BookService(emailNotif);
var memberSvc = new MemberService(emailNotif);
var reviewSvc = new ReviewService(emailNotif);

bookSvc.BorrowBook(5);
memberSvc.RegisterMember("John Doe");
reviewSvc.AddReview("Amazing book!", 5);

Console.WriteLine("\n✅ POLYMORPHISM ELEGANCE:");
Console.WriteLine("   - BookService, MemberService, ReviewService all independent");
Console.WriteLine("   - All use BaseNotifier the same way");
Console.WriteLine("   - Change notification backend? Swap notifier once, all services adapt");
Console.WriteLine("   - If services need different notifiers: create specific instances\n");

// ============================================================================
// SECTION 6: Breaking It - Understanding The Rules
// ============================================================================

Console.WriteLine("═══════════════════════════════════════════════════════════════");
Console.WriteLine("🔨 SECTION 6: Breaking It - Understand The Rules\n");

Console.WriteLine("1️⃣  Try: Call Send() through parent type (polymorphism works)");
BaseNotifier testNotifier = new EmailNotifier();
testNotifier.Send("Test message");
Console.WriteLine("   ✅ Polymorphism: Parent type runs child's implementation\n");

Console.WriteLine("2️⃣  Call Send() through child type directly (still works)");
EmailNotifier directNotifier = new EmailNotifier();
directNotifier.Send("Direct call");
Console.WriteLine("   ✅ Works but NOT polymorphism - locked to EmailNotifier\n");

Console.WriteLine("3️⃣  What happens with a list of mixed types?");
var mixedNotifiers = new List<BaseNotifier>
{
    new EmailNotifier(),
    new SmsNotifier(),
    new PushNotifier()
};

Console.WriteLine("   Calling Send() on each:");
foreach (var n in mixedNotifiers)
{
    n.Send("Message");
}
Console.WriteLine("   ✅ Different outputs - same method call - runtime decides\n");

Console.WriteLine("4️⃣  Try: Assign parent instance to child variable (fails)");
Console.WriteLine("   Code: EmailNotifier bad = new BaseNotifier();");
Console.WriteLine("   💥 Result: Compiler error!");
Console.WriteLine("   Why: Parent might not have all child's properties/methods\n");

Console.WriteLine("5️⃣  Remove virtual from parent - what runs?");

Console.WriteLine("   Code: BrokenBaseNotifier notifier = new BrokenChildNotifier();");
BrokenBaseNotifier brokenTest = new BrokenChildNotifier();
brokenTest.Send("test");
Console.WriteLine("   Result: Parent's version runs (child can't override without virtual)\n");

Console.WriteLine("6️⃣  Remove override from child with virtual parent - what runs?");

Console.WriteLine("   Code: GoodBaseNotifier notifier = new HiddenChildNotifier();");
GoodBaseNotifier hiddenTest = new HiddenChildNotifier();
hiddenTest.Send("test");
Console.WriteLine("   Result: Parent's version runs (child's method is hidden, not overriding)\n");

// ============================================================================
// SECTION 7: The Connection - How Three Pillars Work Together
// ============================================================================

Console.WriteLine("═══════════════════════════════════════════════════════════════");
Console.WriteLine("🔗 SECTION 7: How The Three Pillars Connect\n");

Console.WriteLine("ENCAPSULATION (Floor 2 Pillar 1):");
Console.WriteLine("  → States are protected, controlled through methods");
Console.WriteLine("  → Example: Book has private _isAvailable, controlled via Borrow()/Return()\n");

Console.WriteLine("INHERITANCE (Floor 2 Pillar 2):");
Console.WriteLine("  → Parent classes share common code with children");
Console.WriteLine("  → Example: BaseService, BookService, MemberService");
Console.WriteLine("  → Eliminates code duplication\n");

Console.WriteLine("POLYMORPHISM (Floor 2 Pillar 3):");
Console.WriteLine("  → One shape, many forms, runtime decides behavior");
Console.WriteLine("  → Example: BaseNotifier with Email/SMS/Push/Slack/Telegram forms");
Console.WriteLine("  → Services know SHAPE but not specific types");
Console.WriteLine("  → No if/else chains, no tight coupling\n");

Console.WriteLine("TOGETHER:");
Console.WriteLine("  → Encapsulation + Inheritance + Polymorphism");
Console.WriteLine("  → Protected state (encapsulation)");
Console.WriteLine("  → Shared code (inheritance)");
Console.WriteLine("  → Flexible behavior (polymorphism)");
Console.WriteLine("  → One connected system 🎯\n");

// ============================================================================
// FINAL SUMMARY
// ============================================================================

Console.WriteLine("═══════════════════════════════════════════════════════════════");
Console.WriteLine("✅ POLYMORPHISM COMPLETE");
Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
Console.WriteLine("KEY RULES:");
Console.WriteLine("  ✅ virtual  → Parent allows this to be overridden");
Console.WriteLine("  ✅ override → Child officially replaces parent's implementation");
Console.WriteLine("  ✅ Parent type = Child instance → Runtime polymorphism kicks in");
Console.WriteLine("  ✅ Same method, different behaviors → decided at runtime");
Console.WriteLine("");
Console.WriteLine("THE POWER:");
Console.WriteLine("  ✅ Services work with SHAPES not specific types");
Console.WriteLine("  ✅ Add new forms without changing existing code");
Console.WriteLine("  ✅ No if/else chains");
Console.WriteLine("  ✅ Loose coupling");
Console.WriteLine("  ✅ Open/Closed Principle (open for extension, closed for modification)");
Console.WriteLine("═══════════════════════════════════════════════════════════════");
