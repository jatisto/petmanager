using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace petmanager.Migrations
{
    public partial class UserLogPropertyNameChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MyProperty",
                table: "UserLogs",
                newName: "DateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "UserLogs",
                newName: "MyProperty");
        }
    }
}
