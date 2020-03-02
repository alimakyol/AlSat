using System;

using Microsoft.AspNetCore.Identity;

namespace AlSat.Data.Models
{
	public class User : IdentityUser<Guid>
	{
		public string Token { get; set; }

		public bool IsPassive { get; set; }
	}
}
