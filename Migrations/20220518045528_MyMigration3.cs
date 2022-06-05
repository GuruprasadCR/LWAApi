using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LWAApi.Migrations
{
    public partial class MyMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblProducts_TblCatergories_CategoryId",
                table: "TblProducts");

            migrationBuilder.DropIndex(
                name: "IX_TblProducts_CategoryId",
                table: "TblProducts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TblProducts_CategoryId",
                table: "TblProducts",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblProducts_TblCatergories_CategoryId",
                table: "TblProducts",
                column: "CategoryId",
                principalTable: "TblCatergories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
