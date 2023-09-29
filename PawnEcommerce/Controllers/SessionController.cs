using System;
using Microsoft.AspNetCore.Mvc;
using Service.Session;

using PawnEcommerce.DTO;

namespace PawnEcommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : ControllerBase
	{
		private ISessionService _sessionService { get; set; }

        public SessionController(ISessionService sessionService)
		{
			_sessionService = sessionService;
		}

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                var token = _sessionService.Authenticate(request.Email, request.Password);

                return Ok(new LoginResponse{ Token = token });
            }
            catch (System.Exception ex) 
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }

    }
}

