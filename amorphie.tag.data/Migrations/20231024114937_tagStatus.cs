using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace amorphie.tag.data.Migrations
{
    /// <inheritdoc />
    public partial class tagStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EntityDataSource",
                keyColumn: "Id",
                keyValue: new Guid("14158929-136d-4156-9a55-1261837b9c77"));

            migrationBuilder.DeleteData(
                table: "EntityDataSource",
                keyColumn: "Id",
                keyValue: new Guid("3a973a8c-3e48-4a49-9037-d6d3e0db38dd"));

            migrationBuilder.DeleteData(
                table: "EntityDataSource",
                keyColumn: "Id",
                keyValue: new Guid("5c16195d-aad8-4c72-b3df-6a7406e95765"));

            migrationBuilder.DeleteData(
                table: "EntityDataSource",
                keyColumn: "Id",
                keyValue: new Guid("6199927c-672e-4b13-a1f6-436130c4047e"));

            migrationBuilder.DeleteData(
                table: "EntityDataSource",
                keyColumn: "Id",
                keyValue: new Guid("63527c16-5a2e-4d4f-acc6-45c44da39fc6"));

            migrationBuilder.DeleteData(
                table: "EntityDataSource",
                keyColumn: "Id",
                keyValue: new Guid("87e2aa37-ab77-4644-83b5-add81b9c7029"));

            migrationBuilder.DeleteData(
                table: "EntityDataSource",
                keyColumn: "Id",
                keyValue: new Guid("87ec1b24-e55b-4226-93d7-71463ffb1dea"));

            migrationBuilder.DeleteData(
                table: "EntityDataSource",
                keyColumn: "Id",
                keyValue: new Guid("c2012a11-43aa-497e-ab02-6c40f2362c31"));

            migrationBuilder.DeleteData(
                table: "TagRelations",
                keyColumn: "Id",
                keyValue: new Guid("0f8267d4-7163-4a1c-a78c-aa3764e18d25"));

            migrationBuilder.DeleteData(
                table: "TagRelations",
                keyColumn: "Id",
                keyValue: new Guid("447cacd1-8ff4-497b-a42c-ee7bd3953ed9"));

            migrationBuilder.DeleteData(
                table: "TagRelations",
                keyColumn: "Id",
                keyValue: new Guid("beea6721-35ee-4bb8-93a8-e93666c553b9"));

            migrationBuilder.DeleteData(
                table: "TagRelations",
                keyColumn: "Id",
                keyValue: new Guid("c685b526-4d80-403a-a527-3d7b488835c0"));

            migrationBuilder.DeleteData(
                table: "TagRelations",
                keyColumn: "Id",
                keyValue: new Guid("c8fe329e-9903-4aa8-8f70-513699efb201"));

            migrationBuilder.DeleteData(
                table: "TagRelations",
                keyColumn: "Id",
                keyValue: new Guid("cd5fdc94-de20-46fc-9fa1-8c4f7ff0116c"));

            migrationBuilder.DeleteData(
                table: "TagRelations",
                keyColumn: "Id",
                keyValue: new Guid("cff7ffbf-e61f-4b7c-9171-93feede1b01b"));

            migrationBuilder.DeleteData(
                table: "TagRelations",
                keyColumn: "Id",
                keyValue: new Guid("f24d10bc-49dd-4740-962e-7bf4226fc329"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("2c05eda0-37af-406d-aa20-d3cddbbc5559"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("6364ea95-7503-4213-906f-b9b33ac2125e"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("785c4491-ec21-4803-ad41-af68ffd48c08"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("8989d945-6e7c-4b6b-97e8-aea0f154b9ca"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("ecbe79d5-3ce6-47b8-868a-a776c0c01785"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("fe00fb16-f2c2-47e6-b46d-b72b4fb5d649"));

            migrationBuilder.DeleteData(
                table: "Views",
                keyColumn: "Id",
                keyValue: new Guid("2bdebb20-26c1-4645-bedf-9040e9f0c629"));

            migrationBuilder.DeleteData(
                table: "Views",
                keyColumn: "Id",
                keyValue: new Guid("722c7e8b-da74-4ef7-8096-1547412c7d48"));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Tags",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Domains",
                keyColumn: "Id",
                keyValue: new Guid("107f4644-57cd-46ff-80de-004c6cd44712"),
                columns: new[] { "CreatedAt", "CreatedBy", "ModifiedAt", "ModifiedBy" },
                values: new object[] { new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1760), new Guid("965e0e28-8f6a-4ff8-b930-aec9f7bac374"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1760), new Guid("177ac5a9-d520-45b2-a4ab-a01935b50035") });

            migrationBuilder.UpdateData(
                table: "Entities",
                keyColumn: "Id",
                keyValue: new Guid("107f4644-57cd-46ff-80de-004c6cd44710"),
                columns: new[] { "CreatedAt", "CreatedBy", "ModifiedAt", "ModifiedBy" },
                values: new object[] { new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1800), new Guid("ca9da072-7b14-40ad-92ad-31192cb24462"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1800), new Guid("2b7e7c34-876a-44fb-93ec-1bea6a40607a") });

            migrationBuilder.UpdateData(
                table: "Entities",
                keyColumn: "Id",
                keyValue: new Guid("107f4644-57cd-46ff-80de-004c6cd44715"),
                columns: new[] { "CreatedAt", "CreatedBy", "ModifiedAt", "ModifiedBy" },
                values: new object[] { new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1790), new Guid("cc8668f9-a0d9-438c-b062-95bae429393e"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1790), new Guid("672fefd5-f7db-4010-93b0-653c140317bb") });

            migrationBuilder.UpdateData(
                table: "EntityData",
                keyColumn: "Id",
                keyValue: new Guid("107f4644-57cd-46ff-80de-004c6cd44722"),
                columns: new[] { "CreatedAt", "CreatedBy", "ModifiedAt", "ModifiedBy" },
                values: new object[] { new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1830), new Guid("6def6862-ae70-4b0a-8731-b888352c558d"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1830), new Guid("f07698ab-b413-4256-95d1-a204de7a11e2") });

            migrationBuilder.UpdateData(
                table: "EntityData",
                keyColumn: "Id",
                keyValue: new Guid("107f4644-57cd-46ff-80de-004c6cd44734"),
                columns: new[] { "CreatedAt", "CreatedBy", "ModifiedAt", "ModifiedBy" },
                values: new object[] { new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1820), new Guid("02d79699-def7-4356-892c-f70f1475795a"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1810), new Guid("bddc3771-2af2-411c-b209-1eaace1a6cc1") });

            migrationBuilder.UpdateData(
                table: "EntityData",
                keyColumn: "Id",
                keyValue: new Guid("107f4644-57cd-46ff-80de-004c6cd44755"),
                columns: new[] { "CreatedAt", "CreatedBy", "ModifiedAt", "ModifiedBy" },
                values: new object[] { new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1840), new Guid("e312909b-400b-46f9-800b-cb168f6f06db"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1840), new Guid("1c0957f2-8921-45ac-b9bd-859f2b142e48") });

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

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("107f4644-57cd-46ff-80de-004c6cd44756"),
                columns: new[] { "CreatedAt", "CreatedBy", "ModifiedAt", "ModifiedBy", "Status" },
                values: new object[] { new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1520), new Guid("0d9cc6b6-6de3-43f0-9fb5-15a2ca53f973"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1510), new Guid("39d4d84d-88bb-44cc-b63a-4769ab100be8"), null });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("107f4644-57cd-46ff-80de-004c6cd44778"),
                columns: new[] { "CreatedAt", "CreatedBy", "ModifiedAt", "ModifiedBy", "Status" },
                values: new object[] { new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1460), new Guid("9b63372d-7f79-4719-8e3e-f0b0149ca076"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1460), new Guid("63b5aef5-0265-49fe-a2c9-671b4964dc19"), null });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "CreatedDate", "LastModifiedDate", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Name", "Status", "Ttl", "Url" },
                values: new object[,]
                {
                    { new Guid("26a18f7b-8198-4bf1-9661-2c1dc80c2fb1"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1500), new Guid("7a314d44-291c-4357-9772-59b83b262a9b"), null, null, null, new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1500), new Guid("1130c028-4743-4dc4-b126-32e6eb154990"), null, "idm", null, null, null },
                    { new Guid("2d773ae4-4d0d-40ec-b1f5-595dd9a13d0a"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1590), new Guid("42afdb3f-0210-44af-a3a2-dc58ee964c26"), null, null, null, new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1590), new Guid("6c37bcee-cc2f-4552-943c-74b4bd972944"), null, "burgan-bank-turkey", null, 10, "http://localhost:3000/cb.bankInfo" },
                    { new Guid("6061ebdc-8fe4-4a0c-a0b2-e9f6baa18709"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1550), new Guid("6f1b1514-d78d-42fb-a8a6-e88ac0652262"), null, null, null, new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1540), new Guid("0fc8a03f-f20a-4922-a4b3-c52c27087a6e"), null, "loan-partner", null, 10, "http://localhost:3000/cb.partner/@reference" },
                    { new Guid("980d4716-3fd5-401a-aec8-e409f23c528f"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1560), new Guid("aec4e771-d87a-4624-b8b5-dbf04c8a649c"), null, null, null, new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1560), new Guid("6052e0b0-68db-4e1b-b2a6-16c6dbc4be74"), null, "loan-partner-staff", null, 10, "http://localhost:3000/cb.partner/@partner/staff/@reference" },
                    { new Guid("d7425487-a927-4164-a667-19c8efdbec1b"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1530), new Guid("d0ecbc2c-7816-4524-b9db-9067545917c9"), null, null, null, new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1530), new Guid("33a261e8-b4e7-471b-b6f0-e635fb544320"), null, "corporate-customer", null, 10, "http://localhost:3000/cb.customers?reference=@reference" },
                    { new Guid("e212dddc-8f93-4425-93dc-73a9b6f8672d"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1580), new Guid("3c03b700-8b55-4668-996f-66c48cdb18ec"), null, null, null, new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1570), new Guid("f6208b3a-7831-4985-91ba-88380083fc6d"), null, "burgan-staff", null, 10, "http://localhost:3000/cb.staff/@reference" }
                });

            migrationBuilder.InsertData(
                table: "Views",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "TagId", "TagName", "Type", "ViewTemplateName" },
                values: new object[,]
                {
                    { new Guid("822465bf-8361-4851-8c09-a124cf67af20"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1720), new Guid("4dd19e4d-0414-4b8c-bd5e-92d44ae5a7fc"), null, new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1710), new Guid("6f38cde0-1809-4b28-90fe-5175de8b5e37"), null, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "retail-customer", 2, "retail-customer-flutter" },
                    { new Guid("82cd8416-2d04-4b5e-b9e7-87735155267b"), new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1700), new Guid("0af7a3a2-1168-4861-b275-5d81f564fbbd"), null, new DateTime(2023, 10, 24, 11, 49, 37, 314, DateTimeKind.Utc).AddTicks(1700), new Guid("129aedc4-e8ae-4d4d-aca0-56e1502562c3"), null, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "retail-customer", 1, "retail-customer-mini-html" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tags");

            migrationBuilder.UpdateData(
                table: "Domains",
                keyColumn: "Id",
                keyValue: new Guid("107f4644-57cd-46ff-80de-004c6cd44712"),
                columns: new[] { "CreatedAt", "CreatedBy", "ModifiedAt", "ModifiedBy" },
                values: new object[] { new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9922), new Guid("22c91f2f-7f9e-4b25-baac-0d3d415a1b0e"), new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9920), new Guid("e0d281f4-91cc-435b-af9c-dad9b45ad771") });

            migrationBuilder.UpdateData(
                table: "Entities",
                keyColumn: "Id",
                keyValue: new Guid("107f4644-57cd-46ff-80de-004c6cd44710"),
                columns: new[] { "CreatedAt", "CreatedBy", "ModifiedAt", "ModifiedBy" },
                values: new object[] { new DateTime(2023, 6, 13, 11, 40, 24, 575, DateTimeKind.Utc).AddTicks(19), new Guid("6b9e44f2-091e-4356-916f-d673a6104d0a"), new DateTime(2023, 6, 13, 11, 40, 24, 575, DateTimeKind.Utc).AddTicks(17), new Guid("82beefe1-24ed-4c8a-9ca9-7d19793c3bca") });

            migrationBuilder.UpdateData(
                table: "Entities",
                keyColumn: "Id",
                keyValue: new Guid("107f4644-57cd-46ff-80de-004c6cd44715"),
                columns: new[] { "CreatedAt", "CreatedBy", "ModifiedAt", "ModifiedBy" },
                values: new object[] { new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9976), new Guid("4158f9a5-b561-4373-ae85-f21c46f5f158"), new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9969), new Guid("83dbe7e1-3eb5-4c9f-b24f-68175f6b58db") });

            migrationBuilder.UpdateData(
                table: "EntityData",
                keyColumn: "Id",
                keyValue: new Guid("107f4644-57cd-46ff-80de-004c6cd44722"),
                columns: new[] { "CreatedAt", "CreatedBy", "ModifiedAt", "ModifiedBy" },
                values: new object[] { new DateTime(2023, 6, 13, 11, 40, 24, 575, DateTimeKind.Utc).AddTicks(97), new Guid("cc935829-0e9d-4584-9f00-266afe09895e"), new DateTime(2023, 6, 13, 11, 40, 24, 575, DateTimeKind.Utc).AddTicks(95), new Guid("22b1dd79-803a-40e1-bf16-24b7e0dc44c9") });

            migrationBuilder.UpdateData(
                table: "EntityData",
                keyColumn: "Id",
                keyValue: new Guid("107f4644-57cd-46ff-80de-004c6cd44734"),
                columns: new[] { "CreatedAt", "CreatedBy", "ModifiedAt", "ModifiedBy" },
                values: new object[] { new DateTime(2023, 6, 13, 11, 40, 24, 575, DateTimeKind.Utc).AddTicks(56), new Guid("87c0fb2f-c30c-47ef-b91d-2d96858060d1"), new DateTime(2023, 6, 13, 11, 40, 24, 575, DateTimeKind.Utc).AddTicks(54), new Guid("8cc56652-4343-4bbd-b294-c1681d39eed4") });

            migrationBuilder.UpdateData(
                table: "EntityData",
                keyColumn: "Id",
                keyValue: new Guid("107f4644-57cd-46ff-80de-004c6cd44755"),
                columns: new[] { "CreatedAt", "CreatedBy", "ModifiedAt", "ModifiedBy" },
                values: new object[] { new DateTime(2023, 6, 13, 11, 40, 24, 575, DateTimeKind.Utc).AddTicks(131), new Guid("02f5664f-8585-4bfe-bfbd-9b366fa0bead"), new DateTime(2023, 6, 13, 11, 40, 24, 575, DateTimeKind.Utc).AddTicks(124), new Guid("e01fd37b-6e07-40dd-a30f-802c1fb66420") });

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

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("107f4644-57cd-46ff-80de-004c6cd44756"),
                columns: new[] { "CreatedAt", "CreatedBy", "ModifiedAt", "ModifiedBy" },
                values: new object[] { new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9316), new Guid("659f5a42-6510-4749-9a36-959ffc1aa2d8"), new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9314), new Guid("70a1204a-2750-4280-9da2-cd3ec2bdb467") });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("107f4644-57cd-46ff-80de-004c6cd44778"),
                columns: new[] { "CreatedAt", "CreatedBy", "ModifiedAt", "ModifiedBy" },
                values: new object[] { new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9168), new Guid("c9818ced-0248-49cd-a75f-1c1b172c4368"), new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9163), new Guid("9bc8f9bd-90c6-456a-8b2a-ca40dbca4c5b") });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "CreatedDate", "LastModifiedDate", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Name", "Ttl", "Url" },
                values: new object[,]
                {
                    { new Guid("2c05eda0-37af-406d-aa20-d3cddbbc5559"), new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9370), new Guid("f2ac04d3-5d6a-4c2a-98bb-77f9df392e8f"), null, null, null, new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9367), new Guid("64daeee6-bb69-45a3-8a7f-6919b4b9bfee"), null, "corporate-customer", 10, "http://localhost:3000/cb.customers?reference=@reference" },
                    { new Guid("6364ea95-7503-4213-906f-b9b33ac2125e"), new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9469), new Guid("0884c075-a3a3-4ecd-b3cf-3b3ceec25507"), null, null, null, new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9467), new Guid("2c9159a5-b808-41ba-8de9-433510db1896"), null, "burgan-staff", 10, "http://localhost:3000/cb.staff/@reference" },
                    { new Guid("785c4491-ec21-4803-ad41-af68ffd48c08"), new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9398), new Guid("2896c170-83ac-4741-9efb-9bda3862da34"), null, null, null, new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9396), new Guid("c5d90686-90eb-4ab3-a635-2ca5ec6d7fcc"), null, "loan-partner", 10, "http://localhost:3000/cb.partner/@reference" },
                    { new Guid("8989d945-6e7c-4b6b-97e8-aea0f154b9ca"), new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9434), new Guid("ed21daa1-167d-4a74-86cb-c8ed32f8ec8f"), null, null, null, new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9433), new Guid("94901b0f-78bd-4718-99f0-b51adecc5859"), null, "loan-partner-staff", 10, "http://localhost:3000/cb.partner/@partner/staff/@reference" },
                    { new Guid("ecbe79d5-3ce6-47b8-868a-a776c0c01785"), new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9497), new Guid("1ad3b69d-fb78-4b8f-8473-645483860ad4"), null, null, null, new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9495), new Guid("fd9b1f9e-42fb-4da0-a4fd-b942185d4a79"), null, "burgan-bank-turkey", 10, "http://localhost:3000/cb.bankInfo" },
                    { new Guid("fe00fb16-f2c2-47e6-b46d-b72b4fb5d649"), new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9286), new Guid("98ef09f0-a462-40b7-9e42-94295c622755"), null, null, null, new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9284), new Guid("56150df5-67d2-4683-a8ff-7338aca205d4"), null, "idm", null, null }
                });

            migrationBuilder.InsertData(
                table: "Views",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "TagId", "TagName", "Type", "ViewTemplateName" },
                values: new object[,]
                {
                    { new Guid("2bdebb20-26c1-4645-bedf-9040e9f0c629"), new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9760), new Guid("92857083-1d50-482f-844e-a0d476467e81"), null, new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9759), new Guid("808cb08c-2125-4556-9b25-321f118655d6"), null, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "retail-customer", 1, "retail-customer-mini-html" },
                    { new Guid("722c7e8b-da74-4ef7-8096-1547412c7d48"), new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9801), new Guid("c3dc6426-bf58-4244-bc58-fe612ecb49d4"), null, new DateTime(2023, 6, 13, 11, 40, 24, 574, DateTimeKind.Utc).AddTicks(9799), new Guid("7e2300a7-8a5f-4ef3-81b4-13bb48ec91db"), null, new Guid("107f4644-57cd-46ff-80de-004c6cd44756"), "retail-customer", 2, "retail-customer-flutter" }
                });
        }
    }
}
