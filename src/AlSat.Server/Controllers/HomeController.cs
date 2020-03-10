using System.Collections.Generic;
using System.Linq;

using AlSat.Server.DAL;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlSat.Server.Controllers
{
	[ApiVersion("0.1")]
	[Route("v{version:apiVersion}/[controller]/[action]")]
	public class HomeController : BaseController
	{
		[AllowAnonymous]
		[HttpGet]
		public IEnumerable<string> GetTest()
		{
			Logger.Info("Gettest in home");

			var users = MainDbContext.User.ToList();

			return users.Select(m => m.PhoneNumber).ToList();
		}

		[HttpGet]
		public IEnumerable<string> GetTestAuth()
		{
			Logger.Info("Gettest in home (Authorized)");

			var users = MainDbContext.User.ToList();

			return users.Select(m => m.PhoneNumber).ToList();
		}
	}
}