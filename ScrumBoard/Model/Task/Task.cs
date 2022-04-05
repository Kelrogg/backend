namespace ScrumBoard.Model.Task
{
    internal class Task : ITask
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskPriority Priority { get; set; }

        public Task(string title, string description, TaskPriority priority)
        {
            Title = title;
            Description = description;
            Priority = priority;
        }
    }
}
