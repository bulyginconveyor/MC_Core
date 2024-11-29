using core_service.infrastructure.repository.postgresql.context;
using Microsoft.AspNetCore.Mvc;

namespace core_service.application.rest_controllers
{
    [Route("api/currencies")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        public CurrencyController(PostgreSqlDbContext context)
        {
            var res = context.Currencies.ToList();
        }
        
        [HttpGet]
        public ActionResult Get() => Ok();
    }
}
