using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingApplication.DAL.Migrations
{
    /// <inheritdoc />
    public partial class added_new_admin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "superadmin.parking@gmail.com");

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Email", "IsSuperAdmin", "ParkingTemplateId", "Password" },
                values: new object[] { 2, "secret@gmail.com", false, null, "123" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "123");
        }
    }
}
