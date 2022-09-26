using Microsoft.EntityFrameworkCore.Migrations;

namespace EmptyProject.Migrations
{
    public partial class AlterEmployeesData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Bilal BADAH");

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Department", "Email", "Image", "Name" },
                values: new object[] { 2, 2, "bilal2@gmail.com", "/images/02.jpg", "Ayman Badah" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Bilal");
        }
    }
}
