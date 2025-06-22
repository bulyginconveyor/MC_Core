using core_service.application.rest_api.DTO;
using core_service.domain.logic.filters.bank_account.contribution;
using core_service.domain.logic.filters.bank_account.credit;
using core_service.domain.models;
using core_service.domain.models.enums;
using core_service.domain.models.valueobjects;
using core_service.domain.models.valueobjects.enums;
using core_service.infrastructure.repository.interfaces;
using core_service.services.Result;

namespace core_service.domain.logic;

public class CreditBankAccountLogic(
    IDbRepository<CreditBankAccount> rep, 
    IDbRepository<Currency> repCurrency, 
    IDbRepository<ActiveBankAccount> repActiveBankAccount
    )
{
    private IDbRepository<CreditBankAccount> _rep = rep;
    private IDbRepository<Currency> _repCurrency = repCurrency;
    private IDbRepository<ActiveBankAccount> _repActiveBankAccount = repActiveBankAccount;
    
    public async Task<Result<List<DTOCreditBankAccount>>> GetAll(CreditBankAccountFilter? filter)
    {
        var resGet = filter == null ? 
            await _rep.GetAll() 
            : await _rep.GetAll(filter.ToExpression());
        
        if(resGet.IsError)
            return Result<List<DTOCreditBankAccount>>.Error(new List<DTOCreditBankAccount>(), resGet.ErrorMessage);

        return Result<List<DTOCreditBankAccount>>.Success(resGet.Value.Select(e => (DTOCreditBankAccount)e).ToList());
    }
    
    public async Task<Result<DTOCreditBankAccount>> GetOneById(Guid id)
    {
        var resGet = await _rep.GetOne(id);
        if(resGet.Value is null)
            return Result<DTOCreditBankAccount>.Error(null!, "Not found");
        
        return Result<DTOCreditBankAccount>.Success(resGet.Value!);
    }

    public async Task<Result> Add(DataDTOCreditBankAccount dataDto, Guid userId)
    {
        var resCurrency = await _repCurrency.GetOne(dataDto.CurrencyId);
        if(resCurrency.IsError)
            return Result.Error(resCurrency.ErrorMessage!);

        UDecimal amount = UDecimal.Parse(dataDto.Amount);
        UDecimal initPayment = UDecimal.Parse(dataDto.InitPayment);
        Percent percent = (dataDto.Percent is not null && dataDto.Percent > 0) ? Percent.Create((decimal)dataDto.Percent) : Percent.Zero;
        
        UnitTerm unitTerm;
        Term term;
        if (dataDto.Unit is not null)
        {
            unitTerm = (UnitTerm)Enum.Parse(typeof(UnitTerm), dataDto.Unit);
            term = Term.Create(unitTerm, dataDto.CountUnits ?? 0);
        }
        else
            term = null;
        
        TypeCreditBankAccount typeCredit = (TypeCreditBankAccount)Enum.Parse(typeof(TypeCreditBankAccount), dataDto.TypeCredit);
        var resLoanObject = dataDto.LoanObjectId is null 
            ? null 
            : await _repActiveBankAccount.GetOne((Guid)dataDto.LoanObjectId);
        var loanObject = resLoanObject?.Value ?? null;
        Name purposeLoan = dataDto.PurposeLoan is null 
            ? Name.Empty
            : Name.Create(dataDto.PurposeLoan);
        
        Loan loan = new Loan(amount, initPayment, dataDto.StartDate, typeCredit, loanObject, purposeLoan, percent, term);
        
        CreditBankAccount dto =
            new CreditBankAccount(userId, dataDto.Name, dataDto.Color, resCurrency.Value!, loan, dataDto.Balance);
        
        var resAdd = await _rep.Add(dto);
        if(resAdd.IsError)
            return Result.Error(resAdd.ErrorMessage!);
        
        var resSave = await _rep.Save();
        if(resSave.IsError)
            return Result.Error(resSave.ErrorMessage!);
        
        return Result.Success();
    }

    public async Task<Result> Update(DataDTOCreditBankAccount dataDto)
    {
        if(dataDto.Id is null)
            return Result.Error("Id is null");
        
        var resCurrency = await _repCurrency.GetOne(dataDto.CurrencyId);
        if(resCurrency.IsError)
            return Result.Error(resCurrency.ErrorMessage!);

        UDecimal amount = UDecimal.Parse(dataDto.Amount);
        UDecimal initPayment = UDecimal.Parse(dataDto.InitPayment);
        Percent percent = (dataDto.Percent is not null && dataDto.Percent > 0) ? Percent.Create((decimal)dataDto.Percent) : Percent.Zero;

        UnitTerm unitTerm;
        Term term;
        if (dataDto.Unit is not null)
        {
            unitTerm = (UnitTerm)Enum.Parse(typeof(UnitTerm), dataDto.Unit);
            term = Term.Create(unitTerm, dataDto.CountUnits ?? 0);
        }
        else
            term = null;

        TypeCreditBankAccount typeCredit = (TypeCreditBankAccount)Enum.Parse(typeof(TypeCreditBankAccount), dataDto.TypeCredit);
        var resLoanObject = dataDto.LoanObjectId is null 
            ? null 
            : await _repActiveBankAccount.GetOne((Guid)dataDto.LoanObjectId);
        var loanObject = resLoanObject?.Value ?? null;
        Name purposeLoan = dataDto.PurposeLoan is null 
            ? Name.Empty
            : Name.Create(dataDto.PurposeLoan);
        
        Loan loan = new Loan(amount, initPayment, dataDto.StartDate, typeCredit, loanObject, purposeLoan, percent, term);
        
        CreditBankAccount dto = new CreditBankAccount(
            (Guid)dataDto.Id, 
            dataDto.Name, 
            dataDto.Color, 
            resCurrency.Value!, 
            loan, 
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
