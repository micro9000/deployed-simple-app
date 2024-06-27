using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminUserSimpleApp.Migrations
{
    /// <inheritdoc />
    public partial class updatedefaultusername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b74ddd14-6340-4840-95c2-db12554843e5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "UserName" },
                values: new object[] { "e79cd3a9-6c44-4d6b-99c0-1dcefc3fb48d", "AQAAAAIAAYagAAAAEHt4IUqv0jowO0ggnc2ujMW2xTbzYSk2DOAhp3eiJ+Q4GL1mxYP94zlqsmez6KC7zA==", "Default Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b74ddd14-6340-4840-95c2-db12554843e5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "UserName" },
                values: new object[] { "6f135b49-674e-4289-bd51-33d5a04bfcf9", "AQAAAAIAAYagAAAAELNt7AD+HMkvoM+P6CXcOyO8Q0zkx2J8Np3cGrOeO8qFeDraQvq/FT54VKxWoXv/MA==", "Admin" });
        }
    }
}
