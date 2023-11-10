﻿// <auto-generated />
using System;
using AIRCOM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AIRCOM.Models.Airoport", b =>
                {
                    b.Property<int>("AirportID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Coordinates")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Direction")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("AirportID");

                    b.ToTable("Airoports");
                });

            modelBuilder.Entity("AIRCOM.Models.Client", b =>
                {
                    b.Property<int>("ClientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ClientID");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("AIRCOM.Models.CustomerService", b =>
                {
                    b.Property<int>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Code");

                    b.ToTable("CustomerService");
                });

            modelBuilder.Entity("AIRCOM.Models.Installation", b =>
                {
                    b.Property<int>("InstallationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AiroportID")
                        .HasColumnType("int");

                    b.Property<string>("Direction")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Ubication")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("InstallationID");

                    b.HasIndex("AiroportID");

                    b.ToTable("Installation");
                });

            modelBuilder.Entity("AIRCOM.Models.ServicesInstallation", b =>
                {
                    b.Property<int>("Price")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AiroportID")
                        .HasColumnType("int");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<int>("InstallationID")
                        .HasColumnType("int");

                    b.HasKey("Price");

                    b.HasIndex("Code");

                    b.HasIndex("InstallationID");

                    b.ToTable("ServicesInstallation");
                });

            modelBuilder.Entity("AIRCOM.Models.Ships", b =>
                {
                    b.Property<string>("Plate")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("ClientID")
                        .HasColumnType("int");

                    b.Property<int>("Crew")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Plate");

                    b.HasIndex("ClientID");

                    b.ToTable("Ships");
                });

            modelBuilder.Entity("AIRCOM.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Pwd")
                        .HasColumnType("longtext");

                    b.Property<string>("Role")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AIRCOM.Models.on_site", b =>
                {
                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("AiroportID")
                        .HasColumnType("int");

                    b.Property<string>("Assessment")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ClientID")
                        .HasColumnType("int");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<int>("InstallationID")
                        .HasColumnType("int");

                    b.Property<int>("Total_price")
                        .HasColumnType("int");

                    b.HasKey("Fecha");

                    b.HasIndex("AiroportID");

                    b.HasIndex("ClientID");

                    b.HasIndex("Code");

                    b.HasIndex("InstallationID");

                    b.ToTable("on_site");
                });

            modelBuilder.Entity("AIRCOM.Models.Installation", b =>
                {
                    b.HasOne("AIRCOM.Models.Airoport", "Airoport")
                        .WithMany("Installations")
                        .HasForeignKey("AiroportID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Airoport");
                });

            modelBuilder.Entity("AIRCOM.Models.ServicesInstallation", b =>
                {
                    b.HasOne("AIRCOM.Models.CustomerService", "CustomerService")
                        .WithMany("ServicesInstallations")
                        .HasForeignKey("Code")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AIRCOM.Models.Airoport", "Airoport")
                        .WithMany("ServicesInstallations")
                        .HasForeignKey("InstallationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AIRCOM.Models.Installation", "Installation")
                        .WithMany("ServicesInstallations")
                        .HasForeignKey("InstallationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Airoport");

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
                    b.HasOne("AIRCOM.Models.Airoport", "Airoport")
                        .WithMany("On_Sites")
                        .HasForeignKey("AiroportID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AIRCOM.Models.Client", "Clients")
                        .WithMany("On_Sites")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AIRCOM.Models.CustomerService", "CustomerService")
                        .WithMany("On_Sites")
                        .HasForeignKey("Code")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AIRCOM.Models.Installation", "Installation")
                        .WithMany("on_sites")
                        .HasForeignKey("InstallationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Airoport");

                    b.Navigation("Clients");

                    b.Navigation("CustomerService");

                    b.Navigation("Installation");
                });

            modelBuilder.Entity("AIRCOM.Models.Airoport", b =>
                {
                    b.Navigation("Installations");

                    b.Navigation("On_Sites");

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
                    b.Navigation("ServicesInstallations");

                    b.Navigation("on_sites");
                });
#pragma warning restore 612, 618
        }
    }
}
