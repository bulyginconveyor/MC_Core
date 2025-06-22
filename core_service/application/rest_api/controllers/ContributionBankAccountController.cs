using core_service.application.rest_api.DTO;
using core_service.domain.logic;
using core_service.domain.logic.filters.bank_account.contribution;
using core_service.services.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace core_service.application.rest_api.controllers
{
    [Route("bank_accounts/contribution")]
    [ApiController]
    public class ContributionBankAccountController(ContributionBankAccountLogic logic) : ControllerBase
    {
        private ContributionBankAccountLogic _logic = logic;

        [HttpPatch]
        public async Task<ActionResult<List<DTOContributionBankAccount>>> GetAll(
            [FromBody] ContributionBankAccountFilter? filter = null)
        {
            var resGet = await _logic.GetAll(filter);
            if (resGet.IsError)
                return NoContent();

            return Ok(resGet.Value);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DTOContributionBankAccount>> GetById(Guid id)
        {
            var resGet = await _logic.GetOneById(id);
            if (resGet.IsError)
                return NoContent();

            return Ok(resGet.Value);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] DataDTOContributionBankAccount contributionBankAccount)
        {
            var userId = JwtHelper.UserId(HttpContext.Request.Headers.Authorization);
            
            var resAdd = await _logic.Add(contributionBankAccount, userId);
            if (resAdd.IsError)
                return BadRequest("Не удалось добавить счет!");
                
            return Created();
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] DataDTOContributionBankAccount contributionBankAccount)
        {
            var resUpdate = await _logic.Update(contributionBankAccount);
            if (resUpdate.IsError)
                return BadRequest("Не удалось обновить счет!");
                
            return Ok();
        }
    }
}
