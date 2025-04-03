using core_service.domain.models.@base;

namespace core_service.infrastructure.repository.postgresql.models;

public class HiddenCategory
{
    public Guid CategoryId { get; set; }
    public Guid UserId { get; set; }
}
