namespace ScrumBoard.Exception
{
    public class TaskAlreadyExistsException : System.Exception
    {
        public TaskAlreadyExistsException()
            : base("task already exists") {}
    }
}
