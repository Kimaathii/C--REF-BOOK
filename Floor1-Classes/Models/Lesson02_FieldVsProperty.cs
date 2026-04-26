namespace Floor1_Classes.Models;

/// <summary>
/// LESSON: Field vs Property
/// =========================
/// A field is raw storage.
/// A property is a controlled gateway with logic.
/// 
/// You can't add get/set logic to a field.
/// You can only add it to a property.
/// </summary>

public class FieldVsPropertyDemo
{
    // ============================================================
    // ❌ THE PROBLEM: Raw field with no control
    // ============================================================
    public class UserWithField
    {
        public string Email;  // totally exposed, no control whatsoever

        // 💀 You CANNOT do this on a field:
        // public string Email
        // {
        //     get { return Email.ToLower(); }  // COMPILER ERROR
        // }
        // Fields don't have get/set accessors.
    }

    // ============================================================
    // ✅ SOLUTION 1: Property with custom logic
    // ============================================================
    public class UserWithProperty
    {
        private string _email;  // raw storage (field)

        // Controlled gateway (property)
        public string Email
        {
            get { return _email?.ToLower() ?? "no-email"; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Email cannot be empty");

                if (!value.Contains("@"))
                    throw new ArgumentException("Email must contain @");

                _email = value;
            }
        }

        public bool EmailIsValid() => !string.IsNullOrEmpty(_email) && _email.Contains("@");
    }

    // ============================================================
    // ✅ SOLUTION 2: Auto-property (shorthand for simple cases)
    // ============================================================
    public class UserWithAutoProperty
    {
        // C# compiler creates the private field behind the scenes
        // You don't see _email, but it exists
        public string Email { get; set; }

        // Private setter: anyone can READ, only this class can WRITE
        public string DisplayName { get; private set; }

        public void UpdateDisplayName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Display name cannot be empty");

            DisplayName = newName;  // only legal place to set it
        }
    }

    // ============================================================
    // KEY DIFFERENCE
    // ============================================================
    // Field:    raw storage only. NO get/set logic possible.
    // Property: storage + controlled access. get/set logic welcome.
    //
    // Use field when: you need raw storage for internal use
    // Use property when: you need to expose data or add validation
}
