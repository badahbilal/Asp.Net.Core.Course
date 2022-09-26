using Microsoft.EntityFrameworkCore.Migrations;

namespace EmptyProject.Migrations
{
    public partial class SeedThreeEmplyees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Department", "Email", "Image", "Name" },
                values: new object[] { 3, 3, "AyoubNa@gmail.com", "/images/02.jpg", "Ayoub Badah" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
