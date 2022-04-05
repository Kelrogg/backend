namespace ScrumBoard.Exception
{
    public class ColumnAlreadyExistsException : System.Exception
    {
        public ColumnAlreadyExistsException()
            : base("column already exists") {}
    }
}
