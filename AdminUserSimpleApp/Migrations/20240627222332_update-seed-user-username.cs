using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminUserSimpleApp.Migrations
{
    /// <inheritdoc />
    public partial class updateseeduserusername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b74ddd14-6340-4840-95c2-db12554843e5"),
                columns: new[] { "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "RANIELGARCIA@GMAIL.COM", "AQAAAAIAAYagAAAAECVFjviRCarp9FCOIArBJQQc3cAo8fTBaNFm1MDb3ZWtAH+7piZ9ey7WoZD7hMCzFQ==", "ranielgarcia@gmail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b74ddd14-6340-4840-95c2-db12554843e5"),
                columns: new[] { "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "RANIELGARCIA", "AQAAAAIAAYagAAAAEO37DucB/5wLmzNnUUKWMY2rKhY1iU1euaj4rVvr4whLw3j9vgoxfMdRXrMtMmsjcA==", "RanielGarcia" });
        }
    }
}
