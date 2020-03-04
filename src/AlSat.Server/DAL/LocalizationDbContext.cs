using AlSat.Server.Models;

using Microsoft.EntityFrameworkCore;

namespace AlSat.Server.DAL
{
	public class LocalizationDbContext : DbContext
	{
		public LocalizationDbContext(DbContextOptions<LogDbContext> options) : base(options)
		{

		}

		public DbSet<Localization> Localization { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

		}
	}
}
