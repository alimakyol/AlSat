using Microsoft.EntityFrameworkCore.Migrations;

namespace AlSat.Server.MigrationsMain
{
	public partial class InitialCreate : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
					name: "Localization",
					columns: table => new
					{
						Id = table.Column<int>(nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						CultureCode = table.Column<string>(maxLength: 10, nullable: false),
						KeyText = table.Column<string>(nullable: false),
						Translation = table.Column<string>(nullable: true)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_Localization", x => x.Id);
					});

			migrationBuilder.CreateTable(
					name: "User",
					columns: table => new
					{
						Id = table.Column<int>(nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						ManagerId = table.Column<int>(nullable: true),
						UserName = table.Column<string>(maxLength: 100, nullable: false),
						NormalizedUserName = table.Column<string>(maxLength: 100, nullable: false),
						Email = table.Column<string>(maxLength: 100, nullable: false),
						NormalizedEmail = table.Column<string>(maxLength: 100, nullable: false),
						PhoneNumber = table.Column<string>(maxLength: 20, nullable: false),
						PasswordHash = table.Column<string>(maxLength: 100, nullable: false),
						EmailConfirmed = table.Column<bool>(nullable: false),
						PhoneConfirmed = table.Column<bool>(nullable: false),
						Token = table.Column<string>(maxLength: 1000, nullable: false),
						IsManager = table.Column<bool>(nullable: false),
						IsActive = table.Column<bool>(nullable: false),
						RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_User", x => x.Id);
						table.ForeignKey(
											name: "FK_User_User_ManagerId",
											column: x => x.ManagerId,
											principalTable: "User",
											principalColumn: "Id",
											onDelete: ReferentialAction.Restrict);
					});

			migrationBuilder.CreateIndex(
					name: "IX_User_ManagerId",
					table: "User",
					column: "ManagerId");

			migrationBuilder.CreateIndex(
					name: "IX_User_NormalizedEmail",
					table: "User",
					column: "NormalizedEmail",
					unique: true);

			migrationBuilder.CreateIndex(
					name: "IX_User_NormalizedUserName",
					table: "User",
					column: "NormalizedUserName",
					unique: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
					name: "Localization");

			migrationBuilder.DropTable(
					name: "User");
		}
	}
}
