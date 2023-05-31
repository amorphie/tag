using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace amorphie.tag.data.Migrations
{
    /// <inheritdoc />
    public partial class addguid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Domains",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Entities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DomainId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entities_Domains_DomainId",
                        column: x => x.DomainId,
                        principalTable: "Domains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntityData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Field = table.Column<string>(type: "text", nullable: false),
                    Ttl = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityData_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Domains",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "Description", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Name" },
                values: new object[,]
                {
                    { new Guid("5b2bdd78-083f-41cc-a1d3-c02ebf0b347b"), new DateTime(2023, 5, 31, 9, 6, 8, 383, DateTimeKind.Utc).AddTicks(6671), new Guid("00000000-0000-0000-0000-000000000000"), null, "Domain 2 Description", new DateTime(2023, 5, 31, 9, 6, 8, 383, DateTimeKind.Utc).AddTicks(6672), new Guid("00000000-0000-0000-0000-000000000000"), null, "Domain 2" },
                    { new Guid("c9f909b0-6afc-4932-ba84-68ed5260acca"), new DateTime(2023, 5, 31, 9, 6, 8, 383, DateTimeKind.Utc).AddTicks(6664), new Guid("00000000-0000-0000-0000-000000000000"), null, "Domain 1 Description", new DateTime(2023, 5, 31, 9, 6, 8, 383, DateTimeKind.Utc).AddTicks(6667), new Guid("00000000-0000-0000-0000-000000000000"), null, "Domain 1" }
                });

            migrationBuilder.InsertData(
                table: "Entities",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "Description", "DomainId", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Name" },
                values: new object[,]
                {
                    { new Guid("7597240a-b917-4e4a-874f-a24efd2af0c8"), new DateTime(2023, 5, 31, 9, 6, 8, 383, DateTimeKind.Utc).AddTicks(6732), new Guid("00000000-0000-0000-0000-000000000000"), null, "Entity 1 Description", new Guid("c9f909b0-6afc-4932-ba84-68ed5260acca"), new DateTime(2023, 5, 31, 9, 6, 8, 383, DateTimeKind.Utc).AddTicks(6733), new Guid("00000000-0000-0000-0000-000000000000"), null, "Entity 1" },
                    { new Guid("d72db8e1-d3a0-4930-95a4-c0b0b25ce9c5"), new DateTime(2023, 5, 31, 9, 6, 8, 383, DateTimeKind.Utc).AddTicks(6745), new Guid("00000000-0000-0000-0000-000000000000"), null, "Entity 2 Description", new Guid("5b2bdd78-083f-41cc-a1d3-c02ebf0b347b"), new DateTime(2023, 5, 31, 9, 6, 8, 383, DateTimeKind.Utc).AddTicks(6745), new Guid("00000000-0000-0000-0000-000000000000"), null, "Entity 2" }
                });

            migrationBuilder.InsertData(
                table: "EntityData",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "EntityId", "Field", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Ttl" },
                values: new object[,]
                {
                    { new Guid("0046cda7-975c-4459-a850-b913f781b5e5"), new DateTime(2023, 5, 31, 9, 6, 8, 383, DateTimeKind.Utc).AddTicks(6761), new Guid("00000000-0000-0000-0000-000000000000"), null, new Guid("7597240a-b917-4e4a-874f-a24efd2af0c8"), "Field 1", new DateTime(2023, 5, 31, 9, 6, 8, 383, DateTimeKind.Utc).AddTicks(6761), new Guid("00000000-0000-0000-0000-000000000000"), null, 10 },
                    { new Guid("361b7b37-cdc6-46e4-90e1-0cf9a3feb1de"), new DateTime(2023, 5, 31, 9, 6, 8, 383, DateTimeKind.Utc).AddTicks(6766), new Guid("00000000-0000-0000-0000-000000000000"), null, new Guid("d72db8e1-d3a0-4930-95a4-c0b0b25ce9c5"), "Field 2", new DateTime(2023, 5, 31, 9, 6, 8, 383, DateTimeKind.Utc).AddTicks(6766), new Guid("00000000-0000-0000-0000-000000000000"), null, 20 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entities_DomainId",
                table: "Entities",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityData_EntityId",
                table: "EntityData",
                column: "EntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityData");

            migrationBuilder.DropTable(
                name: "Entities");

            migrationBuilder.DropTable(
                name: "Domains");
        }
    }
}
