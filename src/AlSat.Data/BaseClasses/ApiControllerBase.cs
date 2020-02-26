using Microsoft.AspNetCore.Mvc;

namespace AlSat.Data.BaseClasses
{
	[ApiController]
	[Route("api/[controller]/[action]")]

	public abstract class ApiControllerBase : ControllerBase
	{
	}
}
