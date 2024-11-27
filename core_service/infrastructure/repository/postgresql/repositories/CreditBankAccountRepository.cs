using core_service.domain;
using core_service.infrastructure.repository.postgresql.context;
using core_service.infrastructure.repository.postgresql.repositories.@base;
using core_service.services.Result;
using Microsoft.EntityFrameworkCore;

namespace core_service.infrastructure.repository.postgresql.repositories;

public class CreditBankAccountRepository(PostgreSqlDbContext context) : BaseBankAccountRepository<CreditBankAccount>(context)
{
    public override async Task<Result> Update(CreditBankAccount entity)
    {
        try
        {
            await _context.CreditBankAccounts
                .Where(db => db.Id == entity.Id)
                .ExecuteUpdateAsync(db =>
                    db.SetProperty(e => e.Name, entity.Name)
                        .SetProperty(e => e.Color, entity.Color)
                        .SetProperty(e => e.Balance, entity.Balance)
                        .SetProperty(e => e.Currency, entity.Currency)
                        .SetProperty(e => e.Type, entity.Type)
                        .SetProperty(e => e.Amount, entity.Amount)
                        .SetProperty(e => e.InitPayment, entity.InitPayment)
                        .SetProperty(e => e.Term, entity.Term)
                        .SetProperty(e => e.DateRange, entity.DateRange)
                        .SetProperty(e => e.LoanObject, entity.LoanObject)
                        .SetProperty(e => e.PurposeLoan, entity.PurposeLoan)
                        .SetProperty(e => e.TypeCredit, entity.TypeCredit)
                        
                        .SetProperty(e => e.UpdatedAt, DateTime.UtcNow)
                );
            
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
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