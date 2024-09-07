using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TCC_System_Data.Migrations
{
    public partial class TCCMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AUTH_Claims",
                columns: table => new
                {
                    ClaimID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordCreatedBy = table.Column<string>(type: "VARCHAR(30)", nullable: true),
                    RecordUpdatedBy = table.Column<string>(type: "VARCHAR(30)", nullable: true),
                    RecordCreationDate = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    RecordUpdateDate = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    NomeClaim = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTH_Claims", x => x.ClaimID);
                });

            migrationBuilder.CreateTable(
                name: "AUTH_Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordCreatedBy = table.Column<string>(type: "VARCHAR(30)", nullable: true),
                    RecordUpdatedBy = table.Column<string>(type: "VARCHAR(30)", nullable: true),
                    RecordCreationDate = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    RecordUpdateDate = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    Login = table.Column<string>(nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(200)", nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Language = table.Column<int>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTH_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AUTH_UserClaims",
                columns: table => new
                {
                    UsuarioID = table.Column<int>(nullable: false),
                    ClaimID = table.Column<int>(nullable: false),
                    RecordCreatedBy = table.Column<string>(type: "VARCHAR(30)", nullable: true),
                    RecordUpdatedBy = table.Column<string>(type: "VARCHAR(30)", nullable: true),
                    RecordCreationDate = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    RecordUpdateDate = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTH_UserClaims", x => new { x.UsuarioID, x.ClaimID });
                    table.ForeignKey(
                        name: "FK_AUTH_UserClaims_AUTH_Claims_ClaimID",
                        column: x => x.ClaimID,
                        principalTable: "AUTH_Claims",
                        principalColumn: "ClaimID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AUTH_UserClaims_AUTH_Users_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "AUTH_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_UserClaims_ClaimID",
                table: "AUTH_UserClaims",
                column: "ClaimID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AUTH_UserClaims");

            migrationBuilder.DropTable(
                name: "AUTH_Claims");

            migrationBuilder.DropTable(
                name: "AUTH_Users");
        }
    }
}
