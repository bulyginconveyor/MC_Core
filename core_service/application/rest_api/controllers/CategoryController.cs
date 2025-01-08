using core_service.application.rest_controllers.DTO;
using core_service.domain.logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace core_service.application.rest_api.controllers
{
    [Route("categories")]
    [ApiController]
    public class CategoryController(CategoryLogic logic) : ControllerBase
    {
        private CategoryLogic _logic = logic;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DTOCategory>>> GetAll(string? filterName)
        {
            var resGet = await _logic.GetAll(filterName);
            if (resGet.IsError)
                return BadRequest("Упс...");

            if (resGet.Value is null)
                return NotFound();
            return Ok(resGet.Value);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DTOCategory>> GetById(Guid id)
        {
            var resGet = await _logic.GetById(id);
            if (resGet.IsError)
                return NotFound();
            
            return Ok(resGet.Value);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] DTOCategory category)
        {
            var resAdd = await _logic.Add(category);
            if (resAdd.IsError)
                return BadRequest("Не удалось добавить категорию!");
            
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] DTOCategory category)
        {
            var resUpdate = await _logic.Update(category);
            if (resUpdate.IsError)
                return BadRequest("Не удалось обновить категорию!");

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(Guid id)
        {
            var resDelete = await _logic.SoftDeleteById(id);
            if (resDelete.IsError)
                return BadRequest("Не удалось удалить категорию!");

            return Ok();
        }
    }
}
