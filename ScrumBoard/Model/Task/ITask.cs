namespace ScrumBoard.Model.Task
{
    public interface ITask
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskPriority Priority { get; set; }
    }
}
