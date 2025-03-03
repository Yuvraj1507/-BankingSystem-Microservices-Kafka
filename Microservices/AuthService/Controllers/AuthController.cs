using AuthService.Commands.CreateAccount;
using AuthService.Commands.Login;
using AuthService.Common.Filters;
using AuthService.Exceptions;
using AuthService.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private UserManager<Entities.Account> _userManager;
        private IMediator _mediator;

        public AuthController(UserManager<Entities.Account> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;

        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
        {
            try
            {
                var token = await _mediator.Send(new LoginCommand(_userManager, model));
                return Ok(token);
            }
            catch (BadCredentialsException e)
            {
                return Unauthorized(e.Message);
            }
        }


        [HttpPost("register")]
        [ServiceFilter(typeof(NameFilter))]
        public async Task<IActionResult> Register([FromBody] RegistrationModel model)
        {
            try
            {
                var login = await _mediator.Send(new CreateAccountCommand(_userManager, model));
                return Ok(login);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
