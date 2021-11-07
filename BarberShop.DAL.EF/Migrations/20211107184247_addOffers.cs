using Microsoft.EntityFrameworkCore.Migrations;

namespace BarberShop.DAL.EF.Migrations
{
    public partial class addOffers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Cost", "Title" },
                values: new object[] { 15, "Clippers only" });

            migrationBuilder.InsertData(
                table: "Offers",
                columns: new[] { "Id", "Cost", "Duration", "Title" },
                values: new object[] { 6, 40, 45, "Head Shave" });

            migrationBuilder.InsertData(
                table: "Offers",
                columns: new[] { "Id", "Cost", "Duration", "Title" },
                values: new object[] { 5, 50, 45, "Skin Fade with Haircut" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Cost", "Title" },
                values: new object[] { 40, "Trim" });
        }
    }
}
