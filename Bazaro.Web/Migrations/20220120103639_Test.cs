using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bazaro.Web.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntryTag_Entry_EntryId",
                table: "EntryTag");

            migrationBuilder.DropIndex(
                name: "IX_EntryTag_EntryId",
                table: "EntryTag");

            migrationBuilder.DropColumn(
                name: "EntryId",
                table: "EntryTag");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserFolderReference",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsShared",
                table: "UserFolderReference",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "NextItemId",
                table: "Item",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserFolderReference");

            migrationBuilder.DropColumn(
                name: "IsShared",
                table: "UserFolderReference");

            migrationBuilder.AlterColumn<int>(
                name: "NextItemId",
                table: "Item",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EntryId",
                table: "EntryTag",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntryTag_EntryId",
                table: "EntryTag",
                column: "EntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_EntryTag_Entry_EntryId",
                table: "EntryTag",
                column: "EntryId",
                principalTable: "Entry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
