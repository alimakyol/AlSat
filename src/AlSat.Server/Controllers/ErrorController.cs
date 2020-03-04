using AlSat.Server.VModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlSat.Server.Controllers
{
	public class ErrorController : BaseController
	{
		[AllowAnonymous]
		[Route("/error")]
		[ApiExplorerSettings(IgnoreApi = true)]
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