using Floor1_Classes.Models;

Console.WriteLine("═══════════════════════════════════════════════════════════════");
Console.WriteLine("FLOOR 1: FIVE CRITICAL CONCEPTS YOU MUST OWN");
Console.WriteLine("═══════════════════════════════════════════════════════════════");

DemoBankAccount();
DemoThisKeyword();
DemoStaticMembers();

// ==============================================================================
// LESSON 1: UNDERSCORE, FIELD vs PROPERTY, READONLY
// ==============================================================================
void DemoBankAccount()
{
    Console.WriteLine("\n\n🔴 LESSON 1: Underscore · Field vs Property · readonly");
    Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

    var account = new BankAccount("ACC-2024-001", "John Doe", 1000m);
    Console.WriteLine("✅ Account created");
    account.PrintStatement();

    Console.WriteLine("\n🔨 Making deposits and withdrawals...");
    account.Deposit(500m);
    account.Withdraw(300m);
    account.PrintStatement();

    Console.WriteLine("\n🔨 Trying to break it (should all fail safely):");
    try { account.Withdraw(50000m); }
    catch (InvalidOperationException ex) { Console.WriteLine($"✅ {ex.Message}"); }

    try { account.Deposit(-100m); }
    catch (ArgumentException ex) { Console.WriteLine($"✅ {ex.Message}"); }

    Console.WriteLine("\n💡 KEY CONCEPTS:");
    Console.WriteLine("   • _accountNumber = underscore means 'private field'");
    Console.WriteLine("   • Balance property = controlled access to data");
    Console.WriteLine("   • readonly = set once, never changes");
}

// ==============================================================================
// LESSON 2: THIS KEYWORD - Disambiguation & Constructor Chaining
// ==============================================================================
void DemoThisKeyword()
{
    Console.WriteLine("\n\n🟡 LESSON 2: this Keyword");
    Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

    // Demo 1: Constructor chaining - avoiding duplication
    Console.WriteLine("\n📚 Creating Library Books (with constructor chaining):");
    
    // Full constructor with all details
    var book1 = new LibraryBook("Clean Code", "Robert C. Martin", true, "Programming");
    Console.WriteLine($"✅ Book 1: {book1.Title} by {book1.Author}");
    Console.WriteLine($"   Genre: {book1.Genre}, Available: {book1.IsAvailable}");

    // Convenience constructor (chains to full constructor with defaults)
    var book2 = new LibraryBook("The Pragmatic Programmer", "Hunt & Thomas");
    Console.WriteLine($"✅ Book 2: {book2.Title} by {book2.Author}");
    Console.WriteLine($"   Genre: {book2.Genre}, Available: {book2.IsAvailable}");
    Console.WriteLine($"   (defaults applied via constructor chaining)");

    // Another convenience constructor
    var book3 = new LibraryBook("Design Patterns", "Gang of Four", "Software Design");
    Console.WriteLine($"✅ Book 3: {book3.Title}");
    Console.WriteLine($"   Genre: {book3.Genre}");

    // Demo 2: User registration with this
    Console.WriteLine("\n👤 Creating Users (with this disambiguation):");
    
    var user1 = new UserWithConstructorChaining("john@example.com", "johndoe");
    Console.WriteLine($"✅ User 1: {user1.Username} ({user1.Email})");
    Console.WriteLine($"   Verified: {user1.IsEmailVerified}");

    var user2 = new UserWithConstructorChaining(101, "admin@example.com", "admin");
    Console.WriteLine($"✅ User 2: {user2.Username} ({user2.Email})");
    Console.WriteLine($"   Verified: {user2.IsEmailVerified}");

    Console.WriteLine("\n💡 KEY CONCEPTS:");
    Console.WriteLine("   • : this(...) chains to another constructor");
    Console.WriteLine("   • Eliminates duplicated code");
    Console.WriteLine("   • Only master constructor updates when adding properties");
    Console.WriteLine("   • this.Property disambiguates when names collide");
}

