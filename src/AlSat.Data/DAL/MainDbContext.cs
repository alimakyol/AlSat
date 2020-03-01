using AlSat.Data.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlSat.Data.DAL
{
	public class MainDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
	{
		public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Role>().ToTable("Role");
			builder.Entity<RoleClaim>().ToTable("RoleClaim");
			builder.Entity<User>().ToTable("User");
			builder.Entity<UserClaim>().ToTable("UserClaim");
			builder.Entity<UserLogin>().ToTable("UserLogin");
			builder.Entity<UserRole>().ToTable("UserRole");
			builder.Entity<UserToken>().ToTable("UserToken");
		}
	}
}
