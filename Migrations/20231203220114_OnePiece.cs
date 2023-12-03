using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIRCOM.Migrations
{
    /// <inheritdoc />
    public partial class OnePiece : Migration
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
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Coordinates = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Direction = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
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
                    CI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Pwd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientID);
                });

            migrationBuilder.CreateTable(
                name: "Repairs",
                columns: table => new
                {
                    RepairID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repairs", x => x.RepairID);
                });

            migrationBuilder.CreateTable(
                name: "Installations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstallationID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ubication = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirportID = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Installations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Installations_Airports_AirportID",
                        column: x => x.AirportID,
                        principalTable: "Airports",
                        principalColumn: "AirportID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    WorkerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Pwd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    AirportID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.WorkerID);
                    table.ForeignKey(
                        name: "FK_Workers_Airports_AirportID",
                        column: x => x.AirportID,
                        principalTable: "Airports",
                        principalColumn: "AirportID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shipss",
                columns: table => new
                {
                    Plate = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Crew = table.Column<int>(type: "int", nullable: false),
                    Pass = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    NextFly = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClientID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipss", x => x.Plate);
                    table.ForeignKey(
                        name: "FK_Shipss_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ClientID");
                });

            migrationBuilder.CreateTable(
                name: "RepairInstallations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Stars = table.Column<float>(type: "real", nullable: true),
                    Votes = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    InstallationID = table.Column<int>(type: "int", nullable: false),
                    RepairID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairInstallations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RepairInstallations_Installations_InstallationID",
                        column: x => x.InstallationID,
                        principalTable: "Installations",
                        principalColumn: "ID",
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
                    Code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Stars = table.Column<float>(type: "real", nullable: true),
                    Votes = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    InstallationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesInstallations", x => x.Code);
                    table.ForeignKey(
                        name: "FK_ServicesInstallations_Installations_InstallationID",
                        column: x => x.InstallationID,
                        principalTable: "Installations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Historys",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OwnerRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plate = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ArrivalID = table.Column<int>(type: "int", nullable: true),
                    ExitID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historys", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Historys_Airports_ArrivalID",
                        column: x => x.ArrivalID,
                        principalTable: "Airports",
                        principalColumn: "AirportID");
                    table.ForeignKey(
                        name: "FK_Historys_Airports_ExitID",
                        column: x => x.ExitID,
                        principalTable: "Airports",
                        principalColumn: "AirportID");
                    table.ForeignKey(
                        name: "FK_Historys_Shipss_Plate",
                        column: x => x.Plate,
                        principalTable: "Shipss",
                        principalColumn: "Plate",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepairShips",
                columns: table => new
                {
                    RSID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Init = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Finish = table.Column<DateTime>(type: "datetime2", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Stars = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RepairInstallationID = table.Column<int>(type: "int", nullable: false),
                    Plate = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairShips", x => x.RSID);
                    table.ForeignKey(
                        name: "FK_RepairShips_RepairInstallations_RepairInstallationID",
                        column: x => x.RepairInstallationID,
                        principalTable: "RepairInstallations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepairShips_Shipss_Plate",
                        column: x => x.Plate,
                        principalTable: "Shipss",
                        principalColumn: "Plate",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "On_Site",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Stars = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<int>(type: "int", nullable: false),
                    ClientID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_On_Site", x => x.ID);
                    table.ForeignKey(
                        name: "FK_On_Site_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_On_Site_ServicesInstallations_Code",
                        column: x => x.Code,
                        principalTable: "ServicesInstallations",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Airports_AirportID",
                table: "Airports",
                column: "AirportID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Airports_Coordinates",
                table: "Airports",
                column: "Coordinates",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Airports_Direction",
                table: "Airports",
                column: "Direction",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Airports_Name",
                table: "Airports",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ClientID",
                table: "Clients",
                column: "ClientID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Email",
                table: "Clients",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Historys_ArrivalID",
                table: "Historys",
                column: "ArrivalID");

            migrationBuilder.CreateIndex(
                name: "IX_Historys_ExitID",
                table: "Historys",
                column: "ExitID");

            migrationBuilder.CreateIndex(
                name: "IX_Historys_ID",
                table: "Historys",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Historys_Plate",
                table: "Historys",
                column: "Plate");

            migrationBuilder.CreateIndex(
                name: "IX_Installations_AirportID",
                table: "Installations",
                column: "AirportID");

            migrationBuilder.CreateIndex(
                name: "IX_Installations_ID",
                table: "Installations",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_On_Site_ClientID",
                table: "On_Site",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_On_Site_Code",
                table: "On_Site",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_On_Site_ID",
                table: "On_Site",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RepairInstallations_ID",
                table: "RepairInstallations",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RepairInstallations_InstallationID",
                table: "RepairInstallations",
                column: "InstallationID");

            migrationBuilder.CreateIndex(
                name: "IX_RepairInstallations_RepairID",
                table: "RepairInstallations",
                column: "RepairID");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_Name",
                table: "Repairs",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_RepairID",
                table: "Repairs",
                column: "RepairID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RepairShips_Plate",
                table: "RepairShips",
                column: "Plate");

            migrationBuilder.CreateIndex(
                name: "IX_RepairShips_RepairInstallationID",
                table: "RepairShips",
                column: "RepairInstallationID");

            migrationBuilder.CreateIndex(
                name: "IX_RepairShips_RSID",
                table: "RepairShips",
                column: "RSID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServicesInstallations_Code",
                table: "ServicesInstallations",
                column: "Code",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Workers_AirportID",
                table: "Workers",
                column: "AirportID");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_Email",
                table: "Workers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workers_WorkerID",
                table: "Workers",
                column: "WorkerID",
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
                name: "RepairShips");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DropTable(
                name: "ServicesInstallations");

            migrationBuilder.DropTable(
                name: "RepairInstallations");

            migrationBuilder.DropTable(
                name: "Shipss");

            migrationBuilder.DropTable(
                name: "Installations");

            migrationBuilder.DropTable(
                name: "Repairs");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Airports");
        }
    }
}
