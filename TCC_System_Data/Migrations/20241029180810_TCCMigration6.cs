using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TCC_System_Data.Migrations
{
    public partial class TCCMigration6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Ardu_Modulo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Ardu_Modulo");
        }
    }
}
