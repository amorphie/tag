using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace amorphie.tag.data.Migrations
{
    /// <inheritdoc />
    public partial class seed1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Ttl",
                table: "Tags",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Name", "Ttl", "Url" },
                values: new object[,]
                {
                    { "corporate-customer", 10, "http://localhost:3000/cb.customers/@param1" },
                    { "idm", null, null },
                    { "loan-partner", 10, "http://localhost:3000/cb.partner/@partner" },
                    { "retail-customer", 100, "http://localhost:3000/cb.customers/@param1?dbid=@param2&loveid=@param3" },
                    { "retail-loan", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Name",
                keyValue: "corporate-customer");

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Name",
                keyValue: "idm");

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Name",
                keyValue: "loan-partner");

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Name",
                keyValue: "retail-customer");

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Name",
                keyValue: "retail-loan");

            migrationBuilder.AlterColumn<int>(
                name: "Ttl",
                table: "Tags",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
