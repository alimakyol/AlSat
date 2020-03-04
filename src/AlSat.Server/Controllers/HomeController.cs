using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AlSat.Data.DAL;

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
			return new List<string> { "Str1", "Str2", "Str3" }.ToArray();
		}
	}
}