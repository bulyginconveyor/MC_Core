using System.Linq.Expressions;
using core_service.application.rest_controllers.DTO;
using core_service.domain.logic.filters.bank_account.debet;
using core_service.domain.models;
using core_service.infrastructure.repository.enums;
using core_service.infrastructure.repository.interfaces;
using core_service.services.Result;

namespace core_service.domain.logic;

public class DebetBankAccountLogic(IDbRepository<DebetBankAccount> rep)
{
    private IDbRepository<DebetBankAccount> _rep = rep;

    public async Task<Result<List<DTODebetBankAccount>>> GetAll(DebetBankAccountFilter? filter = null)
    {
        var resGet = filter is null
            ? await _rep.GetAll(Tracking.No)
            : await _rep.GetAll(filter?.ToExpression(), Tracking.No);
        
        if(resGet.IsError)
            return Result<List<DTODebetBankAccount>>.Error(new (), resGet.ErrorMessage);

        var dtos = resGet.Value!.Select(e => (DTODebetBankAccount)e!).ToList();
        
        return Result<List<DTODebetBankAccount>>.Success(dtos);
    }

    public async Task<Result<DTODebetBankAccount>> GetOneById(Guid id)
    {
        var resGet = await _rep.GetOne(id, Tracking.No);
        if(resGet.Value is null)
            return Result<DTODebetBankAccount>.Error(null, "Not found");
        
        return Result<DTODebetBankAccount>.Success(resGet.Value);
    }

    public async Task<Result> Add(DTODebetBankAccount dto)
    {
        var resAdd = await _rep.Add(dto);
        if(resAdd.IsError)
            return Result.Error(resAdd.ErrorMessage);
        
        var resSave = await _rep.Save();
        if(resSave.IsError)
            return Result.Error(resSave.ErrorMessage);
        
        return Result.Success();
    }

    public async Task<Result> Update(DTODebetBankAccount dto)
    {
        var resUpdate = await _rep.Update(dto);
        if(resUpdate.IsError)
            return Result.Error(resUpdate.ErrorMessage);
        
        var resSave = await _rep.Save();
        if(resSave.IsError)
            return Result.Error(resSave.ErrorMessage);
        
        return Result.Success();
    }
}




