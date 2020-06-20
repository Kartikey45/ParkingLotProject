using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class FourthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkingDetails");

            migrationBuilder.CreateTable(
                name: "ParkingLotDetails",
                columns: table => new
                {
                    ParkingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VehicleOwnerName = table.Column<string>(nullable: false),
                    VehicleNumber = table.Column<string>(nullable: false),
                    VehicalBrand = table.Column<string>(nullable: false),
                    VehicalColor = table.Column<string>(nullable: false),
                    DriverName = table.Column<string>(nullable: false),
                    ParkingSlot = table.Column<string>(nullable: false),
                    ParkingUserCategory = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    ParkingDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingLotDetails", x => x.ParkingID);
                });

            migrationBuilder.CreateTable(
                name: "VehicleUnpark",
                columns: table => new
                {
                    VehicleUnParkID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParkingID = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    UnParkDate = table.Column<DateTime>(nullable: false),
                    TotalTime = table.Column<double>(nullable: false),
                    TotalAmount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleUnpark", x => x.VehicleUnParkID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkingLotDetails");

            migrationBuilder.DropTable(
                name: "VehicleUnpark");

            migrationBuilder.CreateTable(
                name: "ParkingDetails",
                columns: table => new
                {
                    ParkingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ChargePerHr = table.Column<double>(nullable: false),
                    EntryTime = table.Column<DateTime>(nullable: false),
                    ExitTime = table.Column<DateTime>(nullable: false),
                    ParkingSlotNo = table.Column<int>(nullable: false),
                    ParkingStatus = table.Column<bool>(nullable: false),
                    VehicalBrand = table.Column<string>(nullable: true),
                    VehicalColor = table.Column<string>(nullable: true),
                    VehicalNo = table.Column<string>(nullable: true),
                    VehicalParkingUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingDetails", x => x.ParkingID);
                });
        }
    }
}
