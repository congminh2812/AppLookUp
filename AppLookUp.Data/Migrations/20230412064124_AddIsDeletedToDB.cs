using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppLookUp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Informations_AspNetUsers_DeletedBy",
                table: "Informations");

            migrationBuilder.DropIndex(
                name: "IX_Informations_DeletedBy",
                table: "Informations");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Informations");

            migrationBuilder.DropColumn(
                name: "DeletedTime",
                table: "Informations");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Informations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Informations");

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

            migrationBuilder.CreateIndex(
                name: "IX_Informations_DeletedBy",
                table: "Informations",
                column: "DeletedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Informations_AspNetUsers_DeletedBy",
                table: "Informations",
                column: "DeletedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
