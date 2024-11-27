namespace core_service.domain.@base;

public interface IDbModel
{
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; }
    public DateTime? DeletedAt { get; }
}