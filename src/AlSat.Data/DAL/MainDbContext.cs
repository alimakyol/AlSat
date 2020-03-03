
using AlSat.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace AlSat.Data.DAL
{
	public class MainDbContext : DbContext // IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
	{
		public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<User>().Property(m => m.RowVersion).IsConcurrencyToken();

			//builder.Entity<Role>().ToTable("Role");
			//builder.Entity<RoleClaim>().ToTable("RoleClaim");
			//builder.Entity<User>().ToTable("User");
			//builder.Entity<UserClaim>().ToTable("UserClaim");
			//builder.Entity<UserLogin>().ToTable("UserLogin");
			//builder.Entity<UserRole>().ToTable("UserRole");
			//builder.Entity<UserToken>().ToTable("UserToken");
		}

		public DbSet<User> User { get; set; }
	}
}
