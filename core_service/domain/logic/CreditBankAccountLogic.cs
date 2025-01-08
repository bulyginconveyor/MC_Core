using core_service.application.rest_controllers.DTO;
using core_service.domain.logic.filters.bank_account.contribution;
using core_service.domain.logic.filters.bank_account.credit;
using core_service.domain.models;
using core_service.infrastructure.repository.enums;
using core_service.infrastructure.repository.interfaces;
using core_service.services.Result;

namespace core_service.domain.logic;

public class CreditBankAccountLogic(IDbRepository<CreditBankAccount> rep)
{
    private IDbRepository<CreditBankAccount> _rep = rep;
    
    public async Task<Result<List<DTOCreditBankAccount>>> GetAll(CreditBankAccountFilter? filter)
    {
        var resGet = filter == null ? 
            await _rep.GetAll() 
            : await _rep.GetAll(filter.ToExpression());
        
        if(resGet.IsError)
            return Result<List<DTOCreditBankAccount>>.Error(new List<DTOCreditBankAccount>(), resGet.ErrorMessage);

        return Result<List<DTOCreditBankAccount>>.Success(resGet.Value!.Select(e => (DTOCreditBankAccount)e!).ToList());
    }
    
    public async Task<Result<DTOCreditBankAccount>> GetOneById(Guid id)
    {
        var resGet = await _rep.GetOne(id, Tracking.No);
        if(resGet.Value is null)
            return Result<DTOCreditBankAccount>.Error(null!, "Not found");
        
        return Result<DTOCreditBankAccount>.Success(resGet.Value!);
    }

    public async Task<Result> Add(DTOCreditBankAccount dto)
    {
        var resAdd = await _rep.Add(dto);
        if(resAdd.IsError)
            return Result.Error(resAdd.ErrorMessage!);
        
        return Result.Success();
    }

    public async Task<Result> Update(DTOCreditBankAccount dto)
    {
        var resUpdate = await _rep.Update(dto);
        if(resUpdate.IsError)
            return Result.Error(resUpdate.ErrorMessage!);
        
        return Result.Success();
    }
}
