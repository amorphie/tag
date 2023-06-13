using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace amorphie.tag.data.Migrations
{
    /// <inheritdoc />
    public partial class MyFirstMigration : Migration
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
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Entities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DomainId = table.Column<Guid>(type: "uuid", nullable: false),
                    DomainName = table.Column<string>(type: "text", nullable: true),
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
                name: "TagRelations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnerName = table.Column<string>(type: "text", nullable: false),
                    TagId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_TagRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagRelations_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Views",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TagId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_Views", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Views_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
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
                    EntityName = table.Column<string>(type: "text", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "EntityDataSource",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityDataId = table.Column<Guid>(type: "uuid", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    TagId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_EntityDataSource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityDataSource_EntityData_EntityDataId",
                        column: x => x.EntityDataId,
                        principalTable: "EntityData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntityDataSource_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Domains",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "Description", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Name" },
                values: new object[] { new Guid("107f4644-57cd-46ff-80de-004c6cd44712"), new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9922), new Guid("22c91f2f-7f9e-4b25-baac-0d3d415a1b0e"), null, "Identity Management Platform", new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9920), new Guid("e0d281f4-91cc-435b-af9c-dad9b45ad771"), null, "idm" });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "CreatedDate", "LastModifiedDate", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Name", "Ttl", "Url" },
                values: new object[,]
                {
                    { new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9316), new Guid("659f5a42-6510-4749-9a36-959ffc1aa2d8"), null, null, null, new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9314), new Guid("70a1204a-2750-4280-9da2-cd3ec2bdb467"), null, "retail-customer", 5, "http://localhost:3000/cb.customers?reference=@reference" },
                    { new Guid("107f4644-57cd-46ff-80de-004c6cd44778"), new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9168), new Guid("c9818ced-0248-49cd-a75f-1c1b172c4368"), null, null, null, new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9163), new Guid("9bc8f9bd-90c6-456a-8b2a-ca40dbca4c5b"), null, "retail-loan", null, null },
                    { new Guid("2c05eda0-37af-406d-aa20-d3cddbbc5559"), new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9370), new Guid("f2ac04d3-5d6a-4c2a-98bb-77f9df392e8f"), null, null, null, new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9367), new Guid("64daeee6-bb69-45a3-8a7f-6919b4b9bfee"), null, "corporate-customer", 10, "http://localhost:3000/cb.customers?reference=@reference" },
                    { new Guid("6364ea95-7503-4213-906f-b9b33ac2125e"), new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9469), new Guid("0884c075-a3a3-4ecd-b3cf-3b3ceec25507"), null, null, null, new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9467), new Guid("2c9159a5-b808-41ba-8de9-433510db1896"), null, "burgan-staff", 10, "http://localhost:3000/cb.staff/@reference" },
                    { new Guid("785c4491-ec21-4803-ad41-af68ffd48c08"), new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9398), new Guid("2896c170-83ac-4741-9efb-9bda3862da34"), null, null, null, new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9396), new Guid("c5d90686-90eb-4ab3-a635-2ca5ec6d7fcc"), null, "loan-partner", 10, "http://localhost:3000/cb.partner/@reference" },
                    { new Guid("8989d945-6e7c-4b6b-97e8-aea0f154b9ca"), new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9434), new Guid("ed21daa1-167d-4a74-86cb-c8ed32f8ec8f"), null, null, null, new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9433), new Guid("94901b0f-78bd-4718-99f0-b51adecc5859"), null, "loan-partner-staff", 10, "http://localhost:3000/cb.partner/@partner/staff/@reference" },
                    { new Guid("ecbe79d5-3ce6-47b8-868a-a776c0c01785"), new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9497), new Guid("1ad3b69d-fb78-4b8f-8473-645483860ad4"), null, null, null, new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9495), new Guid("fd9b1f9e-42fb-4da0-a4fd-b942185d4a79"), null, "burgan-bank-turkey", 10, "http://localhost:3000/cb.bankInfo" },
                    { new Guid("fe00fb16-f2c2-47e6-b46d-b72b4fb5d649"), new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9286), new Guid("98ef09f0-a462-40b7-9e42-94295c622755"), null, null, null, new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9284), new Guid("56150df5-67d2-4683-a8ff-7338aca205d4"), null, "idm", null, null }
                });

            migrationBuilder.InsertData(
                table: "Entities",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "Description", "DomainId", "DomainName", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Name" },
                values: new object[,]
                {
                    { new Guid("107f4644-57cd-46ff-80de-004c6cd44710"), new DateTime(2023, 6, 13, 11, 40, 24, 575, DateTimeKind.Utc).AddTicks(19), new Guid("6b9e44f2-091e-4356-916f-d673a6104d0a"), null, "Scope repository", new Guid("107f4644-57cd-46ff-80de-004c6cd44712"), "idm", new DateTime(2023, 6, 13, 11, 40, 24, 575, DateTimeKind.Utc).AddTicks(17), new Guid("82beefe1-24ed-4c8a-9ca9-7d19793c3bca"), null, "scope" },
                    { new Guid("107f4644-57cd-46ff-80de-004c6cd44715"), new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9976), new Guid("4158f9a5-b561-4373-ae85-f21c46f5f158"), null, "User repository", new Guid("107f4644-57cd-46ff-80de-004c6cd44712"), "idm", new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9969), new Guid("83dbe7e1-3eb5-4c9f-b24f-68175f6b58db"), null, "user" }
                });

            migrationBuilder.InsertData(
                table: "TagRelations",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "OwnerName", "TagId", "TagName" },
                values: new object[,]
                {
                    { new Guid("0f8267d4-7163-4a1c-a78c-aa3764e18d25"), new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9866), new Guid("aa1da4b2-dc90-4380-9828-f86160082a97"), null, new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9864), new Guid("0497f47b-7443-437a-8a80-00a85b7ce900"), null, "retail-loan", new Guid("107f4644-57cd-46ff-80de-004c6cd44778"), "loan-partner" },
                    { new Guid("447cacd1-8ff4-497b-a42c-ee7bd3953ed9"), new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9572), new Guid("52f79f83-2047-4d73-944f-de3776990d9d"), null, new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9570), new Guid("f33129f7-f254-4c1d-87d3-bae68edee87b"), null, "idm", new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "retail-customer" },
                    { new Guid("beea6721-35ee-4bb8-93a8-e93666c553b9"), new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9709), new Guid("022011e5-3917-4d4a-8d06-a9d7f41c8b93"), null, new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9706), new Guid("e69abd40-ab87-4960-9f33-3292ab8c2dc2"), null, "idm", new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "burgan-bank-turkey" },
                    { new Guid("c685b526-4d80-403a-a527-3d7b488835c0"), new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9836), new Guid("9fa5e09a-79ce-4154-a1f7-e705f27fa137"), null, new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9829), new Guid("b706ba7a-e56c-4276-a5b4-b8246acce4ad"), null, "retail-loan", new Guid("107f4644-57cd-46ff-80de-004c6cd44778"), "retail-customer" },
                    { new Guid("c8fe329e-9903-4aa8-8f70-513699efb201"), new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9534), new Guid("727f1875-2b10-45f9-a155-224aafbf3a9f"), null, new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9526), new Guid("89799ffb-7000-4f62-8465-74186063e27d"), null, "idm", new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "corporate-customer" },
                    { new Guid("cd5fdc94-de20-46fc-9fa1-8c4f7ff0116c"), new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9608), new Guid("e8eb7be8-7fc3-4ae8-8d43-5f740b559b7c"), null, new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9606), new Guid("e721f825-795c-4e8f-8693-68871e5c2a3c"), null, "idm", new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "loan-partner" },
                    { new Guid("cff7ffbf-e61f-4b7c-9171-93feede1b01b"), new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9644), new Guid("c290933a-cb80-4271-bb59-c2c85d9a35ea"), null, new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9642), new Guid("1c463fe0-4811-4a63-aea0-d2ae94b6b3ec"), null, "idm", new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "loan-partner-staff" },
                    { new Guid("f24d10bc-49dd-4740-962e-7bf4226fc329"), new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9675), new Guid("e946aa03-ab6a-4904-a90a-8fcdfd32d5c7"), null, new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9673), new Guid("e17c2e3f-b017-478c-8448-3c30099f5adb"), null, "idm", new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "burgan-staff" }
                });

            migrationBuilder.InsertData(
                table: "Views",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "TagId", "TagName", "Type", "ViewTemplateName" },
                values: new object[,]
                {
                    { new Guid("2bdebb20-26c1-4645-bedf-9040e9f0c629"), new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9760), new Guid("92857083-1d50-482f-844e-a0d476467e81"), null, new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9759), new Guid("808cb08c-2125-4556-9b25-321f118655d6"), null, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "retail-customer", 1, "retail-customer-mini-html" },
                    { new Guid("722c7e8b-da74-4ef7-8096-1547412c7d48"), new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9801), new Guid("c3dc6426-bf58-4244-bc58-fe612ecb49d4"), null, new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9799), new Guid("7e2300a7-8a5f-4ef3-81b4-13bb48ec91db"), null, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "retail-customer", 2, "retail-customer-flutter" }
                });

            migrationBuilder.InsertData(
                table: "EntityData",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "EntityId", "EntityName", "Field", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Ttl" },
                values: new object[,]
                {
                    { new Guid("107f4644-57cd-46ff-80de-004c6cd44722"), new DateTime(2023, 6, 13, 11, 40, 24, 575, DateTimeKind.Utc).AddTicks(97), new Guid("cc935829-0e9d-4584-9f00-266afe09895e"), null, new Guid("107f4644-57cd-46ff-80de-004c6cd44715"), "user", "lastname", new DateTime(2023, 6, 13, 11, 40, 24, 575, DateTimeKind.Utc).AddTicks(95), new Guid("22b1dd79-803a-40e1-bf16-24b7e0dc44c9"), null, null },
                    { new Guid("107f4644-57cd-46ff-80de-004c6cd44734"), new DateTime(2023, 6, 13, 11, 40, 24, 575, DateTimeKind.Utc).AddTicks(56), new Guid("87c0fb2f-c30c-47ef-b91d-2d96858060d1"), null, new Guid("107f4644-57cd-46ff-80de-004c6cd44715"), "user", "firstname", new DateTime(2023, 6, 13, 11, 40, 24, 575, DateTimeKind.Utc).AddTicks(54), new Guid("8cc56652-4343-4bbd-b294-c1681d39eed4"), null, null },
                    { new Guid("107f4644-57cd-46ff-80de-004c6cd44755"), new DateTime(2023, 6, 13, 11, 40, 24, 575, DateTimeKind.Utc).AddTicks(131), new Guid("02f5664f-8585-4bfe-bfbd-9b366fa0bead"), null, new Guid("107f4644-57cd-46ff-80de-004c6cd44710"), "scope", "title", new DateTime(2023, 6, 13, 11, 40, 24, 575, DateTimeKind.Utc).AddTicks(124), new Guid("e01fd37b-6e07-40dd-a30f-802c1fb66420"), null, null }
                });

            migrationBuilder.InsertData(
                table: "EntityDataSource",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "DataPath", "EntityDataId", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Order", "TagId", "TagName" },
                values: new object[,]
                {
                    { new Guid("14158929-136d-4156-9a55-1261837b9c77"), new DateTime(2023, 6, 13, 11, 40, 24, 575, DateTimeKind.Utc).AddTicks(234), new Guid("e07e5857-0a3c-4aa4-9e18-8acbb8a78e5b"), null, "$.firstname", new Guid("107f4644-57cd-46ff-80de-004c6cd44734"), new DateTime(2023, 6, 13, 11, 40, 24, 575, DateTimeKind.Utc).AddTicks(226), new Guid("18a4d329-1357-4115-a863-10e33016afac"), null, 3, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "retail-customer" },
                    { new Guid("3a973a8c-3e48-4a49-9037-d6d3e0db38dd"), new DateTime(2023, 6, 13, 11, 40, 24, 575, DateTimeKind.Utc).AddTicks(397), new Guid("f8b81de9-356a-4fbd-82ed-02e0d99ed34c"), null, "$.partner-staff.fullname", new Guid("107f4644-57cd-46ff-80de-004c6cd44734"), new DateTime(2023, 6, 13, 11, 40, 24, 575, DateTimeKind.Utc).AddTicks(394), new Guid("5f12ec6b-2b2e-4123-80da-a8e6fe8bca55"), null, 2, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "loan-partner-staff" },
                    { new Guid("5c16195d-aad8-4c72-b3df-6a7406e95765"), new DateTime(2023, 6, 13, 11, 40, 24, 575, DateTimeKind.Utc).AddTicks(333), new Guid("351e25f1-1f5f-4f65-8b0b-f1a30554971a"), null, "$.lastname", new Guid("107f4644-57cd-46ff-80de-004c6cd44734"), new DateTime(2023, 6, 13, 11, 40, 24, 575, DateTimeKind.Utc).AddTicks(331), new Guid("7b2bcd59-8d7f-44a6-a1b7-ad557afb9f77"), null, 3, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "retail-customer" },
                    { new Guid("6199927c-672e-4b13-a1f6-436130c4047e"), new DateTime(2023, 6, 13, 11, 40, 24, 575, DateTimeKind.Utc).AddTicks(365), new Guid("ff3b6b98-a612-4d06-9bf9-0f1150a03ee9"), null, "$.firstname", new Guid("107f4644-57cd-46ff-80de-004c6cd44734"), new DateTime(2023, 6, 13, 11, 40, 24, 575, DateTimeKind.Utc).AddTicks(363), new Guid("9c44ce61-4939-471f-b85a-07a8472f343f"), null, 1, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "burgan-staff" },
                    { new Guid("63527c16-5a2e-4d4f-acc6-45c44da39fc6"), new DateTime(2023, 6, 13, 11, 40, 24, 575, DateTimeKind.Utc).AddTicks(196), new Guid("c3b21d4c-57c8-4ec2-b717-05ee9c5724b2"), null, "$.partner-staff.fullname", new Guid("107f4644-57cd-46ff-80de-004c6cd44734"), new DateTime(2023, 6, 13, 11, 40, 24, 575, DateTimeKind.Utc).AddTicks(195), new Guid("52a5c37f-794a-481f-9413-310481598545"), null, 2, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "loan-partner-staff" },
                    { new Guid("87e2aa37-ab77-4644-83b5-add81b9c7029"), new DateTime(2023, 6, 13, 11, 40, 24, 575, DateTimeKind.Utc).AddTicks(162), new Guid("6eb027bd-e4c9-4850-b9bc-7a9db05f32b3"), null, "$.firstname", new Guid("107f4644-57cd-46ff-80de-004c6cd44734"), new DateTime(2023, 6, 13, 11, 40, 24, 575, DateTimeKind.Utc).AddTicks(160), new Guid("d8fa87a8-9a48-4327-987d-ca5ee760011a"), null, 1, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "burgan-staff" },
                    { new Guid("87ec1b24-e55b-4226-93d7-71463ffb1dea"), new DateTime(2023, 6, 13, 11, 40, 24, 575, DateTimeKind.Utc).AddTicks(264), new Guid("b56f7490-42cb-4344-8969-28f1c47fb52d"), null, "$.lastname", new Guid("107f4644-57cd-46ff-80de-004c6cd44734"), new DateTime(2023, 6, 13, 11, 40, 24, 575, DateTimeKind.Utc).AddTicks(262), new Guid("1cb979ab-fc81-4ec3-bed5-93e1609f0c81"), null, 1, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "burgan-staff" },
                    { new Guid("c2012a11-43aa-497e-ab02-6c40f2362c31"), new DateTime(2023, 6, 13, 11, 40, 24, 575, DateTimeKind.Utc).AddTicks(293), new Guid("4bb5d7fb-6d7c-4e48-96f6-a8a1bfbcab52"), null, "$.partner-staff.fullname", new Guid("107f4644-57cd-46ff-80de-004c6cd44734"), new DateTime(2023, 6, 13, 11, 40, 24, 575, DateTimeKind.Utc).AddTicks(291), new Guid("254bb9f3-c498-49e4-bc14-01aaf51d3e51"), null, 2, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "loan-partner-staff" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entities_DomainId",
                table: "Entities",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityData_EntityId",
                table: "EntityData",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityDataSource_EntityDataId",
                table: "EntityDataSource",
                column: "EntityDataId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityDataSource_TagId",
                table: "EntityDataSource",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_TagRelations_TagId",
                table: "TagRelations",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Views_TagId",
                table: "Views",
                column: "TagId");
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
