using core_service.services.Result;

namespace core_service.infrastructure.repository.interfaces;

public interface IRepositoryForHiddenCategory<T> where T : class
{
    public Task<Result<IEnumerable<T>>> GetAll(Guid? userId);
    public Task<Result> Add(T entity);
    public Task<Result> Delete(T entity);
}
