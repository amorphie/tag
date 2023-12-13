using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace amorphie.tag.data.Migrations
{
    /// <inheritdoc />
    public partial class fullText : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EntityData",
                keyColumn: "Id",
                keyValue: new Guid("107f4644-57cd-46ff-80de-004c6cd44722"));

            migrationBuilder.DeleteData(
                table: "EntityData",
                keyColumn: "Id",
                keyValue: new Guid("107f4644-57cd-46ff-80de-004c6cd44755"));

            migrationBuilder.DeleteData(
                table: "EntityDataSource",
                keyColumn: "Id",
                keyValue: new Guid("09eeb1b1-3948-4078-995d-4d775633a6f3"));

            migrationBuilder.DeleteData(
                table: "EntityDataSource",
                keyColumn: "Id",
                keyValue: new Guid("318cc749-2dbe-43de-afb2-a1ef18e836a3"));

            migrationBuilder.DeleteData(
                table: "EntityDataSource",
                keyColumn: "Id",
                keyValue: new Guid("3f79b824-067a-42c4-a594-a64567d19025"));

            migrationBuilder.DeleteData(
                table: "EntityDataSource",
                keyColumn: "Id",
                keyValue: new Guid("68809d09-83a4-48a6-bdec-d20fcd858b2b"));

            migrationBuilder.DeleteData(
                table: "EntityDataSource",
                keyColumn: "Id",
                keyValue: new Guid("9faf5ca6-ec6b-4888-bdcf-20cc71714d10"));

            migrationBuilder.DeleteData(
                table: "EntityDataSource",
                keyColumn: "Id",
                keyValue: new Guid("a8b3afcf-2991-4363-a6b9-c9ab26f0cde2"));

            migrationBuilder.DeleteData(
                table: "EntityDataSource",
                keyColumn: "Id",
                keyValue: new Guid("d70ceb8f-717a-49b9-aaa9-b66c6a51c6e7"));

            migrationBuilder.DeleteData(
                table: "EntityDataSource",
                keyColumn: "Id",
                keyValue: new Guid("ea11c428-467c-4706-96d0-6065b6019fa0"));

            migrationBuilder.DeleteData(
                table: "TagRelations",
                keyColumn: "Id",
                keyValue: new Guid("2afcaea3-deab-4b52-8deb-dcc11c15bf3e"));

            migrationBuilder.DeleteData(
                table: "TagRelations",
                keyColumn: "Id",
                keyValue: new Guid("7bd7a975-3a12-4f6a-b27c-d6f28784ab46"));

            migrationBuilder.DeleteData(
                table: "TagRelations",
                keyColumn: "Id",
                keyValue: new Guid("a9417fdf-6cf7-4307-8a1b-b0cd3dae1acb"));

            migrationBuilder.DeleteData(
                table: "TagRelations",
                keyColumn: "Id",
                keyValue: new Guid("aea66ffc-e68a-4283-92bd-6f2745208e4d"));

            migrationBuilder.DeleteData(
                table: "TagRelations",
                keyColumn: "Id",
                keyValue: new Guid("af8229f1-02ac-475e-833e-4ee989cf18bf"));

            migrationBuilder.DeleteData(
                table: "TagRelations",
                keyColumn: "Id",
                keyValue: new Guid("d03f7d22-fdc7-4681-a597-0a9e9af7b69c"));

            migrationBuilder.DeleteData(
                table: "TagRelations",
                keyColumn: "Id",
                keyValue: new Guid("edc4f8da-4538-49ed-90bd-5967a46e0e42"));

            migrationBuilder.DeleteData(
                table: "TagRelations",
                keyColumn: "Id",
                keyValue: new Guid("fdebc769-52ce-4716-80e5-c386e857079a"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("26a18f7b-8198-4bf1-9661-2c1dc80c2fb1"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("2d773ae4-4d0d-40ec-b1f5-595dd9a13d0a"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("6061ebdc-8fe4-4a0c-a0b2-e9f6baa18709"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("980d4716-3fd5-401a-aec8-e409f23c528f"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("d7425487-a927-4164-a667-19c8efdbec1b"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("e212dddc-8f93-4425-93dc-73a9b6f8672d"));

            migrationBuilder.DeleteData(
                table: "Views",
                keyColumn: "Id",
                keyValue: new Guid("822465bf-8361-4851-8c09-a124cf67af20"));

            migrationBuilder.DeleteData(
                table: "Views",
                keyColumn: "Id",
                keyValue: new Guid("82cd8416-2d04-4b5e-b9e7-87735155267b"));

            migrationBuilder.DeleteData(
                table: "Entities",
                keyColumn: "Id",
                keyValue: new Guid("107f4644-57cd-46ff-80de-004c6cd44710"));

            migrationBuilder.DeleteData(
                table: "EntityData",
                keyColumn: "Id",
                keyValue: new Guid("107f4644-57cd-46ff-80de-004c6cd44734"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("107f4644-57cd-46ff-80de-004c6cd44756"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("107f4644-57cd-46ff-80de-004c6cd44778"));

            migrationBuilder.DeleteData(
                table: "Entities",
                keyColumn: "Id",
                keyValue: new Guid("107f4644-57cd-46ff-80de-004c6cd44715"));

            migrationBuilder.DeleteData(
                table: "Domains",
                keyColumn: "Id",
                keyValue: new Guid("107f4644-57cd-46ff-80de-004c6cd44712"));

            migrationBuilder.AddColumn<NpgsqlTsVector>(
                name: "SearchVector",
                table: "Tags",
                type: "tsvector",
                nullable: true,
                computedColumnSql: "to_tsvector('english', coalesce(\"Name\", '') || ' ' || coalesce(\"Status\", ''))",
                stored: true);

            migrationBuilder.AddColumn<NpgsqlTsVector>(
                name: "SearchVector",
                table: "EntityData",
                type: "tsvector",
                nullable: true,
                computedColumnSql: "to_tsvector('english', coalesce(\"Field\", '') || ' ' || coalesce(\"EntityName\", ''))",
                stored: true);

            migrationBuilder.AddColumn<NpgsqlTsVector>(
                name: "SearchVector",
                table: "Entities",
                type: "tsvector",
                nullable: true,
                computedColumnSql: "to_tsvector('english', coalesce(\"Name\", '') || ' ' || coalesce(\"DomainName\", ''))",
                stored: true);

            migrationBuilder.AddColumn<NpgsqlTsVector>(
                name: "SearchVector",
                table: "Domains",
                type: "tsvector",
                nullable: true,
                computedColumnSql: "to_tsvector('english', coalesce(\"Name\", ''))",
                stored: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_SearchVector",
                table: "Tags",
                column: "SearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");

            migrationBuilder.CreateIndex(
                name: "IX_EntityData_SearchVector",
                table: "EntityData",
                column: "SearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");

            migrationBuilder.CreateIndex(
                name: "IX_Entities_SearchVector",
                table: "Entities",
                column: "SearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");

            migrationBuilder.CreateIndex(
                name: "IX_Domains_SearchVector",
                table: "Domains",
                column: "SearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tags_SearchVector",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_EntityData_SearchVector",
                table: "EntityData");

            migrationBuilder.DropIndex(
                name: "IX_Entities_SearchVector",
                table: "Entities");

            migrationBuilder.DropIndex(
                name: "IX_Domains_SearchVector",
                table: "Domains");

            migrationBuilder.DropColumn(
                name: "SearchVector",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "SearchVector",
                table: "EntityData");

            migrationBuilder.DropColumn(
                name: "SearchVector",
                table: "Entities");

            migrationBuilder.DropColumn(
                name: "SearchVector",
                table: "Domains");

            migrationBuilder.InsertData(
                table: "Domains",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "Description", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Name" },
                values: new object[] { new Guid("107f4644-57cd-46ff-80de-004c6cd44712"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1760), new Guid("965e0e28-8f6a-4ff8-b930-aec9f7bac374"), null, "Identity Management Platform", new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1760), new Guid("177ac5a9-d520-45b2-a4ab-a01935b50035"), null, "idm" });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "CreatedDate", "LastModifiedDate", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Name", "Status", "Ttl", "Url" },
                values: new object[,]
                {
                    { new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1520), new Guid("0d9cc6b6-6de3-43f0-9fb5-15a2ca53f973"), null, null, null, new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1510), new Guid("39d4d84d-88bb-44cc-b63a-4769ab100be8"), null, "retail-customer", null, 5, "http://localhost:3000/cb.customers?reference=@reference" },
                    { new Guid("107f4644-57cd-46ff-80de-004c6cd44778"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1460), new Guid("9b63372d-7f79-4719-8e3e-f0b0149ca076"), null, null, null, new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1460), new Guid("63b5aef5-0265-49fe-a2c9-671b4964dc19"), null, "retail-loan", null, null, null },
                    { new Guid("26a18f7b-8198-4bf1-9661-2c1dc80c2fb1"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1500), new Guid("7a314d44-291c-4357-9772-59b83b262a9b"), null, null, null, new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1500), new Guid("1130c028-4743-4dc4-b126-32e6eb154990"), null, "idm", null, null, null },
                    { new Guid("2d773ae4-4d0d-40ec-b1f5-595dd9a13d0a"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1590), new Guid("42afdb3f-0210-44af-a3a2-dc58ee964c26"), null, null, null, new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1590), new Guid("6c37bcee-cc2f-4552-943c-74b4bd972944"), null, "burgan-bank-turkey", null, 10, "http://localhost:3000/cb.bankInfo" },
                    { new Guid("6061ebdc-8fe4-4a0c-a0b2-e9f6baa18709"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1550), new Guid("6f1b1514-d78d-42fb-a8a6-e88ac0652262"), null, null, null, new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1540), new Guid("0fc8a03f-f20a-4922-a4b3-c52c27087a6e"), null, "loan-partner", null, 10, "http://localhost:3000/cb.partner/@reference" },
                    { new Guid("980d4716-3fd5-401a-aec8-e409f23c528f"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1560), new Guid("aec4e771-d87a-4624-b8b5-dbf04c8a649c"), null, null, null, new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1560), new Guid("6052e0b0-68db-4e1b-b2a6-16c6dbc4be74"), null, "loan-partner-staff", null, 10, "http://localhost:3000/cb.partner/@partner/staff/@reference" },
                    { new Guid("d7425487-a927-4164-a667-19c8efdbec1b"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1530), new Guid("d0ecbc2c-7816-4524-b9db-9067545917c9"), null, null, null, new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1530), new Guid("33a261e8-b4e7-471b-b6f0-e635fb544320"), null, "corporate-customer", null, 10, "http://localhost:3000/cb.customers?reference=@reference" },
                    { new Guid("e212dddc-8f93-4425-93dc-73a9b6f8672d"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1580), new Guid("3c03b700-8b55-4668-996f-66c48cdb18ec"), null, null, null, new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1570), new Guid("f6208b3a-7831-4985-91ba-88380083fc6d"), null, "burgan-staff", null, 10, "http://localhost:3000/cb.staff/@reference" }
                });

            migrationBuilder.InsertData(
                table: "Entities",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "Description", "DomainId", "DomainName", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Name" },
                values: new object[,]
                {
                    { new Guid("107f4644-57cd-46ff-80de-004c6cd44710"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1800), new Guid("ca9da072-7b14-40ad-92ad-31192cb24462"), null, "Scope repository", new Guid("107f4644-57cd-46ff-80de-004c6cd44712"), "idm", new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1800), new Guid("2b7e7c34-876a-44fb-93ec-1bea6a40607a"), null, "scope" },
                    { new Guid("107f4644-57cd-46ff-80de-004c6cd44715"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1790), new Guid("cc8668f9-a0d9-438c-b062-95bae429393e"), null, "User repository", new Guid("107f4644-57cd-46ff-80de-004c6cd44712"), "idm", new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1790), new Guid("672fefd5-f7db-4010-93b0-653c140317bb"), null, "user" }
                });

            migrationBuilder.InsertData(
                table: "TagRelations",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "OwnerName", "TagId", "TagName" },
                values: new object[,]
                {
                    { new Guid("2afcaea3-deab-4b52-8deb-dcc11c15bf3e"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1640), new Guid("7ef6de76-ade6-49fe-a4e0-ce859e288a26"), null, new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1640), new Guid("efee7a40-673a-4398-8e45-049d18ae1b96"), null, "idm", new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "loan-partner" },
                    { new Guid("7bd7a975-3a12-4f6a-b27c-d6f28784ab46"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1670), new Guid("5c9e2e28-e7f3-4cb4-91e7-f3ff3d2bb8bd"), null, new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1670), new Guid("570c1c48-6bc6-445c-82f5-b7029f6dd7e4"), null, "idm", new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "burgan-staff" },
                    { new Guid("a9417fdf-6cf7-4307-8a1b-b0cd3dae1acb"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1650), new Guid("3e1dd27a-2e27-43e3-b67d-de202c727feb"), null, new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1650), new Guid("ef6a77a4-e9fe-4535-acc7-88c035cd99c6"), null, "idm", new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "loan-partner-staff" },
                    { new Guid("aea66ffc-e68a-4283-92bd-6f2745208e4d"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1730), new Guid("147062bc-7eb6-45c0-911b-3e2a8942e066"), null, new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1730), new Guid("a148ae2c-b867-4b53-abf9-c10f0d56ad5e"), null, "retail-loan", new Guid("107f4644-57cd-46ff-80de-004c6cd44778"), "retail-customer" },
                    { new Guid("af8229f1-02ac-475e-833e-4ee989cf18bf"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1600), new Guid("b583b918-fa70-422c-83a8-9441a89f6873"), null, new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1600), new Guid("2709e045-f458-4d2a-9509-0742934dffe9"), null, "idm", new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "corporate-customer" },
                    { new Guid("d03f7d22-fdc7-4681-a597-0a9e9af7b69c"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1750), new Guid("e7c6a7bf-9e27-4617-bc76-0ff40e9d58b8"), null, new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1740), new Guid("a41d55f8-579a-4972-a2dc-c510738de4f2"), null, "retail-loan", new Guid("107f4644-57cd-46ff-80de-004c6cd44778"), "loan-partner" },
                    { new Guid("edc4f8da-4538-49ed-90bd-5967a46e0e42"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1680), new Guid("d8ede3a7-cc66-424c-bf76-d306a41bc81e"), null, new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1680), new Guid("7a8dbcdd-e00b-4932-97ab-f917ac0e56fb"), null, "idm", new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "burgan-bank-turkey" },
                    { new Guid("fdebc769-52ce-4716-80e5-c386e857079a"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1620), new Guid("9b92b1d7-638a-4d8e-a6e9-1f9d15000f1c"), null, new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1620), new Guid("98bb4af8-2f05-49b8-bdd7-93a415a6f905"), null, "idm", new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "retail-customer" }
                });

            migrationBuilder.InsertData(
                table: "Views",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "TagId", "TagName", "Type", "ViewTemplateName" },
                values: new object[,]
                {
                    { new Guid("822465bf-8361-4851-8c09-a124cf67af20"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1720), new Guid("4dd19e4d-0414-4b8c-bd5e-92d44ae5a7fc"), null, new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1710), new Guid("6f38cde0-1809-4b28-90fe-5175de8b5e37"), null, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "retail-customer", 2, "retail-customer-flutter" },
                    { new Guid("82cd8416-2d04-4b5e-b9e7-87735155267b"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1700), new Guid("0af7a3a2-1168-4861-b275-5d81f564fbbd"), null, new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1700), new Guid("129aedc4-e8ae-4d4d-aca0-56e1502562c3"), null, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "retail-customer", 1, "retail-customer-mini-html" }
                });

            migrationBuilder.InsertData(
                table: "EntityData",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "EntityId", "EntityName", "Field", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Ttl" },
                values: new object[,]
                {
                    { new Guid("107f4644-57cd-46ff-80de-004c6cd44722"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1830), new Guid("6def6862-ae70-4b0a-8731-b888352c558d"), null, new Guid("107f4644-57cd-46ff-80de-004c6cd44715"), "user", "lastname", new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1830), new Guid("f07698ab-b413-4256-95d1-a204de7a11e2"), null, null },
                    { new Guid("107f4644-57cd-46ff-80de-004c6cd44734"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1820), new Guid("02d79699-def7-4356-892c-f70f1475795a"), null, new Guid("107f4644-57cd-46ff-80de-004c6cd44715"), "user", "firstname", new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1810), new Guid("bddc3771-2af2-411c-b209-1eaace1a6cc1"), null, null },
                    { new Guid("107f4644-57cd-46ff-80de-004c6cd44755"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1840), new Guid("e312909b-400b-46f9-800b-cb168f6f06db"), null, new Guid("107f4644-57cd-46ff-80de-004c6cd44710"), "scope", "title", new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1840), new Guid("1c0957f2-8921-45ac-b9bd-859f2b142e48"), null, null }
                });

            migrationBuilder.InsertData(
                table: "EntityDataSource",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "DataPath", "EntityDataId", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Order", "TagId", "TagName" },
                values: new object[,]
                {
                    { new Guid("09eeb1b1-3948-4078-995d-4d775633a6f3"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1890), new Guid("11c6259d-a482-4c7b-ac95-b9e4af54138f"), null, "$.firstname", new Guid("107f4644-57cd-46ff-80de-004c6cd44734"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1890), new Guid("6730bd5d-f284-405b-af20-b2a0c7c14f12"), null, 3, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "retail-customer" },
                    { new Guid("318cc749-2dbe-43de-afb2-a1ef18e836a3"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1950), new Guid("cfc30ce5-5209-4bf0-ac3b-138fff8276ee"), null, "$.firstname", new Guid("107f4644-57cd-46ff-80de-004c6cd44734"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1950), new Guid("9b2aec49-a0f3-42ab-91d2-4895946a15ac"), null, 1, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "burgan-staff" },
                    { new Guid("3f79b824-067a-42c4-a594-a64567d19025"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1960), new Guid("5c3e62c6-bed1-4022-a4d2-dfbb58faa9c2"), null, "$.partner-staff.fullname", new Guid("107f4644-57cd-46ff-80de-004c6cd44734"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1960), new Guid("70bc5971-cb80-4685-a390-3c4f03f7a7ae"), null, 2, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "loan-partner-staff" },
                    { new Guid("68809d09-83a4-48a6-bdec-d20fcd858b2b"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1910), new Guid("41a992f7-454b-4c38-a23e-4d523d623e71"), null, "$.lastname", new Guid("107f4644-57cd-46ff-80de-004c6cd44734"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1900), new Guid("e818aef7-5d5c-479d-a2ac-37e00e858d38"), null, 1, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "burgan-staff" },
                    { new Guid("9faf5ca6-ec6b-4888-bdcf-20cc71714d10"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1880), new Guid("81753147-b950-4733-8f2a-6f30e25d2351"), null, "$.partner-staff.fullname", new Guid("107f4644-57cd-46ff-80de-004c6cd44734"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1870), new Guid("6281cad9-580a-44e9-a0cd-5c2e34044662"), null, 2, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "loan-partner-staff" },
                    { new Guid("a8b3afcf-2991-4363-a6b9-c9ab26f0cde2"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1920), new Guid("ff666524-5e75-4cf2-a039-1da69adad5b5"), null, "$.partner-staff.fullname", new Guid("107f4644-57cd-46ff-80de-004c6cd44734"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1920), new Guid("bdd755f4-80a4-49e1-8481-1b32b0a5757e"), null, 2, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "loan-partner-staff" },
                    { new Guid("d70ceb8f-717a-49b9-aaa9-b66c6a51c6e7"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1940), new Guid("90d2da22-a488-4057-b6a3-45a9b042a505"), null, "$.lastname", new Guid("107f4644-57cd-46ff-80de-004c6cd44734"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1930), new Guid("0d7af6fc-1871-432c-ac67-9dbe9ab3df1d"), null, 3, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "retail-customer" },
                    { new Guid("ea11c428-467c-4706-96d0-6065b6019fa0"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1860), new Guid("d45cfc70-cf4d-4374-b8f0-43dd7ddefc12"), null, "$.firstname", new Guid("107f4644-57cd-46ff-80de-004c6cd44734"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1860), new Guid("134cfc14-a0d2-42cc-a6ed-d6b8282f66c6"), null, 1, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "burgan-staff" }
                });
        }
    }
}
