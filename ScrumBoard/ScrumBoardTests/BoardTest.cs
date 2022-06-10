using Xunit;
using ScrumBoard.Exception;
using ScrumBoard.Model.Task;
using ScrumBoard.Model.Column;
using ScrumBoard.Model.Board;

namespace ScrumBoardTest
{
    public class BoardTest
    {
        [Fact]
        public void CreateBoard_ItHasTitleAndEmptyColumns()
        {
            IBoard board = MockBoard();

            Assert.Equal(_mockTitle, board.Title);
        }

        [Fact]
        public void AddColumn_ItAppearsInTheList()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();

            board.AddColumn(column);

            Assert.Collection(board.FindAllColumns(),
                    boardColumn => Assert.Equal(column, boardColumn)
                );
        }

        [Fact]
        public void AddSeveralColumns_TheyAppearInTheOrderOfAddition()
        {
            IBoard board = MockBoard();
            int amount = 3;

            for (int i = 0; i < amount; ++i)
            {
                board.AddColumn(new Column(i.ToString()));
            }

            Assert.Collection(board.FindAllColumns(),
                    column => Assert.Equal("0", column.Title),
                    column => Assert.Equal("1", column.Title),
                    column => Assert.Equal("2", column.Title)
                );
        }

        [Fact]
        public void AddBoardWithTheSameTitle_ThrowsException()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();

            board.AddColumn(column);

