using System.Linq.Expressions;
using core_service.application.rest_controllers.DTO;
using core_service.domain.logic.filters.bank_account;
using core_service.domain.logic.filters.bank_account.active;
using core_service.domain.models;
using core_service.infrastructure.repository.enums;
using core_service.infrastructure.repository.interfaces;
using core_service.services.ExpressionHelpers;
using core_service.services.Result;

namespace core_service.domain.logic;

public class ActiveBankAccountLogic(IDbRepository<ActiveBankAccount> rep)
{
    private IDbRepository<ActiveBankAccount> _rep = rep;

    public async Task<Result<List<DTOActiveBankAccount>>> GetAll(ActiveBankAccountFilter? filter = null)
    {
        var resGet = filter is null
            ? await _rep.GetAll(Tracking.No)
            : await _rep.GetAll(filter!.ToExpression(), Tracking.No);
        
        if(resGet.IsError)
            return Result<List<DTOActiveBankAccount>>.Error(new List<DTOActiveBankAccount>(), resGet.ErrorMessage);

        return Result<List<DTOActiveBankAccount>>.Success(resGet.Value!.Select(e => (DTOActiveBankAccount)e!).ToList());
    }
    
    public async Task<Result<DTOActiveBankAccount>> GetOneById(Guid id)
    {
        var resGet = await _rep.GetOne(id, Tracking.No);
        if(resGet.Value is null)
            return Result<DTOActiveBankAccount>.Error(null!, "Not found");
        
        return Result<DTOActiveBankAccount>.Success(resGet.Value!);
    }

    public async Task<Result> Add(DTOActiveBankAccount dto)
    {
        var resAdd = await _rep.Add(dto);
        if(resAdd.IsError)
            return Result.Error(resAdd.ErrorMessage!);
        
        var resSave = await _rep.Save();
        if(resSave.IsError)
            return Result.Error(resSave.ErrorMessage!);
        
        return Result.Success();
    }

    public async Task<Result> Update(DTOActiveBankAccount dto)
    {
        var resUpdate = await _rep.Update(dto);
        if(resUpdate.IsError)
            return Result.Error(resUpdate.ErrorMessage!);
        
        var resSave = await _rep.Save();
        if(resSave.IsError)
            return Result.Error(resSave.ErrorMessage!);
        
        return Result.Success();
    }
    
}




