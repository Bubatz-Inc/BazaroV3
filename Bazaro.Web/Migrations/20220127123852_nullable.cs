using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Bazaro.Web.Migrations
{
    public partial class nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FolderEntryReference",
                table: "FolderEntryReference");

            migrationBuilder.AlterColumn<int>(
                name: "FolderId",
                table: "FolderEntryReference",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "EntryId",
                table: "FolderEntryReference",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "FolderEntryReference",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "FolderEntryReference",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "FolderEntryReference",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_FolderEntryReference",
                table: "FolderEntryReference",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FolderEntryReference_EntryId",
                table: "FolderEntryReference",
                column: "EntryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FolderEntryReference",
                table: "FolderEntryReference");

            migrationBuilder.DropIndex(
                name: "IX_FolderEntryReference_EntryId",
                table: "FolderEntryReference");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "FolderEntryReference");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "FolderEntryReference");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "FolderEntryReference");

            migrationBuilder.AlterColumn<int>(
                name: "FolderId",
                table: "FolderEntryReference",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EntryId",
                table: "FolderEntryReference",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FolderEntryReference",
                table: "FolderEntryReference",
                columns: new[] { "EntryId", "FolderId" });
        }
    }
}
