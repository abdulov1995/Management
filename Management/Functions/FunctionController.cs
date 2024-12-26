using Management.PgSqlFunction;
using Microsoft.AspNetCore.Mvc;

namespace Management.PgSqlFunction
{
    [ApiController]
    [Route("api/[controller]")]
    public class FunctionController : ControllerBase
    {
        private readonly FunctionService _functionService;

        public FunctionController(FunctionService functionService)
        {
            _functionService = functionService;
        }

        [HttpGet("usernames/{roleId}")]
        public async Task<IActionResult> GetUserNamesByRoleId(int roleId)
        {
            var userNames = await _functionService.GetUserNamesByRoleIdAsync(roleId);
            return Ok(userNames);
        }
    }
}