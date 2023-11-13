using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIRCOM.Migrations
{
    /// <inheritdoc />
    public partial class Reparation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Installation_Airoports_AiroportID",
                table: "Installation");

            migrationBuilder.DropForeignKey(
                name: "FK_on_site_Airoports_AiroportID",
                table: "on_site");

            migrationBuilder.DropForeignKey(
                name: "FK_on_site_Clients_ClientID",
                table: "on_site");

            migrationBuilder.DropForeignKey(
                name: "FK_on_site_CustomerService_Code",
                table: "on_site");

            migrationBuilder.DropForeignKey(
                name: "FK_on_site_Installation_InstallationID",
                table: "on_site");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicesInstallation_Airoports_InstallationID",
                table: "ServicesInstallation");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicesInstallation_CustomerService_Code",
                table: "ServicesInstallation");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicesInstallation_Installation_InstallationID",
                table: "ServicesInstallation");

            migrationBuilder.DropForeignKey(
                name: "FK_Ships_Clients_ClientID",
                table: "Ships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ships",
                table: "Ships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServicesInstallation",
                table: "ServicesInstallation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_on_site",
                table: "on_site");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Installation",
                table: "Installation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerService",
                table: "CustomerService");

            migrationBuilder.RenameTable(
                name: "Ships",
                newName: "Shipss");

            migrationBuilder.RenameTable(
                name: "ServicesInstallation",
                newName: "ServicesInstallations");

            migrationBuilder.RenameTable(
                name: "on_site",
                newName: "On_Sites");

            migrationBuilder.RenameTable(
                name: "Installation",
                newName: "Installments");

            migrationBuilder.RenameTable(
                name: "CustomerService",
                newName: "CustomerServices");

            migrationBuilder.RenameIndex(
                name: "IX_Ships_ClientID",
                table: "Shipss",
                newName: "IX_Shipss_ClientID");

            migrationBuilder.RenameIndex(
                name: "IX_ServicesInstallation_InstallationID",
                table: "ServicesInstallations",
                newName: "IX_ServicesInstallations_InstallationID");

            migrationBuilder.RenameIndex(
                name: "IX_ServicesInstallation_Code",
                table: "ServicesInstallations",
                newName: "IX_ServicesInstallations_Code");

            migrationBuilder.RenameIndex(
                name: "IX_on_site_InstallationID",
                table: "On_Sites",
                newName: "IX_On_Sites_InstallationID");

            migrationBuilder.RenameIndex(
                name: "IX_on_site_Code",
                table: "On_Sites",
                newName: "IX_On_Sites_Code");

            migrationBuilder.RenameIndex(
                name: "IX_on_site_ClientID",
                table: "On_Sites",
                newName: "IX_On_Sites_ClientID");

            migrationBuilder.RenameIndex(
                name: "IX_on_site_AiroportID",
                table: "On_Sites",
                newName: "IX_On_Sites_AiroportID");

            migrationBuilder.RenameIndex(
                name: "IX_Installation_AiroportID",
                table: "Installments",
                newName: "IX_Installments_AiroportID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shipss",
                table: "Shipss",
                column: "Plate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServicesInstallations",
                table: "ServicesInstallations",
                column: "Price");

            migrationBuilder.AddPrimaryKey(
                name: "PK_On_Sites",
                table: "On_Sites",
                column: "Fecha");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Installments",
                table: "Installments",
                column: "InstallationID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerServices",
                table: "CustomerServices",
                column: "Code");

            migrationBuilder.CreateTable(
                name: "Reparation",
                columns: table => new
                {
                    Code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descriptipon = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reparation", x => x.Code);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InstReparations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AiroportID = table.Column<int>(type: "int", nullable: false),
                    InstallationID = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstReparations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstReparations_Airoports_AiroportID",
                        column: x => x.AiroportID,
                        principalTable: "Airoports",
                        principalColumn: "AirportID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstReparations_Installments_InstallationID",
                        column: x => x.InstallationID,
                        principalTable: "Installments",
                        principalColumn: "InstallationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstReparations_Reparation_Code",
                        column: x => x.Code,
                        principalTable: "Reparation",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_InstReparations_AiroportID",
                table: "InstReparations",
                column: "AiroportID");

            migrationBuilder.CreateIndex(
                name: "IX_InstReparations_Code",
                table: "InstReparations",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_InstReparations_InstallationID",
                table: "InstReparations",
                column: "InstallationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Installments_Airoports_AiroportID",
                table: "Installments",
                column: "AiroportID",
                principalTable: "Airoports",
                principalColumn: "AirportID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_On_Sites_Airoports_AiroportID",
                table: "On_Sites",
                column: "AiroportID",
                principalTable: "Airoports",
                principalColumn: "AirportID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_On_Sites_Clients_ClientID",
                table: "On_Sites",
                column: "ClientID",
                principalTable: "Clients",
                principalColumn: "ClientID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_On_Sites_CustomerServices_Code",
                table: "On_Sites",
                column: "Code",
                principalTable: "CustomerServices",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_On_Sites_Installments_InstallationID",
                table: "On_Sites",
                column: "InstallationID",
                principalTable: "Installments",
                principalColumn: "InstallationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicesInstallations_Airoports_InstallationID",
                table: "ServicesInstallations",
                column: "InstallationID",
                principalTable: "Airoports",
                principalColumn: "AirportID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicesInstallations_CustomerServices_Code",
                table: "ServicesInstallations",
                column: "Code",
                principalTable: "CustomerServices",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicesInstallations_Installments_InstallationID",
                table: "ServicesInstallations",
                column: "InstallationID",
                principalTable: "Installments",
                principalColumn: "InstallationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shipss_Clients_ClientID",
                table: "Shipss",
                column: "ClientID",
                principalTable: "Clients",
                principalColumn: "ClientID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Installments_Airoports_AiroportID",
                table: "Installments");

            migrationBuilder.DropForeignKey(
                name: "FK_On_Sites_Airoports_AiroportID",
                table: "On_Sites");

            migrationBuilder.DropForeignKey(
                name: "FK_On_Sites_Clients_ClientID",
                table: "On_Sites");

            migrationBuilder.DropForeignKey(
                name: "FK_On_Sites_CustomerServices_Code",
                table: "On_Sites");

            migrationBuilder.DropForeignKey(
                name: "FK_On_Sites_Installments_InstallationID",
                table: "On_Sites");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicesInstallations_Airoports_InstallationID",
                table: "ServicesInstallations");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicesInstallations_CustomerServices_Code",
                table: "ServicesInstallations");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicesInstallations_Installments_InstallationID",
                table: "ServicesInstallations");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipss_Clients_ClientID",
                table: "Shipss");

            migrationBuilder.DropTable(
                name: "InstReparations");

            migrationBuilder.DropTable(
                name: "Reparation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shipss",
                table: "Shipss");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServicesInstallations",
                table: "ServicesInstallations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_On_Sites",
                table: "On_Sites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Installments",
                table: "Installments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerServices",
                table: "CustomerServices");

            migrationBuilder.RenameTable(
                name: "Shipss",
                newName: "Ships");

            migrationBuilder.RenameTable(
                name: "ServicesInstallations",
                newName: "ServicesInstallation");

            migrationBuilder.RenameTable(
                name: "On_Sites",
                newName: "on_site");

            migrationBuilder.RenameTable(
                name: "Installments",
                newName: "Installation");

            migrationBuilder.RenameTable(
                name: "CustomerServices",
                newName: "CustomerService");

            migrationBuilder.RenameIndex(
                name: "IX_Shipss_ClientID",
                table: "Ships",
                newName: "IX_Ships_ClientID");

            migrationBuilder.RenameIndex(
                name: "IX_ServicesInstallations_InstallationID",
                table: "ServicesInstallation",
                newName: "IX_ServicesInstallation_InstallationID");

            migrationBuilder.RenameIndex(
                name: "IX_ServicesInstallations_Code",
                table: "ServicesInstallation",
                newName: "IX_ServicesInstallation_Code");

            migrationBuilder.RenameIndex(
                name: "IX_On_Sites_InstallationID",
                table: "on_site",
                newName: "IX_on_site_InstallationID");

            migrationBuilder.RenameIndex(
                name: "IX_On_Sites_Code",
                table: "on_site",
                newName: "IX_on_site_Code");

            migrationBuilder.RenameIndex(
                name: "IX_On_Sites_ClientID",
                table: "on_site",
                newName: "IX_on_site_ClientID");

            migrationBuilder.RenameIndex(
                name: "IX_On_Sites_AiroportID",
                table: "on_site",
                newName: "IX_on_site_AiroportID");

            migrationBuilder.RenameIndex(
                name: "IX_Installments_AiroportID",
                table: "Installation",
                newName: "IX_Installation_AiroportID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ships",
                table: "Ships",
                column: "Plate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServicesInstallation",
                table: "ServicesInstallation",
                column: "Price");

            migrationBuilder.AddPrimaryKey(
                name: "PK_on_site",
                table: "on_site",
                column: "Fecha");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Installation",
                table: "Installation",
                column: "InstallationID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerService",
                table: "CustomerService",
                column: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Installation_Airoports_AiroportID",
                table: "Installation",
                column: "AiroportID",
                principalTable: "Airoports",
                principalColumn: "AirportID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_on_site_Airoports_AiroportID",
                table: "on_site",
                column: "AiroportID",
                principalTable: "Airoports",
                principalColumn: "AirportID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_on_site_Clients_ClientID",
                table: "on_site",
                column: "ClientID",
                principalTable: "Clients",
                principalColumn: "ClientID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_on_site_CustomerService_Code",
                table: "on_site",
                column: "Code",
                principalTable: "CustomerService",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_on_site_Installation_InstallationID",
                table: "on_site",
                column: "InstallationID",
                principalTable: "Installation",
                principalColumn: "InstallationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicesInstallation_Airoports_InstallationID",
                table: "ServicesInstallation",
                column: "InstallationID",
                principalTable: "Airoports",
                principalColumn: "AirportID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicesInstallation_CustomerService_Code",
                table: "ServicesInstallation",
                column: "Code",
                principalTable: "CustomerService",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicesInstallation_Installation_InstallationID",
                table: "ServicesInstallation",
                column: "InstallationID",
                principalTable: "Installation",
                principalColumn: "InstallationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ships_Clients_ClientID",
                table: "Ships",
                column: "ClientID",
                principalTable: "Clients",
                principalColumn: "ClientID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
