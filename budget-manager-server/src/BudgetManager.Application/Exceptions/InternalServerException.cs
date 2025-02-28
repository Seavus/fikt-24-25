namespace BudgetManager.Application.Exceptions;

internal class InternalServerException : Exception
{
    public InternalServerException(string message) : base(message) { }
}