// ==============================================================================
// LESSON 3: STATIC MEMBERS - Class level vs Instance level
// ==============================================================================
void DemoStaticMembers()
{
    Console.WriteLine("\n\n🟢 LESSON 3: static Keyword");
    Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

    // Demo 1: Static methods (no object needed)
    Console.WriteLine("\n🔐 Using static PasswordHelper (no object needed):");
    string password = "MySecurePass123!";
    
    bool isStrong = PasswordHelper.IsStrong(password);
    Console.WriteLine($"✅ Is '{password}' strong? {isStrong}");

    string hash = PasswordHelper.Hash(password);
    Console.WriteLine($"✅ Hashed: {hash.Substring(0, 20)}...");

    bool verified = PasswordHelper.Verify(password, hash);
    Console.WriteLine($"✅ Verify original password: {verified}");

    bool verified2 = PasswordHelper.Verify("WrongPassword", hash);
    Console.WriteLine($"✅ Verify wrong password: {verified2}");

    // Demo 2: Static vs Instance members mixed in one class
    Console.WriteLine("\n📕 Book Class (mixing instance and static):");
    
    Console.WriteLine($"Books created so far: {Book.TotalBooksCreated}");

    var book1 = new Book("Clean Code", "Robert Martin");
    var book2 = new Book("Clean Architecture", "Robert Martin");
    var book3 = new Book("The Pragmatic Programmer", "Hunt & Thomas");

    Console.WriteLine($"✅ Created 3 books");
    Console.WriteLine($"   Total books CREATED (static): {Book.TotalBooksCreated}");
    Console.WriteLine($"   book1 available (instance): {book1.IsAvailable}");
    Console.WriteLine($"   book2 available (instance): {book2.IsAvailable}");

    book1.Borrow();
    Console.WriteLine($"✅ Borrowed book1");
    Console.WriteLine($"   book1 available now: {book1.IsAvailable}");
    Console.WriteLine($"   book2 available (unchanged): {book2.IsAvailable}");

    Console.WriteLine($"\n   Total books CREATED (still the same): {Book.TotalBooksCreated}");

    // Demo 3: Static constants
    Console.WriteLine("\n⚙️  Using static Constants:");
    Console.WriteLine($"   App Name: {AppConstants.AppName}");
    Console.WriteLine($"   App Version: {AppConstants.AppVersion}");
    Console.WriteLine($"   Max Login Attempts: {AppConstants.MaxLoginAttempts}");

    // Demo 4: Extension methods (static methods on other types)
    Console.WriteLine("\n🧮 Extension Methods (static methods on built-in types):");
    int num = 5;
    Console.WriteLine($"   {num}.Square() = {num.Square()}");
    Console.WriteLine($"   {num}.IsEven() = {num.IsEven()}");
    Console.WriteLine($"   {num}.IsOdd() = {num.IsOdd()}");

    // Demo 5: Logging with static class
    Console.WriteLine("\n📝 Using static Logger:");
    Logger.Log("Application started");
    Logger.Log("User logged in");
    Logger.Log("Document saved");
    Console.WriteLine($"   Total logs: {Logger.LogCount}");

    // Demo 6: Events with static counters
    Console.WriteLine("\n🎪 Events (static system counter + instance data):");
    var evt1 = new Event("Tech Conference", DateTime.UtcNow.AddDays(7));
    var evt2 = new Event("Meetup", DateTime.UtcNow.AddDays(14));

    evt1.AddAttendee();
    evt1.AddAttendee();
    evt2.AddAttendee();

    Console.WriteLine($"✅ {evt1}");
    Console.WriteLine($"✅ {evt2}");
    Console.WriteLine($"   Total events (static): {Event.TotalEvents}");
    Console.WriteLine($"   System uptime: {Event.SystemUptime.TotalSeconds:F1} seconds");

    Console.WriteLine("\n💡 KEY CONCEPTS:");
    Console.WriteLine("   • static methods: no object needed, call on class");
    Console.WriteLine("   • Instance members: each object has its own copy");
    Console.WriteLine("   • Static members: shared across all objects of the class");
    Console.WriteLine("   • Extension methods: add methods to existing types");
    Console.WriteLine("   • static class: everything inside must be static");
}

Console.WriteLine("\n\n═══════════════════════════════════════════════════════════════");
Console.WriteLine("✅ FLOOR 1 COMPLETE: You now own these five concepts");
Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
Console.WriteLine("1. Underscore convention (_fieldName)");
Console.WriteLine("2. Field vs Property (storage vs controlled access)");
Console.WriteLine("3. readonly (set once, never changes)");
Console.WriteLine("4. this keyword (disambiguation + constructor chaining)");
Console.WriteLine("5. static keyword (class-level members, no object needed)");
Console.WriteLine("═══════════════════════════════════════════════════════════════\n");

Console.WriteLine("\n\n═══════════════════════════════════════════════════════");
Console.WriteLine("✅ DEMONSTRATION COMPLETE");
Console.WriteLine("Data is protected. Class owns it completely.");
Console.WriteLine("═══════════════════════════════════════════════════════");

