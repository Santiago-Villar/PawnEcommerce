using Microsoft.AspNetCore.Mvc;
using PawnEcommerce.DTO;
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

        // [Authorization()]
        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UserCreateModel updateUser)
        {
            var user = updateUser.ToEntity();
            user.Id = id;
            _userService.UpdateUser(user);
            return Ok();
        }

        // [Authorization("Admin")]
        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _userService.DeleteUser(id);
            return Ok();
        }
        
        
    }
}