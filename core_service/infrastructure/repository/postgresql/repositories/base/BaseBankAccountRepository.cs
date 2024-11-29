using System.Linq.Expressions;
using core_service.domain;
using core_service.infrastructure.repository.enums;
using core_service.infrastructure.repository.postgresql.context;
using core_service.services.Result;
using Microsoft.EntityFrameworkCore;

namespace core_service.infrastructure.repository.postgresql.repositories.@base;

public class BaseBankAccountRepository<B>(DbContext context) : BaseRepository<B>(context) where B : BankAccount
{
    public override async Task<Result<IEnumerable<B>>> GetAll()
    {
        var res = await _context.Set<B>()
            .Include(c => c.Currency)
            .Where(c => c.DeletedAt == null)
            .ToListAsync();
        
        if (res.Count == 0)
            return Result<IEnumerable<B>>.Error(res, "BankAccounts not found");
        
        return Result<IEnumerable<B>>.Success(res);
    }
    public override async Task<Result<IEnumerable<B>>> GetAll(Tracking tracking)
    {
        if(tracking == Tracking.Yes)
            return await this.GetAll();
        
        var res = await _context.Set<B>()
            .AsNoTracking()
            .Include(c => c.Currency)
            .Where(c => c.DeletedAt == null)
            .ToListAsync();
        
        if (res.Count == 0)
            return Result<IEnumerable<B>>.Error(res, "BankAccounts not found");
        
        return Result<IEnumerable<B>>.Success(res);
    }
    public override async Task<Result<IEnumerable<B>>> GetAll(Tracking tracking, Expression<Func<B, bool>> filter)
    {
        List<B> res;
        
        if (tracking == Tracking.No)
            res = await _context.Set<B>()
                .AsNoTracking()
                .Include(b => b.Currency)
                .Where(filter)
                .Where(c => c.DeletedAt == null)
                .ToListAsync();
        else 
            res = await _context.Set<B>()
                .Include(b => b.Currency)
                .Where(filter)
                .Where(c => c.DeletedAt == null)
                .ToListAsync();
        
        if (res.Count == 0)
            return Result<IEnumerable<B>>.Error(res, "BankAccounts not found");
            
        return Result<IEnumerable<B>>.Success(res);
    }

    public override async Task<Result<B>> GetOne(Guid id)
    {
        var res = await _context.Set<B>()
            .Include(c => c.Currency)
            .Where(c => c.DeletedAt == null)
            .FirstOrDefaultAsync(c => c.Id == id);

        return res == null ? 
            Result<B>.Error(res!, "BankAccount not found") 
            : 
            Result<B>.Success(res);
    }
    public override async Task<Result<B>> GetOne(Guid id, Tracking tracking)
    {
        B? res;
        
        if(tracking == Tracking.Yes)
            res = await _context.Set<B>()
                .Include(c => c.Currency)
                .Where(c => c.DeletedAt == null)
                .FirstOrDefaultAsync(c => c.Id == id);
        else 
            res = await _context.Set<B>()
                .AsNoTracking()
                .Include(c => c.Currency)
                .Where(c => c.DeletedAt == null)
                .FirstOrDefaultAsync(c => c.Id == id);
        
        return res == null ? 
            Result<B>.Error(res!, "BankAccount not found") 
            : 
            Result<B>.Success(res);
    }
    public override async Task<Result<B>> GetOne(Expression<Func<B, bool>> filter, Tracking tracking)
    {
        B? res;

        if (tracking == Tracking.Yes)
            res = await _context.Set<B>()
                .Include(c => c.Currency)
                .Where(c => c.DeletedAt == null)
                .FirstOrDefaultAsync(filter);
        else 
            res = await _context.Set<B>()
                .AsNoTracking()
                .Include(c => c.Currency)
                .Where(c => c.DeletedAt == null)
                .FirstOrDefaultAsync(filter);
        
        return res == null ? 
            Result<B>.Error(res!, "BankAccount not found") 
            : 
            Result<B>.Success(res);
    }

    public override async Task<Result<B>> LoadData(B entity)
    {
        var list = await _context.Set<Operation>()
            .Include(o => o.DebetBankAccount)
            .Include(o => o.CreditBankAccount)
            .Where(o => o.DeletedAt == null && 
                        ((o.DebetBankAccount != null && o.DebetBankAccount.Id == entity.Id) 
                         || 
                        (o.CreditBankAccount != null && o.CreditBankAccount.Id == entity.Id)))
            .OrderByDescending(o => o.CreatedAt)
            .Take(10)
            .ToListAsync();
        
        var res = entity.SetOperations(list);
        
        return res.IsError ? 
            Result<B>.Error(entity, "Load data error! " + res.ErrorMessage)
            :
            Result<B>.Success(entity);
    }
}