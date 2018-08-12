using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace petmanager.Migrations
{
    public partial class IsSellingPropertyAddedToPet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSelling",
                table: "Pets",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSelling",
                table: "Pets");
        }
    }
}
