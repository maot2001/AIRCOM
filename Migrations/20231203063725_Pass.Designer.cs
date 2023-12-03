﻿// <auto-generated />
using System;
using AIRCOM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AIRCOM.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20231203063725_Pass")]
    partial class Pass
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AIRCOM.Models.Airport", b =>
                {
                    b.Property<int>("AirportID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AirportID"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Coordinates")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Direction")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AirportID");

                    b.HasIndex("AirportID")
                        .IsUnique();

                    b.HasIndex("Coordinates")
                        .IsUnique();

                    b.HasIndex("Direction")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("AIRCOM.Models.Client", b =>
                {
                    b.Property<int>("ClientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClientID"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("CI")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pwd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClientID");

                    b.HasIndex("ClientID")
                        .IsUnique();

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("AIRCOM.Models.History", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime?>("ArrivalDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ArrivalID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ExitDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ExitID")
                        .HasColumnType("int");

                    b.Property<string>("OwnerRole")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Plate")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("ArrivalID");

                    b.HasIndex("ExitID");

                    b.HasIndex("ID")
                        .IsUnique();

                    b.HasIndex("Plate");

                    b.ToTable("Historys");
                });

            modelBuilder.Entity("AIRCOM.Models.Installation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("AirportID")
                        .HasColumnType("int");

                    b.Property<int>("InstallationID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ubication")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("AirportID");

                    b.HasIndex("ID")
                        .IsUnique();

                    b.ToTable("Installations");
                });

            modelBuilder.Entity("AIRCOM.Models.Repair", b =>
                {
                    b.Property<int>("RepairID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RepairID"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RepairID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("RepairID")
                        .IsUnique();

                    b.ToTable("Repairs");
                });

            modelBuilder.Entity("AIRCOM.Models.RepairInstallation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("InstallationID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int>("RepairID")
                        .HasColumnType("int");

                    b.Property<float?>("Stars")
                        .HasColumnType("real");

                    b.Property<int?>("Votes")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ID")
                        .IsUnique();

                    b.HasIndex("InstallationID");

                    b.HasIndex("RepairID");

                    b.ToTable("RepairInstallations");
                });

            modelBuilder.Entity("AIRCOM.Models.RepairShip", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Finish")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Init")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Plate")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int>("RepairInstallationID")
                        .HasColumnType("int");

                    b.Property<int?>("Stars")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Time")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ID")
                        .IsUnique();

                    b.HasIndex("Plate");

                    b.HasIndex("RepairInstallationID");

                    b.ToTable("RepairShips");
                });

            modelBuilder.Entity("AIRCOM.Models.ServicesInstallation", b =>
                {
                    b.Property<int>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Code"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("InstallationID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<float?>("Stars")
                        .HasColumnType("real");

                    b.Property<int?>("Votes")
                        .HasColumnType("int");

                    b.HasKey("Code");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("InstallationID");

                    b.ToTable("ServicesInstallations");
                });

            modelBuilder.Entity("AIRCOM.Models.Ships", b =>
                {
                    b.Property<string>("Plate")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int?>("ClientID")
                        .HasColumnType("int");

                    b.Property<int>("Crew")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Pass")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Plate");

                    b.HasIndex("ClientID");

                    b.HasIndex("Plate")
                        .IsUnique();

                    b.ToTable("Shipss");
                });

            modelBuilder.Entity("AIRCOM.Models.Worker", b =>
                {
                    b.Property<int>("WorkerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WorkerID"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("AirportID")
                        .HasColumnType("int");

                    b.Property<string>("CI")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pwd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WorkerID");

                    b.HasIndex("AirportID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("WorkerID")
                        .IsUnique();

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("AIRCOM.Models.on_site", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("ClientID")
                        .HasColumnType("int");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int?>("Stars")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ClientID");

                    b.HasIndex("Code");

                    b.HasIndex("ID")
                        .IsUnique();

                    b.ToTable("On_Site");
                });

            modelBuilder.Entity("AIRCOM.Models.History", b =>
                {
                    b.HasOne("AIRCOM.Models.Airport", "ArrivalAirport")
                        .WithMany()
                        .HasForeignKey("ArrivalID");

                    b.HasOne("AIRCOM.Models.Airport", "ExitAirport")
                        .WithMany()
                        .HasForeignKey("ExitID");

                    b.HasOne("AIRCOM.Models.Ships", "Ships")
                        .WithMany()
                        .HasForeignKey("Plate")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArrivalAirport");

                    b.Navigation("ExitAirport");

                    b.Navigation("Ships");
                });

            modelBuilder.Entity("AIRCOM.Models.Installation", b =>
                {
                    b.HasOne("AIRCOM.Models.Airport", "Airport")
                        .WithMany("Installations")
                        .HasForeignKey("AirportID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Airport");
                });

            modelBuilder.Entity("AIRCOM.Models.RepairInstallation", b =>
                {
                    b.HasOne("AIRCOM.Models.Installation", "Installation")
                        .WithMany("RepairInstallations")
                        .HasForeignKey("InstallationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AIRCOM.Models.Repair", "Repair")
                        .WithMany("RepairInstallations")
                        .HasForeignKey("RepairID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Installation");

                    b.Navigation("Repair");
                });

            modelBuilder.Entity("AIRCOM.Models.RepairShip", b =>
                {
                    b.HasOne("AIRCOM.Models.Ships", "Ships")
                        .WithMany("Reports")
                        .HasForeignKey("Plate")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AIRCOM.Models.RepairInstallation", "RepairInstallation")
                        .WithMany("RepairShips")
                        .HasForeignKey("RepairInstallationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RepairInstallation");

                    b.Navigation("Ships");
                });

            modelBuilder.Entity("AIRCOM.Models.ServicesInstallation", b =>
                {
                    b.HasOne("AIRCOM.Models.Installation", "Installation")
                        .WithMany("ServicesInstallations")
                        .HasForeignKey("InstallationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Installation");
                });

            modelBuilder.Entity("AIRCOM.Models.Ships", b =>
                {
                    b.HasOne("AIRCOM.Models.Client", "Clients")
                        .WithMany("Shipss")
                        .HasForeignKey("ClientID");

                    b.Navigation("Clients");
                });

            modelBuilder.Entity("AIRCOM.Models.Worker", b =>
                {
                    b.HasOne("AIRCOM.Models.Airport", "Airport")
                        .WithMany("Workers")
                        .HasForeignKey("AirportID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Airport");
                });

            modelBuilder.Entity("AIRCOM.Models.on_site", b =>
                {
                    b.HasOne("AIRCOM.Models.Client", "Client")
                        .WithMany("On_Sites")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AIRCOM.Models.ServicesInstallation", "ServiceInstallation")
                        .WithMany("On_Sites")
                        .HasForeignKey("Code")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("ServiceInstallation");
                });

            modelBuilder.Entity("AIRCOM.Models.Airport", b =>
                {
                    b.Navigation("Installations");

                    b.Navigation("Workers");
                });

            modelBuilder.Entity("AIRCOM.Models.Client", b =>
                {
                    b.Navigation("On_Sites");

                    b.Navigation("Shipss");
                });

            modelBuilder.Entity("AIRCOM.Models.Installation", b =>
                {
                    b.Navigation("RepairInstallations");

                    b.Navigation("ServicesInstallations");
                });

            modelBuilder.Entity("AIRCOM.Models.Repair", b =>
                {
                    b.Navigation("RepairInstallations");
                });

            modelBuilder.Entity("AIRCOM.Models.RepairInstallation", b =>
                {
                    b.Navigation("RepairShips");
                });

            modelBuilder.Entity("AIRCOM.Models.ServicesInstallation", b =>
                {
                    b.Navigation("On_Sites");
                });

            modelBuilder.Entity("AIRCOM.Models.Ships", b =>
                {
                    b.Navigation("Reports");
                });
#pragma warning restore 612, 618
        }
    }
}
