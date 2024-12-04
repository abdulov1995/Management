using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management
{
    [ApiController]
    [Route("api/roles")]
    [Authorize(Roles = "Admin")]
    public class BaseController
    {
    }
}
