namespace Hooshmand.Models;

public class Project : GlobalProperties
{
    public string Title { get; set; }
    public string Description { get; set; }

    // Relations
    public PhoneBooks PhoneBooks { get; set; }
    public int PhoneBookId { get; set; }
}
