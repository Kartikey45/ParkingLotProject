﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RepositoryLayer.DBContext;

namespace RepositoryLayer.Migrations
{
    [DbContext(typeof(ParkingLotDbContext))]
    [Migration("20200620054604_SecondMigration")]
    partial class SecondMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CommonLayer.ParkingDetails", b =>
                {
                    b.Property<int>("ParkingID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("ChargePerHr");

                    b.Property<DateTime>("EntryTime");

                    b.Property<DateTime>("ExitTime");

                    b.Property<int>("ParkingSlotNo");

                    b.Property<bool>("ParkingStatus");

                    b.Property<string>("VehicalBrand");

                    b.Property<string>("VehicalColor");

                    b.Property<string>("VehicalNo");

                    b.Property<string>("VehicalParkingUser");

                    b.HasKey("ParkingID");

                    b.ToTable("ParkingDetails");
                });

            modelBuilder.Entity("CommonLayer.UserDetails", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("UserDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
