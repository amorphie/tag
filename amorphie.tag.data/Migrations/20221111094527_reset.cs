using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace amorphie.tag.data.Migrations
{
    /// <inheritdoc />
    public partial class reset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Domains",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domains", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: true),
                    Ttl = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Entites",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    DomainName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entites", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Entites_Domains_DomainName",
                        column: x => x.DomainName,
                        principalTable: "Domains",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagRelations",
                columns: table => new
                {
                    OwnerName = table.Column<string>(type: "text", nullable: false),
                    TagName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagRelations", x => new { x.OwnerName, x.TagName });
                    table.ForeignKey(
                        name: "FK_TagRelations_Tags_OwnerName",
                        column: x => x.OwnerName,
                        principalTable: "Tags",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagRelations_Tags_TagName",
                        column: x => x.TagName,
                        principalTable: "Tags",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Views",
                columns: table => new
                {
                    TagName = table.Column<string>(type: "text", nullable: false),
                    ViewTemplateName = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Views", x => new { x.TagName, x.ViewTemplateName });
                    table.ForeignKey(
                        name: "FK_Views_Tags_TagName",
                        column: x => x.TagName,
                        principalTable: "Tags",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntityData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityName = table.Column<string>(type: "text", nullable: true),
                    Field = table.Column<string>(type: "text", nullable: false),
                    Ttl = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityData_Entites_EntityName",
                        column: x => x.EntityName,
                        principalTable: "Entites",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateTable(
                name: "EntityDataSource",
                columns: table => new
                {
                    EntityDataId = table.Column<Guid>(type: "uuid", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    TagName = table.Column<string>(type: "text", nullable: true),
                    DataPath = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityDataSource", x => new { x.EntityDataId, x.Order });
                    table.ForeignKey(
                        name: "FK_EntityDataSource_EntityData_EntityDataId",
                        column: x => x.EntityDataId,
                        principalTable: "EntityData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntityDataSource_Tags_TagName",
                        column: x => x.TagName,
                        principalTable: "Tags",
                        principalColumn: "Name");
                });

            migrationBuilder.InsertData(
                table: "Domains",
                columns: new[] { "Name", "Description" },
                values: new object[] { "idm", "Identity Management Platform" });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Name", "Ttl", "Url" },
                values: new object[,]
                {
                    { "burgan-bank-turkey", 10, "http://localhost:3000/cb.bankInfo" },
                    { "burgan-staff", 10, "http://localhost:3000/cb.staff/@param1" },
                    { "corporate-customer", 10, "http://localhost:3000/cb.customers/@param1" },
                    { "idm", null, null },
                    { "loan-partner", 10, "http://localhost:3000/cb.partner/@partner" },
                    { "loan-partner-staff", 10, "http://localhost:3000/cb.partner/@partner/staff/@user" },
                    { "retail-customer", 5, "http://localhost:3000/cb.customers/@param1" },
                    { "retail-loan", null, null }
                });

            migrationBuilder.InsertData(
                table: "Entites",
                columns: new[] { "Name", "Description", "DomainName" },
                values: new object[,]
                {
                    { "scope", "Scope repository", "idm" },
                    { "user", "User repository", "idm" }
                });

            migrationBuilder.InsertData(
                table: "TagRelations",
                columns: new[] { "OwnerName", "TagName" },
                values: new object[,]
                {
                    { "idm", "burgan-bank-turkey" },
                    { "idm", "burgan-staff" },
                    { "idm", "corporate-customer" },
                    { "idm", "loan-partner" },
                    { "idm", "loan-partner-staff" },
                    { "idm", "retail-customer" },
                    { "retail-loan", "loan-partner" },
                    { "retail-loan", "retail-customer" }
                });

            migrationBuilder.InsertData(
                table: "Views",
                columns: new[] { "TagName", "ViewTemplateName", "Type" },
                values: new object[,]
                {
                    { "retail-customer", "retail-customer-flutter", 2 },
                    { "retail-customer", "retail-customer-mini-html", 0 }
                });

            migrationBuilder.InsertData(
                table: "EntityData",
                columns: new[] { "Id", "EntityName", "Field", "Ttl" },
                values: new object[,]
                {
                    { new Guid("107f4644-57cd-46ff-80de-004c6cd44704"), "user", "firstname", null },
                    { new Guid("207f4644-57cd-46ff-80de-004c6cd44704"), "user", "lastname", null },
                    { new Guid("307f4644-57cd-46ff-80de-004c6cd44704"), "scope", "title", null }
                });

            migrationBuilder.InsertData(
                table: "EntityDataSource",
                columns: new[] { "EntityDataId", "Order", "DataPath", "TagName" },
                values: new object[,]
                {
                    { new Guid("107f4644-57cd-46ff-80de-004c6cd44704"), 1, "$body.name", "burgan-staff" },
                    { new Guid("107f4644-57cd-46ff-80de-004c6cd44704"), 2, "$body.staf-name", "loan-partner-staff" },
                    { new Guid("107f4644-57cd-46ff-80de-004c6cd44704"), 3, "$body.firstname", "retail-customer" },
                    { new Guid("207f4644-57cd-46ff-80de-004c6cd44704"), 1, "$body.last", "burgan-staff" },
                    { new Guid("207f4644-57cd-46ff-80de-004c6cd44704"), 2, "$body.staf-last-name", "loan-partner-staff" },
                    { new Guid("207f4644-57cd-46ff-80de-004c6cd44704"), 3, "$body.lastname", "retail-customer" },
                    { new Guid("307f4644-57cd-46ff-80de-004c6cd44704"), 1, "$body.fullname", "burgan-staff" },
                    { new Guid("307f4644-57cd-46ff-80de-004c6cd44704"), 2, "$body.fullname", "loan-partner-staff" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entites_DomainName",
                table: "Entites",
                column: "DomainName");

            migrationBuilder.CreateIndex(
                name: "IX_EntityData_EntityName",
                table: "EntityData",
                column: "EntityName");

            migrationBuilder.CreateIndex(
                name: "IX_EntityDataSource_TagName",
                table: "EntityDataSource",
                column: "TagName");

            migrationBuilder.CreateIndex(
                name: "IX_TagRelations_TagName",
                table: "TagRelations",
                column: "TagName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityDataSource");

            migrationBuilder.DropTable(
                name: "TagRelations");

            migrationBuilder.DropTable(
                name: "Views");

            migrationBuilder.DropTable(
                name: "EntityData");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Entites");

            migrationBuilder.DropTable(
                name: "Domains");
        }
    }
}
