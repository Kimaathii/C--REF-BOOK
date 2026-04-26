namespace Floor1_Classes.Models;

/// <summary>
/// LESSON: Underscore Convention
/// ============================
/// The underscore (_) is NOT a C# rule. It's a convention.
/// But it's a powerful one that prevents real bugs.
/// 
/// Let's see what happens WITHOUT it, then WITH it.
/// </summary>

public class UnderscoreConventionDemo
{
    // ============================================================
    // ❌ THE PROBLEM: Name confusion without underscore
    // ============================================================
    public class AuthServiceBROKEN
    {
        private string passwordHash;  // no underscore

        public void SetPasswordHashWrong(string passwordHash)  // same name!
        {
            // 💀 CRITICAL BUG: Which passwordHash is this?
            passwordHash = passwordHash;  // Parameter assigned to itself!
            // The field NEVER gets set. It's still empty.
            // This silently compiles and silently breaks at runtime.
        }
    }

    // ============================================================
    // ✅ THE SOLUTION: Underscore makes intent crystal clear
    // ============================================================
    public class AuthServiceFIXED
    {
        private string _passwordHash;  // 👈 underscore = "I'm a field"

        public void SetPasswordHashRight(string passwordHash)  // parameter, no underscore
        {
            // ✅ CRYSTAL CLEAR: which is which?
            _passwordHash = passwordHash;
            // LEFT (_passwordHash) = private field
            // RIGHT (passwordHash) = parameter
            // Zero confusion. Zero bugs.
        }
    }
}
