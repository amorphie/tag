using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.tag.data.Migrations
{
    /// <inheritdoc />
    public partial class seedupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EntityDataSource",
                keyColumns: new[] { "EntityDataId", "Order" },
                keyValues: new object[] { new Guid("107f4644-57cd-46ff-80de-004c6cd44704"), 1 },
                column: "DataPath",
                value: "$body.firstname");

            migrationBuilder.UpdateData(
                table: "EntityDataSource",
                keyColumns: new[] { "EntityDataId", "Order" },
                keyValues: new object[] { new Guid("107f4644-57cd-46ff-80de-004c6cd44704"), 2 },
                column: "DataPath",
                value: "$body.partner-staff.fullname");

            migrationBuilder.UpdateData(
                table: "EntityDataSource",
                keyColumns: new[] { "EntityDataId", "Order" },
                keyValues: new object[] { new Guid("207f4644-57cd-46ff-80de-004c6cd44704"), 1 },
                column: "DataPath",
                value: "$body.lastname");

            migrationBuilder.UpdateData(
                table: "EntityDataSource",
                keyColumns: new[] { "EntityDataId", "Order" },
                keyValues: new object[] { new Guid("207f4644-57cd-46ff-80de-004c6cd44704"), 2 },
                column: "DataPath",
                value: "body.partner-staff.fullname");

            migrationBuilder.UpdateData(
                table: "EntityDataSource",
                keyColumns: new[] { "EntityDataId", "Order" },
                keyValues: new object[] { new Guid("307f4644-57cd-46ff-80de-004c6cd44704"), 1 },
                column: "DataPath",
                value: "$body.firstname");

            migrationBuilder.UpdateData(
                table: "EntityDataSource",
                keyColumns: new[] { "EntityDataId", "Order" },
                keyValues: new object[] { new Guid("307f4644-57cd-46ff-80de-004c6cd44704"), 2 },
                column: "DataPath",
                value: "$body.partner-staff.fullname");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EntityDataSource",
                keyColumns: new[] { "EntityDataId", "Order" },
                keyValues: new object[] { new Guid("107f4644-57cd-46ff-80de-004c6cd44704"), 1 },
                column: "DataPath",
                value: "$body.name");

            migrationBuilder.UpdateData(
                table: "EntityDataSource",
                keyColumns: new[] { "EntityDataId", "Order" },
                keyValues: new object[] { new Guid("107f4644-57cd-46ff-80de-004c6cd44704"), 2 },
                column: "DataPath",
                value: "$body.staf-name");

            migrationBuilder.UpdateData(
                table: "EntityDataSource",
                keyColumns: new[] { "EntityDataId", "Order" },
                keyValues: new object[] { new Guid("207f4644-57cd-46ff-80de-004c6cd44704"), 1 },
                column: "DataPath",
                value: "$body.last");

            migrationBuilder.UpdateData(
                table: "EntityDataSource",
                keyColumns: new[] { "EntityDataId", "Order" },
                keyValues: new object[] { new Guid("207f4644-57cd-46ff-80de-004c6cd44704"), 2 },
                column: "DataPath",
                value: "$body.staf-last-name");

            migrationBuilder.UpdateData(
                table: "EntityDataSource",
                keyColumns: new[] { "EntityDataId", "Order" },
                keyValues: new object[] { new Guid("307f4644-57cd-46ff-80de-004c6cd44704"), 1 },
                column: "DataPath",
                value: "$body.fullname");

            migrationBuilder.UpdateData(
                table: "EntityDataSource",
                keyColumns: new[] { "EntityDataId", "Order" },
                keyValues: new object[] { new Guid("307f4644-57cd-46ff-80de-004c6cd44704"), 2 },
                column: "DataPath",
                value: "$body.fullname");

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Name",
                keyValue: "burgan-staff",
                column: "Url",
                value: "http://localhost:3000/cb.staff/@param1");

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Name",
                keyValue: "corporate-customer",
                column: "Url",
                value: "http://localhost:3000/cb.customers/@param1");

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Name",
                keyValue: "loan-partner",
                column: "Url",
                value: "http://localhost:3000/cb.partner/@partner");

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Name",
                keyValue: "loan-partner-staff",
                column: "Url",
                value: "http://localhost:3000/cb.partner/@partner/staff/@user");

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Name",
                keyValue: "retail-customer",
                column: "Url",
                value: "http://localhost:3000/cb.customers/@param1");
        }
    }
}
