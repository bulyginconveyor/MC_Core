using core_service.application.rest_api.DTO;
using core_service.domain.models;
using core_service.infrastructure.repository.interfaces;
using core_service.infrastructure.repository.postgresql.models;
using core_service.services.Result;

namespace core_service.domain.logic;

public class CategoryLogic(IDbRepository<Category> rep, IRepositoryForHiddenCategory<HiddenCategory> repHiddenCategory)
{
    private IDbRepository<Category> _rep = rep;
    private IRepositoryForHiddenCategory<HiddenCategory> _repHiddenCategory = repHiddenCategory;

    public async Task<Result<List<DTOCategory>>> GetAll(string filterName = null)
    {
        var resGet = string.IsNullOrEmpty(filterName)
            ? await _rep.GetAll()
            : await _rep.GetAll(c => c.Name.Value.ToLower().Contains(filterName.ToLower()));
        
        if(resGet.IsError)
            return Result<List<DTOCategory>>.Error(null, resGet.ErrorMessage);

        return Result<List<DTOCategory>>.Success(
            resGet.Value.ToList()
                .Select(x => (DTOCategory)x)
                .ToList()
            );
    }

    public async Task<Result<List<DTOCategory>>> GetAllByUserId(Guid userId, string filtername = null)
    {
        var resHiddenCategories = await _repHiddenCategory.GetAll(userId);
        if(resHiddenCategories.IsError)
            return Result<List<DTOCategory>>.Error(null!, resHiddenCategories.ErrorMessage);
        
        var resGet = string.IsNullOrEmpty(filtername)
            ? await _rep.GetAll(c => 
                !resHiddenCategories.Value!.Select(hc => hc.CategoryId).Contains(c.Id) 
                && c.Name.Value!.ToLower().Contains(filtername.ToLower()))
            : await _rep.GetAll(c => !resHiddenCategories.Value!.Select(hc => hc.CategoryId).Contains(c.Id));
        
        if(resGet.IsError)
            return Result<List<DTOCategory>>.Error(null!, resGet.ErrorMessage);

        return Result<List<DTOCategory>>.Success(
            resGet.Value!.ToList()
                .Select(x => (DTOCategory)x!)
                .ToList()
            );
    }
    
    public async Task<Result<List<DTOCategory>>> GetAllHiddenByUserId(Guid userId, string filtername = null)
    {
        var resHiddenCategories = await _repHiddenCategory.GetAll(userId);
        if(resHiddenCategories.IsError)
            return Result<List<DTOCategory>>.Error(null!, resHiddenCategories.ErrorMessage);
        
        var resGet = string.IsNullOrEmpty(filtername)
            ? await _rep.GetAll(c => 
                resHiddenCategories.Value!.Select(hc => hc.CategoryId).Contains(c.Id) 
                && c.Name.Value!.ToLower().Contains(filtername.ToLower()))
            : await _rep.GetAll(c => resHiddenCategories.Value!.Select(hc => hc.CategoryId).Contains(c.Id));
        
        if(resGet.IsError)
            return Result<List<DTOCategory>>.Error(null!, resGet.ErrorMessage);

        return Result<List<DTOCategory>>.Success(
            resGet.Value!.ToList()
                .Select(x => (DTOCategory)x!)
                .ToList()
        );
    }

    public async Task<Result<DTOCategory>> GetById(Guid id)
    {
        var resGet = await _rep.GetOne(id);
        
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

    public async Task<Result> Hide(Category category, Guid userId)
    {
        var resAdd = await _repHiddenCategory.Add(new HiddenCategory { CategoryId = category.Id, UserId = userId });
        if (resAdd.IsError)
            return resAdd;
        var resSave = await _rep.Save();
        if (resSave.IsError)
            return resSave;
        
        return Result.Success();
    }
}
