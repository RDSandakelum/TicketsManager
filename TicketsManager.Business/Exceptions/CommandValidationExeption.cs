namespace TicketsManager.Business.Exceptions;

public class CommandValidationExeption : Exception
{
    public CommandValidationExeption(string message) : base(message) { }
}
