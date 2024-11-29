namespace core_service.domain.@base;

public interface IDbModel
{
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; }
}