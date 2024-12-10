using System.Linq.Expressions;
using core_service.domain;
using core_service.infrastructure.repository.enums;
using core_service.infrastructure.repository.postgresql.context;
using core_service.infrastructure.repository.postgresql.repositories.@base;
using core_service.infrastructure.repository.postgresql.repositories.exceptions;
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
    public override async Task<Result<IEnumerable<Operation>>> GetAll(Expression<Func<Operation, bool>> filter, Tracking tracking = Tracking.Yes)
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
    public override async Task<Result<Operation>> GetOne(Guid id, Tracking tracking = Tracking.Yes)
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
    public override async Task<Result<Operation>> GetOne(Expression<Func<Operation, bool>> filter, Tracking tracking = Tracking.Yes)
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
    
    public override async Task<Result> Add(Operation entity)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            ArgumentNullException.ThrowIfNull(entity);

            entity.CreditBankAccount?.Balance.Decrease(entity.Amount);
            entity.DebetBankAccount?.Balance.Increase(entity.Amount);

            if (entity.CreditBankAccount != null) _context.Set<BankAccount>().Update(entity.CreditBankAccount);
            if (entity.DebetBankAccount != null) _context.Set<BankAccount>().Update(entity.DebetBankAccount);
            
            await _context.Set<Operation>().AddAsync(entity);
            
            await _context.SaveChangesAsync();
            
            await transaction.CommitAsync();
            
            return Result.Success();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return Result.Error(ex.Message);
        }
    }
    public override async Task<Result> Update(Operation entity)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            ArgumentNullException.ThrowIfNull(entity);
            var oldOperation = await _context.Set<Operation>().AsNoTracking().FirstOrDefaultAsync(o => o.Id == entity.Id);
            
            if (oldOperation == null) return Result.Error($"Not found operation by Id! (id = {entity.Id}) ");
            
            entity.UpdatedAt = DateTime.UtcNow;
            
            if(oldOperation.Amount == entity.Amount)
                _context.Set<Operation>().Update(entity);
            else
            {
                decimal diff = oldOperation.Amount - entity.Amount;
                switch (diff)
                {
                    case > 0:
                    {
                        if (entity.DebetBankAccount != null)
                            if (entity.DebetBankAccount.Balance.TryDecrease(diff))
                                entity.DebetBankAccount.Balance.Decrease(diff);
                            else
                                throw new NotEnoughMoney($"Not enough money in credit account! (id = {entity.Id}) ");
                        
                        entity.CreditBankAccount?.Balance.Increase(diff);
                        break;
                    }
                    case < 0:
                    {
                        if (entity.CreditBankAccount != null)
                            if (entity.CreditBankAccount.Balance.TryDecrease(-diff))
                                entity.CreditBankAccount.Balance.Increase(diff);
                            else
                                throw new NotEnoughMoney($"Not enough money in debet account! (id = {entity.Id}) ");
                    
                        entity.DebetBankAccount?.Balance.Decrease(diff);
                        break;
                    }
                }
            }
            if(entity.CreditBankAccount != null)
                _context.Set<BankAccount>().Update(entity.CreditBankAccount);
            if(entity.DebetBankAccount != null)
                _context.Set<BankAccount>().Update(entity.DebetBankAccount);
            
            _context.Set<Operation>().Update(entity);
            
            await _context.SaveChangesAsync();
            
            await transaction.CommitAsync();

            return Result.Success();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return Result.Error(ex.Message);
        }
    }
    public override async Task<Result> Delete(Operation entity)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            ArgumentNullException.ThrowIfNull(entity);

            entity.CreditBankAccount?.Balance.Increase(entity.Amount);
            entity.DebetBankAccount?.Balance.Decrease(entity.Amount);

            if (entity.CreditBankAccount != null)
                _context.Set<BankAccount>().Update(entity.CreditBankAccount);
            if (entity.DebetBankAccount != null)
                _context.Set<BankAccount>().Update(entity.DebetBankAccount);

            await base.Delete(entity);

            await _context.SaveChangesAsync();
            
            await transaction.CommitAsync();

            return Result.Success();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return Result.Error(ex.Message);
        }
    }

    public override async Task AddRange(IEnumerable<Operation> entities)
    {
        foreach (var entity in entities)
            await this.Add(entity);
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