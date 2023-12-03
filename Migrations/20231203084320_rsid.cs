using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIRCOM.Migrations
{
    /// <inheritdoc />
    public partial class rsid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "RepairShips",
                newName: "RSID");

            migrationBuilder.RenameIndex(
                name: "IX_RepairShips_ID",
                table: "RepairShips",
                newName: "IX_RepairShips_RSID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RSID",
                table: "RepairShips",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_RepairShips_RSID",
                table: "RepairShips",
                newName: "IX_RepairShips_ID");
        }
    }
}
