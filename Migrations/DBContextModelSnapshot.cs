﻿// <auto-generated />
using System;
using AIRCOM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AIRCOM.Migrations
{
    [DbContext(typeof(DBContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AIRCOM.Models.Airport", b =>
                {
                    b.Property<int>("AirportID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AirportID"));

                    b.Property<string>("Coordinates")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AirportID");

                    b.HasIndex("AirportID")
                        .IsUnique();

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("AIRCOM.Models.Client", b =>
                {
                    b.Property<int>("ClientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClientID"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pwd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClientID");

                    b.HasIndex("ClientID")
                        .IsUnique();

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("AIRCOM.Models.CustomerService", b =>
                {
                    b.Property<int>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Code"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Code");

                    b.ToTable("CustomerServices");
                });

            modelBuilder.Entity("AIRCOM.Models.History", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AirportID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("OwnerRole")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Plate")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AirportID");

                    b.HasIndex("Date")
                        .IsUnique()
                        .HasFilter("[Date] IS NOT NULL");

                    b.HasIndex("Plate")
                        .IsUnique()
                        .HasFilter("[Plate] IS NOT NULL");

                    b.ToTable("Historys");
                });

            modelBuilder.Entity("AIRCOM.Models.Installation", b =>
                {
                    b.Property<int>("InstallationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InstallationID"));

                    b.Property<int>("AirportID")
                        .HasColumnType("int");

                    b.Property<string>("Direction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ubication")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InstallationID");

                    b.HasIndex("AirportID");

                    b.HasIndex("InstallationID")
                        .IsUnique();

                    b.ToTable("Installation");
                });

            modelBuilder.Entity("AIRCOM.Models.Repair", b =>
                {
                    b.Property<int>("RepairID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RepairID"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RepairID");

                    b.HasIndex("RepairID")
                        .IsUnique();

                    b.ToTable("Repairs");
                });

            modelBuilder.Entity("AIRCOM.Models.RepairInstallation", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("AirportID")
                        .HasColumnType("int");

                    b.Property<int>("InstallationID")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("RepairID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AirportID");

                    b.HasIndex("InstallationID");

                    b.HasIndex("RepairID");

                    b.ToTable("RepairInstallations");
                });

            modelBuilder.Entity("AIRCOM.Models.RepairShip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AirportID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Finish")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Init")
                        .HasColumnType("datetime2");

                    b.Property<int>("InstallationID")
                        .HasColumnType("int");

                    b.Property<int>("Plate")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("RepairID")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Time")
                        .HasColumnType("int");

                    b.Property<int>("Valoration")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AirportID");

                    b.HasIndex("Init")
                        .IsUnique();

                    b.HasIndex("InstallationID");

                    b.HasIndex("Plate");

                    b.HasIndex("RepairID");

                    b.ToTable("RepairShips");
                });

            modelBuilder.Entity("AIRCOM.Models.ServicesInstallation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("AirportID")
                        .HasColumnType("int");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<int>("InstallationID")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Valoration")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("AirportID");

                    b.HasIndex("Code");

                    b.HasIndex("InstallationID");

                    b.ToTable("ServicesInstallations");
                });

            modelBuilder.Entity("AIRCOM.Models.Ships", b =>
                {
                    b.Property<int>("Plate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Plate"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("ClientID")
                        .HasColumnType("int");

                    b.Property<int>("Crew")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Plate");

                    b.HasIndex("ClientID");

                    b.HasIndex("Plate")
                        .IsUnique();

                    b.ToTable("Shipss");
                });

            modelBuilder.Entity("AIRCOM.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pwd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AIRCOM.Models.on_site", b =>
                {
                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("AirportID")
                        .HasColumnType("int");

                    b.Property<string>("Assessment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ClientID")
                        .HasColumnType("int");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<int>("InstallationID")
                        .HasColumnType("int");

                    b.Property<int>("Total_price")
                        .HasColumnType("int");

                    b.HasKey("Fecha");

                    b.HasIndex("AirportID");

                    b.HasIndex("ClientID");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("Fecha")
                        .IsUnique();

                    b.ToTable("On_Site");
                });

            modelBuilder.Entity("AIRCOM.Models.History", b =>
                {
                    b.HasOne("AIRCOM.Models.Airport", "Airport")
                        .WithMany("History")
                        .HasForeignKey("AirportID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AIRCOM.Models.Ships", "Ships")
                        .WithMany()
                        .HasForeignKey("Plate");

                    b.Navigation("Airport");

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
                    b.HasOne("AIRCOM.Models.Airport", null)
                        .WithMany("RepairInstallation")
                        .HasForeignKey("AirportID");

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
                    b.HasOne("AIRCOM.Models.Airport", null)
                        .WithMany("RepairShip")
                        .HasForeignKey("AirportID");

                    b.HasOne("AIRCOM.Models.Installation", "Installation")
                        .WithMany("RepairShip")
                        .HasForeignKey("InstallationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AIRCOM.Models.Ships", "Ships")
                        .WithMany("Reports")
                        .HasForeignKey("Plate")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AIRCOM.Models.Repair", "Repair")
                        .WithMany("RepairShip")
                        .HasForeignKey("RepairID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Installation");

                    b.Navigation("Repair");

                    b.Navigation("Ships");
                });

            modelBuilder.Entity("AIRCOM.Models.ServicesInstallation", b =>
                {
                    b.HasOne("AIRCOM.Models.Airport", null)
                        .WithMany("ServicesInstallations")
                        .HasForeignKey("AirportID");

                    b.HasOne("AIRCOM.Models.CustomerService", "CustomerService")
                        .WithMany("ServicesInstallations")
                        .HasForeignKey("Code")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AIRCOM.Models.Installation", "Installation")
                        .WithMany("ServicesInstallations")
                        .HasForeignKey("InstallationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CustomerService");

                    b.Navigation("Installation");
                });

            modelBuilder.Entity("AIRCOM.Models.Ships", b =>
                {
                    b.HasOne("AIRCOM.Models.Client", "Clients")
                        .WithMany("Shipss")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clients");
                });

            modelBuilder.Entity("AIRCOM.Models.on_site", b =>
                {
                    b.HasOne("AIRCOM.Models.Airport", "Airport")
                        .WithMany("On_Sites")
                        .HasForeignKey("AirportID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AIRCOM.Models.Client", "Client")
                        .WithMany("On_Sites")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AIRCOM.Models.CustomerService", "CustomerService")
                        .WithMany("On_Sites")
                        .HasForeignKey("Code")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Airport");

                    b.Navigation("Client");

                    b.Navigation("CustomerService");
                });

            modelBuilder.Entity("AIRCOM.Models.Airport", b =>
                {
                    b.Navigation("History");

                    b.Navigation("Installations");

                    b.Navigation("On_Sites");

                    b.Navigation("RepairInstallation");

                    b.Navigation("RepairShip");

                    b.Navigation("ServicesInstallations");
                });

            modelBuilder.Entity("AIRCOM.Models.Client", b =>
                {
                    b.Navigation("On_Sites");

                    b.Navigation("Shipss");
                });

            modelBuilder.Entity("AIRCOM.Models.CustomerService", b =>
                {
                    b.Navigation("On_Sites");

                    b.Navigation("ServicesInstallations");
                });

            modelBuilder.Entity("AIRCOM.Models.Installation", b =>
                {
                    b.Navigation("RepairInstallations");

                    b.Navigation("RepairShip");

                    b.Navigation("ServicesInstallations");
                });

            modelBuilder.Entity("AIRCOM.Models.Repair", b =>
                {
                    b.Navigation("RepairInstallations");

                    b.Navigation("RepairShip");
                });

            modelBuilder.Entity("AIRCOM.Models.Ships", b =>
                {
                    b.Navigation("Reports");
                });
#pragma warning restore 612, 618
        }
    }
}
