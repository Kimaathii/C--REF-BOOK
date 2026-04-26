namespace Floor2_OOP.Inheritance.Models;

/// <summary>
/// MemberService - inherits from BaseService
/// Does NOT override LogActivity - uses parent's version as-is
/// Demonstrates that children can choose to override or not
/// </summary>
public class MemberService : BaseService  // 👈 inherits from BaseService
{
    private readonly List<Member> _members;

    public MemberService()
        : base("MemberService")  // : base calls parent constructor
    {
        _members = new List<Member>();
    }

    // MemberService does NOT override LogActivity
    // It uses parent's version exactly as written in BaseService

    public void RegisterMember(string name, string email)
    {
        var member = new Member(name, email);  // uses convenience constructor
        _members.Add(member);
        LogActivity($"Member registered: {member.Name}");  // parent's version
    }

    public void RegisterAdmin(string name, string email)
    {
        var admin = new Member(name, email, "Admin");  // uses master constructor
        _members.Add(admin);
        LogActivity($"Admin registered: {admin.Name}");
    }

    public void AssignRole(int memberId, string role)
    {
        var member = FindMember(memberId);
        member.AssignRole(role);  // encapsulation - Member validates role
        LogActivity($"Role '{role}' assigned to {member.Name}");
    }

    public void UpdateMemberEmail(int memberId, string newEmail)
    {
        var member = FindMember(memberId);
        member.UpdateEmail(newEmail);  // encapsulation - Member validates email
        LogActivity($"Email updated for {member.Name}");
    }

    public void PrintAllMembers()
    {
        Console.WriteLine("\n--- All Members ---");

        if (_members.Count == 0)
        {
            Console.WriteLine("  No members registered");
            return;
        }

        foreach (var member in _members)
            Console.WriteLine($"  {member}");

        Console.WriteLine("-------------------\n");
    }

    private Member FindMember(int memberId)
    {
        var member = _members.FirstOrDefault(m => m.Id == memberId);

        if (member == null)
            throw new InvalidOperationException($"Member with id {memberId} not found");

        return member;
    }
}
