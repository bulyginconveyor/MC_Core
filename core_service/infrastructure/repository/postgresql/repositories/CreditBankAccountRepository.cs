using core_service.domain;
using core_service.infrastructure.repository.postgresql.context;
using core_service.infrastructure.repository.postgresql.repositories.@base;
using core_service.services.Result;
using Microsoft.EntityFrameworkCore;

namespace core_service.infrastructure.repository.postgresql.repositories;

public class CreditBankAccountRepository(DbContext context) : BaseBankAccountRepository<CreditBankAccount>(context)
{
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