using core_service.application.rest_controllers.DTO;
using core_service.domain.logic;
using core_service.domain.logic.filters.bank_account.contribution;
using core_service.domain.logic.filters.bank_account.credit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace core_service.application.rest_api.controllers
{
    [Route("bank_accounts/credit")]
    [ApiController]
    public class CreditBankAccountController(CreditBankAccountLogic logic) : ControllerBase
    {
        private CreditBankAccountLogic _logic = logic;

        [HttpPatch]
        public async Task<ActionResult<List<DTOCreditBankAccount>>> GetAll(
            [FromBody] CreditBankAccountFilter? filter = null)
        {
            var resGet = await _logic.GetAll(filter);
            if (resGet.IsError)
                return NoContent();

            return Ok(resGet.Value);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DTOCreditBankAccount>> GetById(Guid id)
        {
            var resGet = await _logic.GetOneById(id);
            if (resGet.IsError)
                return NoContent();

            return Ok(resGet.Value);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] DTOCreditBankAccount contributionBankAccount)
        {
            var resAdd = await _logic.Add(contributionBankAccount);
            if (resAdd.IsError)
                return BadRequest("Не удалось добавить счет!");
                
            return Created();
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] DTOCreditBankAccount contributionBankAccount)
        {
            var resUpdate = await _logic.Update(contributionBankAccount);
            if (resUpdate.IsError)
                return BadRequest("Не удалось обновить счет!");
                
            return Ok();
        }
    }
}
