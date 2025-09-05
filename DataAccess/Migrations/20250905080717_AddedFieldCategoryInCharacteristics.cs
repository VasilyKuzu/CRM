using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    /// <inheritdoc />
    public partial class AddedFieldCategoryInCharacteristics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Characteristics",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Characteristics_CategoryID",
                table: "Characteristics",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_Categories_CategoryID",
                table: "Characteristics",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_Categories_CategoryID",
                table: "Characteristics");

            migrationBuilder.DropIndex(
                name: "IX_Characteristics_CategoryID",
                table: "Characteristics");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Characteristics");
        }
    }
}
