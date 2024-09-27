using Microsoft.EntityFrameworkCore.Migrations;

namespace TCC_System_Data.Migrations
{
    public partial class TCCMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modulo_Produto_ProductId",
                table: "Modulo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Modulo",
                table: "Modulo");

            migrationBuilder.RenameTable(
                name: "Modulo",
                newName: "Ardu_Modulo");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Produto",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Modulo_ProductId",
                table: "Ardu_Modulo",
                newName: "IX_Ardu_Modulo_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ardu_Modulo",
                table: "Ardu_Modulo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ardu_Modulo_Produto_ProductId",
                table: "Ardu_Modulo",
                column: "ProductId",
                principalTable: "Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ardu_Modulo_Produto_ProductId",
                table: "Ardu_Modulo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ardu_Modulo",
                table: "Ardu_Modulo");

            migrationBuilder.RenameTable(
                name: "Ardu_Modulo",
                newName: "Modulo");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Produto",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Ardu_Modulo_ProductId",
                table: "Modulo",
                newName: "IX_Modulo_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Modulo",
                table: "Modulo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Modulo_Produto_ProductId",
                table: "Modulo",
                column: "ProductId",
                principalTable: "Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
