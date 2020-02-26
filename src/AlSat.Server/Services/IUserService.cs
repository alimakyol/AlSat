using AlSat.Server.Models;

namespace AlSat.Server.Services
{
	public interface IUserService
	{
		UserInfo Authenticate(string username, string password);
	}
}
