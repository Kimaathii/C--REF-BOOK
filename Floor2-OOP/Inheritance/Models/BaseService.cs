namespace Floor2_OOP.Inheritance.Models;

/// <summary>
/// BaseService — Parent class with shared functionality
/// All services inherit from this to avoid code duplication
/// </summary>
public class BaseService
{
    // protected = children can access, outside world cannot
    protected readonly List<string> _activityLog;
    protected readonly string _serviceName;

    public BaseService(string serviceName)
    {
        _serviceName = serviceName;
        _activityLog = new List<string>();
    }

    // virtual = children are ALLOWED to override this if they want
    public virtual void LogActivity(string message)
    {
        var log = $"[{_serviceName}] [{DateTime.Now:HH:mm:ss}] {message}";
        _activityLog.Add(log);
        Console.WriteLine(log);
    }

    // shared method - available to ALL children automatically
    public void PrintAllLogs()
    {
        Console.WriteLine($"\n--- {_serviceName} Activity Logs ---");

        if (_activityLog.Count == 0)
        {
            Console.WriteLine("  No activity yet");
            return;
        }

        foreach (var log in _activityLog)
            Console.WriteLine($"  {log}");

        Console.WriteLine("-----------------------------------\n");
    }
}
