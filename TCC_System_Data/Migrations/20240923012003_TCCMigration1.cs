using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TCC_System_Data.Migrations
{
    public partial class TCCMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessageAction",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RecordCreatedBy = table.Column<string>(type: "VARCHAR(30)", nullable: true),
                    RecordUpdatedBy = table.Column<string>(type: "VARCHAR(30)", nullable: true),
                    RecordCreationDate = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    RecordUpdateDate = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    Type = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    Action = table.Column<string>(type: "VARCHAR(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageAction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RecordCreatedBy = table.Column<string>(type: "VARCHAR(30)", nullable: true),
                    RecordUpdatedBy = table.Column<string>(type: "VARCHAR(30)", nullable: true),
                    RecordCreationDate = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    RecordUpdateDate = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modulo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RecordCreatedBy = table.Column<string>(type: "VARCHAR(30)", nullable: true),
                    RecordUpdatedBy = table.Column<string>(type: "VARCHAR(30)", nullable: true),
                    RecordCreationDate = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    RecordUpdateDate = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    Type = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    Value = table.Column<string>(nullable: true),
                    ProductId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modulo_Produto_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Modulo_ProductId",
                table: "Modulo",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageAction");

            migrationBuilder.DropTable(
                name: "Modulo");

            migrationBuilder.DropTable(
                name: "Produto");
        }
    }
}
