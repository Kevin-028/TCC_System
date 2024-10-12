using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TCC_System_Data.Migrations
{
    public partial class TCCMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProjectID",
                table: "MessageAction",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectID",
                table: "MessageAction");
        }
    }
}
