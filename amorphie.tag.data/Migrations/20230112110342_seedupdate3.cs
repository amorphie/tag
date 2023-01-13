using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.tag.data.Migrations
{
    /// <inheritdoc />
    public partial class seedupdate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Name",
                keyValue: "burgan-bank-turkey",
                column: "Url",
                value: "http://localhost:3001/cb.bankInfo");

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Name",
                keyValue: "burgan-staff",
                column: "Url",
                value: "http://localhost:3001/cb.staff/@reference");

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Name",
                keyValue: "corporate-customer",
                column: "Url",
                value: "http://localhost:3001/cb.customers?reference=@reference");

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Name",
                keyValue: "loan-partner",
                column: "Url",
                value: "http://localhost:3001/cb.partner/@reference");

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Name",
                keyValue: "loan-partner-staff",
                column: "Url",
                value: "http://localhost:3001/cb.partner/@partner/staff/@reference");

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Name",
                keyValue: "retail-customer",
                column: "Url",
                value: "http://localhost:3001/cb.customers?reference=@reference");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Name",
                keyValue: "burgan-bank-turkey",
                column: "Url",
                value: "http://localhost:3000/cb.bankInfo");

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Name",
                keyValue: "burgan-staff",
                column: "Url",
                value: "http://localhost:3000/cb.staff/@reference");

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Name",
                keyValue: "corporate-customer",
                column: "Url",
                value: "http://localhost:3000/cb.customers?reference=@reference");

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Name",
                keyValue: "loan-partner",
                column: "Url",
                value: "http://localhost:3000/cb.partner/@reference");

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Name",
                keyValue: "loan-partner-staff",
                column: "Url",
                value: "http://localhost:3000/cb.partner/@partner/staff/@reference");

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Name",
                keyValue: "retail-customer",
                column: "Url",
                value: "http://localhost:3000/cb.customers?reference=@reference");
        }
    }
}
