namespace core_service.infrastructure.repository.postgresql.repositories.exceptions;

public class NotEnoughMoney : Exception
{
    public NotEnoughMoney(string message) : base(message)
    {
    }
}