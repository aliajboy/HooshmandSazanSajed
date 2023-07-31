namespace Hooshmand.Models;

public class Project : GlobalProperties
{
    public string Title { get; set; }
    public string Description { get; set; }

    // Relations
    public Customer Customer { get; set; }
    public int CustomerId { get; set; }
}
