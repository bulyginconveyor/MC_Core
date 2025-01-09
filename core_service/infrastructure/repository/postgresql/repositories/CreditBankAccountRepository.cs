using System.Linq.Expressions;
using core_service.domain;
using core_service.domain.models;
using core_service.infrastructure.repository.enums;
using core_service.infrastructure.repository.postgresql.context;
using core_service.infrastructure.repository.postgresql.repositories.@base;
using core_service.services.Result;
using Microsoft.EntityFrameworkCore;

namespace core_service.infrastructure.repository.postgresql.repositories;

public class CreditBankAccountRepository(DbContext context) : BaseBankAccountRepository<CreditBankAccount>(context)
{
    public override async Task<Result<IEnumerable<CreditBankAccount>>> GetAll()
    {
        var res = await _context.Set<CreditBankAccount>()
            .Include(c => c.Currency)
            .Include(c => c.Term)
            .Where(c => c.DeletedAt == null)
            .ToListAsync();
        
        if (res.Count == 0)
            return Result<IEnumerable<CreditBankAccount>>.Error(res, "BankAccounts not found");
        
        return Result<IEnumerable<CreditBankAccount>>.Success(res);
    }
    public override async Task<Result<IEnumerable<CreditBankAccount>>> GetAll(Tracking tracking)
    {
        if(tracking == Tracking.Yes)
            return await this.GetAll();
        
        var res = await _context.Set<CreditBankAccount>()
            .AsNoTracking()
            .Include(c => c.Currency)
            .Include(c => c.Term)
            .Where(c => c.DeletedAt == null)
            .ToListAsync();
        
        if (res.Count == 0)
            return Result<IEnumerable<CreditBankAccount>>.Error(res, "BankAccounts not found");
        
        return Result<IEnumerable<CreditBankAccount>>.Success(res);
    }
    public override async Task<Result<IEnumerable<CreditBankAccount>>> GetAll(Expression<Func<CreditBankAccount, bool>> filter, Tracking tracking = Tracking.Yes)
    {
        List<CreditBankAccount> res;
        
        if (tracking == Tracking.No)
            res = await _context.Set<CreditBankAccount>()
                .AsNoTracking()
                .Include(b => b.Currency)
                .Include(c => c.Term)
                .Where(filter)
                .Where(c => c.DeletedAt == null)
                .ToListAsync();
        else 
            res = await _context.Set<CreditBankAccount>()
                .Include(b => b.Currency)
                .Include(c => c.Term)
                .Where(filter)
                .Where(c => c.DeletedAt == null)
                .ToListAsync();
        
        if (res.Count == 0)
            return Result<IEnumerable<CreditBankAccount>>.Error(res, "BankAccounts not found");
            
        return Result<IEnumerable<CreditBankAccount>>.Success(res);
    }

    public override async Task<Result<CreditBankAccount>> GetOne(Guid id)
    {
        var res = await _context.Set<CreditBankAccount>()
            .Include(c => c.Currency)
            .Include(c => c.Term)
            .Where(c => c.DeletedAt == null)
            .FirstOrDefaultAsync(c => c.Id == id);

        return res == null ? 
            Result<CreditBankAccount>.Error(res!, "BankAccount not found") 
            : 
            Result<CreditBankAccount>.Success(res);
    }
    public override async Task<Result<CreditBankAccount>> GetOne(Guid id, Tracking tracking = Tracking.Yes)
    {
        CreditBankAccount? res;
        
        if(tracking == Tracking.Yes)
            res = await _context.Set<CreditBankAccount>()
                .Include(c => c.Currency)
                .Include(c => c.Term)
                .Where(c => c.DeletedAt == null)
                .FirstOrDefaultAsync(c => c.Id == id);
        else 
            res = await _context.Set<CreditBankAccount>()
                .AsNoTracking()
                .Include(c => c.Currency)
                .Include(c => c.Term)
                .Where(c => c.DeletedAt == null)
                .FirstOrDefaultAsync(c => c.Id == id);
        
        return res == null ? 
            Result<CreditBankAccount>.Error(res!, "BankAccount not found") 
            : 
            Result<CreditBankAccount>.Success(res);
    }
    public override async Task<Result<CreditBankAccount>> GetOne(Expression<Func<CreditBankAccount, bool>> filter, Tracking tracking = Tracking.Yes)
    {
        CreditBankAccount? res;

        if (tracking == Tracking.Yes)
            res = await _context.Set<CreditBankAccount>()
                .Include(c => c.Currency)
                .Include(c => c.Term)
                .Where(c => c.DeletedAt == null)
                .FirstOrDefaultAsync(filter);
        else 
            res = await _context.Set<CreditBankAccount>()
                .AsNoTracking()
                .Include(c => c.Currency)
                .Include(c => c.Term)
                .Where(c => c.DeletedAt == null)
                .FirstOrDefaultAsync(filter);
        
        return res == null ? 
            Result<CreditBankAccount>.Error(res!, "BankAccount not found") 
            : 
            Result<CreditBankAccount>.Success(res);
    }
    
    public override async Task<Result<CreditBankAccount>> LoadData(CreditBankAccount entity)
    {
        var baseRes = await base.LoadData(entity);
        if (baseRes.IsError)
            return baseRes;

        var baseResEntity = baseRes.Value!;
        await _context.Entry(baseResEntity).Reference(e => e.LoanObject).LoadAsync();

        return Result<CreditBankAccount>.Success(baseResEntity);
    }
}
