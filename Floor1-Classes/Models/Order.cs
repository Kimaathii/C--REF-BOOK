namespace Floor1_Classes.Models;

/// <summary>
/// Option 2: Private Setter (Most Common in Real Apps)
/// ====================================================
/// Best for: Data that everyone can READ, but only this class can WRITE
/// Pattern: public getter + private setter + controlled methods
/// 
/// The outside world can see the state, but can only change it through
/// methods that enforce business rules.
/// </summary>
public class Order
{
    // Status can be read by anyone, but only set by methods in this class
    public string Status { get; private set; } = "Pending";

    // When the order was placed (read-only, set once at creation)
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    // When it was completed (read-only, set when Completed() is called)
    public DateTime? CompletedAt { get; private set; }

    // Items in the order (can be read, modified through methods)
    public List<string> Items { get; private set; } = new();

    /// <summary>
    /// The ONLY way to add items to this order.
    /// Enforces: order must still be pending, item cannot be empty.
    /// </summary>
    public void AddItem(string itemName)
    {
        if (Status != "Pending")
            throw new InvalidOperationException($"Cannot add items to a {Status} order");

        if (string.IsNullOrWhiteSpace(itemName))
            throw new ArgumentException("Item name cannot be empty");

        Items.Add(itemName.Trim());
    }

    /// <summary>
    /// The ONLY way to complete an order.
    /// Enforces: order must be pending, must have items, automatically sets timestamp.
    /// </summary>
    public void Complete()
    {
        if (Status != "Pending")
            throw new InvalidOperationException($"Cannot complete a {Status} order");

        if (Items.Count == 0)
            throw new InvalidOperationException("Cannot complete an order with no items");

        Status = "Completed";
        CompletedAt = DateTime.UtcNow;  // ✅ Automatically tracked, you don't forget!
    }

    /// <summary>
    /// The ONLY way to cancel an order.
    /// Enforces: cannot cancel if already completed.
    /// </summary>
    public void Cancel()
    {
        if (Status == "Completed")
            throw new InvalidOperationException("Cannot cancel a completed order");

        Status = "Cancelled";
    }
}
