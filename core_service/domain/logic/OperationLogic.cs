using System.Linq.Expressions;
using core_service.application.rest_api.DTO;
using core_service.domain.logic.filters.operation;
using core_service.domain.models;
using core_service.domain.models.valueobjects;
using core_service.domain.models.valueobjects.enums;
using core_service.infrastructure.repository.enums;
using core_service.infrastructure.repository.interfaces;
using core_service.services.ExpressionHelpers;
using core_service.services.Result;

namespace core_service.domain.logic;

public class OperationLogic(
    IDbRepository<Operation> rep,
    IDbRepository<BankAccount> repBankAccount,
    IDbRepository<Category> repCategory)
{
    private IDbRepository<Operation> _rep = rep;
    private IDbRepository<BankAccount> _repBankAccount = repBankAccount;
    private IDbRepository<Category> _repCategory = repCategory;

    public async Task<Result<uint>> GetCountPages(uint countPerPage, OperationFilter? filter = null)
    {
        var countRes = filter is null
            ? await _rep.PagesCount(countPerPage)
            : await _rep.PagesCount(countPerPage, filter?.ToExpression());
        if(countRes.IsError)
            return Result<uint>.Error(0, countRes.ErrorMessage);
        
        return Result<uint>.Success((uint)countRes.Value);
    }

    public async Task<Result<List<DTOOperation>>> GetByPage(uint countPerPag, uint pageNumber, OperationFilter? filter = null)
    {
        var res = filter is null
            ? await _rep.GetByPage(countPerPag, pageNumber)
            : await _rep.GetByPage(countPerPag, pageNumber, filter?.ToExpression());
        
        if(res.IsError)
            return Result<List<DTOOperation>>.Error(new List<DTOOperation>(), res.ErrorMessage);

        var list = res.Value!.Select(e => (DTOOperation)e!).ToList(); 
        return Result<List<DTOOperation>>.Success(list);
    }

    public async Task<Result<DTOOperation>> GetById(Guid id)
    {
        var operation = await _rep.GetOne(id, Tracking.No);
        if(operation.Value is null)
            return Result<DTOOperation>.Error(null, "Not found");
        
        return Result<DTOOperation>.Success(operation.Value);
    }

    public async Task<Result> Add(DataDTOOperation dataDto)
    {
        Name name = Name.Create(dataDto.Name!);
        UDecimal amount = UDecimal.Parse(dataDto.Amount);
        var period = dataDto.PeriodType is null || dataDto.PeriodValue is null 
            ? null
            : Period.Create((TypePeriod)Enum.Parse(typeof(TypePeriod), dataDto.PeriodType!), (ushort)dataDto.PeriodValue!);
        var credit = dataDto.CreditBankAccountId is null
            ? null
            : await _repBankAccount.GetOne((Guid)dataDto.CreditBankAccountId);
        var debet = dataDto.DebetBankAccountId is null
            ? null
            : await _repBankAccount.GetOne((Guid)dataDto.DebetBankAccountId);
        var category = dataDto.CategoryId is null
            ? null
            : await _repCategory.GetOne((Guid)dataDto.CategoryId);

        Operation dto = new Operation(
            name, 
            dataDto.Date, 
            amount, 
            period, 
            credit?.Value ?? null, 
            debet?.Value ?? null, 
            category?.Value ?? null);
        await _rep.Add(dto);
        
        return await _rep.Save();
    }

    public async Task<Result> Update(DataDTOOperation dataDto)
    {
        if(dataDto.Id is null)
            return Result.Error("Id is null");
        
        Name name = Name.Create(dataDto.Name!);
        UDecimal amount = UDecimal.Parse(dataDto.Amount);
        var period = dataDto.PeriodType is null || dataDto.PeriodValue is null 
            ? null
            : Period.Create((TypePeriod)Enum.Parse(typeof(TypePeriod), dataDto.PeriodType!), (ushort)dataDto.PeriodValue!);
        var credit = dataDto.CreditBankAccountId is null
            ? null
            : await _repBankAccount.GetOne((Guid)dataDto.CreditBankAccountId);
        var debet = dataDto.DebetBankAccountId is null
            ? null
            : await _repBankAccount.GetOne((Guid)dataDto.DebetBankAccountId);
        var category = dataDto.CategoryId is null
            ? null
            : await _repCategory.GetOne((Guid)dataDto.CategoryId);
        
        Operation dto = new Operation(
            (Guid)dataDto.Id,
            name, 
            dataDto.Date, 
            amount, 
            period, 
            credit?.Value ?? null, 
            debet?.Value ?? null, 
            category?.Value ?? null);
        
        var resUpdate = await _rep.Update(dto);
        if(resUpdate.IsError)
            return Result.Error("Can't update operation");
        
        return await _rep.Save();
    }
    
    public async Task<Result> SoftDeleteById(Guid id) => await _rep.Delete(id);
}




