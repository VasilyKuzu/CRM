using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CRM.Migrations
{
    /// <inheritdoc />
    public partial class ChangedCharacteristics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "Characteristics");

            migrationBuilder.CreateTable(
                name: "CharacteristicValues",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductID = table.Column<int>(type: "integer", nullable: false),
                    CharacteristicID = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacteristicValues", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CharacteristicValues_Characteristics_CharacteristicID",
                        column: x => x.CharacteristicID,
                        principalTable: "Characteristics",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacteristicValues_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicValues_CharacteristicID",
                table: "CharacteristicValues",
                column: "CharacteristicID");

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicValues_ProductID",
                table: "CharacteristicValues",
                column: "ProductID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacteristicValues");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "Characteristics",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
