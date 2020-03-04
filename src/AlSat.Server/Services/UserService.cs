using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using AlSat.Server.Helpers;
using AlSat.Server.VModels;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AlSat.Server.Services
{
	public class UserService : IUserService
	{
		private readonly AppSettings mAppSettings;

		public UserService(IOptions<AppSettings> appSettings)
		{
			mAppSettings = appSettings.Value;
		}

		public UserInfo Authenticate(string username, string password)
		{
			//var user =_users.SingleOrDefault(x => x.Username == username && x.Password == password);

			//// return null if user not found
			//if (user == null)
			//	return null;

			var userInfo = new UserInfo { UserGuid = Guid.NewGuid() };

			// authentication successful so generate jwt token
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(mAppSettings.Secret);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
					{
						new Claim(ClaimTypes.Name, "1") // user.Id.ToString())
					}),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			userInfo.Token = tokenHandler.WriteToken(token);

			return userInfo;
		}

		public bool IsTokenValid(string token)
		{
			// TODO: Check user token to see if user changed password or user is disabled.

			return true;
		}
	}
}
