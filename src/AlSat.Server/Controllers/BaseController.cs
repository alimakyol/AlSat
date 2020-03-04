using AlSat.Data.DAL;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

using NLog;

namespace AlSat.Server.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]/[action]")]
	public abstract class BaseController : ControllerBase
	{
		private MainDbContext _mainDbContext;
		private Logger _logger;

		public MainDbContext MainDbContext => _mainDbContext ?? (_mainDbContext = HttpContext.RequestServices.GetService<MainDbContext>());
		public Logger Logger => _logger ?? (_logger = LogManager.GetCurrentClassLogger());

		public BaseController()
		{
		}
	}
}
