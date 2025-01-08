using core_service.application.rest_controllers.DTO;
using core_service.domain;
using core_service.domain.logic;
using core_service.infrastructure.repository.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace core_service.application.rest_api.controllers
{
    [Route("currencies")]
    [ApiController]
    public class CurrencyController(CurrencyLogic logic) : ControllerBase
    {
        private CurrencyLogic _logic = logic;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DTOCurrency>>> GetAll()
        {
            var resGet = await _logic.GetAll();
            if (resGet.IsError)
                return BadRequest("Упс...");

            if (resGet.Value is null)
                return NotFound();
            return Ok(resGet.Value);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody]DTOCurrency currency)
        {
            var resAdd = await _logic.Add(currency);
            if (resAdd.IsError)
                return BadRequest("Не удалось добавить валюту!");
            
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody]DTOCurrency currency)
        {
            var resUpdate = await _logic.Update(currency);
            if (resUpdate.IsError)
                return BadRequest("Не удалось обновить валюту!");

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(Guid id)
        {
            var resDelete = await _logic.SoftDeleteById(id);
            if (resDelete.IsError)
                return BadRequest("Не удалось удалить валюту!");

            return Ok();
        }
    }
}
