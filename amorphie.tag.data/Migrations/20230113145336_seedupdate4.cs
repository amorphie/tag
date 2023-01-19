using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.tag.data.Migrations
{
    /// <inheritdoc />
    public partial class seedupdate4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntityData_Entities_EntityName",
                table: "EntityData");

            migrationBuilder.AlterColumn<string>(
                name: "EntityName",
                table: "EntityData",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "EntityData",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "EntityData",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EntityData",
                keyColumn: "Id",
                keyValue: new Guid("107f4644-57cd-46ff-80de-004c6cd44704"),
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "EntityData",
                keyColumn: "Id",
                keyValue: new Guid("207f4644-57cd-46ff-80de-004c6cd44704"),
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "EntityData",
                keyColumn: "Id",
                keyValue: new Guid("307f4644-57cd-46ff-80de-004c6cd44704"),
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Name",
                keyValue: "retail-customer",
                column: "CreatedDate",
                value: new DateTime(2023, 1, 13, 14, 53, 36, 110, DateTimeKind.Utc).AddTicks(7688));

            migrationBuilder.AddForeignKey(
                name: "FK_EntityData_Entities_EntityName",
                table: "EntityData",
                column: "EntityName",
                principalTable: "Entities",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntityData_Entities_EntityName",
                table: "EntityData");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "EntityData");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "EntityData");

            migrationBuilder.AlterColumn<string>(
                name: "EntityName",
                table: "EntityData",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Name",
                keyValue: "retail-customer",
                column: "CreatedDate",
                value: new DateTime(2023, 1, 13, 12, 50, 3, 821, DateTimeKind.Utc).AddTicks(9256));

            migrationBuilder.AddForeignKey(
                name: "FK_EntityData_Entities_EntityName",
                table: "EntityData",
                column: "EntityName",
                principalTable: "Entities",
                principalColumn: "Name");
        }
    }
}
