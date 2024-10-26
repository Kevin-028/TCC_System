using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TCC_System_Data.Migrations
{
    public partial class TCCMigration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blog_Post",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RecordCreatedBy = table.Column<string>(type: "VARCHAR(30)", nullable: true),
                    RecordUpdatedBy = table.Column<string>(type: "VARCHAR(30)", nullable: true),
                    RecordCreationDate = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    RecordUpdateDate = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    Title = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    Body = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog_Post", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blog_Post");
        }
    }
}
