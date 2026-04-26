using Floor2_OOP.Encapsulation.Models;

Console.WriteLine("═══════════════════════════════════════════════════════════════");
Console.WriteLine("FLOOR 2: PILLAR 1 — ENCAPSULATION");
Console.WriteLine("Bundle data + behavior · Hide internals · Protect state");
Console.WriteLine("═══════════════════════════════════════════════════════════════\n");

DemoBankAccount();
DemoBook();
DemoUser();

// ==============================================================================
// DEMO 1: BankAccount — Data Protection Through Encapsulation
// ==============================================================================
void DemoBankAccount()
{
    Console.WriteLine("\n🔴 LESSON 1: BankAccount Encapsulation");
    Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

    var account = new BankAccount("John Doe", "ACC-2024-001", 1000m);
    Console.WriteLine($"✅ Account created: {account}");

    Console.WriteLine("\n🔨 Valid operations:");
    account.Deposit(500m);
    Console.WriteLine($"✅ Deposited $500: {account}");

    account.Withdraw(200m);
    Console.WriteLine($"✅ Withdrew $200: {account}");

    Console.WriteLine("\n🔨 Trying to break it (should all fail safely):");

    // Try 1: Set balance directly
    Console.WriteLine("\n1️⃣  Try: account._balance = -999999");
    // account._balance = -999999;  // 💥 COMPILER ERROR
    Console.WriteLine("   ✅ Prevented (private field)");

    // Try 2: Use property setter
    Console.WriteLine("\n2️⃣  Try: account.Balance = 500");
    // account.Balance = 500;  // 💥 COMPILER ERROR - no setter
    Console.WriteLine("   ✅ Prevented (read-only property)");

    // Try 3: Withdraw more than balance
    Console.WriteLine("\n3️⃣  Try: account.Withdraw(999999)");
    try
    {
        account.Withdraw(999999m);
        Console.WriteLine("   ❌ BUG: Withdrew more than balance!");
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"   ✅ Caught: {ex.Message}");
    }

    // Try 4: Deposit negative amount
    Console.WriteLine("\n4️⃣  Try: account.Deposit(-100)");
    try
    {
        account.Deposit(-100m);
        Console.WriteLine("   ❌ BUG: Negative deposit allowed!");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"   ✅ Caught: {ex.Message}");
    }

    Console.WriteLine("\n💡 ENCAPSULATION IN ACTION:");
    Console.WriteLine("   ✅ Private fields: _balance is protected");
    Console.WriteLine("   ✅ Read-only properties: Balance cannot be set directly");
    Console.WriteLine("   ✅ Public gateways: Deposit() and Withdraw() enforce rules");
    Console.WriteLine("   ✅ Result: Account is SELF-PROTECTING");
}

// ==============================================================================
// DEMO 2: Book — State Transitions Through Methods
// ==============================================================================
void DemoBook()
{
    Console.WriteLine("\n\n🟡 LESSON 2: Book Encapsulation");
    Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

    var book = new Book(1, "Clean Code", "Robert C. Martin");
    Console.WriteLine($"✅ Book created: {book}");

    Console.WriteLine("\n🔨 Valid state transitions:");
    book.Borrow();
    Console.WriteLine($"✅ Book borrowed: {book}");

    book.Return();
    Console.WriteLine($"✅ Book returned: {book}");

    book.Borrow();
    Console.WriteLine($"✅ Book borrowed again: {book}");

    Console.WriteLine("\n🔨 Trying invalid transitions:");

    // Try 1: Borrow when already borrowed
    Console.WriteLine("\n1️⃣  Try: book.Borrow() (already borrowed)");
    try
    {
        book.Borrow();
        Console.WriteLine("   ❌ BUG: Borrowed an already borrowed book!");
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"   ✅ Caught: {ex.Message}");
    }

    // Try 2: Return when not borrowed
    book.Return();
    Console.WriteLine("\n2️⃣  Book returned first");
    try
    {
        book.Return();
        Console.WriteLine("   ❌ BUG: Returned a non-borrowed book!");
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"   ✅ Caught: {ex.Message}");
    }

    // Try 3: Set IsAvailable directly
    Console.WriteLine("\n3️⃣  Try: book.IsAvailable = true");
    // book.IsAvailable = true;  // 💥 COMPILER ERROR
    Console.WriteLine("   ✅ Prevented (read-only property)");

    // Try 4: Corrupt the title
    Console.WriteLine("\n4️⃣  Try: book.Title = null");
    // book.Title = null;  // 💥 COMPILER ERROR
    Console.WriteLine("   ✅ Prevented (no setter on property)");

    Console.WriteLine("\n💡 ENCAPSULATION IN ACTION:");
    Console.WriteLine("   ✅ State tracked internally (_isAvailable)");
    Console.WriteLine("   ✅ Only valid transitions allowed (Borrow/Return)");
    Console.WriteLine("   ✅ Invalid state transitions throw errors");
    Console.WriteLine("   ✅ Book enforces its own business rules");
}

