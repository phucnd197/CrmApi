using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crm_Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiKeys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiKeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfficialCustomers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficialCustomers", x => x.CustomerId);
                });

            migrationBuilder.InsertData(
                table: "ApiKeys",
                columns: new[] { "Id", "Created", "Name" },
                values: new object[] { new Guid("4ffebbdf-fdd5-46ab-ae2b-db205056bb53"), new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Key for FakeThirdParty" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiKeys");

            migrationBuilder.DropTable(
                name: "OfficialCustomers");
        }
    }
}
