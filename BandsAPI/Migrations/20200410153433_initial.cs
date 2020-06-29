using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BandsAPI.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bands",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Founded = table.Column<DateTime>(nullable: false),
                    MainGenre = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 400, nullable: true),
                    BandId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Albums_Bands_BandId",
                        column: x => x.BandId,
                        principalTable: "Bands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Bands",
                columns: new[] { "Id", "Founded", "MainGenre", "Name" },
                values: new object[,]
                {
                    { new Guid("88b3c63a-5b77-43cc-b0c0-d1a92fb701a9"), new DateTime(1980, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Heavy Metal", "Metallica" },
                    { new Guid("68340f77-6e52-4df6-97b8-da14d02e7b5a"), new DateTime(1980, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Heavy Metal", "Metallica" },
                    { new Guid("02798642-87b3-481b-a63f-183221f4223b"), new DateTime(1980, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rock", "Abba" },
                    { new Guid("ca4a6a3b-13e0-411c-9f84-28e56b80b99e"), new DateTime(1980, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jazz", "Hellua" }
                });

            migrationBuilder.InsertData(
                table: "Albums",
                columns: new[] { "Id", "BandId", "Description", "Title" },
                values: new object[,]
                {
                    { new Guid("b4d79528-4615-4214-a4b4-c36a232e115a"), new Guid("88b3c63a-5b77-43cc-b0c0-d1a92fb701a9"), "One of the finest bands it is", "Master of Puppets" },
                    { new Guid("ebeaed26-91b6-45e7-80bc-ad720d865f40"), new Guid("68340f77-6e52-4df6-97b8-da14d02e7b5a"), "Destiny plays a role", "Hero of the destiny" },
                    { new Guid("f9e894ec-ff7a-427e-a75e-a7c19e39027a"), new Guid("02798642-87b3-481b-a63f-183221f4223b"), "Romantic songs this album has", "King of Romance" },
                    { new Guid("d5669b26-3415-4682-9c3a-7796b6ce5ca6"), new Guid("ca4a6a3b-13e0-411c-9f84-28e56b80b99e"), "Harmonic destruction never returns back", "Cites of the harmony" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_BandId",
                table: "Albums",
                column: "BandId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "Bands");
        }
    }
}
