using Management.Roles.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Management.Roles
{


    [ApiController]
    [Route("api/roles")]
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
        public IActionResult Create([FromBody]CreateRoleDto role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _roleService.Create(role);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] int roleId, UpdateRoleDto updatedRoleDto)
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
