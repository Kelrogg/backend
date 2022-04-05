using System;
using ScrumBoard.Model.Task;
using ScrumBoard.Model.Board;
using ScrumBoard.Model.Column;

namespace ScrumBoard
{
    class ScrumBoard
    {
        public static void Main()
        {
            try
            {
                IBoard board = new Board("Barracks");
                IColumn todoColumn = new Column("In queue");
                IColumn training = new Column("Training");

                board.AddColumn(todoColumn);
                board.AddColumn(training);

                Console.WriteLine("\n Initial board  \n");
                PrintBoard(board);

                ITask rifleman = new Task("Rifleman", "Ground ranged unit", TaskPriority.MEDIUM);
                board.AddTaskToColumn(rifleman);
                Console.WriteLine("\n  One task added  \n");
                PrintBoard(board);

                board.AdvanceTask(todoColumn.Title, rifleman.Title);
                Console.WriteLine("\n  Task advanced  \n");
                PrintBoard(board);

                ITask longRifles = new Task("Long Rifles", "Increases the range of the Rifleman's attack", TaskPriority.HIGH);
                board.AddTaskToColumn(longRifles, todoColumn.Title);
                Console.WriteLine("\n  Task added in \"In queue\"  \n");
                PrintBoard(board);

                board.ChangeTaskTitle(training.Title, rifleman.Title, "Peon");
                board.ChangeTaskDescription(training.Title, rifleman.Title, "the lowest station amongst the Orcish Horde");
                board.ChangeTaskPriority(training.Title, rifleman.Title, TaskPriority.HIGH);
                Console.WriteLine("\n  Updated rifleman task  \n");
                PrintBoard(board);

                board.AdvanceTask(todoColumn.Title, longRifles.Title);
                Console.WriteLine("\n  All in training  \n");
                PrintBoard(board);

                Console.WriteLine("\n  Advance task in last column  \n");
                board.AdvanceTask(training.Title, rifleman.Title);
                PrintBoard(board);

                board.RemoveTask(training.Title, longRifles.Title);
                Console.WriteLine("\n  Removed all tasks  \n");
                PrintBoard(board);

            }
            catch (System.Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private static void PrintBoard(IBoard board)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (IColumn column in board.FindAllColumns())
            {
                PrintColumn(column);
            }
            Console.ResetColor();
        }

        private static void PrintColumn(IColumn column)
        {
            Console.WriteLine($"   {column.Title}  ");
            PrintTasks(column);
        }

        private static void PrintTasks(IColumn column)
        {
            foreach (ITask task in column.FindAllTasks())
            {
                PrintTask(task);
            }
        }

        private static void PrintTask(ITask task)
        {
            Console.WriteLine($"  [{TaskPriorityToString[(int)task.Priority]}] {task.Title}: {task.Description}");
        }

        private static readonly string[] TaskPriorityToString = 
        {
            "HIGH",
            "MEDIUM",
            "LOW",
            "NONE"
        };
    }
}
