using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace petmanager.Migrations
{
    public partial class ownerIdTypeInPetChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_AspNetUsers_OwnerId1",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_OwnerId1",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "OwnerId1",
                table: "Pets");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Pets",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Pets_OwnerId",
                table: "Pets",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_AspNetUsers_OwnerId",
                table: "Pets",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_AspNetUsers_OwnerId",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_OwnerId",
                table: "Pets");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Pets",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "OwnerId1",
                table: "Pets",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_OwnerId1",
                table: "Pets",
                column: "OwnerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_AspNetUsers_OwnerId1",
                table: "Pets",
                column: "OwnerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
