using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminUserSimpleApp.Migrations
{
    /// <inheritdoc />
    public partial class fixdefaultuserinfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "38575020-bd14-4f0d-820f-f2e0ac97f2f8", true, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEPl9LX4bysC+tc4b46F4ZFKn75UMfIDfhB9CHXvYjaIBQDa8buJp4KWIifZArHJoRA==", "7a67e61f-ebdd-48db-aedd-0cfe77b7ce24" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7e163126-9699-4407-a640-3162588546d6", false, null, null, null, "314308dc-a4b8-4636-a06b-4b273ace0b7d" });
        }
    }
}
