using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace petmanager.Migrations
{
    public partial class IsLockedAndLockedByUserIdFieldAddedTOPet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Pets",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LockedByUserId",
                table: "Pets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "LockedByUserId",
                table: "Pets");
        }
    }
}
