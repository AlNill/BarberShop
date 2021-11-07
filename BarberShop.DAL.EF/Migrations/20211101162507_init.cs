using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BarberShop.DAL.EF.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Barbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Information = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Barbers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusyRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BarberId = table.Column<int>(type: "int", nullable: false),
                    RecordTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusyRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusyRecords_Barbers_BarberId",
                        column: x => x.BarberId,
                        principalTable: "Barbers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusyRecords_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NickName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserReview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BarberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Barbers_BarberId",
                        column: x => x.BarberId,
                        principalTable: "Barbers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Barbers",
                columns: new[] { "Id", "FatherName", "ImagePath", "Information", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, "Ivanovich", "/images/standart_short.jpg", "Cool information about barber Ivanov must be here.", "Ivan", "Ivanov" },
                    { 2, "Petrovich", "/images/standart_short.jpg", "Cool information about barber Petrov must be here.", "Petr", "Petrov" },
                    { 3, "Pupkinovich", "/images/standart_short.jpg", "Cool information about barber Pupkin must be here.", "Pup", "Pupkin" }
                });

            migrationBuilder.InsertData(
                table: "Offers",
                columns: new[] { "Id", "Cost", "Duration", "Title" },
                values: new object[,]
                {
                    { 1, 35, 0, "Mans haircut" },
                    { 2, 30, 0, "Child haircut" },
                    { 3, 30, 0, "Bread trim" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FatherName", "Name", "NickName", "Password", "RoleId", "Surname" },
                values: new object[,]
                {
                    { 1, "us3rt35t@yandex.ru", "Admin", "Admin", "Admin", "Admin", 1, "Admin" },
                    { 19, "us3rt35t@yandex.ru", "FatherName18", "Name18", "User18", "User18", 2, "Surname18" },
                    { 18, "us3rt35t@yandex.ru", "FatherName17", "Name17", "User17", "User17", 2, "Surname17" },
                    { 17, "us3rt35t@yandex.ru", "FatherName16", "Name16", "User16", "User16", 2, "Surname16" },
                    { 16, "us3rt35t@yandex.ru", "FatherName15", "Name15", "User15", "User15", 2, "Surname15" },
                    { 15, "us3rt35t@yandex.ru", "FatherName14", "Name14", "User14", "User14", 2, "Surname14" },
                    { 14, "us3rt35t@yandex.ru", "FatherName13", "Name13", "User13", "User13", 2, "Surname13" },
                    { 13, "us3rt35t@yandex.ru", "FatherName12", "Name12", "User12", "User12", 2, "Surname12" },
                    { 12, "us3rt35t@yandex.ru", "FatherName11", "Name11", "User11", "User11", 2, "Surname11" },
                    { 20, "us3rt35t@yandex.ru", "FatherName19", "Name19", "User19", "User19", 2, "Surname19" },
                    { 11, "us3rt35t@yandex.ru", "FatherName10", "Name10", "User10", "User10", 2, "Surname10" },
                    { 9, "us3rt35t@yandex.ru", "FatherName8", "Name8", "User8", "User8", 2, "Surname8" },
                    { 8, "us3rt35t@yandex.ru", "FatherName7", "Name7", "User7", "User7", 2, "Surname7" },
                    { 7, "us3rt35t@yandex.ru", "FatherName6", "Name6", "User6", "User6", 2, "Surname6" },
                    { 6, "us3rt35t@yandex.ru", "FatherName5", "Name5", "User5", "User5", 2, "Surname5" },
                    { 5, "us3rt35t@yandex.ru", "FatherName4", "Name4", "User4", "User4", 2, "Surname4" },
                    { 4, "us3rt35t@yandex.ru", "FatherName3", "Name3", "User3", "User3", 2, "Surname3" },
                    { 3, "us3rt35t@yandex.ru", "FatherName2", "Name2", "User2", "User2", 2, "Surname2" },
                    { 2, "us3rt35t@yandex.ru", "FatherName1", "Name1", "User1", "User1", 2, "Surname1" },
                    { 10, "us3rt35t@yandex.ru", "FatherName9", "Name9", "User9", "User9", 2, "Surname9" },
                    { 21, "us3rt35t@yandex.ru", "FatherName20", "Name20", "User20", "User20", 2, "Surname20" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusyRecords_BarberId",
                table: "BusyRecords",
                column: "BarberId");

            migrationBuilder.CreateIndex(
                name: "IX_BusyRecords_OfferId",
                table: "BusyRecords",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BarberId",
                table: "Reviews",
                column: "BarberId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusyRecords");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "Barbers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