// ==============================================================================
// DEMO 3: User — Complex State Management
// ==============================================================================
void DemoUser()
{
    Console.WriteLine("\n\n🟢 LESSON 3: User Encapsulation");
    Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

    var user = new User(1, "john@example.com", "johndoe");
    Console.WriteLine($"✅ User created: {user}");

    Console.WriteLine("\n🔨 Valid state transitions:");
    Console.WriteLine($"   Initial: {user}");

    user.Suspend("Violating terms");
    Console.WriteLine($"✅ Suspended: {user}");
    Console.WriteLine($"   SuspendedAt: {user.SuspendedAt:G}");

    user.Reactivate();
    Console.WriteLine($"✅ Reactivated: {user}");

    user.Delete();
    Console.WriteLine($"✅ Deleted: {user}");

    Console.WriteLine("\n🔨 Trying invalid transitions:");

    // Try 1: Suspend an already suspended user
    var user2 = new User(2, "jane@example.com", "janedoe");
    user2.Suspend();
    Console.WriteLine("\n1️⃣  Try: user2.Suspend() (already suspended)");
    try
    {
        user2.Suspend();
        Console.WriteLine("   ❌ BUG: Suspended an already suspended user!");
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"   ✅ Caught: {ex.Message}");
    }

    // Try 2: Reactivate a non-suspended user
    var user3 = new User(3, "bob@example.com", "bobsmith");
    Console.WriteLine("\n2️⃣  Try: user3.Reactivate() (never suspended)");
    try
    {
        user3.Reactivate();
        Console.WriteLine("   ❌ BUG: Reactivated a never-suspended user!");
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"   ✅ Caught: {ex.Message}");
    }

    // Try 3: Reactivate a deleted user
    var user4 = new User(4, "alice@example.com", "alicegreen");
    user4.Delete();
    Console.WriteLine("\n3️⃣  Try: user4.Reactivate() (deleted)");
    try
    {
        user4.Reactivate();
        Console.WriteLine("   ❌ BUG: Reactivated a deleted user!");
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"   ✅ Caught: {ex.Message}");
    }

    // Try 4: Set Status directly
    Console.WriteLine("\n4️⃣  Try: user.Status = 'Admin'");
    // user.Status = "Admin";  // 💥 COMPILER ERROR
    Console.WriteLine("   ✅ Prevented (read-only property)");

    // Try 5: Delete a deleted user
    Console.WriteLine("\n5️⃣  Try: user.Delete() (already deleted)");
    try
    {
        user.Delete();
        Console.WriteLine("   ❌ BUG: Deleted an already deleted user!");
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"   ✅ Caught: {ex.Message}");
    }

    Console.WriteLine("\n💡 ENCAPSULATION IN ACTION:");
    Console.WriteLine("   ✅ Status is private (can't be set to invalid values)");
    Console.WriteLine("   ✅ Only valid transitions: Active → Suspended → Active → Deleted");
    Console.WriteLine("   ✅ Complex state logic lives IN the class");
    Console.WriteLine("   ✅ Outside code can't violate business rules");
}

Console.WriteLine("\n\n═══════════════════════════════════════════════════════════════");
Console.WriteLine("✅ ENCAPSULATION COMPLETE");
Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
Console.WriteLine("Classes bundle data + behavior");
Console.WriteLine("Encapsulation protects data from corruption");
Console.WriteLine("Business rules live INSIDE the class");
Console.WriteLine("Outside world can only interact through safe gateways");
Console.WriteLine("═══════════════════════════════════════════════════════════════\n");
