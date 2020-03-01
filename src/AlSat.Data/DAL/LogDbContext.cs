using System;

using AlSat.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace AlSat.Data.DAL
{
	public class LogDbContext : DbContext
	{
		public LogDbContext(DbContextOptions<LogDbContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<LogAudit>().Property(m => m.ChangedFields).HasColumnType("xml");
			builder.Entity<LogAudit>().Property(m => m.TimeStamp).HasConversion(m => m, m => DateTime.SpecifyKind(m, DateTimeKind.Utc));

			builder.Entity<LogSystem>().Property(m => m.TimeStamp).HasConversion(m => m, m => DateTime.SpecifyKind(m, DateTimeKind.Utc));
		}
	}
}
