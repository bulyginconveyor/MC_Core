using core_service.domain.models.@base;
using core_service.services.Result;

namespace core_service.infrastructure.repository.interfaces;

public interface ICacheRepositoryWithLists<T> : ICacheRepository<T> where T : class
{
    public Task<Result<List<T>>> GetAll();
    public Task<Result> Add(List<T> entities);
    public Task<Result> Add(List<T> entities, TimeSpan timeLife);
    
    public Task<Result> Update(List<T> entities);
    public Task<Result> Update(List<T> entities, TimeSpan timeLife);

    public Task<Result> UnSetCollection();
}
