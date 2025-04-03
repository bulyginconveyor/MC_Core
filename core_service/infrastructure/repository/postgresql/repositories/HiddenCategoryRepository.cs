using core_service.infrastructure.repository.interfaces;
using core_service.infrastructure.repository.postgresql.models;
using core_service.services.Result;
using Microsoft.EntityFrameworkCore;

namespace core_service.infrastructure.repository.postgresql.repositories;

public class HiddenCategoryRepository(DbContext context) : IRepositoryForHiddenCategory<HiddenCategory>
{
    public async Task<Result<IEnumerable<HiddenCategory>>> GetAll(Guid? userId)
    {
        var resGet = await context.Set<HiddenCategory>()
            .Where(x => x.UserId == userId)
            .ToListAsync();
        
        return Result<IEnumerable<HiddenCategory>>.Success(resGet);
    }

    public async Task<Result> Add(HiddenCategory entity)
    {
        try
        {
            await context.Set<HiddenCategory>().AddAsync(entity);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return Result<HiddenCategory>.Error(null, ex.Message);
        }
        
        return Result<HiddenCategory>.Success(entity);
    }

    public async Task<Result> Delete(HiddenCategory entity)
    {
        try
        {
            await context.Set<HiddenCategory>()
                .Where(hc => hc.CategoryId == entity.CategoryId && hc.UserId == entity.UserId)
                .ExecuteDeleteAsync();
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
        
        return Result.Success();
    }
}
