using core_service.application.rest_controllers.DTO;
using core_service.domain.models;
using core_service.infrastructure.repository.enums;
using core_service.infrastructure.repository.interfaces;
using core_service.services.Result;

namespace core_service.domain.logic;

public class CategoryLogic(IDbRepository<Category> rep)
{
    private IDbRepository<Category> _rep = rep;

    public async Task<Result<List<DTOCategory>>> GetAll(string filterName = null)
    {
        var resGet = string.IsNullOrEmpty(filterName)
            ? await _rep.GetAll(Tracking.No)
            : await _rep.GetAll(c => c.Name.Value.ToLower().Contains(filterName.ToLower()), Tracking.No);
        
        if(resGet.IsError)
            return Result<List<DTOCategory>>.Error(null, resGet.ErrorMessage);

        return Result<List<DTOCategory>>.Success(
            resGet.Value.ToList()
                .Select(x => (DTOCategory)x)
                .ToList()
            );
    }

    public async Task<Result<DTOCategory>> GetById(Guid id)
    {
        var resGet = await _rep.GetOne(id, Tracking.No);
        
        if(resGet.IsError)
            return Result<DTOCategory>.Error(null, resGet.ErrorMessage);

        return Result<DTOCategory>.Success(resGet.Value);
    }

    public async Task<Result> Add(DTOCategory dto)
    {
        await _rep.Add(dto);
        var resSave = await _rep.Save();

        return resSave;
    }

    public async Task<Result> Update(DTOCategory dto)
    {
        await _rep.Update(dto);
        var resSave = await _rep.Save();

        return resSave;
    }
    
    public async Task<Result> SoftDeleteById(Guid id) => await _rep.Delete(id);
}
