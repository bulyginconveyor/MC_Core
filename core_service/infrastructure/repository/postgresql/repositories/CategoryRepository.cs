using core_service.domain;
using core_service.domain.models;
using core_service.infrastructure.repository.enums;
using core_service.infrastructure.repository.postgresql.repositories.@base;
using core_service.services.Result;
using Microsoft.EntityFrameworkCore;

namespace core_service.infrastructure.repository.postgresql.repositories;

public class CategoryRepository(DbContext context) : BaseRepository<Category>(context)
{
    public override async Task<Result<IEnumerable<Category>>> GetAll()
    {
        var res = await _context.Set<Category>()
            .Include(c => c.SubCategories)
            .Where(c => c.DeletedAt == null)
            .Where(c => c.SubCategories.Count > 0)
            .ToListAsync();
        
        return res.Count == 0 ? 
            Result<IEnumerable<Category>>.Error(res, "Categories not found") 
            : 
            Result<IEnumerable<Category>>.Success(res);
    }
    public override async Task<Result<IEnumerable<Category>>> GetAll(Tracking tracking)
    {
        if(tracking == Tracking.Yes)
            return await this.GetAll();
        
        var res = await _context.Set<Category>()
            .AsNoTracking()
            .Include(c => c.SubCategories)
            .Where(e => e.DeletedAt == null)
            .Where(c => c.SubCategories.Count() > 0)
            .ToListAsync();
        
        if (res == null || res.Count() == 0)
            return Result<IEnumerable<Category>>.Error(res, "Categories not found");
        
        return Result<IEnumerable<Category>>.Success(res);
    }

    public override async Task<Result> Update(Category entity)
    {
        try
        {
            var res = await this.GetOne(entity.Id);
            if (res.IsError)
                return Result.Error(res.ErrorMessage!);

            var category = res.Value!;
            category.ChangeSubCategories(entity.SubCategories);
            
            _context.Set<Category>().Update(category);
            
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }

    public override async Task<Result<Category>> LoadData(Category entity)
    {
        await _context.Entry(entity).Collection(e => e.SubCategories).LoadAsync();
        return Result<Category>.Success(entity);
    }
}