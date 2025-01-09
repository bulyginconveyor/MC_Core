using core_service.application.rest_controllers.DTO;
using core_service.domain.logic;
using core_service.domain.logic.filters.bank_account;
using core_service.domain.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace core_service.application.rest_api.controllers
{
    [Route("bank_accounts")]
    [ApiController]
    public class BankAccountController(BankAccountLogic logic) : ControllerBase
    {
        private BankAccountLogic _logic = logic;

        [HttpPatch]
        public async Task<ActionResult<List<DTOBankAccount>>> GetAll(BankAccountFilter<BankAccount>? filter = null)
        {
            var resGet = await _logic.GetAll(filter);
            if (resGet.IsError)
                return NoContent();

            return Ok(resGet.Value);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DTOBankAccount>> GetById(Guid id)
        {
            var resGet = await _logic.GetOneById(id);
            if (resGet.IsError)
                return NoContent();

            return Ok(resGet.Value);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var resDelete = await _logic.SoftDelete(id);
            if (resDelete.IsError)
                return BadRequest("Не удалось удалить счет!");
            return Ok();
        }
    }
}
