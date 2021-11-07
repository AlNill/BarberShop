using Microsoft.EntityFrameworkCore.Migrations;

namespace BarberShop.DAL.EF.Migrations
{
    public partial class seedDataCorrecting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Barbers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FatherName", "Information", "Name", "Surname" },
                values: new object[] { "Mark", "Have hairstylist license and Organizational and time-management abilities.", "Joe", "God" });

            migrationBuilder.UpdateData(
                table: "Barbers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FatherName", "Information", "Surname" },
                values: new object[] { "Aleksandrovich", "High school diploma.\r\nStrong communication, listening, and interpersonal skills.", "Shark" });

            migrationBuilder.UpdateData(
                table: "Barbers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FatherName", "Information", "Name", "Surname" },
                values: new object[] { "John", "Have a GED certificate. \r\nAttention to detail.", "Mark", "House" });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "BarberId", "UserId", "UserReview" },
                values: new object[,]
                {
                    { 1, 1, 2, "I moved from the Twin Cities and was looking for an actual Barber vs salon that calls themselves barbers. There are only 2 shops in La X that are legit Barbers. I go to Barber Joe in Valley View Mall. The guy is phenomenal! Can book the appointment online and he has a little kiosk style store by the food court. I can not say enough good things about him. Check him out." },
                    { 2, 2, 3, "It was our first time using Barber Petr. My sons never had a haircut he’s loved, until now! Great work, fast, efficient and friendly! We will be back!" },
                    { 3, 1, 5, "A great haircut at a great price. Conversation with Joe is always easy and fun. Highly recommended" },
                    { 4, 3, 7, "Barber Mark is highly professional. Will definitely recommend." },
                    { 5, 2, 12, "Barber Petr is awesome! He treats you like a person and not a hair trimmer number like commercial shops. He knows his trade and is extremely skilled. He even asks how your last haircut worked out and grew out to make the next one even better. Do yourself a favor and visit Barber Petr. He is the real deal, and you'll be glad you did!" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Barbers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FatherName", "Information", "Name", "Surname" },
                values: new object[] { "Ivanovich", "Cool information about barber Ivanov must be here.", "Ivan", "Ivanov" });

            migrationBuilder.UpdateData(
                table: "Barbers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FatherName", "Information", "Surname" },
                values: new object[] { "Petrovich", "Cool information about barber Petrov must be here.", "Petrov" });

            migrationBuilder.UpdateData(
                table: "Barbers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FatherName", "Information", "Name", "Surname" },
                values: new object[] { "Pupkinovich", "Cool information about barber Pupkin must be here.", "Pup", "Pupkin" });
        }
    }
}
