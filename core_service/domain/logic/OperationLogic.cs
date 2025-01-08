using System.Linq.Expressions;
using core_service.application.rest_controllers.DTO;
using core_service.domain.logic.filters.operation;
using core_service.domain.models;
using core_service.infrastructure.repository.enums;
using core_service.infrastructure.repository.interfaces;
using core_service.services.ExpressionHelpers;
using core_service.services.Result;

namespace core_service.domain.logic;

public class OperationLogic(IDbRepository<Operation> rep)
{
    private IDbRepository<Operation> _rep = rep;

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

    public async Task<Result> Add(DTOOperation dto)
    {
        await _rep.Add(dto);
        return await _rep.Save();
    }

    public async Task<Result> Update(DTOOperation dto)
    {
        await _rep.Update(dto);
        return await _rep.Save();
    }
    
    public async Task<Result> SoftDeleteById(Guid id) => await _rep.Delete(id);
}




