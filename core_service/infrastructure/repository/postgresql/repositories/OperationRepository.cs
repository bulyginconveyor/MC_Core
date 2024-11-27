using System.Linq.Expressions;
using core_service.domain;
using core_service.infrastructure.repository.enums;
using core_service.infrastructure.repository.postgresql.context;
using core_service.infrastructure.repository.postgresql.repositories.@base;
using core_service.services.Result;
using Microsoft.EntityFrameworkCore;

namespace core_service.infrastructure.repository.postgresql.repositories;

public class OperationRepository(PostgreSqlDbContext context) : BaseRepository<Operation>(context)
{
    public override async Task<Result<IEnumerable<Operation>>> GetAll()
    {
        var res = await _context.Operations
            .Include(o => o.Period)
            .Include(o => o.CreditBankAccount)
            .Include(o => o.DebetBankAccount)
            .Include(o => o.Category)
            .Where(o => o.DeletedAt == null)
            .ToListAsync();

        return Result<IEnumerable<Operation>>.Success(res);
    }
    public override async Task<Result<IEnumerable<Operation>>> GetAll(Tracking tracking)
    {
        if (tracking == Tracking.Yes)
            return await GetAll();
        
        var res = await _context.Operations
                .AsNoTracking()
                .Include(o => o.Period)
                .Include(o => o.CreditBankAccount)
                .Include(o => o.DebetBankAccount)
                .Include(o => o.Category)
                .Where(o => o.DeletedAt == null)
                .ToListAsync();
        
        return Result<IEnumerable<Operation>>.Success(res);
    }
    public override async Task<Result<IEnumerable<Operation>>> GetAll(Tracking tracking, Expression<Func<Operation, bool>> filter)
    {
        var res = tracking == Tracking.Yes
            ? await _context.Operations
                .Include(o => o.Period)
                .Include(o => o.CreditBankAccount)
                .Include(o => o.DebetBankAccount)
                .Include(o => o.Category)
                .Where(o => o.DeletedAt == null)
                .Where(filter)
                .ToListAsync()
            : await _context.Operations
                .AsNoTracking()
                .Include(o => o.Period)
                .Include(o => o.CreditBankAccount)
                .Include(o => o.DebetBankAccount)
                .Include(o => o.Category)
                .Where(o => o.DeletedAt == null)
                .Where(filter)
                .ToListAsync();

        return Result<IEnumerable<Operation>>.Success(res);
    }

    public override async Task<Result<Operation>> GetOne(Guid id)
    {
        var res = await _context.Operations
            .Include(o => o.Period)
            .Include(o => o.CreditBankAccount)
            .Include(o => o.DebetBankAccount)
            .Include(o => o.Category)
            .Where(o => o.DeletedAt == null)
            .FirstOrDefaultAsync(o => o.Id == id);

        return res is null
            ? Result<Operation>.Error(res, $"Not found operation by Id! (id = {id}) ")
            : Result<Operation>.Success(res);
    }
    public override async Task<Result<Operation>> GetOne(Guid id, Tracking tracking)
    {
        if (tracking == Tracking.Yes)
            return await GetOne(id);
        
        var res = await _context.Operations
            .AsNoTracking()
            .Include(o => o.Period)
            .Include(o => o.CreditBankAccount)
            .Include(o => o.DebetBankAccount)
            .Include(o => o.Category)
            .Where(o => o.DeletedAt == null)
            .FirstOrDefaultAsync(o => o.Id == id);

        return res is null
            ? Result<Operation>.Error(res, $"Not found operation by Id! (id = {id}) ")
            : Result<Operation>.Success(res);
    }
    public override async Task<Result<Operation>> GetOne(Expression<Func<Operation, bool>> filter, Tracking tracking)
    {
        var res = tracking == Tracking.Yes ?
            await _context.Operations
                .Include(o => o.Period)
                .Include(o => o.CreditBankAccount)
                .Include(o => o.DebetBankAccount)
                .Include(o => o.Category)
                .Where(o => o.DeletedAt == null)
                .Where(filter)
                .FirstOrDefaultAsync(filter)
            :
            await _context.Operations
                .AsNoTracking()
                .Include(o => o.Period)
                .Include(o => o.CreditBankAccount)
                .Include(o => o.DebetBankAccount)
                .Include(o => o.Category)
                .Where(o => o.DeletedAt == null)
                .Where(filter)
                .FirstOrDefaultAsync(filter);
            
        return res is null
            ? Result<Operation>.Error(res, $"Not found operation by filter!")
            : Result<Operation>.Success(res);
    }

    public override async Task<Result> Update(Operation entity)
    {
        try
        {
            await _context.Operations
                .Where(o => o.Id == entity.Id)
                .ExecuteUpdateAsync(
                o => o
                    .SetProperty(e => e.Name, entity.Name)
                    .SetProperty(e => e.Date, entity.Date)
                    .SetProperty(e => e.Amount, entity.Amount)
                    .SetProperty(e => e.Period, entity.Period)
                    .SetProperty(e => e.CreditBankAccount, entity.CreditBankAccount)
                    .SetProperty(e => e.DebetBankAccount, entity.DebetBankAccount)
                    .SetProperty(e => e.Category, entity.Category)
                    .SetProperty(e => e.Status, entity.Status)
                    
                    .SetProperty(e => e.UpdatedAt, DateTime.UtcNow)
                );

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }

    public override async Task<Result<Operation>> LoadData(Operation entity)
    {
        await _context.Entry(entity).Reference(o => o.Period).LoadAsync();
        await _context.Entry(entity).Reference(o => o.DebetBankAccount).LoadAsync();
        await _context.Entry(entity).Reference(o => o.CreditBankAccount).LoadAsync();
        await _context.Entry(entity).Reference(o => o.Category).LoadAsync();

        return Result<Operation>.Success(entity);
    }
}