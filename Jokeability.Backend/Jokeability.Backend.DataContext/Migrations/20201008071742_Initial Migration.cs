using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Jokeability.Backend.DataContext.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.EnsureSchema(
                name: "GS");

            migrationBuilder.CreateTable(
                name: "tblUsers",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    isWithBadge = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblMasterSetting",
                schema: "GS",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Group = table.Column<string>(nullable: true),
                    isActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMasterSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblJokes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JokerID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    isActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblJokes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblJokes_tblUsers_JokerID",
                        column: x => x.JokerID,
                        principalSchema: "dbo",
                        principalTable: "tblUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblJokeStats",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(nullable: false),
                    JokerID = table.Column<int>(nullable: false),
                    JokeID = table.Column<int>(nullable: false),
                    ReactionID = table.Column<int>(nullable: false),
                    isActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblJokeStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblJokeStats_tblJokes_JokeID",
                        column: x => x.JokeID,
                        principalSchema: "dbo",
                        principalTable: "tblJokes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblJokes_JokerID",
                schema: "dbo",
                table: "tblJokes",
                column: "JokerID");

            migrationBuilder.CreateIndex(
                name: "IX_tblJokeStats_JokeID",
                schema: "dbo",
                table: "tblJokeStats",
                column: "JokeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblJokeStats",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tblMasterSetting",
                schema: "GS");

            migrationBuilder.DropTable(
                name: "tblJokes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tblUsers",
                schema: "dbo");
        }
    }
}