            Assert.Throws<ColumnAlreadyExistsException>(() => board.AddColumn(column));
        }

        [Fact]
        public void AddMoreThan10Columns_ThrowsException()
        {
            IBoard board = MockBoard();
            for (int i = 0; i < 10; ++i)
            {
                board.AddColumn(new Column(i.ToString()));
            }

            Assert.Throws<ColumnCountExceededException>(() => board.AddColumn(MockColumn()));
        }

        [Fact]
        public void FindExistingColumn_ReturnsColumn()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();

            board.AddColumn(column);

            Assert.Equal(column, board.FindColumnByTitle(column.Title));
        }

        [Fact]
        public void FindNonExistingColumn_ReturnsNull()
        {
            IBoard board = MockBoard();

            Assert.Null(board.FindColumnByTitle(""));
        }

        [Fact]
        public void AdvanceExistingTask_ItMovesToTheNextColumn()
        {
            IBoard board = MockBoard();
            IColumn column1 = new Column("1");
            IColumn column2 = new Column("2");
            board.AddColumn(column1);
            board.AddColumn(column2);
            ITask task = MockTask();
            board.AddTaskToColumn(task, "1");

            board.AdvanceTask("1", task.Title);

            Assert.Empty(column1.FindAllTasks());
            Assert.Collection(column2.FindAllTasks(),
                    columnTask => Assert.Equal(task, columnTask)
                );
        }

        [Fact]
        public void AdvanceNonExistingTask_ThrowsException()
        {
            IBoard board = MockBoard();
            for (int i = 0; i < 5; ++i)
            {
                board.AddColumn(new Column(i.ToString()));
            }

            Assert.Throws<TaskNotFoundException>(() => board.AdvanceTask("0", "No task"));
        }

        [Fact]
        public void AdvanceTaskPastLastColumn_ThrowsException()
        {
            IBoard board = MockBoard();
            board.AddColumn(MockColumn());
            ITask task = MockTask();
            board.AddTaskToColumn(task);

            Assert.Throws<ColumnCountExceededException>(() => board.AdvanceTask(_mockColumnTitle, task.Title));
        }

        [Fact]
        public void AdvanceTaskWithNoColumns_ThrowsException()
        {
            IBoard board = MockBoard();

            Assert.Throws<NoColumnsException>(() => board.AdvanceTask("No column", "_"));
        }

        [Fact]
        public void AddTaskToBoardWithColumns_ItAppearsInTheFirstColumn()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();
            ITask task = MockTask();
            board.AddColumn(column);

            board.AddTaskToColumn(task);

            Assert.Collection(column.FindAllTasks(),
                    columnTask => Assert.Equal(task, columnTask)
                );
        }

        [Fact]
        public void AddTaskToBoardWithoutColumns_ThrowsException()
        {
            IBoard board = MockBoard();
            ITask task = MockTask();

            Assert.Throws<NoColumnsException>(() => board.AddTaskToColumn(task));
        }

        [Fact]
        public void AddTaskToExistingColumn_ItAppearsInTheColumn()
        {
            IBoard board = MockBoard();
            for (int i = 0; i < 5; ++i)
            {
                board.AddColumn(new Column(i.ToString()));
            }
            IColumn column = MockColumn();
            board.AddColumn(column);
            ITask task = MockTask();

            board.AddTaskToColumn(task, column.Title);

            Assert.Collection(column.FindAllTasks(),
                    columnTask => Assert.Equal(task, columnTask)
                );
        }

        [Fact]
        public void AddTaskToNonExistingColumn_ThrowsException()
        {
            IBoard board = MockBoard();
            for (int i = 0; i < 5; ++i)
            {
                board.AddColumn(new Column(i.ToString()));
            }
            ITask task = MockTask();

            Assert.Throws<ColumnNotFoundException>(() => board.AddTaskToColumn(task, "No column"));
        }

        [Fact]
        public void RemoveExistingTask_RemovesTaskFromTheColumn()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();
            board.AddColumn(column);
            ITask task = MockTask();
            column.AddTask(task);

            board.RemoveTask(column.Title, task.Title);

            Assert.Empty(column.FindAllTasks());
        }

        [Fact]
        public void RemoveNonExistingTask_ColumnRemainsUnchanged()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();
            ITask task = MockTask();
            column.AddTask(task);

            board.RemoveTask(column.Title, task.Title);

            Assert.Collection(column.FindAllTasks(),
                    columnTask => Assert.Equal(task, columnTask)
                );
        }

        [Fact]
        public void ChangeExistingColumnTitle_TitleChanges()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();
            board.AddColumn(column);
            string newTitle = "Updated";

            board.ChangeColumnTitle(column.Title, newTitle);

            Assert.Collection(board.FindAllColumns(),
                    column => Assert.Equal(newTitle, column.Title)
                );
        }

        [Fact]
        public void ChangeNonExistingColumnTitle_ThrowsException()
        {
            IBoard board = MockBoard();

            Assert.Throws<ColumnNotFoundException>(() => board.ChangeColumnTitle("No column", "Updated"));
        }

        [Fact]
        public void ChangeExistingTaskTitle_TitleChanges()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();
            board.AddColumn(column);
            ITask task = MockTask();
            column.AddTask(task);
            string newTitle = "Updated";

            board.ChangeTaskTitle(column.Title, task.Title, newTitle);

            Assert.Equal(newTitle, task.Title);
        }

        [Fact]
        public void ChangeNonExistingTaskTitle_ThrowsException()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();
            board.AddColumn(column);
            ITask task = MockTask();
            column.AddTask(task);

            Assert.Throws<TaskNotFoundException>(() => board.ChangeTaskTitle(column.Title, task.Title, "Updated"));
        }

        [Fact]
        public void ChangeExistingTaskDescription_DescriptionChanges()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();
            board.AddColumn(column);
            ITask task = MockTask();
            column.AddTask(task);
            string newDescription = "Updated";

            board.ChangeTaskDescription(column.Title, task.Title, newDescription);

            Assert.Equal(newDescription, task.Description);
        }

        [Fact]
        public void ChangeNonExistingTaskDescription_ThrowsException()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();
            board.AddColumn(column);
            ITask task = MockTask();
            column.AddTask(task);

            Assert.Throws<TaskNotFoundException>(() => board.ChangeTaskDescription(column.Title, task.Title, "Updated"));
        }

        [Fact]
        public void ChangeExistingTaskPriority_PriorityChanges()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();
            board.AddColumn(column);
            ITask task = MockTask();
            column.AddTask(task);
            TaskPriority newPriority = TaskPriority.NONE;

            board.ChangeTaskPriority(column.Title, task.Title, newPriority);

            Assert.Equal(newPriority, task.Priority);
        }

        [Fact]
        public void ChangeNonExistingTaskPriority_ThrowsException()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();
            board.AddColumn(column);
            ITask task = MockTask();
            column.AddTask(task);

            Assert.Throws<TaskNotFoundException>(() => board.ChangeTaskPriority(column.Title, task.Title, TaskPriority.NONE));
        }

        private IBoard MockBoard()
        {
            return new Board(_mockTitle);
        }

        private IColumn MockColumn()
        {
            return new Column(_mockTitle);
        }

        private ITask MockTask()
        {
            return new Task(_mockTitle, _mockDescription, _mockPriority);
        }

        private string _mockTitle = "Board title";

        private string _mockColumnTitle = "Column title";

        private string _mockDescription = "Description";
        private TaskPriority _mockPriority = TaskPriority.MEDIUM;
    }
}
