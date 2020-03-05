using System;
using AlSat.Server.VModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AlSat.Server.Controllers
{
	[ApiVersion("0.1")]
	[ApiExplorerSettings(IgnoreApi = true)]
	public class ErrorController : BaseController
	{
		[AllowAnonymous]
		[Route("/error")]
		[HttpGet]
		public IActionResult Error()
		{
			BaseResponse response = new BaseResponse
			{
				Status = Enums.ResponseStatus.Error,
				Message = "An error occured."
			};

			return Ok(response);
		}
	}
}