using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIRCOM.Migrations
{
    /// <inheritdoc />
    public partial class tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tipo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nationality = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CustomerService",
                columns: table => new
                {
                    Code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerService", x => x.Code);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Installation",
                columns: table => new
                {
                    InstallationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Direction = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ubication = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AiroportID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Installation", x => x.InstallationID);
                    table.ForeignKey(
                        name: "FK_Installation_Airoports_AiroportID",
                        column: x => x.AiroportID,
                        principalTable: "Airoports",
                        principalColumn: "AirportID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Ships",
                columns: table => new
                {
                    Plate = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Model = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Crew = table.Column<int>(type: "int", nullable: false),
                    ClientID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ships", x => x.Plate);
                    table.ForeignKey(
                        name: "FK_Ships_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "on_site",
                columns: table => new
                {
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Assessment = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Total_price = table.Column<int>(type: "int", nullable: false),
                    InstallationID = table.Column<int>(type: "int", nullable: false),
                    AiroportID = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    ClientID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_on_site", x => x.Fecha);
                    table.ForeignKey(
                        name: "FK_on_site_Airoports_AiroportID",
                        column: x => x.AiroportID,
                        principalTable: "Airoports",
                        principalColumn: "AirportID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_on_site_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_on_site_CustomerService_Code",
                        column: x => x.Code,
                        principalTable: "CustomerService",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_on_site_Installation_InstallationID",
                        column: x => x.InstallationID,
                        principalTable: "Installation",
                        principalColumn: "InstallationID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ServicesInstallation",
                columns: table => new
                {
                    Price = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InstallationID = table.Column<int>(type: "int", nullable: false),
                    AiroportID = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesInstallation", x => x.Price);
                    table.ForeignKey(
                        name: "FK_ServicesInstallation_Airoports_InstallationID",
                        column: x => x.InstallationID,
                        principalTable: "Airoports",
                        principalColumn: "AirportID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicesInstallation_CustomerService_Code",
                        column: x => x.Code,
                        principalTable: "CustomerService",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicesInstallation_Installation_InstallationID",
                        column: x => x.InstallationID,
                        principalTable: "Installation",
                        principalColumn: "InstallationID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Installation_AiroportID",
                table: "Installation",
                column: "AiroportID");

            migrationBuilder.CreateIndex(
                name: "IX_on_site_AiroportID",
                table: "on_site",
                column: "AiroportID");

            migrationBuilder.CreateIndex(
                name: "IX_on_site_ClientID",
                table: "on_site",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_on_site_Code",
                table: "on_site",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_on_site_InstallationID",
                table: "on_site",
                column: "InstallationID");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesInstallation_Code",
                table: "ServicesInstallation",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesInstallation_InstallationID",
                table: "ServicesInstallation",
                column: "InstallationID");

            migrationBuilder.CreateIndex(
                name: "IX_Ships_ClientID",
                table: "Ships",
                column: "ClientID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "on_site");

            migrationBuilder.DropTable(
                name: "ServicesInstallation");

            migrationBuilder.DropTable(
                name: "Ships");

            migrationBuilder.DropTable(
                name: "CustomerService");

            migrationBuilder.DropTable(
                name: "Installation");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
