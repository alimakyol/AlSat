using AlSat.Server.Models;
using AlSat.Server.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlSat.Server.Controllers
{
	public class AccountController : BaseController
	{
		private IUserService mUserService;

		public AccountController(IUserService userService)
		{
			mUserService = userService;
		}

		[AllowAnonymous]
		[HttpPost]
		public IActionResult Login([FromBody]Login model)
		{
			UserInfo userInfo = mUserService.Authenticate(model.UserName, model.Password);

			if (userInfo == null)
				return BadRequest(new { message = "User name or password is incorrect" });

			return Ok(userInfo);
		}

		[AllowAnonymous]
		[HttpPost]
		public void Register()
		{

		}
	}
}