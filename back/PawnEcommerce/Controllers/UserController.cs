using Microsoft.AspNetCore.Mvc;
using Service.DTO.User;
using PawnEcommerce.Middlewares;
using Service.User;

namespace PawnEcommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ExceptionMiddleware]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost]
        public IActionResult SignUp([FromBody] UserCreateModel newUser)
        {
            _userService.SignUp(newUser.ToEntity());
            return Ok();
        }

        [Authorization("Admin")]
        [HttpGet]
        public IActionResult Get()
        {

            var users = _userService.GetAll();
            var userDTOs = users.Select(u => new UserDTO
            {
                Id = u.Id,
                Address = u.Address,
                Roles = u.Roles.Select(r => r.ToString()).ToList(),
                Email = u.Email
            }).ToList();

            return Ok(userDTOs);
        }

        [Authorization("Admin")]
        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UserUpdateModel updateUser)
        {
            User user = _userService.UpdateUserUsingDTO(id, updateUser);
            return Ok(user);
        }

        [Authorization("Admin")]
        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _userService.DeleteUser(id);
            return Ok();
        }
        
        
    }
}