using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace AlSat.Server.MigrationsLog
{
	public partial class InitialCreate : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
					name: "LogAudit",
					columns: table => new
					{
						Id = table.Column<int>(nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						TimeStamp = table.Column<DateTime>(nullable: false),
						Level = table.Column<string>(maxLength: 10, nullable: true),
						Controller = table.Column<string>(maxLength: 100, nullable: true),
						Action = table.Column<string>(maxLength: 100, nullable: true),
						Logger = table.Column<string>(maxLength: 100, nullable: true),
						ClientIpAddress = table.Column<string>(maxLength: 50, nullable: true),
						Url = table.Column<string>(maxLength: 2000, nullable: true),
						HttpMethod = table.Column<string>(maxLength: 10, nullable: true),
						CompanyId = table.Column<int>(nullable: true),
						UserId = table.Column<int>(nullable: true),
						Email = table.Column<string>(maxLength: 100, nullable: true),
						UserFullName = table.Column<string>(maxLength: 100, nullable: true),
						Message = table.Column<string>(nullable: true),
						EntityName = table.Column<string>(maxLength: 100, nullable: true),
						DbAction = table.Column<string>(maxLength: 10, nullable: true),
						KeyFields = table.Column<string>(maxLength: 50, nullable: true),
						ChangedFields = table.Column<string>(type: "xml", nullable: true),
						TimeElapsed = table.Column<int>(nullable: true)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_LogAudit", x => x.Id);
					});

			migrationBuilder.CreateTable(
					name: "LogSystem",
					columns: table => new
					{
						Id = table.Column<int>(nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						TimeStamp = table.Column<DateTime>(nullable: false),
						Level = table.Column<string>(maxLength: 10, nullable: true),
						Controller = table.Column<string>(maxLength: 100, nullable: true),
						Action = table.Column<string>(maxLength: 100, nullable: true),
						Logger = table.Column<string>(maxLength: 100, nullable: true),
						ClientIpAddress = table.Column<string>(maxLength: 50, nullable: true),
						Url = table.Column<string>(maxLength: 2000, nullable: true),
						HttpMethod = table.Column<string>(maxLength: 10, nullable: true),
						CompanyId = table.Column<int>(nullable: true),
						UserId = table.Column<int>(nullable: true),
						Email = table.Column<string>(maxLength: 100, nullable: true),
						UserFullName = table.Column<string>(maxLength: 100, nullable: true),
						Message = table.Column<string>(nullable: true),
						Exception = table.Column<string>(nullable: true),
						StackTrace = table.Column<string>(nullable: true)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_LogSystem", x => x.Id);
					});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
					name: "LogAudit");

			migrationBuilder.DropTable(
					name: "LogSystem");
		}
	}
}
