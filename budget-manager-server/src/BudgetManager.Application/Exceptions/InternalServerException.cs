namespace BudgetManager.Application.Exceptions;

public class InternalServerException : Exception
{
    public InternalServerException(string message) : base(message) { }
}
