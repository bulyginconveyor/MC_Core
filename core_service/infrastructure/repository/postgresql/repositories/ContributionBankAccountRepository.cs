using core_service.domain;
using core_service.infrastructure.repository.postgresql.context;
using core_service.infrastructure.repository.postgresql.repositories.@base;
using core_service.services.Result;
using Microsoft.EntityFrameworkCore;

namespace core_service.infrastructure.repository.postgresql.repositories;

public class ContributionBankAccountRepository(PostgreSqlDbContext context) : BaseBankAccountRepository<СontributionBankAccount>(context)
{
    public override async Task<Result> Update(СontributionBankAccount entity)
    {
        try
        {
            await _context.СontributionBankAccounts
                .Where(db => db.Id == entity.Id)
                .ExecuteUpdateAsync(db =>
                    db.SetProperty(e => e.Name, entity.Name)
                        .SetProperty(e => e.Color, entity.Color)
                        .SetProperty(e => e.Balance, entity.Balance)
                        .SetProperty(e => e.Currency, entity.Currency)
                        .SetProperty(e => e.Type, entity.Type)
                        .SetProperty(e => e.DateRange, entity.DateRange)
                        .SetProperty(e => e.ActualСlosed, entity.ActualСlosed)
                        .SetProperty(e => e.Amount, entity.Amount)
                        .SetProperty(e => e.TypeContribution, entity.TypeContribution)
                        .SetProperty(e => e.Percent, entity.Percent)
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