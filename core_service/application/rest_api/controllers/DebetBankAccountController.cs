using core_service.application.rest_api.DTO;
using core_service.domain.logic;
using core_service.domain.logic.filters.bank_account.debet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace core_service.application.rest_api.controllers
{
    [Route("bank_accounts/debet")]
    [ApiController]
    public class DebetBankAccountController(DebetBankAccountLogic logic) : ControllerBase
    {
        private DebetBankAccountLogic _logic = logic;

        [HttpPatch]
        public async Task<ActionResult<List<DTODebetBankAccount>>> GetAll(
            [FromBody] DebetBankAccountFilter? filter = null)
        {
            var resGet = await _logic.GetAll(filter);
            if (resGet.IsError)
                return NoContent();

            return Ok(resGet.Value);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DTODebetBankAccount>> GetById(Guid id)
        {
            var resGet = await _logic.GetOneById(id);
            if (resGet.IsError)
                return NoContent();

            return Ok(resGet.Value);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] DataDTODebetBankAccount debetBankAccount)
        {
            var resAdd = await _logic.Add(debetBankAccount);
            if (resAdd.IsError)
                return BadRequest("Не удалось добавить счет!");

            return Created();
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] DataDTODebetBankAccount debetBankAccount)
        {
            var resUpdate = await _logic.Update(debetBankAccount);
            if (resUpdate.IsError)
                return BadRequest("Не удалось обновить счет!");

            return Ok();
        }
    }
}
