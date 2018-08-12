using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace petmanager.Migrations
{
    public partial class RelationAddedBetweenPetAndWatchlists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Watchlists_PetId",
                table: "Watchlists",
                column: "PetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Watchlists_Pets_PetId",
                table: "Watchlists",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Watchlists_Pets_PetId",
                table: "Watchlists");

            migrationBuilder.DropIndex(
                name: "IX_Watchlists_PetId",
                table: "Watchlists");
        }
    }
}
