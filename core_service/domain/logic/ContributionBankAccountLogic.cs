using core_service.application.rest_controllers.DTO;
using core_service.domain.logic.filters.bank_account.contribution;
using core_service.domain.models;
using core_service.infrastructure.repository.enums;
using core_service.infrastructure.repository.interfaces;
using core_service.services.Result;

namespace core_service.domain.logic;

public class ContributionBankAccountLogic(IDbRepository<ContributionBankAccount> rep)
{
    private IDbRepository<ContributionBankAccount> _rep = rep;

    public async Task<Result<List<DTOContributionBankAccount>>> GetAll(ContributionBankAccountFilter? filter)
    {
        var resGet = filter == null ? 
            await _rep.GetAll() 
            : await _rep.GetAll(filter.ToExpression());
        
        if(resGet.IsError)
            return Result<List<DTOContributionBankAccount>>.Error(new List<DTOContributionBankAccount>(), resGet.ErrorMessage);

        return Result<List<DTOContributionBankAccount>>.Success(resGet.Value!.Select(e => (DTOContributionBankAccount)e!).ToList());
    }
    
    public async Task<Result<DTOContributionBankAccount>> GetOneById(Guid id)
    {
        var resGet = await _rep.GetOne(id, Tracking.No);
        if(resGet.Value is null)
            return Result<DTOContributionBankAccount>.Error(null!, "Not found");
        
        return Result<DTOContributionBankAccount>.Success(resGet.Value!);
    }

    public async Task<Result> Add(DTOContributionBankAccount dto)
    {
        var resAdd = await _rep.Add(dto);
        if(resAdd.IsError)
            return Result.Error(resAdd.ErrorMessage!);
        
        var resSave = await _rep.Save();
        if(resSave.IsError)
            return Result.Error(resSave.ErrorMessage!);
        
        return Result.Success();
    }

    public async Task<Result> Update(DTOContributionBankAccount dto)
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
