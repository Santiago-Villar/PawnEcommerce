using Microsoft.AspNetCore.Mvc;
using PawnEcommerce.DTO;
using Service.User;

namespace PawnEcommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
    }
}