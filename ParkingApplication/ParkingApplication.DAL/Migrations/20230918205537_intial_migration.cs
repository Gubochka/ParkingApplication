using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParkingTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    FloorsCount = table.Column<int>(type: "integer", nullable: false),
                    SlotsCount = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerId = table.Column<int>(type: "integer", nullable: false),
                    CarName = table.Column<string>(type: "text", nullable: false),
                    CarNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    ParkingTemplateId = table.Column<int>(type: "integer", nullable: true),
                    IsSuperAdmin = table.Column<bool>(type: "boolean", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CarId = table.Column<int>(type: "integer", nullable: false),
                    ParkingTemplateId = table.Column<int>(type: "integer", nullable: false),
                    FloorNumber = table.Column<int>(type: "integer", nullable: false),
                    SlotNumber = table.Column<int>(type: "integer", nullable: false),
                    StandsUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parking_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Parking_ParkingTemplates_ParkingTemplateId",
                        column: x => x.ParkingTemplateId,
                        principalTable: "ParkingTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Email", "IsSuperAdmin", "ParkingTemplateId", "Password" },
                values: new object[] { 1, "superadmin.parking@gmail.com", true, null, "0AR815h3QMT0LWCVa2W/ulXWdACkk9OmlFpDQAd277X3cw7L" });

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
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_OwnerId",
                table: "Cars",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_Id",
                table: "Owners",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Parking_CarId",
                table: "Parking",
                column: "CarId");

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
                name: "Parking");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "ParkingTemplates");

            migrationBuilder.DropTable(
                name: "Owners");
        }
    }
}
