using Management.Auth.Dto;
using Management.Roles.Model;
using Management.Users.Dto;
using Management.Users.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management.Users
{
    [ApiController]
    [Route("api/user")]
    [Authorize(Roles = "Admin")]

    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAllAsync()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailDto>> GetByIdAsync(int id)
        {
            var userDetail = await _userService.GetByIdAsync(id);
            return Ok(userDetail);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] UserCreateDto user)
        {
            var createdUser = await _userService.CreateAsync(user);
            return Ok(createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UserUpdateDto updatedUser)
        {
            await _userService.UpdateAsync(id, updatedUser);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _userService.DeleteAsync(id);
            return NoContent(); 
        }
    }

}

