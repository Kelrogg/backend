using Xunit;
using ScrumBoard.Model.Task;

namespace ScrumBoardTest
{
    public class TaskTest
    {
        [Fact]
        public void CreateTask_ItHasProperties()
        {
            ITask task = MockTask();

            Assert.Equal(_mockTitle, task.Title);
            Assert.Equal(_mockDescription, task.Description);
            Assert.Equal(_mockPriority, task.Priority);
        }

        [Fact]
        public void ChangeTaskTitle_TitleChanges()
        {
            ITask task = MockTask();
            string newTitle = "Updated";

            task.Title = newTitle;

            Assert.Equal(newTitle, task.Title);
        }

        [Fact]
        public void ChangeTaskDescription_DescriptionChanges()
        {
            ITask task = MockTask();
            string newDescription = "Updated";

            task.Description = newDescription;

            Assert.Equal(newDescription, task.Description);
        }

        [Fact]
        public void ChangeTaskPriority_PriorityChanges()
        {
            ITask task = MockTask();
            TaskPriority newPriority = TaskPriority.HIGH;

            task.Priority = newPriority;

            Assert.Equal(newPriority, task.Priority);
        }

        private ITask MockTask()
        {
            return new Task(_mockTitle, _mockDescription, _mockPriority);
        }

        private string _mockTitle = "Lazy to name :)";
        private string _mockDescription = "alt+255";
        private TaskPriority _mockPriority = TaskPriority.NONE;
    }
}
