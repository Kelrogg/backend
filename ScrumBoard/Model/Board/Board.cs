using ScrumBoard.Exception;
using ScrumBoard.Model.Task;
using ScrumBoard.Model.Column;
using System.Collections.Generic;

namespace ScrumBoard.Model.Board
{
    internal class Board : IBoard
    {
        public string Title { get; set; }

        public Board(string title)
        {
            Title = title;
            _columns = new List<IColumn>();
        }

        public void AddColumn(IColumn column)
        {
            if (_columns.Count == MAX_COLUMNS)
            {
                throw new ColumnCountExceededException();
            }

            if (FindColumnByTitle(column.Title) != null)
            {
                throw new ColumnAlreadyExistsException();
            }

            _columns.Add(column);
        }

        public IReadOnlyCollection<IColumn> FindAllColumns()
        {
            return _columns;
        }

        public IColumn? FindColumnByTitle(string title)
        {
            return _columns.Find(column => column.Title == title);
        }

        public void AdvanceTask(string columnTitle, string taskTitle)
        {
            if (_columns.Count == 0)
            {
                throw new NoColumnsException();
            }

            int columnIndex = _columns.FindIndex(elem => elem.Title == columnTitle);
            if (columnIndex == -1)
            {
                throw new ColumnNotFoundException();
            }

            ITask? task = _columns[columnIndex].FindTaskByTitle(taskTitle);
            if (task != null)
            {
                _columns[columnIndex].RemoveTaskByTitle(taskTitle);

                if (columnIndex != _columns.Count - 1)
                {
                    _columns[columnIndex + 1].AddTask(task);
                }
                return;
            }

            throw new TaskNotFoundException();
        }

        public void AddTaskToColumn(ITask task, string? columnTitle = null)
        {
            if (_columns.Count == 0)
            {
                throw new NoColumnsException();
            }

            if (columnTitle == null)
            {
                _columns[0].AddTask(task);
                return;
            }

            IColumn? column = FindColumnByTitle(columnTitle);
            if (column == null)
            {
                throw new ColumnNotFoundException();
            }

            column.AddTask(task);
        }

        public void RemoveTask(string columnTitle, string taskTitle)
        {
            IColumn? column = FindColumnByTitle(columnTitle);
            if (column == null)
            {
                throw new ColumnNotFoundException();
            }

            column.RemoveTaskByTitle(taskTitle);
            
        }

        public void ChangeColumnTitle(string columnTitle, string newTitle)
        {
            IColumn? column = FindColumnByTitle(columnTitle);
            if (column == null)
            {
                throw new ColumnNotFoundException();
            }

            column.Title = newTitle;
        }

        public void ChangeTaskTitle(string columnTitle, string taskTitle, string newTitle)
        {
            IColumn? column = FindColumnByTitle(columnTitle);
            if (column == null)
            {
                throw new ColumnNotFoundException();
            }

            ITask? task = column.FindTaskByTitle(taskTitle);
                if (task != null)
                {
                    task.Title = newTitle;
                    return;
                }

            throw new TaskNotFoundException();
        }

        public void ChangeTaskDescription(string columnTitle, string taskTitle, string newDescription)
        {
            IColumn? column = FindColumnByTitle(columnTitle);
            if (column == null)
            {
                throw new ColumnNotFoundException();
            }

            ITask? task = column.FindTaskByTitle(taskTitle);
            if (task != null)
            {
                task.Description = newDescription;
                return;
            }

            throw new TaskNotFoundException();
        }

        public void ChangeTaskPriority(string columnTitle, string taskTitle, TaskPriority newPriority)
        {
            IColumn? column = FindColumnByTitle(columnTitle);
            if (column == null)
            {
                throw new ColumnNotFoundException();
            }

            ITask? task = column.FindTaskByTitle(taskTitle);
            if (task != null)
            {
                task.Priority = newPriority;
                return;
            }

            throw new TaskNotFoundException();
        }

        private const int MAX_COLUMNS = 10;
        private List<IColumn> _columns;
    }
}
