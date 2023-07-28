namespace Hooshmand.Models;

public class UserTask : GlobalProperties
{
    public string Title { get; set; }
    public string Description { get; set; }
    public ApplicationUser SenderUser { get; set; }
    public ApplicationUser ReceiverUser { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsTimePassed { get; set; } = false;
    public bool IsHghPriority { get; set; } = false;
    public bool IsFinished { get; set; } = false;
    public List<SmallTask> SmallTasks { get; set; } = new List<SmallTask>();
    public List<SubTask> SubTasks { get; set; } = new List<SubTask>();
}

public class SmallTask : GlobalProperties
{
    public string Title { get; set; }
    public float Progress { get; set; }

    // Relations
    public int UserTaskId { get; set; }
    public UserTask UserTask { get; set; }
}

public class SubTask : UserTask
{

    // Relations
    public int UserTaskId { get; set; }
    public UserTask UserTask { get; set; }
}