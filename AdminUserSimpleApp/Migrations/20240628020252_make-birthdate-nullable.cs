using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminUserSimpleApp.Migrations
{
    /// <inheritdoc />
    public partial class makebirthdatenullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "BirthDate",
                table: "AspNetUsers",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b74ddd14-6340-4840-95c2-db12554843e5"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEFqjkW9qbRGdCEHX8lJX6cYWGouyGcSscJIOD5SyfMnLDh0JWp19KPLCwGPgDDtnww==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "BirthDate",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b74ddd14-6340-4840-95c2-db12554843e5"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAECVFjviRCarp9FCOIArBJQQc3cAo8fTBaNFm1MDb3ZWtAH+7piZ9ey7WoZD7hMCzFQ==");
        }
    }
}
