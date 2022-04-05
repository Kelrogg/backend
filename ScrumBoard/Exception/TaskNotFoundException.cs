namespace ScrumBoard.Exception
{
    public class TaskNotFoundException : System.Exception
    {
        public TaskNotFoundException()
            : base("task not found") {}
    }
}
