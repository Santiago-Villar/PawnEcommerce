using Microsoft.AspNetCore.Mvc;
using Service.DTO.User;
using PawnEcommerce.Middlewares;
using Service.User;
using Service.Session;

namespace PawnEcommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ExceptionMiddleware]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IServiceProvider _serviceProvider;

        public UserController(IUserService userService, IServiceProvider serviceProvider)
        {
            _userService = userService;
            _serviceProvider = serviceProvider;
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

        [HttpGet("Profile")]
        public IActionResult GetProfile()
        {
            using var scope = _serviceProvider.CreateScope();
            var sessionService = scope.ServiceProvider.GetRequiredService<ISessionService>();
            var user = sessionService.GetCurrentUser();

            if (user == null)
            {
                return BadRequest("User was not found");
            }

            var response = new UserDTO()
            {
                Id = user.Id,
                Email = user.Email,
                Address = user.Address,
            };
            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UserUpdateModel updateUser)
        {
            using var scope = _serviceProvider.CreateScope();
            var sessionService = scope.ServiceProvider.GetRequiredService<ISessionService>();
            var user = sessionService.GetCurrentUser();

            if ((user == null || user.Id != id) && !user.Roles.Contains(Service.User.Role.RoleType.Admin))
            {
                return BadRequest("User has no access");
            }

            User response = _userService.UpdateUserUsingDTO(id, updateUser);
            return Ok(ToUserDTO(response));
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