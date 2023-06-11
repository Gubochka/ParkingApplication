using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingApplication.DAL.Migrations
{
    /// <inheritdoc />
    public partial class added_parking_price : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "ParkingTemplates",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "ParkingTemplates");

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Email", "IsSuperAdmin", "ParkingTemplateId", "Password" },
                values: new object[] { 2, "secret@gmail.com", false, null, "123" });
        }
    }
}
