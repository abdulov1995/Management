using Management.Roles.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Management.Roles
{
    [ApiController]
    [Route("api/role")]
    [Authorize(Roles = "Admin")]
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<ActionResult<List<RoleDto>>> GetAll()
        {
            var roles = await _roleService.GetAllAsync();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDetailDto>> GetById(int id)
        {
            var roleDetail = await _roleService.GetByIdAsync(id);
            if (roleDetail == null)
            {
                return NotFound();
            }
            return Ok(roleDetail);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleCreateDto role)
        {
            await _roleService.CreateAsync(role);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int roleId, [FromBody] RoleUpdateDto updatedRoleDto)
        {
            await _roleService.UpdateAsync(roleId, updatedRoleDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int roleId)
        {
            await _roleService.DeleteAsync(roleId);
            return NoContent(); 
        }
    }
}
