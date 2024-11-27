using System.Linq.Expressions;
using core_service.domain.@base;
using core_service.infrastructure.repository.enums;
using core_service.infrastructure.repository.interfaces;
using core_service.infrastructure.repository.postgresql.context;
using core_service.services.Result;
using Microsoft.EntityFrameworkCore;

namespace core_service.infrastructure.repository.postgresql.repositories.@base;

public abstract class BaseRepository<T>(PostgreSqlDbContext context) 
    : IDbRepository<T> where T : class, IDbModel, IEntity<Guid>
{
    protected readonly PostgreSqlDbContext _context = context;

    public async Task<Result> Save()
    {
        try
        {
            await _context.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
    
    public virtual async Task<Result<IEnumerable<T>>> GetAll()
    {
        var result = await _context.Set<T>().Where(e => e.DeletedAt == null).ToListAsync();
        
        return Result<IEnumerable<T>>.Success(result);
    }
    public virtual async Task<Result<IEnumerable<T>>> GetAll(Tracking tracking)
    {
        if(tracking == Tracking.No)
        {
            var resNoTracking = await _context.Set<T>().AsNoTracking().Where(e => e.DeletedAt == null).ToListAsync();
            return Result<IEnumerable<T>>.Success(resNoTracking);
        }
        
        return await this.GetAll();
    }
    public virtual async Task<Result<IEnumerable<T>>> GetAll(Tracking tracking, Expression<Func<T, bool>> filter)
    {
        var result = tracking == Tracking.No ? 
            await _context.Set<T>().AsNoTracking().Where(filter).Where(e => e.DeletedAt == null).ToListAsync() :
            await _context.Set<T>().Where(filter).ToListAsync();
        
        return Result<IEnumerable<T>>.Success(result);
    }
    
    public virtual async Task<Result<T>> GetOne(Guid id)
    {
        var result = await _context.Set<T>().Where(e => e.DeletedAt == null).FirstOrDefaultAsync(e => e.Id == id);
        
        if(result == null || result.DeletedAt != null)
            return Result<T>.Error(null!, $"Not found by {id}. Return null");
        
        return Result<T>.Success(result);
    }
    public virtual async Task<Result<T>> GetOne(Guid id, Tracking tracking)
    {
        if (tracking == Tracking.No)
        {
            var resNoTracking = await _context.Set<T>().AsNoTracking().Where(e => e.DeletedAt == null).FirstOrDefaultAsync(e => e.Id == id);
            if(resNoTracking == null)
                return Result<T>.Error(null!, $"Not found. Return null");
            
            return Result<T>.Success(resNoTracking);
        }

        return await this.GetOne(id);
    }
    public virtual async Task<Result<T>> GetOne(Expression<Func<T, bool>> filter, Tracking tracking)
    {
        var result = tracking == Tracking.No ?
            await _context.Set<T>().AsNoTracking().Where(e => e.DeletedAt == null).FirstOrDefaultAsync(filter) :
            await _context.Set<T>().Where(e => e.DeletedAt == null).FirstOrDefaultAsync(filter);

        if (result == null)
            return Result<T>.Error(null!,$"Not found. Return null");
        
        return Result<T>.Success(result);
    }
    
    public virtual async Task<Result> Add(T entity)
    {
        try
        {
            await _context.Set<T>().AddAsync(entity);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
    public virtual async Task AddRange(IEnumerable<T> entities)
    {
        List<Task> tasks = [];
        tasks.AddRange(entities.Select(this.Add));

        await Task.WhenAll(tasks);
    }    
    
    public abstract Task<Result> Update(T entity);
    public virtual async Task UpdateRange(IEnumerable<T> entities)
    {
        List<Task> tasks = [];
        tasks.AddRange(entities.Select(this.Update));

        await Task.WhenAll(tasks);
    }
    
    public virtual async Task<Result> Delete(Guid id)
    {
        try
        {
            await _context
                .Set<T>()
                .Where(e => e.Id == id)
                .ExecuteUpdateAsync(e => 
                    e.SetProperty(model => model.DeletedAt, DateTime.UtcNow)
                );
            
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
    public virtual async Task<Result> Delete(T entity)
    {
        try
        {
            await _context
                .Set<T>()
                .Where(e => e.Id == entity.Id)
                .ExecuteUpdateAsync(e => 
                    e.SetProperty(model => model.DeletedAt, DateTime.UtcNow)
                );
            
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
    public virtual async Task DeleteRange(IEnumerable<T> entities)
    {
        List<Task> tasks = [];
        
        tasks.AddRange(entities.Select(this.Delete));

        await Task.WhenAll(tasks);
    }

    public abstract Task<Result<T>> LoadData(T entity);

    public virtual async Task<Result<bool>> Exists(Expression<Func<T, bool>> filter)
    {
        var result = await this.Count(filter);
        if (result.IsError || result.Value == 0 || result.Value > 1)
            return Result<bool>.Error(false,$"Not found. Return null");
        
        return Result<bool>.Success(true);
    }
    public virtual async Task<Result<long>> Count(Expression<Func<T, bool>> filter)
    {
        var res = await _context.Set<T>()
            .AsNoTracking()
            .Where(e => e.DeletedAt == null)
            .CountAsync(filter);
        if (res == 0)
            return Result<long>.Error(0,$"Not found. Return 0");
        
        return Result<long>.Success(res);
    }
}