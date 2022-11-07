using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace amorphie.tag.data.Migrations
{
    /// <inheritdoc />
    public partial class seed3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TagRelations",
                columns: new[] { "OwnerName", "TagName" },
                values: new object[,]
                {
                    { "idm", "corporate-customer" },
                    { "idm", "loan-partner" },
                    { "idm", "retail-customer" },
                    { "retail-loan", "loan-partner" },
                    { "retail-loan", "retail-customer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TagRelations",
                keyColumns: new[] { "OwnerName", "TagName" },
                keyValues: new object[] { "idm", "corporate-customer" });

            migrationBuilder.DeleteData(
                table: "TagRelations",
                keyColumns: new[] { "OwnerName", "TagName" },
                keyValues: new object[] { "idm", "loan-partner" });

            migrationBuilder.DeleteData(
                table: "TagRelations",
                keyColumns: new[] { "OwnerName", "TagName" },
                keyValues: new object[] { "idm", "retail-customer" });

            migrationBuilder.DeleteData(
                table: "TagRelations",
                keyColumns: new[] { "OwnerName", "TagName" },
                keyValues: new object[] { "retail-loan", "loan-partner" });

            migrationBuilder.DeleteData(
                table: "TagRelations",
                keyColumns: new[] { "OwnerName", "TagName" },
                keyValues: new object[] { "retail-loan", "retail-customer" });
        }
    }
}
