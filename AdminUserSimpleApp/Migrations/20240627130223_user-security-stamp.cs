using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminUserSimpleApp.Migrations
{
    /// <inheritdoc />
    public partial class usersecuritystamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b74ddd14-6340-4840-95c2-db12554843e5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6f135b49-674e-4289-bd51-33d5a04bfcf9", "AQAAAAIAAYagAAAAELNt7AD+HMkvoM+P6CXcOyO8Q0zkx2J8Np3cGrOeO8qFeDraQvq/FT54VKxWoXv/MA==", "1234567890" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b74ddd14-6340-4840-95c2-db12554843e5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c135f8fa-b803-4453-b570-dad4c8f6da6d", "AQAAAAIAAYagAAAAEOalMfmRuWe5kcBLSHxjjLaRRy9DS+BoajWmxJJMC5FiNh+zIuhkILBgp2kuIoMu7A==", null });
        }
    }
}
