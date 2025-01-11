using System.Linq.Expressions;
using core_service.application.rest_api.DTO;
using core_service.domain.logic.filters.bank_account;
using core_service.domain.models;
using core_service.infrastructure.repository.enums;
using core_service.infrastructure.repository.interfaces;
using core_service.services.ExpressionHelpers;
using core_service.services.Result;

namespace core_service.domain.logic;

public class BankAccountLogic(IDbRepository<BankAccount> rep)
{
    private IDbRepository<BankAccount> _rep = rep;

    public async Task<Result<List<DTOBankAccount>>> GetAll(BankAccountFilter<BankAccount>? filter = null)
    {
        var resGet = filter is null
            ? await _rep.GetAll(Tracking.No)
            : await _rep.GetAll(filter?.ToExpression(), Tracking.No);
        
        if(resGet.IsError)
            return Result<List<DTOBankAccount>>.Error(new List<DTOBankAccount>(), resGet.ErrorMessage);

        return Result<List<DTOBankAccount>>.Success(resGet.Value!.Select(e => (DTOBankAccount)e!).ToList());
    }

    public async Task<Result<DTOBankAccount>> GetOneById(Guid id)
    {
        var resGet = await _rep.GetOne(id, Tracking.No);
        if(resGet.Value is null)
            return Result<DTOBankAccount>.Error(null, "Not found");
        
        return Result<DTOBankAccount>.Success(resGet.Value!);
    }

    public async Task<Result> SoftDelete(Guid id) => await _rep.Delete(id);
}




