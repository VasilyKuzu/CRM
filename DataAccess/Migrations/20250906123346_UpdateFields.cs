using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SupplierName",
                table: "Suppliers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "SupplierID",
                table: "Suppliers",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Products",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ProductArticle",
                table: "Products",
                newName: "Article");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "Products",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Categories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Categories",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "BrandName",
                table: "Brands",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "BrandID",
                table: "Brands",
                newName: "ID");

            migrationBuilder.AlterColumn<string>(
                name: "SupplierProductArticle",
                table: "ProductSuppliers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Suppliers",
                newName: "SupplierName");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Suppliers",
                newName: "SupplierID");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "Article",
                table: "Products",
                newName: "ProductArticle");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Products",
                newName: "ProductID");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Categories",
                newName: "CategoryName");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Categories",
                newName: "CategoryID");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Brands",
                newName: "BrandName");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Brands",
                newName: "BrandID");

            migrationBuilder.AlterColumn<string>(
                name: "SupplierProductArticle",
                table: "ProductSuppliers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
