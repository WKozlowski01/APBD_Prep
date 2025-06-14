using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kolokwium_ORM.Migrations
{
    /// <inheritdoc />
    public partial class Init45 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Racers",
                columns: new[] { "RacerId", "FirstName", "LastName" },
                values: new object[] { 1, "Alan", "Prost" });

            migrationBuilder.InsertData(
                table: "Races",
                columns: new[] { "raceid", "Location", "Name", "datetime" },
                values: new object[] { 1, "Monza", "abc", new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "Tracks",
                columns: new[] { "TrackId", "LengthlnKm", "Name" },
                values: new object[] { 1, 3m, "Monza" });

            migrationBuilder.InsertData(
                table: "TrackRaces",
                columns: new[] { "TrackRaceId", "BestTimelnSeconds", "Laps", "RaceId", "TrackId" },
                values: new object[] { 1, 56, 10, 1, 1 });

            migrationBuilder.InsertData(
                table: "RaceParticipations",
                columns: new[] { "RacerId", "TrackRaceId", "FinishTimelnSeconds", "Position" },
                values: new object[] { 1, 1, 20, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RaceParticipations",
                keyColumns: new[] { "RacerId", "TrackRaceId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Racers",
                keyColumn: "RacerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TrackRaces",
                keyColumn: "TrackRaceId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Races",
                keyColumn: "raceid",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tracks",
                keyColumn: "TrackId",
                keyValue: 1);
        }
    }
}
