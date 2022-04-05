namespace ScrumBoard.Exception
{
    public class ColumnCountExceededException : System.Exception
    {
        public ColumnCountExceededException()
            : base("column count exceeded") {}
    }
}
