﻿using System.ComponentModel.DataAnnotations;

namespace AlSat.Server.VModels
{
	public class Login
	{
		[Required]
		public string UserName { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
