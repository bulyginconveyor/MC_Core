using core_service.domain;
using core_service.infrastructure.repository.postgresql.context;
using core_service.infrastructure.repository.postgresql.repositories.@base;
using core_service.services.Result;
using Microsoft.EntityFrameworkCore;

namespace core_service.infrastructure.repository.postgresql.repositories;

public class CurrencyRepository(PostgreSqlDbContext context) : BaseRepository<Currency>(context)
{
    public override async Task<Result> Update(Currency entity)
    {
        try
        {
            await _context.Currencies
                .Where(c => c.Id == entity.Id)
                .ExecuteUpdateAsync(
                    c => c
                        .SetProperty(c => c.FullName, entity.FullName)
                        .SetProperty(c => c.IsoCode, entity.IsoCode)
                        .SetProperty(c => c.ImageUrl, entity.ImageUrl)
                        .SetProperty(c => c.UpdatedAt, DateTime.UtcNow)
                );
            
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }

    public override Task<Result<Currency>> LoadData(Currency entity) => Task.FromResult(Result<Currency>.Success(entity));
}