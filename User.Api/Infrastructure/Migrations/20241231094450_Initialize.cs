using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InternalUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    HashedPassword = table.Column<string>(type: "TEXT", nullable: false),
                    Salt = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalUsers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternalUsers");
        }
    }
}
