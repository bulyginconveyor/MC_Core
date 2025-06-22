using core_service.application.rest_api.DTO;
using core_service.domain.logic;
using core_service.domain.logic.filters.bank_account.active;
using core_service.services.Jwt;
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
        public async Task<ActionResult> Add([FromBody] DataDTOActiveBankAccount activeBankAccount)
        {
            var userId = JwtHelper.UserId(HttpContext.Request.Headers.Authorization);
            
            var resAdd = await _logic.Add(activeBankAccount, userId);
            if (resAdd.IsError)
                return BadRequest("Не удалось добавить счет!");
            
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] DataDTOActiveBankAccount activeBankAccount)
        {
            var resUpdate = await _logic.Update(activeBankAccount);
            if (resUpdate.IsError)
                return BadRequest("Не удалось обновить счет!");
            
            return Ok();
        }
    }
}

