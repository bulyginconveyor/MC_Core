using core_service.application.rest_api.DTO;
using core_service.domain.models;
using core_service.infrastructure.repository.enums;
using core_service.infrastructure.repository.interfaces;
using core_service.services.Result;

namespace core_service.domain.logic;

public class CurrencyLogic(IDbRepository<Currency> rep, ICacheRepositoryWithLists<DTOCurrency> cache)
{
    private IDbRepository<Currency> _rep = rep;
    private ICacheRepositoryWithLists<DTOCurrency> _cache = cache; //TODO: Реализовать Cache

    public async Task<Result<IEnumerable<DTOCurrency>>> GetAll()
    {
        IEnumerable<DTOCurrency>? resGet = null;
        
        // По-моему, фигня идея - хранить всё-всё в кеше... не напасусь я столько ОЗУ
        // ОЗУ напасусь, но, сука, прироста 0! Полный 0!!! Entity, тварь, похоже сразу всё кеширует
        /* var resGetCache = await _cache.GetAll();
        if (resGetCache.IsSuccess && resGetCache.Value.Any())
            return Result<IEnumerable<DTOCurrency>>.Success(resGetCache.Value);
        */
        
        var resGetRep = await _rep.GetAll(Tracking.No);
        
        if(resGetRep.IsError)
            return Result<IEnumerable<DTOCurrency>>.Error(null, resGetRep.ErrorMessage);

        var listDtos = resGetRep.Value.ToList()
                .Select(x => (DTOCurrency)x);
        
        // await _cache.Add(listDtos.ToList());
        
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
