using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIRCOM.Migrations
{
    /// <inheritdoc />
    public partial class err1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NextFly",
                table: "Shipss",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextFly",
                table: "Shipss");
        }
    }
}
