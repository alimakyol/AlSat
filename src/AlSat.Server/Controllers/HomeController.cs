using System.Collections.Generic;

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
			return new List<string> { "Str1", "Str2", "Str3" }.ToArray();
		}

		[HttpGet]
		public IEnumerable<string> GetTestAuth()
		{
			return new List<string> { "Str1", "Str2", "Str3" }.ToArray();
		}
	}
}