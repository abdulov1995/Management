using Management.Auth.Dto;
using Management.Roles.Model;
using Management.Users.Dto;
using Management.Users.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management.Users
{
    [ApiController]
    [Route("api/users")]
   // [Authorize(Roles="Admin")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<List<UserDto>> GetAll()
        {
            return _userService.GetAll();
        }

        [HttpGet("{id}")]
        public UserDetailDto GetById(int id)
        {
            return _userService.GetById(id);
        }

        [HttpPost]
        public IActionResult Create([FromBody] UserCreateDto user)
        {
           
            _userService.Create(user);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UserUpdateDto updatedUser)
        {
           
            _userService.Update(id, updatedUser);
            return Ok();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _userService.Delete(id);
        }
    }

}

