using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingApplication.DAL.Migrations
{
    /// <inheritdoc />
    public partial class initial_migrate : Migration
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
                name: "ParkingTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FloorsCount = table.Column<int>(type: "int", nullable: false),
                    SlotsCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(80)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    ParkingTemplateId = table.Column<int>(type: "int", nullable: true),
                    IsSuperAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admins_ParkingTemplates_ParkingTemplateId",
                        column: x => x.ParkingTemplateId,
                        principalTable: "ParkingTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Parking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    ParkingTemplateId = table.Column<int>(type: "int", nullable: false),
                    FloorNumber = table.Column<int>(type: "int", nullable: false),
                    SlotNumber = table.Column<int>(type: "int", nullable: false),
                    StandsUntil = table.Column<DateTime>(type: "datetime", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parking", x => x.Id);
                    table.UniqueConstraint("AK_Parking_CarId", x => x.CarId);
                    table.ForeignKey(
                        name: "FK_Parking_ParkingTemplates_ParkingTemplateId",
                        column: x => x.ParkingTemplateId,
                        principalTable: "ParkingTemplates",
                        principalColumn: "Id");
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

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Email", "IsSuperAdmin", "ParkingTemplateId", "Password" },
                values: new object[] { 1, "superadmin.parking@gmail.com", true, null, "123" });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_Id",
                table: "Admins",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_ParkingTemplateId",
                table: "Admins",
                column: "ParkingTemplateId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Parking_ParkingTemplateId",
                table: "Parking",
                column: "ParkingTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingTemplates_Id",
                table: "ParkingTemplates",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "Parking");

            migrationBuilder.DropTable(
                name: "ParkingTemplates");
        }
    }
}
