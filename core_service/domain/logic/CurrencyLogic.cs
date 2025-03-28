using core_service.application.rest_api.DTO;
using core_service.domain.models;
using core_service.infrastructure.repository.interfaces;
using core_service.services.Result;

namespace core_service.domain.logic;

public class CurrencyLogic(IDbRepository<Currency> rep)
{
    private IDbRepository<Currency> _rep = rep;

    public async Task<Result<IEnumerable<DTOCurrency>>> GetAll()
    {
        IEnumerable<DTOCurrency>? resGet = null;
        
        var resGetRep = await _rep.GetAll();
        
        if(resGetRep.IsError)
            return Result<IEnumerable<DTOCurrency>>.Error(null, resGetRep.ErrorMessage);

        var listDtos = resGetRep.Value.ToList()
                .Select(x => (DTOCurrency)x);
        
        return Result<IEnumerable<DTOCurrency>>.Success(listDtos);
    }

    public async Task<Result> Add(DTOCurrency currency)
    {
        await _rep.Add(currency);
        var resSave = await _rep.Save();
        
        return resSave;
    }

    public async Task<Result> Update(DTOCurrency currency)
    {
        await _rep.Update(currency);
        var resSave = await _rep.Save();

        return resSave;
    }

    public async Task<Result> SoftDeleteById(Guid currencyId) => await _rep.Delete(currencyId);
}
