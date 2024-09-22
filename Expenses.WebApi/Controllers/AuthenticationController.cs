using System;
using System.Threading.Tasks;
using Expenses.Core;
using Expenses.Core.CustomExceptions;
using Expenses.DB;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.WebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AuthenticationController : ControllerBase
	{
		private readonly IUserServices _userServices;

		public AuthenticationController(IUserServices userServices)
		{
			_userServices = userServices;
        }

        [HttpPost("signup")]
		public async Task<IActionResult> SignUp(User user)
		{
			try
			{
                var result = await _userServices.signUp(user);
                return Created("", result);
			}
			catch(UsernameAlreadyExistException e)
			{
				return StatusCode(409, e.Message);
			}
			
		}


        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(User user)
		{
			try
			{
				var result = await _userServices.signIn(user);
				return Ok(result);
			}
			catch (InvalidUsernamePasswordException e)
			{
                return StatusCode(401, e.Message);

            }
		}
    }
}

