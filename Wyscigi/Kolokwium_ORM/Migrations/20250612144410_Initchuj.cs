using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kolokwium_ORM.Migrations
{
    /// <inheritdoc />
    public partial class Initchuj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Races",
                keyColumn: "raceid",
                keyValue: 1,
                column: "datetime",
                value: new DateTime(2025, 6, 12, 0, 0, 0, 0, DateTimeKind.Local));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Races",
                keyColumn: "raceid",
                keyValue: 1,
                column: "datetime",
                value: new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
