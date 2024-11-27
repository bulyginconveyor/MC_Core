namespace core_service.domain.@base;

public interface IEntity<T>
{
    public T Id { get; set; }
}