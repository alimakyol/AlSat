using System.Collections.Generic;
using System.Linq;

using AlSat.Data.DAL;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlSat.Server.Controllers
{
	public class HomeController : BaseController
	{
		[AllowAnonymous]
		[HttpGet]
		public IEnumerable<string> GetTest()
		{
			Logger.Info("Gettest in home");

			var users = MainDbContext.User.ToList();

			return users.Select(m => m.UserName + " " + (m.Employees == null ? 0 : m.Employees.Count())).ToList();
		}

		[HttpGet]
		public IEnumerable<string> GetTestAuth()
		{
			Logger.Info("Gettest in home (Authorized)");

			var users = MainDbContext.User.ToList();

			return users.Select(m => m.UserName + " " + (m.Employees == null ? 0 : m.Employees.Count())).ToList();
		}
	}
}