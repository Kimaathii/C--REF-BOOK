using Floor2_OOP.Inheritance.Models;

Console.WriteLine("═══════════════════════════════════════════════════════════════");
Console.WriteLine("FLOOR 2: PILLAR 2 — INHERITANCE");
Console.WriteLine("Reuse code · Extend behavior · Eliminate duplication");
Console.WriteLine("═══════════════════════════════════════════════════════════════\n");

// Create services - both inherit from BaseService automatically
var bookService = new BookService();
var memberService = new MemberService();

// ─────────────────────────────────────────────────────────────────────────
// PART 1: Adding Books (BookService)
// ─────────────────────────────────────────────────────────────────────────
Console.WriteLine("📚 ADDING BOOKS (notice how log output differs from members):\n");

bookService.AddBook("Clean Code", "Robert C. Martin");
bookService.AddBook("The Pragmatic Programmer", "David Thomas & Andrew Hunt");
bookService.AddBook("C# In Depth", "Jon Skeet");

bookService.PrintAllBooks();

// ─────────────────────────────────────────────────────────────────────────
// PART 2: Book Operations
// ─────────────────────────────────────────────────────────────────────────
Console.WriteLine("📖 BORROWING AND RETURNING BOOKS:\n");

bookService.BorrowBook(1);
bookService.PrintAllBooks();

bookService.ReturnBook(1);
bookService.PrintAllBooks();

// ─────────────────────────────────────────────────────────────────────────
// PART 3: Registering Members  (MemberService)
// ─────────────────────────────────────────────────────────────────────────
Console.WriteLine("\n👥 REGISTERING MEMBERS (notice same logging style as parent):\n");

memberService.RegisterMember("John Doe", "john@example.com");
memberService.RegisterMember("Jane Smith", "jane@example.com");
memberService.RegisterAdmin("Super Admin", "admin@example.com");

memberService.PrintAllMembers();

// ─────────────────────────────────────────────────────────────────────────
// PART 4: Member Operations
// ─────────────────────────────────────────────────────────────────────────
Console.WriteLine("🔧 MANAGING MEMBERS:\n");

memberService.AssignRole(1, "Moderator");
memberService.PrintAllMembers();

memberService.UpdateMemberEmail(2, "jane.new@example.com");
memberService.PrintAllMembers();

// ─────────────────────────────────────────────────────────────────────────
// PART 5: Activity Logs - Available to Both Services (from BaseService)
// ─────────────────────────────────────────────────────────────────────────
Console.WriteLine("📋 ACTIVITY LOGS (inherited from BaseService):\n");

bookService.PrintAllLogs();
memberService.PrintAllLogs();

// ─────────────────────────────────────────────────────────────────────────
// PART 6: Breaking Things - Constructivist Challenges
// ─────────────────────────────────────────────────────────────────────────
Console.WriteLine("\n🔨 BREAKING IT — Inherited Encapsulation & Validation:\n");

// Challenge 1: Try to corrupt Book state
Console.WriteLine("1️⃣  Try: book.IsAvailable = false (breaks encapsulation)");
try
{
    var book = new Book("Test", "Author");
    // book.IsAvailable = false;  // 💥 COMPILER ERROR - no setter
    Console.WriteLine("   ✅ Prevented (property has no setter)\n");
}
catch (Exception ex)
{
    Console.WriteLine($"   Caught: {ex.Message}\n");
}

// Challenge 2: Try invalid book operations
Console.WriteLine("2️⃣  Try: bookService.BorrowBook(999) (nonexistent book)");
try
{
    bookService.BorrowBook(999);
    Console.WriteLine("   ❌ BUG: Borrowed nonexistent book!");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"   ✅ Caught: {ex.Message}");
}

// Challenge 3: Try to borrow twice
Console.WriteLine("\n3️⃣  Try: bookService.BorrowBook(1) twice");
try
{
    bookService.BorrowBook(1);
    Console.WriteLine("   ❌ BUG: Borrowed same book twice!");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"   ✅ Caught: {ex.Message}");
}

// Challenge 4: Invalid member role
Console.WriteLine("\n4️⃣  Try: memberService.RegisterMember with invalid role");
try
{
    var invalidMember = new Member("Test", "test@example.com", "SuperUser");
    Console.WriteLine("   ❌ BUG: Invalid role accepted!");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"   ✅ Caught: {ex.Message}");
}

// Challenge 5: Member email validation
Console.WriteLine("\n5️⃣  Try: memberService.RegisterMember with invalid email");
try
{
    var invalidEmail = new Member("Test", "notanemail", "Member");
    Console.WriteLine("   ❌ BUG: Invalid email accepted!");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"   ✅ Caught: {ex.Message}");
}

// Challenge 6: Protected field access
Console.WriteLine("\n6️⃣  Try: bookService._activityLog.Clear() (access protected field)");
// bookService._activityLog.Clear();  // 💥 COMPILER ERROR - protected, not accessible
Console.WriteLine("   ✅ Prevented (field is protected, not public)\n");

Console.WriteLine("\n═══════════════════════════════════════════════════════════════");
Console.WriteLine("✅ INHERITANCE COMPLETE");
Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
Console.WriteLine("Both BookService and MemberService:");
Console.WriteLine("  ✅ Inherit _activityLog, _serviceName from BaseService");
Console.WriteLine("  ✅ Inherit LogActivity() and PrintAllLogs()");
Console.WriteLine("  ✅ BookService overrides LogActivity() with custom behavior");
Console.WriteLine("  ✅ MemberService uses parent's LogActivity() as-is");
Console.WriteLine("  ✅ No code duplication");
Console.WriteLine("  ✅ Both work together seamlessly");
Console.WriteLine("═══════════════════════════════════════════════════════════════\n");
