using core_service.application.rest_controllers.DTO;
using core_service.domain.logic;
using core_service.domain.logic.filters.operation;
using core_service.domain.models;
using core_service.infrastructure.repository.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace core_service.application.rest_api.controllers
{
    [Route("operations")]
    [ApiController]
    public class OperationController(OperationLogic logic) : ControllerBase
    {
        private OperationLogic _logic = logic;

        [HttpGet("pages")]
        public async Task<ActionResult<int>> GetPagesCount([FromBody]OperationFilter? filter = null)
        {
            // TODO: Получать кол-во элементов на одной странице из хранилища токена пользователя!
            uint countPerPage = 10;
            var resGet = filter is null ? await _logic.GetCountPages(countPerPage)
                : await _logic.GetCountPages(countPerPage, filter);
            if (resGet.IsError)
                return NotFound();

            return Ok(resGet.Value);
        }

        [HttpGet("page/{number}")]
        public async Task<ActionResult<IEnumerable<DTOOperation>>> GetByPage(int number,
            [FromBody] OperationFilter? filter = null)
        {
            // TODO: Получать кол-во элементов на одной странице из хранилища токена пользователя!
            uint countPerPage = 10;
            var resGet = filter is null ? await _logic.GetByPage(countPerPage, (uint)number)
                : await _logic.GetByPage(countPerPage, (uint)number, filter);
            if (resGet.IsError)
                return NotFound();

            return Ok(resGet.Value);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DTOOperation>> GetById(Guid id)
        {
            var resGet = await _logic.GetById(id);
            if (resGet.IsError)
                return NotFound();

            return Ok(resGet.Value);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] DTOOperation operation)
        {
            var resAdd = await _logic.Add(operation);
            if (resAdd.IsError)
                return BadRequest("Не удалось добавить операцию!");

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] DTOOperation operation)
        {
            var resUpdate = await _logic.Update(operation);
            if (resUpdate.IsError)
                return BadRequest("Не удалось обновить операцию!");

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(Guid id)
        {
            var resDelete = await _logic.SoftDeleteById(id);
            if (resDelete.IsError)
                return BadRequest("Не удалось удалить операцию!");

            return Ok();
        }
    }
}
