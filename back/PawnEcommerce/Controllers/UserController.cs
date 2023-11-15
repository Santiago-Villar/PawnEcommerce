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
            var userDTOs = users.Select(u => ToUserDTO(u)).ToList();
            return Ok(userDTOs);
        }

        [Authorization("Admin")]
        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var user = _userService.Get(id);

            return Ok(ToUserDTO(user));
        }


        [Authorization("Admin")]
        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UserUpdateModel updateUser)
        {
            User user = _userService.UpdateUserUsingDTO(id, updateUser);
            return Ok(ToUserDTO(user));
        }

        [Authorization("Admin")]
        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _userService.DeleteUser(id);
            return Ok();
        }

        private UserDTO ToUserDTO(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Address = user.Address,
                Roles = user.Roles.Select(r => r.ToString()).ToList(),
                Email = user.Email
            };
        }
        
        
    }
}