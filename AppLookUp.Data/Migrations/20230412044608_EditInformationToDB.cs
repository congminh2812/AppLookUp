using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppLookUp.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditInformationToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Informations_AspNetUsers_AppUserId",
                table: "Informations");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Informations",
                newName: "CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Informations_AppUserId",
                table: "Informations",
                newName: "IX_Informations_CreatedBy");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Informations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Informations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedTime",
                table: "Informations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Informations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Informations_DeletedBy",
                table: "Informations",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Informations_UpdatedBy",
                table: "Informations",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Informations_AspNetUsers_CreatedBy",
                table: "Informations",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Informations_AspNetUsers_DeletedBy",
                table: "Informations",
                column: "DeletedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Informations_AspNetUsers_UpdatedBy",
                table: "Informations",
                column: "UpdatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Informations_AspNetUsers_CreatedBy",
                table: "Informations");

            migrationBuilder.DropForeignKey(
                name: "FK_Informations_AspNetUsers_DeletedBy",
                table: "Informations");

            migrationBuilder.DropForeignKey(
                name: "FK_Informations_AspNetUsers_UpdatedBy",
                table: "Informations");

            migrationBuilder.DropIndex(
                name: "IX_Informations_DeletedBy",
                table: "Informations");

            migrationBuilder.DropIndex(
                name: "IX_Informations_UpdatedBy",
                table: "Informations");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Informations");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Informations");

            migrationBuilder.DropColumn(
                name: "DeletedTime",
                table: "Informations");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Informations");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Informations",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Informations_CreatedBy",
                table: "Informations",
                newName: "IX_Informations_AppUserId");

        }
    }
}
