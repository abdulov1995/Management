using Management.Users.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Management.Users
{
    public class UserController
    {
        [ApiController]
        [Route("api/users")]
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
            public IActionResult Create([FromBody] CreateUserDto user)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _userService.Create(user);
                return Ok();
            }

            [HttpPut("{id}")]
            public IActionResult Update([FromBody] int id, UpdateUserDto updatedUser)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
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
}
