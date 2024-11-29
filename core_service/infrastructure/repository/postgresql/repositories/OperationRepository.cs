using System.Linq.Expressions;
using core_service.domain;
using core_service.infrastructure.repository.enums;
using core_service.infrastructure.repository.postgresql.context;
using core_service.infrastructure.repository.postgresql.repositories.@base;
using core_service.services.Result;
using Microsoft.EntityFrameworkCore;

namespace core_service.infrastructure.repository.postgresql.repositories;

public class OperationRepository(DbContext context) : BaseRepository<Operation>(context)
{
    public override async Task<Result<IEnumerable<Operation>>> GetAll()
    {
        var res = await _context.Set<Operation>()
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
        
        var res = await _context.Set<Operation>()
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
            ? await _context.Set<Operation>()
                .Include(o => o.Period)
                .Include(o => o.CreditBankAccount)
                .Include(o => o.DebetBankAccount)
                .Include(o => o.Category)
                .Where(o => o.DeletedAt == null)
                .Where(filter)
                .ToListAsync()
            : await _context.Set<Operation>()
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
        var res = await _context.Set<Operation>()
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
        
        var res = await _context.Set<Operation>()
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
            await _context.Set<Operation>()
                .Include(o => o.Period)
                .Include(o => o.CreditBankAccount)
                .Include(o => o.DebetBankAccount)
                .Include(o => o.Category)
                .Where(o => o.DeletedAt == null)
                .Where(filter)
                .FirstOrDefaultAsync(filter)
            :
            await _context.Set<Operation>()
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

    public override async Task<Result<Operation>> LoadData(Operation entity)
    {
        await _context.Entry(entity).Reference(o => o.Period).LoadAsync();
        await _context.Entry(entity).Reference(o => o.DebetBankAccount).LoadAsync();
        await _context.Entry(entity).Reference(o => o.CreditBankAccount).LoadAsync();
        await _context.Entry(entity).Reference(o => o.Category).LoadAsync();

        return Result<Operation>.Success(entity);
    }
}