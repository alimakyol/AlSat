using AlSat.Server.DAL;
using AlSat.Server.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

using NLog;

namespace AlSat.Server.Controllers
{
	[Authorize]
	[ApiController]
	public abstract class BaseController : ControllerBase
	{
		private MainDbContext _mainDbContext;
		private Logger _logger;
		private Localizer _localizer;

		public MainDbContext MainDbContext => _mainDbContext ?? (_mainDbContext = HttpContext.RequestServices.GetService<MainDbContext>());
		public Logger Logger => _logger ?? (_logger = LogManager.GetCurrentClassLogger());
		public Localizer Localizer => _localizer ?? (_localizer = HttpContext.RequestServices.GetService<Localizer>());

		public BaseController()
		{
		}
	}
}
