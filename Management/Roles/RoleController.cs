using Management.Roles.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management.Roles
{
    [ApiController]
    [Route("api/roles")]
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public ActionResult<List<RoleDto>> GetAll()
        {
            return _roleService.GetAll();
        }

        [HttpGet("{id}")]
        public RoleDetailDto GetById(int id)
        {
            return _roleService.GetById(id);
        }

        [HttpPost]
        public IActionResult Create([FromBody]RoleCreateDto role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _roleService.Create(role);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int roleId, [FromBody] RoleUpdateDto updatedRoleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _roleService.Update(roleId, updatedRoleDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public void Delete(int roleId)
        {
            _roleService.Delete(roleId);
        }
    }

}
