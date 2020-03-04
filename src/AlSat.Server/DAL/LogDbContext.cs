using System;

using AlSat.Server.Models;

using Microsoft.EntityFrameworkCore;

namespace AlSat.Server.DAL
{
	public class LogDbContext : DbContext
	{
		public LogDbContext(DbContextOptions<LogDbContext> options) : base(options)
		{

		}

		public DbSet<LogAudit> LogAudit { get; set; }
		public DbSet<LogSystem> LogSystem { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<LogAudit>().Property(m => m.ChangedFields).HasColumnType("xml");
			builder.Entity<LogAudit>().Property(m => m.TimeStamp).HasConversion(m => m, m => DateTime.SpecifyKind(m, DateTimeKind.Utc));

			builder.Entity<LogSystem>().Property(m => m.TimeStamp).HasConversion(m => m, m => DateTime.SpecifyKind(m, DateTimeKind.Utc));
		}
	}
}
