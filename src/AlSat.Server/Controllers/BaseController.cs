using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlSat.Server.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]/[action]")]
	public abstract class BaseController : ControllerBase
	{
	}
}
