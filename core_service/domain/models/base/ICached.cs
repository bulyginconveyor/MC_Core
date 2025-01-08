using core_service.services.Result;

namespace core_service.domain.models.@base;

public interface ICached<T> where T : class
{
    public Result<string> Caching();
    public Result<T> DeCaching();
}
