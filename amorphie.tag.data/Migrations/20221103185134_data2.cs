using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.tag.data.Migrations
{
    /// <inheritdoc />
    public partial class data2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagRelation_Tags_OwnerName",
                table: "TagRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_TagRelation_Tags_TagName",
                table: "TagRelation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TagRelation",
                table: "TagRelation");

            migrationBuilder.RenameTable(
                name: "TagRelation",
                newName: "TagRelations");

            migrationBuilder.RenameIndex(
                name: "IX_TagRelation_TagName",
                table: "TagRelations",
                newName: "IX_TagRelations_TagName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagRelations",
                table: "TagRelations",
                columns: new[] { "OwnerName", "TagName" });

            migrationBuilder.AddForeignKey(
                name: "FK_TagRelations_Tags_OwnerName",
                table: "TagRelations",
                column: "OwnerName",
                principalTable: "Tags",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagRelations_Tags_TagName",
                table: "TagRelations",
                column: "TagName",
                principalTable: "Tags",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagRelations_Tags_OwnerName",
                table: "TagRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_TagRelations_Tags_TagName",
                table: "TagRelations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TagRelations",
                table: "TagRelations");

            migrationBuilder.RenameTable(
                name: "TagRelations",
                newName: "TagRelation");

            migrationBuilder.RenameIndex(
                name: "IX_TagRelations_TagName",
                table: "TagRelation",
                newName: "IX_TagRelation_TagName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagRelation",
                table: "TagRelation",
                columns: new[] { "OwnerName", "TagName" });

            migrationBuilder.AddForeignKey(
                name: "FK_TagRelation_Tags_OwnerName",
                table: "TagRelation",
                column: "OwnerName",
                principalTable: "Tags",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagRelation_Tags_TagName",
                table: "TagRelation",
                column: "TagName",
                principalTable: "Tags",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
