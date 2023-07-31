namespace Hooshmand.Models;

public class Customer : GlobalProperties
{
    public PhoneBooks PhoneBooks { get; set; }
    public List<Project> Projects { get; set; } = new List<Project>();
}
