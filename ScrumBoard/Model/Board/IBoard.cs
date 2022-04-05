using ScrumBoard.Model.Task;
using ScrumBoard.Model.Column;
using System.Collections.Generic;

namespace ScrumBoard.Model.Board
{
    public interface IBoard
    {
        public string Title { get; set; }

        public void AddColumn(IColumn column);
        public void ChangeColumnTitle(string columnTitle, string newTitle);
        public IReadOnlyCollection<IColumn> FindAllColumns();
        public IColumn? FindColumnByTitle(string title);

        public void AddTaskToColumn(ITask task, string? columnTitle = null);
        public void ChangeTaskTitle(string columnTitle, string taskTitle, string newTitle);
        public void ChangeTaskDescription(string columnTitle, string taskTitle, string newDescription);
        public void ChangeTaskPriority(string columnTitle, string taskTitle, TaskPriority newPriority);
        public void AdvanceTask(string columnTitle, string taskTitle);
        public void RemoveTask(string columnTitle, string taskTitle);
    }
}
