namespace core_service.domain.models.@base;

public interface IDbModel
{
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; }
}