using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace amorphie.tag.data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                values: new object[] { new Guid("107f4644-57cd-46ff-80de-004c6cd44712"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6589), new Guid("e1e17432-e2f7-48a1-a72e-30dce8df4b8d"), null, "Identity Management Platform", new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6588), new Guid("31b628a6-a82c-4349-957d-08569e0a4917"), null, "idm" });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "CreatedDate", "LastModifiedDate", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Name", "Ttl", "Url" },
                values: new object[,]
                {
                    { new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6337), new Guid("cc183c6c-2a19-4b6c-8998-394c0e5b88ef"), null, null, null, new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6323), new Guid("bca1d845-a90a-4363-8eb6-07e4b397f872"), null, "retail-customer", 5, "http://localhost:3000/cb.customers?reference=@reference" },
                    { new Guid("107f4644-57cd-46ff-80de-004c6cd44778"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6258), new Guid("0b4d3907-6e81-4364-9002-820a419bb6bc"), null, null, null, new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6252), new Guid("b6897aad-48ef-4ec1-a326-640297e9ac4f"), null, "retail-loan", null, null },
                    { new Guid("5c376fd5-d29c-47c9-9b8e-f544b333f072"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6310), new Guid("596ef84f-ebe7-44eb-a3a1-79331dd9442a"), null, null, null, new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6309), new Guid("7356ec0e-4778-4a37-be32-f35159d092cc"), null, "idm", null, null },
                    { new Guid("8a44b641-611d-4ff1-abae-97627ae931ad"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6356), new Guid("2e87a49c-b89c-4ab5-b43a-32deef26d336"), null, null, null, new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6356), new Guid("3b98e235-a47f-49e5-b693-06927fb2a5d0"), null, "corporate-customer", 10, "http://localhost:3000/cb.customers?reference=@reference" },
                    { new Guid("97115f58-fa2d-496c-a7d7-58815569c8c4"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6400), new Guid("108542de-e084-4777-ba34-412bf44bf50e"), null, null, null, new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6399), new Guid("b0993c96-2142-4ea8-8465-07708d8f660f"), null, "burgan-staff", 10, "http://localhost:3000/cb.staff/@reference" },
                    { new Guid("e8992016-985d-40db-a3ad-760b27986a6d"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6387), new Guid("48c4cdd3-fc14-4818-9c28-9f62dccb2884"), null, null, null, new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6383), new Guid("c29e7548-404e-44ea-a4a8-3bbd8b2b9b46"), null, "loan-partner-staff", 10, "http://localhost:3000/cb.partner/@partner/staff/@reference" },
                    { new Guid("ebf9c601-fc5e-427a-ab56-7db6ebac6d2a"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6369), new Guid("787331ee-f6a0-4d08-b154-fe00d45f2a0c"), null, null, null, new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6368), new Guid("9fd4bec5-1be8-4180-a88b-3500abab8a9f"), null, "loan-partner", 10, "http://localhost:3000/cb.partner/@reference" },
                    { new Guid("ffaea56d-632e-46c1-82d3-e7d62c39ad79"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6412), new Guid("1bb7a8d2-1005-47cc-97f2-e1ec755f4b2d"), null, null, null, new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6411), new Guid("d6187dd7-3566-4fad-925c-594b99433ea4"), null, "burgan-bank-turkey", 10, "http://localhost:3000/cb.bankInfo" }
                });

            migrationBuilder.InsertData(
                table: "Entities",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "Description", "DomainId", "DomainName", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Name" },
                values: new object[,]
                {
                    { new Guid("107f4644-57cd-46ff-80de-004c6cd44710"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6626), new Guid("0684540b-bb98-4d3a-bd2b-bbf07ee2fe92"), null, "Scope repository", new Guid("107f4644-57cd-46ff-80de-004c6cd44712"), "idm", new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6625), new Guid("894a6d67-7891-43c5-8f56-535d3ea0a7f3"), null, "scope" },
                    { new Guid("107f4644-57cd-46ff-80de-004c6cd44715"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6610), new Guid("6a556f5b-453f-4907-bcdc-3c7b612cd089"), null, "User repository", new Guid("107f4644-57cd-46ff-80de-004c6cd44712"), "idm", new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6609), new Guid("e234fc2a-aeb9-45ac-8cf3-434bb057713a"), null, "user" }
                });

            migrationBuilder.InsertData(
                table: "TagRelations",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "OwnerName", "TagId", "TagName" },
                values: new object[,]
                {
                    { new Guid("0187b898-9395-4e51-980a-1e88e1169843"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6477), new Guid("79a5cb46-445a-4517-a938-f8a9955a56e0"), null, new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6476), new Guid("1623499c-19a2-4be5-84e2-88539a8e501d"), null, "idm", new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "loan-partner-staff" },
                    { new Guid("1cfe2516-59d5-4eb7-bbcd-7d9f30b68bad"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6505), new Guid("5bb5d89f-f6c7-4067-8d98-1b6f6fde74b0"), null, new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6502), new Guid("577121ec-4765-4c4d-aff0-69741666190c"), null, "idm", new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "burgan-bank-turkey" },
                    { new Guid("4261399f-9812-4f72-b41f-d61176b095cd"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6461), new Guid("aec9d7a1-9b28-4008-953f-357f1d80b319"), null, new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6460), new Guid("496632b5-5321-4a09-8016-58d3e6783476"), null, "idm", new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "loan-partner" },
                    { new Guid("4a168afc-e226-428b-a721-91236c618135"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6562), new Guid("30ffc872-0d56-4e3d-8ffc-c05e5fb07cfb"), null, new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6561), new Guid("96fd42b2-dc68-40e8-989d-fd7eaf93e991"), null, "retail-loan", new Guid("107f4644-57cd-46ff-80de-004c6cd44778"), "retail-customer" },
                    { new Guid("582d005c-ff27-4679-8dc7-5c3428df8e10"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6489), new Guid("25dec80a-9969-4b7d-8c74-def67e954166"), null, new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6488), new Guid("a824bac5-c2ad-408e-9151-193589b18922"), null, "idm", new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "burgan-staff" },
                    { new Guid("759ed7a7-ea1a-4776-bc69-678b06e6b0c3"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6574), new Guid("1905d7ef-326a-4d2f-812c-faaeae73223b"), null, new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6573), new Guid("a2aaa0b6-7e89-4a7c-ac6b-7a4a0b52aab7"), null, "retail-loan", new Guid("107f4644-57cd-46ff-80de-004c6cd44778"), "loan-partner" },
                    { new Guid("ba501004-3914-4a32-b9e5-86f25a101186"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6428), new Guid("c3bbab6f-7d11-4c71-b5f6-a39980722938"), null, new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6427), new Guid("15b67b71-5cb1-455f-be6d-fedf6b084d5d"), null, "idm", new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "corporate-customer" },
                    { new Guid("f04c9658-e9d5-419f-a0f7-1159b6d06b87"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6443), new Guid("cf4b0d4c-e51a-4254-8031-4d6dfd29d90b"), null, new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6442), new Guid("597d5646-e224-4db7-b76f-768b32075c08"), null, "idm", new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "retail-customer" }
                });

            migrationBuilder.InsertData(
                table: "Views",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "TagId", "TagName", "Type", "ViewTemplateName" },
                values: new object[,]
                {
                    { new Guid("74df88e7-e038-454f-8804-4bc116d97731"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6548), new Guid("8b9e3b57-693f-4e0f-abe8-4d12736e71aa"), null, new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6547), new Guid("ef494e6e-2486-49a4-bc7b-e92b40e637c8"), null, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "retail-customer", 2, "retail-customer-flutter" },
                    { new Guid("e9b1b0fa-e506-4c1e-82ba-8d8995988f1d"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6531), new Guid("005733b3-21ff-4425-89bf-9a4760bd0958"), null, new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6530), new Guid("0364d059-546f-4bd5-b42d-0c6606d53702"), null, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "retail-customer", 1, "retail-customer-mini-html" }
                });

            migrationBuilder.InsertData(
                table: "EntityData",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "EntityId", "EntityName", "Field", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Ttl" },
                values: new object[,]
                {
                    { new Guid("107f4644-57cd-46ff-80de-004c6cd44722"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6657), new Guid("ba5bd00a-1b5b-455e-bdf1-270f11f99dad"), null, new Guid("107f4644-57cd-46ff-80de-004c6cd44715"), "user", "lastname", new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6656), new Guid("27ede6ed-a6b6-4f9c-a305-60e93561220d"), null, null },
                    { new Guid("107f4644-57cd-46ff-80de-004c6cd44734"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6641), new Guid("fb43f262-d143-4cd4-940f-ad2e04aaea29"), null, new Guid("107f4644-57cd-46ff-80de-004c6cd44715"), "user", "firstname", new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6640), new Guid("e0160346-79f7-4427-a62d-e1c71823e13a"), null, null },
                    { new Guid("107f4644-57cd-46ff-80de-004c6cd44755"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6673), new Guid("83ae0bd3-804e-4da8-9293-4706f4b54998"), null, new Guid("107f4644-57cd-46ff-80de-004c6cd44710"), "scope", "title", new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6672), new Guid("2dd7de92-36d3-4110-b2d0-e4e7d135cc95"), null, null }
                });

            migrationBuilder.InsertData(
                table: "EntityDataSource",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "DataPath", "EntityDataId", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Order", "TagId", "TagName" },
                values: new object[,]
                {
                    { new Guid("0c458e38-018b-483d-9ae7-ff7e28a3e86f"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6787), new Guid("72ebc6ba-ecb7-46fe-8346-60cbfce9d4f3"), null, "$.partner-staff.fullname", new Guid("107f4644-57cd-46ff-80de-004c6cd44734"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6785), new Guid("8f75973e-575c-4056-b7b2-94981e9c5ac3"), null, 2, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "loan-partner-staff" },
                    { new Guid("13d52481-346a-420f-acb2-292f59ffdb80"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6773), new Guid("879769ab-9549-47e5-a386-de3942397923"), null, "$.firstname", new Guid("107f4644-57cd-46ff-80de-004c6cd44734"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6772), new Guid("89150fcc-c3e3-4757-b4de-1df166824d23"), null, 1, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "burgan-staff" },
                    { new Guid("228fe300-30df-4152-94a6-8acf021ea0ae"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6690), new Guid("1113514b-035a-4bd0-bb9f-90e39016a3b9"), null, "$.firstname", new Guid("107f4644-57cd-46ff-80de-004c6cd44734"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6689), new Guid("8dfa056f-ed38-4a58-b3cc-61f899745f29"), null, 1, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "burgan-staff" },
                    { new Guid("3c864600-48ce-4143-93a1-16eee0fd7f53"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6733), new Guid("45f4bb6f-1847-4d90-873f-f56abada4be5"), null, "$.lastname", new Guid("107f4644-57cd-46ff-80de-004c6cd44734"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6732), new Guid("e5077273-3ac6-49b5-abe3-89548ed4a6f0"), null, 1, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "burgan-staff" },
                    { new Guid("5859ba4d-8e9f-4f21-908c-bea4bbd200bd"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6760), new Guid("39e39746-cc02-40d3-82b2-70b287dceb35"), null, "$.lastname", new Guid("107f4644-57cd-46ff-80de-004c6cd44734"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6759), new Guid("b80d258e-5de4-4735-bd1f-d1668a621055"), null, 3, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "retail-customer" },
                    { new Guid("9f601361-46a9-48c8-92ba-bc699973f70f"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6705), new Guid("707a272a-5755-4152-8db8-04aab8428c4f"), null, "$.partner-staff.fullname", new Guid("107f4644-57cd-46ff-80de-004c6cd44734"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6704), new Guid("3d0b5675-0d3d-4df9-af2c-e27b37159fe5"), null, 2, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "loan-partner-staff" },
                    { new Guid("ab7bd6e3-b317-4065-a4fa-b6edc431eee9"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6745), new Guid("a0266be5-9b0e-4311-a122-007023b05652"), null, "$.partner-staff.fullname", new Guid("107f4644-57cd-46ff-80de-004c6cd44734"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6744), new Guid("9972682e-a90f-4107-ad6f-c5e918778516"), null, 2, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "loan-partner-staff" },
                    { new Guid("beda4812-87bc-4bc5-821f-106f21dcafcb"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6720), new Guid("caacd288-677d-439b-b01c-b73b3681245f"), null, "$.firstname", new Guid("107f4644-57cd-46ff-80de-004c6cd44734"), new DateTime(2023, 6, 5, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(6719), new Guid("0670b6e6-d20d-4e6d-b195-2ba1c905cdd1"), null, 3, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "retail-customer" }
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
