using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace amorphie.tag.data.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EntityDataSource",
                keyColumn: "Id",
                keyValue: new Guid("8db2a8f2-48e6-4177-b0f6-e96641c8cdfd"));

            migrationBuilder.DeleteData(
                table: "EntityDataSource",
                keyColumn: "Id",
                keyValue: new Guid("b951a69d-17e7-4ce7-bbcd-3752f02a7d87"));

            migrationBuilder.DeleteData(
                table: "TagRelations",
                keyColumn: "Id",
                keyValue: new Guid("831e75f5-934d-4914-89ab-b72b78713702"));

            migrationBuilder.DeleteData(
                table: "TagRelations",
                keyColumn: "Id",
                keyValue: new Guid("a18a484b-4d02-449b-bfda-0848d47b3d83"));

            migrationBuilder.DeleteData(
                table: "Views",
                keyColumn: "Id",
                keyValue: new Guid("23b159df-e78c-4ca5-b5b7-82606adbdd8a"));

            migrationBuilder.DeleteData(
                table: "Views",
                keyColumn: "Id",
                keyValue: new Guid("9f1b4c6d-17c7-4cb6-814d-aa9650f4b4c4"));

            migrationBuilder.DeleteData(
                table: "EntityData",
                keyColumn: "Id",
                keyValue: new Guid("1a84c62a-01c2-4454-a6c1-d096f8506132"));

            migrationBuilder.DeleteData(
                table: "EntityData",
                keyColumn: "Id",
                keyValue: new Guid("2635ee93-805e-41ed-bca0-bddcecb4c30f"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("4e3772ea-962d-4a37-969e-82cf01c9b006"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("a1eb8ed7-20fa-42a2-b379-9bee23643721"));

            migrationBuilder.DeleteData(
                table: "Entities",
                keyColumn: "Id",
                keyValue: new Guid("00613ba5-a8db-45cc-a2dc-cad9a360d978"));

            migrationBuilder.DeleteData(
                table: "Entities",
                keyColumn: "Id",
                keyValue: new Guid("e72f0bd3-b7ea-4e19-86d3-e0b4597982d1"));

            migrationBuilder.DeleteData(
                table: "Domains",
                keyColumn: "Id",
                keyValue: new Guid("9e53d36d-e970-42d3-9976-6cd6c68f4005"));

            migrationBuilder.DeleteData(
                table: "Domains",
                keyColumn: "Id",
                keyValue: new Guid("a7d9b331-92d5-472d-881e-58587bac1458"));

            migrationBuilder.InsertData(
                table: "Domains",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "Description", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Name" },
                values: new object[,]
                {
                    { new Guid("b5b2d526-be36-4841-8a6a-76021afb57b6"), new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9486), new Guid("00000000-0000-0000-0000-000000000000"), null, "Domain 1 Description", new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9489), new Guid("00000000-0000-0000-0000-000000000000"), null, "Domain 1" },
                    { new Guid("b5c39b67-5365-485e-9d59-3267379f42cd"), new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9494), new Guid("00000000-0000-0000-0000-000000000000"), null, "Domain 2 Description", new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9494), new Guid("00000000-0000-0000-0000-000000000000"), null, "Domain 2" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "CreatedDate", "LastModifiedDate", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Name", "Ttl", "Url" },
                values: new object[,]
                {
                    { new Guid("758b0f5b-0cb7-44ec-b369-c83a8e6af47b"), new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9819), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9821), null, new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9819), new Guid("00000000-0000-0000-0000-000000000000"), null, "Tag 1", 30, "URL 1" },
                    { new Guid("a9c2dd61-bca2-4d33-ab7e-7fce5511c4e2"), new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9827), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9832), null, new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9827), new Guid("00000000-0000-0000-0000-000000000000"), null, "Tag 2", 40, "URL 2" }
                });

            migrationBuilder.InsertData(
                table: "Entities",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "Description", "DomainId", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Name" },
                values: new object[,]
                {
                    { new Guid("b3101953-8e57-452f-9b74-41ff0cfe9b37"), new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9743), new Guid("00000000-0000-0000-0000-000000000000"), null, "Entity 2 Description", new Guid("b5c39b67-5365-485e-9d59-3267379f42cd"), new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9744), new Guid("00000000-0000-0000-0000-000000000000"), null, "Entity 2" },
                    { new Guid("e937c536-290b-4621-a261-23adf038abf7"), new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9735), new Guid("00000000-0000-0000-0000-000000000000"), null, "Entity 1 Description", new Guid("b5b2d526-be36-4841-8a6a-76021afb57b6"), new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9736), new Guid("00000000-0000-0000-0000-000000000000"), null, "Entity 1" }
                });

            migrationBuilder.InsertData(
                table: "TagRelations",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "OwnerName", "TagId", "TagName" },
                values: new object[,]
                {
                    { new Guid("a7882f10-6658-45d6-81f2-c707805f44eb"), new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9845), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9845), new Guid("00000000-0000-0000-0000-000000000000"), null, "Owner 1", new Guid("758b0f5b-0cb7-44ec-b369-c83a8e6af47b"), "" },
                    { new Guid("c984a74f-d411-469b-9cce-bf520b07f88e"), new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9849), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9849), new Guid("00000000-0000-0000-0000-000000000000"), null, "Owner 2", new Guid("a9c2dd61-bca2-4d33-ab7e-7fce5511c4e2"), "" }
                });

            migrationBuilder.InsertData(
                table: "Views",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "TagId", "TagName", "Type", "ViewTemplateName" },
                values: new object[,]
                {
                    { new Guid("2ac7e3a8-3b88-49e2-8e5f-a670ae8ea31f"), new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9862), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9862), new Guid("00000000-0000-0000-0000-000000000000"), null, new Guid("758b0f5b-0cb7-44ec-b369-c83a8e6af47b"), "", 0, "View 1" },
                    { new Guid("dcabb69b-16c8-42c1-9d93-6671b0e3cb10"), new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9865), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9866), new Guid("00000000-0000-0000-0000-000000000000"), null, new Guid("a9c2dd61-bca2-4d33-ab7e-7fce5511c4e2"), "", 5, "View 2" }
                });

            migrationBuilder.InsertData(
                table: "EntityData",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "EntityId", "Field", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Ttl" },
                values: new object[,]
                {
                    { new Guid("360e370c-4039-405e-988b-ce4cc618dc28"), new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9774), new Guid("00000000-0000-0000-0000-000000000000"), null, new Guid("b3101953-8e57-452f-9b74-41ff0cfe9b37"), "Field 2", new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9775), new Guid("00000000-0000-0000-0000-000000000000"), null, 20 },
                    { new Guid("92dd2eb7-3f8f-4d38-90de-5c0be6d8ece4"), new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9770), new Guid("00000000-0000-0000-0000-000000000000"), null, new Guid("e937c536-290b-4621-a261-23adf038abf7"), "Field 1", new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9770), new Guid("00000000-0000-0000-0000-000000000000"), null, 10 }
                });

            migrationBuilder.InsertData(
                table: "EntityDataSource",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "DataPath", "EntityDataId", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Order", "TagId" },
                values: new object[,]
                {
                    { new Guid("229aaa10-0f5d-45bf-b825-22ba1e2d7e8d"), new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9799), new Guid("00000000-0000-0000-0000-000000000000"), null, "Path 1", new Guid("92dd2eb7-3f8f-4d38-90de-5c0be6d8ece4"), new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9799), new Guid("00000000-0000-0000-0000-000000000000"), null, 1, null },
                    { new Guid("e54642a7-ada4-476c-907b-34e22c578aa5"), new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9803), new Guid("00000000-0000-0000-0000-000000000000"), null, "Path 2", new Guid("360e370c-4039-405e-988b-ce4cc618dc28"), new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9803), new Guid("00000000-0000-0000-0000-000000000000"), null, 2, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EntityDataSource",
                keyColumn: "Id",
                keyValue: new Guid("229aaa10-0f5d-45bf-b825-22ba1e2d7e8d"));

            migrationBuilder.DeleteData(
                table: "EntityDataSource",
                keyColumn: "Id",
                keyValue: new Guid("e54642a7-ada4-476c-907b-34e22c578aa5"));

            migrationBuilder.DeleteData(
                table: "TagRelations",
                keyColumn: "Id",
                keyValue: new Guid("a7882f10-6658-45d6-81f2-c707805f44eb"));

            migrationBuilder.DeleteData(
                table: "TagRelations",
                keyColumn: "Id",
                keyValue: new Guid("c984a74f-d411-469b-9cce-bf520b07f88e"));

            migrationBuilder.DeleteData(
                table: "Views",
                keyColumn: "Id",
                keyValue: new Guid("2ac7e3a8-3b88-49e2-8e5f-a670ae8ea31f"));

            migrationBuilder.DeleteData(
                table: "Views",
                keyColumn: "Id",
                keyValue: new Guid("dcabb69b-16c8-42c1-9d93-6671b0e3cb10"));

            migrationBuilder.DeleteData(
                table: "EntityData",
                keyColumn: "Id",
                keyValue: new Guid("360e370c-4039-405e-988b-ce4cc618dc28"));

            migrationBuilder.DeleteData(
                table: "EntityData",
                keyColumn: "Id",
                keyValue: new Guid("92dd2eb7-3f8f-4d38-90de-5c0be6d8ece4"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("758b0f5b-0cb7-44ec-b369-c83a8e6af47b"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("a9c2dd61-bca2-4d33-ab7e-7fce5511c4e2"));

            migrationBuilder.DeleteData(
                table: "Entities",
                keyColumn: "Id",
                keyValue: new Guid("b3101953-8e57-452f-9b74-41ff0cfe9b37"));

            migrationBuilder.DeleteData(
                table: "Entities",
                keyColumn: "Id",
                keyValue: new Guid("e937c536-290b-4621-a261-23adf038abf7"));

            migrationBuilder.DeleteData(
                table: "Domains",
                keyColumn: "Id",
                keyValue: new Guid("b5b2d526-be36-4841-8a6a-76021afb57b6"));

            migrationBuilder.DeleteData(
                table: "Domains",
                keyColumn: "Id",
                keyValue: new Guid("b5c39b67-5365-485e-9d59-3267379f42cd"));

            migrationBuilder.InsertData(
                table: "Domains",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "Description", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Name" },
                values: new object[,]
                {
                    { new Guid("9e53d36d-e970-42d3-9976-6cd6c68f4005"), new DateTime(2023, 6, 1, 7, 22, 17, 986, DateTimeKind.Utc).AddTicks(2006), new Guid("00000000-0000-0000-0000-000000000000"), null, "Domain 1 Description", new DateTime(2023, 6, 1, 7, 22, 17, 986, DateTimeKind.Utc).AddTicks(2009), new Guid("00000000-0000-0000-0000-000000000000"), null, "Domain 1" },
                    { new Guid("a7d9b331-92d5-472d-881e-58587bac1458"), new DateTime(2023, 6, 1, 7, 22, 17, 986, DateTimeKind.Utc).AddTicks(2014), new Guid("00000000-0000-0000-0000-000000000000"), null, "Domain 2 Description", new DateTime(2023, 6, 1, 7, 22, 17, 986, DateTimeKind.Utc).AddTicks(2015), new Guid("00000000-0000-0000-0000-000000000000"), null, "Domain 2" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "CreatedDate", "LastModifiedDate", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Name", "Ttl", "Url" },
                values: new object[,]
                {
                    { new Guid("4e3772ea-962d-4a37-969e-82cf01c9b006"), new DateTime(2023, 6, 1, 7, 22, 17, 986, DateTimeKind.Utc).AddTicks(2144), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTime(2023, 6, 1, 10, 22, 17, 986, DateTimeKind.Local).AddTicks(2151), null, new DateTime(2023, 6, 1, 7, 22, 17, 986, DateTimeKind.Utc).AddTicks(2144), new Guid("00000000-0000-0000-0000-000000000000"), null, "Tag 1", 30, "URL 1" },
                    { new Guid("a1eb8ed7-20fa-42a2-b379-9bee23643721"), new DateTime(2023, 6, 1, 7, 22, 17, 986, DateTimeKind.Utc).AddTicks(2166), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTime(2023, 6, 1, 10, 22, 17, 986, DateTimeKind.Local).AddTicks(2168), null, new DateTime(2023, 6, 1, 7, 22, 17, 986, DateTimeKind.Utc).AddTicks(2166), new Guid("00000000-0000-0000-0000-000000000000"), null, "Tag 2", 40, "URL 2" }
                });

            migrationBuilder.InsertData(
                table: "Entities",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "Description", "DomainId", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Name" },
                values: new object[,]
                {
                    { new Guid("00613ba5-a8db-45cc-a2dc-cad9a360d978"), new DateTime(2023, 6, 1, 7, 22, 17, 986, DateTimeKind.Utc).AddTicks(2063), new Guid("00000000-0000-0000-0000-000000000000"), null, "Entity 2 Description", new Guid("a7d9b331-92d5-472d-881e-58587bac1458"), new DateTime(2023, 6, 1, 7, 22, 17, 986, DateTimeKind.Utc).AddTicks(2063), new Guid("00000000-0000-0000-0000-000000000000"), null, "Entity 2" },
                    { new Guid("e72f0bd3-b7ea-4e19-86d3-e0b4597982d1"), new DateTime(2023, 6, 1, 7, 22, 17, 986, DateTimeKind.Utc).AddTicks(2058), new Guid("00000000-0000-0000-0000-000000000000"), null, "Entity 1 Description", new Guid("9e53d36d-e970-42d3-9976-6cd6c68f4005"), new DateTime(2023, 6, 1, 7, 22, 17, 986, DateTimeKind.Utc).AddTicks(2058), new Guid("00000000-0000-0000-0000-000000000000"), null, "Entity 1" }
                });

            migrationBuilder.InsertData(
                table: "TagRelations",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "OwnerName", "TagId", "TagName" },
                values: new object[,]
                {
                    { new Guid("831e75f5-934d-4914-89ab-b72b78713702"), new DateTime(2023, 6, 1, 7, 22, 17, 986, DateTimeKind.Utc).AddTicks(2191), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTime(2023, 6, 1, 7, 22, 17, 986, DateTimeKind.Utc).AddTicks(2192), new Guid("00000000-0000-0000-0000-000000000000"), null, "Owner 2", new Guid("a1eb8ed7-20fa-42a2-b379-9bee23643721"), "" },
                    { new Guid("a18a484b-4d02-449b-bfda-0848d47b3d83"), new DateTime(2023, 6, 1, 7, 22, 17, 986, DateTimeKind.Utc).AddTicks(2187), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTime(2023, 6, 1, 7, 22, 17, 986, DateTimeKind.Utc).AddTicks(2187), new Guid("00000000-0000-0000-0000-000000000000"), null, "Owner 1", new Guid("4e3772ea-962d-4a37-969e-82cf01c9b006"), "" }
                });

            migrationBuilder.InsertData(
                table: "Views",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "TagId", "TagName", "Type", "ViewTemplateName" },
                values: new object[,]
                {
                    { new Guid("23b159df-e78c-4ca5-b5b7-82606adbdd8a"), new DateTime(2023, 6, 1, 7, 22, 17, 986, DateTimeKind.Utc).AddTicks(2218), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTime(2023, 6, 1, 7, 22, 17, 986, DateTimeKind.Utc).AddTicks(2218), new Guid("00000000-0000-0000-0000-000000000000"), null, new Guid("a1eb8ed7-20fa-42a2-b379-9bee23643721"), "", 5, "View 2" },
                    { new Guid("9f1b4c6d-17c7-4cb6-814d-aa9650f4b4c4"), new DateTime(2023, 6, 1, 7, 22, 17, 986, DateTimeKind.Utc).AddTicks(2213), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTime(2023, 6, 1, 7, 22, 17, 986, DateTimeKind.Utc).AddTicks(2213), new Guid("00000000-0000-0000-0000-000000000000"), null, new Guid("4e3772ea-962d-4a37-969e-82cf01c9b006"), "", 0, "View 1" }
                });

            migrationBuilder.InsertData(
                table: "EntityData",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "EntityId", "Field", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Ttl" },
                values: new object[,]
                {
                    { new Guid("1a84c62a-01c2-4454-a6c1-d096f8506132"), new DateTime(2023, 6, 1, 7, 22, 17, 986, DateTimeKind.Utc).AddTicks(2094), new Guid("00000000-0000-0000-0000-000000000000"), null, new Guid("00613ba5-a8db-45cc-a2dc-cad9a360d978"), "Field 2", new DateTime(2023, 6, 1, 7, 22, 17, 986, DateTimeKind.Utc).AddTicks(2094), new Guid("00000000-0000-0000-0000-000000000000"), null, 20 },
                    { new Guid("2635ee93-805e-41ed-bca0-bddcecb4c30f"), new DateTime(2023, 6, 1, 7, 22, 17, 986, DateTimeKind.Utc).AddTicks(2089), new Guid("00000000-0000-0000-0000-000000000000"), null, new Guid("e72f0bd3-b7ea-4e19-86d3-e0b4597982d1"), "Field 1", new DateTime(2023, 6, 1, 7, 22, 17, 986, DateTimeKind.Utc).AddTicks(2090), new Guid("00000000-0000-0000-0000-000000000000"), null, 10 }
                });

            migrationBuilder.InsertData(
                table: "EntityDataSource",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "DataPath", "EntityDataId", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Order", "TagId" },
                values: new object[,]
                {
                    { new Guid("8db2a8f2-48e6-4177-b0f6-e96641c8cdfd"), new DateTime(2023, 6, 1, 7, 22, 17, 986, DateTimeKind.Utc).AddTicks(2119), new Guid("00000000-0000-0000-0000-000000000000"), null, "Path 2", new Guid("1a84c62a-01c2-4454-a6c1-d096f8506132"), new DateTime(2023, 6, 1, 7, 22, 17, 986, DateTimeKind.Utc).AddTicks(2119), new Guid("00000000-0000-0000-0000-000000000000"), null, 2, null },
                    { new Guid("b951a69d-17e7-4ce7-bbcd-3752f02a7d87"), new DateTime(2023, 6, 1, 7, 22, 17, 986, DateTimeKind.Utc).AddTicks(2115), new Guid("00000000-0000-0000-0000-000000000000"), null, "Path 1", new Guid("2635ee93-805e-41ed-bca0-bddcecb4c30f"), new DateTime(2023, 6, 1, 7, 22, 17, 986, DateTimeKind.Utc).AddTicks(2115), new Guid("00000000-0000-0000-0000-000000000000"), null, 1, null }
                });
        }
    }
}
