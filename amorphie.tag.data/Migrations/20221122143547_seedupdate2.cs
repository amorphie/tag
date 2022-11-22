using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.tag.data.Migrations
{
    /// <inheritdoc />
    public partial class seedupdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EntityDataSource",
                keyColumns: new[] { "EntityDataId", "Order" },
                keyValues: new object[] { new Guid("107f4644-57cd-46ff-80de-004c6cd44704"), 1 },
                column: "DataPath",
                value: "$.firstname");

            migrationBuilder.UpdateData(
                table: "EntityDataSource",
                keyColumns: new[] { "EntityDataId", "Order" },
                keyValues: new object[] { new Guid("107f4644-57cd-46ff-80de-004c6cd44704"), 2 },
                column: "DataPath",
                value: "$.partner-staff.fullname");

            migrationBuilder.UpdateData(
                table: "EntityDataSource",
                keyColumns: new[] { "EntityDataId", "Order" },
                keyValues: new object[] { new Guid("107f4644-57cd-46ff-80de-004c6cd44704"), 3 },
                column: "DataPath",
                value: "$.firstname");

            migrationBuilder.UpdateData(
                table: "EntityDataSource",
                keyColumns: new[] { "EntityDataId", "Order" },
                keyValues: new object[] { new Guid("207f4644-57cd-46ff-80de-004c6cd44704"), 1 },
                column: "DataPath",
                value: "$.lastname");

            migrationBuilder.UpdateData(
                table: "EntityDataSource",
                keyColumns: new[] { "EntityDataId", "Order" },
                keyValues: new object[] { new Guid("207f4644-57cd-46ff-80de-004c6cd44704"), 2 },
                column: "DataPath",
                value: "$.partner-staff.fullname");

            migrationBuilder.UpdateData(
                table: "EntityDataSource",
                keyColumns: new[] { "EntityDataId", "Order" },
                keyValues: new object[] { new Guid("207f4644-57cd-46ff-80de-004c6cd44704"), 3 },
                column: "DataPath",
                value: "$.lastname");

            migrationBuilder.UpdateData(
                table: "EntityDataSource",
                keyColumns: new[] { "EntityDataId", "Order" },
                keyValues: new object[] { new Guid("307f4644-57cd-46ff-80de-004c6cd44704"), 1 },
                column: "DataPath",
                value: "$.firstname");

            migrationBuilder.UpdateData(
                table: "EntityDataSource",
                keyColumns: new[] { "EntityDataId", "Order" },
                keyValues: new object[] { new Guid("307f4644-57cd-46ff-80de-004c6cd44704"), 2 },
                column: "DataPath",
                value: "$.partner-staff.fullname");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                keyValues: new object[] { new Guid("107f4644-57cd-46ff-80de-004c6cd44704"), 3 },
                column: "DataPath",
                value: "$body.firstname");

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
                value: "$body.partner-staff.fullname");

            migrationBuilder.UpdateData(
                table: "EntityDataSource",
                keyColumns: new[] { "EntityDataId", "Order" },
                keyValues: new object[] { new Guid("207f4644-57cd-46ff-80de-004c6cd44704"), 3 },
                column: "DataPath",
                value: "$body.lastname");

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
        }
    }
}
