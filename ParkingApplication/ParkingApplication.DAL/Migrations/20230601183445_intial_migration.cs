using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingApplication.DAL.Migrations
{
    /// <inheritdoc />
    public partial class intial_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(25)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    FloorNumber = table.Column<int>(type: "int", nullable: false),
                    SlotNumber = table.Column<int>(type: "int", nullable: false),
                    StandsUntil = table.Column<DateTime>(type: "datetime", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parking", x => x.Id);
                    table.UniqueConstraint("AK_Parking_CarId", x => x.CarId);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    CarName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CarNumber = table.Column<string>(type: "nvarchar(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cars_Parking_Id",
                        column: x => x.Id,
                        principalTable: "Parking",
                        principalColumn: "CarId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_Id",
                table: "Cars",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_OwnerId",
                table: "Cars",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_Id",
                table: "Owners",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Parking_Id",
                table: "Parking",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "Parking");
        }
    }
}
