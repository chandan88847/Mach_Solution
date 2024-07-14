using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkAPI.Migrations
{
    public partial class park3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParkTable",
                columns: table => new
                {
                    ParkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvailableHours = table.Column<double>(type: "float", nullable: false),
                    PricePerHour = table.Column<double>(type: "float", nullable: false),
                    ExpectedReturnTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VehicleImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Flag = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkTable", x => x.ParkId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkTable");
        }
    }
}
