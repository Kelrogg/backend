using ScrumBoard.Exception;
using ScrumBoard.Model.Task;
using System.Collections.Generic;

namespace ScrumBoard.Model.Column
{
    internal class Column : IColumn
    {
        public string Title { get; set; }

        public Column(string title)
        {
            Title = title;
            _tasks = new List<ITask>();
        }

        public void AddTask(ITask task)
        {
            if (FindTaskByTitle(task.Title) != null)
            {
                throw new TaskAlreadyExistsException();
            }

            _tasks.Add(task);
        }

        public IReadOnlyCollection<ITask> FindAllTasks()
        {
            return _tasks;
        }

        public ITask? FindTaskByTitle(string title)
        {
            return _tasks.Find(task => task.Title == title);
        }

        public void RemoveTaskByTitle(string title)
        {
            _tasks.RemoveAll(task => task.Title == title);
        }

        private List<ITask> _tasks;
    }
}
