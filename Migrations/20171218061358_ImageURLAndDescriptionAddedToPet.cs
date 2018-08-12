using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace petmanager.Migrations
{
    public partial class ImageURLAndDescriptionAddedToPet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Pets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Pets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Pets");
        }
    }
}
