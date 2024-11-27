using core_service.domain;
using core_service.infrastructure.repository.postgresql.context;
using core_service.infrastructure.repository.postgresql.repositories.@base;
using core_service.services.Result;
using Microsoft.EntityFrameworkCore;

namespace core_service.infrastructure.repository.postgresql.repositories;

public class ActiveBankAccountRepository(PostgreSqlDbContext context) : BaseBankAccountRepository<ActiveBankAccount>(context)
{
    public override async Task<Result> Update(ActiveBankAccount entity)
    {
        try
        {
            await _context.ActiveBankAccounts
                .Where(db => db.Id == entity.Id)
                .ExecuteUpdateAsync(db =>
                    db.SetProperty(e => e.Name, entity.Name)
                        .SetProperty(e => e.Color, entity.Color)
                        .SetProperty(e => e.Balance, entity.Balance)
                        .SetProperty(e => e.Currency, entity.Currency)
                        .SetProperty(e => e.Type, entity.Type)
                        .SetProperty(e => e.BuyPrice, entity.BuyPrice)
                        .SetProperty(e => e.BuyDate, entity.BuyDate)
                        .SetProperty(e => e.TypeActive, entity.TypeActive)
                        .SetProperty(e => e.PhotoUrl, entity.PhotoUrl)
                        .SetProperty(e => e.UpdatedAt, DateTime.UtcNow)
                );
            
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}