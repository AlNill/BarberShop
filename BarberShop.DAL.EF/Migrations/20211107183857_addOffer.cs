using Microsoft.EntityFrameworkCore.Migrations;

namespace BarberShop.DAL.EF.Migrations
{
    public partial class addOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Offers",
                columns: new[] { "Id", "Cost", "Duration", "Title" },
                values: new object[] { 4, 40, 45, "Trim" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
