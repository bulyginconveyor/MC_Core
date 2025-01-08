using core_service.domain.models.@base;
using core_service.services.Result;

namespace core_service.infrastructure.repository.interfaces;

public interface ICacheRepository<T> where T : class, ICached<T>
{
    public Task<Result<T>> Get(string key);
    public Task<Result> Add(string key, T entity);
    public Task<Result> Add(string key, T entity, TimeSpan timeLife);
    public Task<Result> Update(string key, T entity);
    public Task<Result> Update(string key, T entity, TimeSpan timeLife);
    public Task<Result> UnSet(string key);
}
