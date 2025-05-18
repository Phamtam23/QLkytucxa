using Microsoft.EntityFrameworkCore.Migrations;

namespace Quanlykytucxa.Migrations
{
    public partial class Addanhloai : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a46aa989-496f-4460-9974-5ec66b24ba4f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c678d73c-28e5-4767-9b66-f11c185642ab");

            migrationBuilder.AddColumn<string>(
                name: "anh",
                table: "loaiPhong",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7b7845bf-80ca-4e91-8411-2c0794c5b8f9", "3dfacc30-c136-4f1b-9852-57a396f50d27", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9f271b13-a1d9-4d89-9f39-64939d268bf9", "64f358d0-68f3-4ad6-92cb-f74e076f1c81", "client", "client" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7b7845bf-80ca-4e91-8411-2c0794c5b8f9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9f271b13-a1d9-4d89-9f39-64939d268bf9");

            migrationBuilder.DropColumn(
                name: "anh",
                table: "loaiPhong");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a46aa989-496f-4460-9974-5ec66b24ba4f", "e9fa71cc-19f3-49f5-b74c-f4f4f456153c", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c678d73c-28e5-4767-9b66-f11c185642ab", "177c2548-ebe0-458d-82d0-f52df4a522ef", "client", "client" });
        }
    }
}
