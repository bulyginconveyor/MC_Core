using System.Linq.Expressions;
using core_service.application.rest_api.DTO;
using core_service.domain.logic.filters.bank_account;
using core_service.domain.logic.filters.bank_account.active;
using core_service.domain.models;
using core_service.domain.models.enums;
using core_service.domain.models.valueobjects;
using core_service.infrastructure.repository.enums;
using core_service.infrastructure.repository.interfaces;
using core_service.services.ExpressionHelpers;
using core_service.services.Result;
using Mono.TextTemplating;

namespace core_service.domain.logic;

public class ActiveBankAccountLogic(IDbRepository<ActiveBankAccount> rep, IDbRepository<Currency> repCurrency)
{
    private IDbRepository<ActiveBankAccount> _rep = rep;
    private IDbRepository<Currency> _repCurrency = repCurrency; 
    
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

    public async Task<Result> Add(DataDTOActiveBankAccount dataDto)
    {
        var resCurrency = await _repCurrency.GetOne(dataDto.CurrencyId);
        if(resCurrency.IsError)
            return Result.Error(resCurrency.ErrorMessage);
        
        UDecimal buyPrice = UDecimal.Parse(dataDto.BuyPrice);
        TypeActiveBankAccount typeActive = (TypeActiveBankAccount)Enum.Parse(typeof(TypeActiveBankAccount), dataDto.TypeActive);
        PhotoUrl photoUrl = dataDto.PhotoUrl is null ? PhotoUrl.Empty : PhotoUrl.Create(dataDto.PhotoUrl);

        Active active = new Active(buyPrice, dataDto.BuyDate, typeActive, photoUrl);
        
        ActiveBankAccount dto = new ActiveBankAccount(dataDto.Name, dataDto.Color, resCurrency.Value!, active);
        
        var resAdd = await _rep.Add(dto);
        if(resAdd.IsError)
            return Result.Error(resAdd.ErrorMessage!);
        
        var resSave = await _rep.Save();
        if(resSave.IsError)
            return Result.Error(resSave.ErrorMessage!);
        
        return Result.Success();
    }

    public async Task<Result> Update(DataDTOActiveBankAccount dataDto)
    {
        if(dataDto.Id is null)
            return Result.Error("Id is null");
        
        var resCurrency = await _repCurrency.GetOne(dataDto.CurrencyId);
        if(resCurrency.IsError)
            return Result.Error(resCurrency.ErrorMessage);
        
        UDecimal buyPrice = UDecimal.Parse(dataDto.BuyPrice);
        TypeActiveBankAccount typeActive = (TypeActiveBankAccount)Enum.Parse(typeof(TypeActiveBankAccount), dataDto.TypeActive);
        PhotoUrl photoUrl = dataDto.PhotoUrl is null ? PhotoUrl.Empty : PhotoUrl.Create(dataDto.PhotoUrl);

        Active active = new Active(buyPrice, dataDto.BuyDate, typeActive, photoUrl);
        
        ActiveBankAccount dto = new ActiveBankAccount((Guid)dataDto.Id, dataDto.Name, dataDto.Color, resCurrency.Value!, active);
        
        var resUpdate = await _rep.Update(dto);
        if(resUpdate.IsError)
            return Result.Error(resUpdate.ErrorMessage!);
        
        var resSave = await _rep.Save();
        if(resSave.IsError)
            return Result.Error(resSave.ErrorMessage!);
        
        return Result.Success();
    }
    
}




