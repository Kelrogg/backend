using ScrumBoard.Model.Task;
using System.Collections.Generic;

namespace ScrumBoard.Model.Column
{
    public interface IColumn
    {
        public string Title { get; set; }

        public void AddTask(ITask task);
        public IReadOnlyCollection<ITask> FindAllTasks();
        public ITask? FindTaskByTitle(string title);
        public void RemoveTaskByTitle(string title);
    }
}
