using AlSat.Data.BaseClasses;

using Microsoft.AspNetCore.Authorization;

namespace AlSat.Server.Controllers
{
	[Authorize]
	public abstract class BaseController : ApiControllerBase
	{
	}
}