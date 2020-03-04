using AlSat.Server.VModels;

namespace AlSat.Server.Services
{
	public interface IUserService
	{
		UserInfo Authenticate(string username, string password);

		bool IsTokenValid(string token);
	}
}
