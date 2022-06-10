using ScrumBoard.Exception;
using ScrumBoard.Model.Task;
using ScrumBoard.Model.Column;
using Xunit;

namespace ScrumBoardTest
{
    public class ColumnTest
    {
        [Fact]
        public void CreateColumn_ItHasTitleAndEmptyTasks()
        {
            IColumn column = MockColumn();

            Assert.Equal(_mockTitle, column.Title);
            Assert.Empty(column.FindAllTasks());
        }

        [Fact]
        public void ChangeColumnTitle_TitleChanges()
        {
            IColumn column = MockColumn();
            string newTitle = "Updated";

            column.Title = newTitle;

            Assert.Equal(newTitle, column.Title);
        }

        [Fact]
        public void AddTask_ItAppearsInTheList()
        {
            IColumn column = MockColumn();
            ITask task = MockTask();

            column.AddTask(task);

            Assert.Collection(column.FindAllTasks(),
                    columnTask => Assert.Equal(task, columnTask)
                );
        }

        [Fact]
        public void AddSeveralTasks_TheyAppearInTheOrderOfAddition()
        {
            IColumn column = MockColumn();
            int amount = 3;

            for (int i = 0; i < amount; ++i)
            {
                column.AddTask(new Task(i.ToString(), _mockDescription, _mockPriority));
            }

            Assert.Collection(column.FindAllTasks(),
                    task => Assert.Equal("0", task.Title),
                    task => Assert.Equal("1", task.Title),
                    task => Assert.Equal("2", task.Title)
                );
        }

        [Fact]
        public void AddTaskWithTheSameTitle_ThrowsException()
        {
            IColumn column = MockColumn();
            ITask task = MockTask();

            column.AddTask(task);

            Assert.Throws<TaskAlreadyExistsException>(() => column.AddTask(task));
        }

        [Fact]
        public void FindExistingTask_ReturnsTask()
        {
            IColumn column = MockColumn();
            ITask task = MockTask();

            column.AddTask(task);

            Assert.Equal(task, column.FindTaskByTitle(task.Title));
        }

        [Fact]
        public void FindNonExistingTask_ReturnsNull()
        {
            IColumn column = MockColumn();

            Assert.Null(column.FindTaskByTitle("Why me not exist"));
        }

        [Fact]
        public void RemoveExistingTask_RemovesTaskFromTheList()
        {
            IColumn column = MockColumn();
            ITask task = MockTask();
            column.AddTask(task);

            column.RemoveTaskByTitle(task.Title);

            Assert.Empty(column.FindAllTasks());
        }

        [Fact]
        public void RemoveNonExistingTask_TaskListRemainsUnchanged()
        {
            IColumn column = MockColumn();
            ITask task = MockTask();
            column.AddTask(task);

            column.RemoveTaskByTitle("Nekaya stroka");

            Assert.Single(column.FindAllTasks());
        }

        private IColumn MockColumn()
        {
            return new Column(_mockTitle);
        }

        private ITask MockTask()
        {
            return new Task(_mockTitle, _mockDescription, _mockPriority);
        }

        private string _mockTitle = "Lazy to name :)";
        private string _mockDescription = "alt+255";
        private TaskPriority _mockPriority = TaskPriority.MEDIUM;
    }
}
