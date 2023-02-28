using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace amorphie.tag.data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Domains",
                columns: table => new
                {
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
                    table.PrimaryKey("PK_Domains", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: true),
                    Ttl = table.Column<int>(type: "integer", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Entities",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DomainName = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entities", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Entities_Domains_DomainName",
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
                    TagName = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
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
                    Type = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
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
                    EntityName = table.Column<string>(type: "text", nullable: false),
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
                        name: "FK_EntityData_Entities_EntityName",
                        column: x => x.EntityName,
                        principalTable: "Entities",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntityDataSource",
                columns: table => new
                {
                    EntityDataId = table.Column<Guid>(type: "uuid", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    TagName = table.Column<string>(type: "text", nullable: false),
                    DataPath = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
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
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Domains",
                columns: new[] { "Name", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "Description", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf" },
                values: new object[] { "idm", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(8677), new Guid("09cd784f-7977-439c-ac0a-73e4c1a95343"), null, "Identity Management Platform", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(8685), new Guid("866df866-89d2-4090-97ae-d95e9669dc41"), new Guid("e8dd17b7-bb28-4f07-8345-d1013e81fd59") });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Name", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "CreatedDate", "LastModifiedDate", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Ttl", "Url" },
                values: new object[,]
                {
                    { "burgan-bank-turkey", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(7838), new Guid("82f64b26-951d-4a38-bc57-68369c33cf13"), null, null, null, new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(7840), new Guid("6377e925-c9cd-48b6-b1ae-a705339aa9d5"), new Guid("935c67b7-8f5a-4959-ac42-24f4d9e89cb7"), 10, "http://localhost:3001/cb.bankInfo" },
                    { "burgan-staff", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(7813), new Guid("6a4dd506-0e16-476d-9f61-f4fa947702f0"), null, null, null, new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(7815), new Guid("2b0c77b3-1e6e-4f6a-b2ec-4d9e3d783347"), new Guid("ddfc11b9-2d39-4212-80f6-9d77c5c103e3"), 10, "http://localhost:3001/cb.staff/@reference" },
                    { "corporate-customer", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(7487), new Guid("60c0f456-ca67-4683-a0ce-fc0034005910"), null, null, null, new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(7491), new Guid("862001d5-fa24-43a5-a67e-92de55c356d6"), new Guid("337c1687-f3fc-4ee4-8204-b99cb01f7eca"), 10, "http://localhost:3001/cb.customers?reference=@reference" },
                    { "idm", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(7100), new Guid("b37e1756-8bdf-4c97-9e3c-3e36fd35a16c"), null, null, null, new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(7103), new Guid("44cef650-b1b2-433b-8f77-b24deec6848c"), new Guid("ff890aec-a2ed-4d67-902c-bdc813d6de76"), null, null },
                    { "loan-partner", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(7515), new Guid("69449ed1-528e-443b-96f0-9719cb7bbaa9"), null, null, null, new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(7518), new Guid("822df56c-abad-4139-966a-a0a14119b6cf"), new Guid("2ac2ddd0-00a7-4c21-a3cd-3e338fdcdcd3"), 10, "http://localhost:3001/cb.partner/@reference" },
                    { "loan-partner-staff", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(7784), new Guid("9f9141f5-ea09-4def-a595-245bc1609ca8"), null, null, null, new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(7788), new Guid("7511c754-846a-43ef-ba92-75500e64620e"), new Guid("809fdeda-ef3e-4aaf-8ee9-5a3ccec6a2a9"), 10, "http://localhost:3001/cb.partner/@partner/staff/@reference" },
                    { "retail-customer", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(7129), new Guid("aa2c9c10-7eaf-45cb-ab1b-253c059d0347"), null, new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(7164), null, new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(7152), new Guid("89581a97-2968-475a-a27d-896c327c6762"), new Guid("cb46b3b4-8e89-42da-a7fd-17bb1f584cfc"), 5, "http://localhost:3001/cb.customers?reference=@reference" },
                    { "retail-loan", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(6969), new Guid("078373c5-e501-455b-a258-1e95c8222a92"), null, null, null, new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(7025), new Guid("b100f296-57aa-4d78-924f-3d0d174bbee7"), new Guid("3686d977-b893-4ca5-bc87-0a79d727d28d"), null, null }
                });

            migrationBuilder.InsertData(
                table: "Entities",
                columns: new[] { "Name", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "Description", "DomainName", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf" },
                values: new object[,]
                {
                    { "scope", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(8747), new Guid("e48d8120-fea8-4614-be50-3c214030443e"), null, "Scope repository", "idm", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(8749), new Guid("f922be6c-c618-44b5-9cb9-145d3871e402"), new Guid("a7c12e56-ebaf-4d5f-8e04-5c2f94037126") },
                    { "user", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(8717), new Guid("5e6bfe31-f2f4-46dd-bb31-9037a428a0b8"), null, "User repository", "idm", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(8720), new Guid("a74fb812-0443-4921-bbdf-78038160aa02"), new Guid("6b046bb8-4f9d-449e-a153-a38db1b9ffcc") }
                });

            migrationBuilder.InsertData(
                table: "TagRelations",
                columns: new[] { "OwnerName", "TagName", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf" },
                values: new object[,]
                {
                    { "idm", "burgan-bank-turkey", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(8300), new Guid("4e45b261-5c0e-452f-a88b-ac0f689294c5"), null, new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(8302), new Guid("cdeb9b95-fc8b-46d8-b6ab-9fd84b693d6f"), new Guid("8bf516a2-d877-4d97-82db-121a3ffc9f19") },
                    { "idm", "burgan-staff", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(8266), new Guid("6a28afc0-6d81-427f-9428-831177f50ed4"), null, new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(8269), new Guid("5e71aad6-2849-4692-ad30-4c1df132018e"), new Guid("0a1c002f-8702-46c7-b38d-ee358dcf1a85") },
                    { "idm", "corporate-customer", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(7950), new Guid("cb0cacdc-0127-4fb4-9e9d-b478ded9d6cd"), null, new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(7953), new Guid("7ef214cb-d730-447c-8b13-2cf41242aa18"), new Guid("90f80811-f480-4e44-aa85-3c9b6454d579") },
                    { "idm", "loan-partner", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(8028), new Guid("bffe6012-ae82-4941-848a-989ae6b77494"), null, new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(8036), new Guid("f25ec2ae-acfb-4116-ac80-fc452867afe3"), new Guid("ebf519b7-b4ce-4afc-9b50-8a979a9c6cc5") },
                    { "idm", "loan-partner-staff", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(8058), new Guid("9539c527-b261-4fdf-964f-8eebbf1b803e"), null, new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(8060), new Guid("f7c677ac-339f-400e-ad46-caa4412a2ea6"), new Guid("19c8ee69-4ac8-419d-8b97-24ecaf85b9b0") },
                    { "idm", "retail-customer", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(8003), new Guid("288d4982-0f4a-46b6-b40c-e6af5c75125b"), null, new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(8006), new Guid("e20371d6-a87a-4189-8214-b247859862a2"), new Guid("e50f21ff-3c55-4a10-b87e-903eac66ccba") },
                    { "retail-loan", "loan-partner", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(8643), new Guid("59409a9e-13bc-4f38-8803-780b7652118b"), null, new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(8646), new Guid("51b674d3-60b0-4b7a-afe4-a0200bb7986c"), new Guid("3ee8bd67-bd8f-4bf9-b8c2-c67969e87436") },
                    { "retail-loan", "retail-customer", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(8620), new Guid("0e5ead29-de34-468c-8035-ac80a069ed9e"), null, new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(8623), new Guid("231e590f-ec92-4fc2-ae02-89b53a9e7ede"), new Guid("a9e67f5b-b6a0-41ae-885a-608c29bdb7c0") }
                });

            migrationBuilder.InsertData(
                table: "Views",
                columns: new[] { "TagName", "ViewTemplateName", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Type" },
                values: new object[,]
                {
                    { "retail-customer", "retail-customer-flutter", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(8587), new Guid("c150facf-8db8-4607-be30-05624ebbee7f"), null, new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(8590), new Guid("c2773572-824f-4d50-89c7-d41ddc723eae"), new Guid("97ac4c25-d91e-4dd2-be6f-c6b4c6ca6d77"), 2 },
                    { "retail-customer", "retail-customer-mini-html", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(8330), new Guid("5ff46d80-d5db-4881-ba5d-6c285ed0becb"), null, new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(8332), new Guid("1762169e-e044-46af-9472-a9c9cdb34123"), new Guid("966e3a77-fc48-4a72-a190-fd65a1a482f5"), 0 }
                });

            migrationBuilder.InsertData(
                table: "EntityData",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "EntityName", "Field", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Ttl" },
                values: new object[,]
                {
                    { new Guid("107f4644-57cd-46ff-80de-004c6cd44704"), new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(8806), new Guid("b1a3577c-6631-4b93-b027-bb36ad4cf44e"), null, "user", "firstname", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(8810), new Guid("9e02a56e-d6d4-49e4-a7b6-fb64ee424abb"), new Guid("81a7da14-768d-436e-a811-f5ccdd9af89b"), null },
                    { new Guid("207f4644-57cd-46ff-80de-004c6cd44704"), new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(8843), new Guid("e9416656-b246-465d-a005-0079a2be9802"), null, "user", "lastname", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(8846), new Guid("a7e59032-7e0a-4dfa-afc4-980c91940404"), new Guid("5a71d4e9-8b35-4dba-93fc-ccdb30fe8bb9"), null },
                    { new Guid("307f4644-57cd-46ff-80de-004c6cd44704"), new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(8886), new Guid("78b89203-56ba-435e-a930-c39093533372"), null, "scope", "title", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(8889), new Guid("ff7ca4d9-ecfa-464e-bc06-a8e3588197b6"), new Guid("2785a214-09ba-40fa-b022-9cc11487af66"), null }
                });

            migrationBuilder.InsertData(
                table: "EntityDataSource",
                columns: new[] { "EntityDataId", "Order", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "DataPath", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "TagName" },
                values: new object[,]
                {
                    { new Guid("107f4644-57cd-46ff-80de-004c6cd44704"), 1, new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(8923), new Guid("e2a94982-0fe3-47bf-b1e7-58fce2c49023"), null, "$.firstname", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(9124), new Guid("ab082004-2175-42a9-bd7b-f1dfdbc8039f"), new Guid("42ded41a-eb56-4066-8d6a-2945bf7df743"), "burgan-staff" },
                    { new Guid("107f4644-57cd-46ff-80de-004c6cd44704"), 2, new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(9161), new Guid("43ebd1fd-41ef-4a96-ba6d-def52f732ac8"), null, "$.partner-staff.fullname", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(9164), new Guid("34692ddd-97c1-41e5-b2c2-0526557d1e36"), new Guid("1e91dfb8-6d57-4752-ba2e-8cbfb2551a0a"), "loan-partner-staff" },
                    { new Guid("107f4644-57cd-46ff-80de-004c6cd44704"), 3, new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(9187), new Guid("464fc1c2-177b-4851-a05f-ccd34d841b4d"), null, "$.firstname", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(9194), new Guid("321bd9f8-913a-4604-878a-48a69132bfae"), new Guid("0c41917b-d9fe-4847-a48a-ef8a74dfea00"), "retail-customer" },
                    { new Guid("207f4644-57cd-46ff-80de-004c6cd44704"), 1, new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(9215), new Guid("eb18b9b6-14a8-447b-b6ec-dc89cec667ae"), null, "$.lastname", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(9218), new Guid("794cb834-c3ff-4450-8324-e4ba9fcfd0e4"), new Guid("7adc42a3-fbe1-4388-b09d-feb466669f6c"), "burgan-staff" },
                    { new Guid("207f4644-57cd-46ff-80de-004c6cd44704"), 2, new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(9239), new Guid("545e15dd-b848-4049-a184-8ac7e30546fc"), null, "$.partner-staff.fullname", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(9241), new Guid("cdecb868-4cac-4a7b-8778-9aba806059da"), new Guid("970e3e71-5353-44af-a32d-8678ed069775"), "loan-partner-staff" },
                    { new Guid("207f4644-57cd-46ff-80de-004c6cd44704"), 3, new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(9269), new Guid("43727ab2-e7ea-421c-80ca-1c36324b2c3e"), null, "$.lastname", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(9271), new Guid("29822d47-637f-4c70-946f-846035a846f6"), new Guid("854243bc-877a-4f98-9dd8-6b17fc982d00"), "retail-customer" },
                    { new Guid("307f4644-57cd-46ff-80de-004c6cd44704"), 1, new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(9474), new Guid("2085349a-f319-4e20-b6bb-3e8af51354f5"), null, "$.firstname", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(9476), new Guid("0836c81a-a8ba-4978-b2fa-d37f8428c41b"), new Guid("a905fca1-2aa3-48a0-bfe0-3417411b0ff0"), "burgan-staff" },
                    { new Guid("307f4644-57cd-46ff-80de-004c6cd44704"), 2, new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(9494), new Guid("e2ae8a10-634b-4cf7-a940-555e435a6726"), null, "$.partner-staff.fullname", new DateTime(2023, 2, 21, 13, 7, 21, 578, DateTimeKind.Utc).AddTicks(9496), new Guid("7c17bb31-fbbc-49bd-a759-ae4027c58422"), new Guid("4e3c28df-6bc7-4766-9b81-0110ef30143b"), "loan-partner-staff" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entities_DomainName",
                table: "Entities",
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
                name: "Entities");

            migrationBuilder.DropTable(
                name: "Domains");
        }
    }
}
