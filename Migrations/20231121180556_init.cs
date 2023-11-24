using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIRCOM.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    AirportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Coordinates = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direction = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.AirportID);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pwd = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientID);
                });

            migrationBuilder.CreateTable(
                name: "CustomerServices",
                columns: table => new
                {
                    Code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerServices", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Repairs",
                columns: table => new
                {
                    RepairID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repairs", x => x.RepairID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pwd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Installation",
                columns: table => new
                {
                    InstallationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ubication = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirportID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Installation", x => x.InstallationID);
                    table.ForeignKey(
                        name: "FK_Installation_Airports_AirportID",
                        column: x => x.AirportID,
                        principalTable: "Airports",
                        principalColumn: "AirportID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shipss",
                columns: table => new
                {
                    Plate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Crew = table.Column<int>(type: "int", nullable: false),
                    ClientID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipss", x => x.Plate);
                    table.ForeignKey(
                        name: "FK_Shipss_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "On_Site",
                columns: table => new
                {
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Assessment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Total_price = table.Column<int>(type: "int", nullable: false),
                    InstallationID = table.Column<int>(type: "int", nullable: false),
                    AirportID = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    ClientID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_On_Site", x => x.Fecha);
                    table.ForeignKey(
                        name: "FK_On_Site_Airports_AirportID",
                        column: x => x.AirportID,
                        principalTable: "Airports",
                        principalColumn: "AirportID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_On_Site_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_On_Site_CustomerServices_Code",
                        column: x => x.Code,
                        principalTable: "CustomerServices",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepairInstallations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    InstallationID = table.Column<int>(type: "int", nullable: false),
                    RepairID = table.Column<int>(type: "int", nullable: false),
                    AirportID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairInstallations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepairInstallations_Airports_AirportID",
                        column: x => x.AirportID,
                        principalTable: "Airports",
                        principalColumn: "AirportID");
                    table.ForeignKey(
                        name: "FK_RepairInstallations_Installation_InstallationID",
                        column: x => x.InstallationID,
                        principalTable: "Installation",
                        principalColumn: "InstallationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepairInstallations_Repairs_RepairID",
                        column: x => x.RepairID,
                        principalTable: "Repairs",
                        principalColumn: "RepairID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicesInstallations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Valoration = table.Column<int>(type: "int", nullable: false),
                    InstallationID = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    AirportID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesInstallations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ServicesInstallations_Airports_AirportID",
                        column: x => x.AirportID,
                        principalTable: "Airports",
                        principalColumn: "AirportID");
                    table.ForeignKey(
                        name: "FK_ServicesInstallations_CustomerServices_Code",
                        column: x => x.Code,
                        principalTable: "CustomerServices",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicesInstallations_Installation_InstallationID",
                        column: x => x.InstallationID,
                        principalTable: "Installation",
                        principalColumn: "InstallationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Historys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OwnerRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plate = table.Column<int>(type: "int", nullable: true),
                    AirportID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Historys_Airports_AirportID",
                        column: x => x.AirportID,
                        principalTable: "Airports",
                        principalColumn: "AirportID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Historys_Shipss_Plate",
                        column: x => x.Plate,
                        principalTable: "Shipss",
                        principalColumn: "Plate");
                });

            migrationBuilder.CreateTable(
                name: "RepairShips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Init = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Finish = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<int>(type: "int", nullable: false),
                    Valoration = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    InstallationID = table.Column<int>(type: "int", nullable: false),
                    RepairID = table.Column<int>(type: "int", nullable: false),
                    Plate = table.Column<int>(type: "int", nullable: false),
                    AirportID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairShips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepairShips_Airports_AirportID",
                        column: x => x.AirportID,
                        principalTable: "Airports",
                        principalColumn: "AirportID");
                    table.ForeignKey(
                        name: "FK_RepairShips_Installation_InstallationID",
                        column: x => x.InstallationID,
                        principalTable: "Installation",
                        principalColumn: "InstallationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepairShips_Repairs_RepairID",
                        column: x => x.RepairID,
                        principalTable: "Repairs",
                        principalColumn: "RepairID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepairShips_Shipss_Plate",
                        column: x => x.Plate,
                        principalTable: "Shipss",
                        principalColumn: "Plate",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Airports_AirportID",
                table: "Airports",
                column: "AirportID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ClientID",
                table: "Clients",
                column: "ClientID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Historys_AirportID",
                table: "Historys",
                column: "AirportID");

            migrationBuilder.CreateIndex(
                name: "IX_Historys_Date",
                table: "Historys",
                column: "Date",
                unique: true,
                filter: "[Date] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Historys_Plate",
                table: "Historys",
                column: "Plate",
                unique: true,
                filter: "[Plate] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Installation_AirportID",
                table: "Installation",
                column: "AirportID");

            migrationBuilder.CreateIndex(
                name: "IX_Installation_InstallationID",
                table: "Installation",
                column: "InstallationID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_On_Site_AirportID",
                table: "On_Site",
                column: "AirportID");

            migrationBuilder.CreateIndex(
                name: "IX_On_Site_ClientID",
                table: "On_Site",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_On_Site_Code",
                table: "On_Site",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_On_Site_Fecha",
                table: "On_Site",
                column: "Fecha",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RepairInstallations_AirportID",
                table: "RepairInstallations",
                column: "AirportID");

            migrationBuilder.CreateIndex(
                name: "IX_RepairInstallations_InstallationID",
                table: "RepairInstallations",
                column: "InstallationID");

            migrationBuilder.CreateIndex(
                name: "IX_RepairInstallations_RepairID",
                table: "RepairInstallations",
                column: "RepairID");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_RepairID",
                table: "Repairs",
                column: "RepairID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RepairShips_AirportID",
                table: "RepairShips",
                column: "AirportID");

            migrationBuilder.CreateIndex(
                name: "IX_RepairShips_Init",
                table: "RepairShips",
                column: "Init",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RepairShips_InstallationID",
                table: "RepairShips",
                column: "InstallationID");

            migrationBuilder.CreateIndex(
                name: "IX_RepairShips_Plate",
                table: "RepairShips",
                column: "Plate");

            migrationBuilder.CreateIndex(
                name: "IX_RepairShips_RepairID",
                table: "RepairShips",
                column: "RepairID");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesInstallations_AirportID",
                table: "ServicesInstallations",
                column: "AirportID");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesInstallations_Code",
                table: "ServicesInstallations",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesInstallations_InstallationID",
                table: "ServicesInstallations",
                column: "InstallationID");

            migrationBuilder.CreateIndex(
                name: "IX_Shipss_ClientID",
                table: "Shipss",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Shipss_Plate",
                table: "Shipss",
                column: "Plate",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Historys");

            migrationBuilder.DropTable(
                name: "On_Site");

            migrationBuilder.DropTable(
                name: "RepairInstallations");

            migrationBuilder.DropTable(
                name: "RepairShips");

            migrationBuilder.DropTable(
                name: "ServicesInstallations");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Repairs");

            migrationBuilder.DropTable(
                name: "Shipss");

            migrationBuilder.DropTable(
                name: "CustomerServices");

            migrationBuilder.DropTable(
                name: "Installation");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Airports");
        }
    }
}
