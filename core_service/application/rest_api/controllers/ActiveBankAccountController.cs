using core_service.application.rest_controllers.DTO;
using core_service.domain.logic;
using core_service.domain.logic.filters.bank_account.active;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace core_service.application.rest_api.controllers
{
    [Route("bank_accounts/active")]
    [ApiController]
    public class ActiveBankAccountController(ActiveBankAccountLogic logic) : ControllerBase
    {
        private ActiveBankAccountLogic _logic = logic;

        [HttpPatch]
        public async Task<ActionResult<List<DTOActiveBankAccount>>> GetAll(
            [FromBody] ActiveBankAccountFilter? filter = null)
        {
            var resGet = await _logic.GetAll(filter);
            if (resGet.IsError)
                return NoContent();

            return Ok(resGet.Value);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DTOActiveBankAccount>> GetById(Guid id)
        {
            var resGet = await _logic.GetOneById(id);
            if (resGet.IsError)
                return NoContent();

            return Ok(resGet.Value);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] DTOActiveBankAccount activeBankAccount)
        {
            var resAdd = await _logic.Add(activeBankAccount);
            if (resAdd.IsError)
                return BadRequest("Не удалось добавить счет!");
            
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] DTOActiveBankAccount activeBankAccount)
        {
            var resUpdate = await _logic.Update(activeBankAccount);
            if (resUpdate.IsError)
                return BadRequest("Не удалось обновить счет!");
            
            return Ok();
        }
    }
}
