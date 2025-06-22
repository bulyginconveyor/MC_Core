using core_service.application.rest_api.DTO;
using core_service.domain.logic.filters.bank_account.contribution;
using core_service.domain.models;
using core_service.domain.models.enums;
using core_service.domain.models.valueobjects;
using core_service.infrastructure.repository.interfaces;
using core_service.services.Result;

namespace core_service.domain.logic;

public class ContributionBankAccountLogic(IDbRepository<ContributionBankAccount> rep, IDbRepository<Currency> repCurrency)
{
    private IDbRepository<ContributionBankAccount> _rep = rep;
    private IDbRepository<Currency> _repCurrency = repCurrency;

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
        var resGet = await _rep.GetOne(id);
        if(resGet.Value is null)
            return Result<DTOContributionBankAccount>.Error(null!, "Not found");
        
        return Result<DTOContributionBankAccount>.Success(resGet.Value!);
    }

    public async Task<Result> Add(DataDTOContributionBankAccount dataDto, Guid userId)
    {
        var resCurrency = await _repCurrency.GetOne(dataDto.CurrencyId);
        if(resCurrency.IsError)
            return Result.Error(resCurrency.ErrorMessage!);
        
        DateRange dateRange = DateRange.Create(dataDto.StartDate, dataDto.EndDate);
        UDecimal amount = UDecimal.Parse(dataDto.Amount);
        TypeContributionBankAccount typeContribution = (TypeContributionBankAccount)Enum.Parse(typeof(TypeContributionBankAccount), dataDto.TypeContribution);
        PercentContribution? percentContribution = dataDto.Percent.HasValue && dataDto.CountDaysForPercent.HasValue ? 
            PercentContribution.Create(UDecimal.Parse(dataDto.Percent.Value), dataDto.CountDaysForPercent.Value) 
            : null;

        Contribution contribution = Contribution.Create(dateRange, amount, typeContribution, dataDto.ActualClosedDate, percentContribution);
        
        var dto = new ContributionBankAccount(userId, dataDto.Name, dataDto.Color, resCurrency.Value!, contribution, true, dataDto.Balance);
        
        var resAdd = await _rep.Add(dto);
        if(resAdd.IsError)
            return Result.Error(resAdd.ErrorMessage!);
        
        var resSave = await _rep.Save();
        if(resSave.IsError)
            return Result.Error(resSave.ErrorMessage!);
        
        return Result.Success();
    }

    public async Task<Result> Update(DataDTOContributionBankAccount dataDto)
    {
        if(dataDto.Id == null)
            return Result.Error("Id is null");
        
        var resCurrency = await _repCurrency.GetOne(dataDto.CurrencyId);
        if(resCurrency.IsError)
            return Result.Error(resCurrency.ErrorMessage!);
        
        DateRange dateRange = DateRange.Create(dataDto.StartDate, dataDto.EndDate);
        UDecimal amount = UDecimal.Parse(dataDto.Amount);
        TypeContributionBankAccount typeContribution = 
            (TypeContributionBankAccount)Enum.Parse(typeof(TypeContributionBankAccount), dataDto.TypeContribution);
        PercentContribution? percentContribution = dataDto.Percent.HasValue && dataDto.CountDaysForPercent.HasValue ? 
            PercentContribution.Create(UDecimal.Parse(dataDto.Percent.Value), dataDto.CountDaysForPercent.Value) 
            : null;

        Contribution contribution = Contribution.Create(
            dateRange, 
            amount, 
            typeContribution, 
            dataDto.ActualClosedDate, 
            percentContribution);
        
        var dto = new ContributionBankAccount(
            (Guid)dataDto.Id, 
            dataDto.Name, 
            dataDto.Color, 
            resCurrency.Value!, 
            contribution, 
            true, 
            dataDto.Balance);
        
        var resUpdate = await _rep.Update(dto);
        if(resUpdate.IsError)
            return Result.Error(resUpdate.ErrorMessage!);
        
        var resSave = await _rep.Save();
        if(resSave.IsError)
            return Result.Error(resSave.ErrorMessage!);
        
        return Result.Success();
    }
}
