using Microsoft.EntityFrameworkCore.Migrations;

namespace Quanlykytucxa.Migrations
{
    public partial class Addanh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "239320f9-5599-43f8-a73e-043e8ebf19fc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7a36344-3e0b-44b1-a1ed-70d7c2a0781b");

            migrationBuilder.AddColumn<string>(
                name: "img1",
                table: "Phong",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "img2",
                table: "Phong",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "img3",
                table: "Phong",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a46aa989-496f-4460-9974-5ec66b24ba4f", "e9fa71cc-19f3-49f5-b74c-f4f4f456153c", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c678d73c-28e5-4767-9b66-f11c185642ab", "177c2548-ebe0-458d-82d0-f52df4a522ef", "client", "client" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a46aa989-496f-4460-9974-5ec66b24ba4f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c678d73c-28e5-4767-9b66-f11c185642ab");

            migrationBuilder.DropColumn(
                name: "img1",
                table: "Phong");

            migrationBuilder.DropColumn(
                name: "img2",
                table: "Phong");

            migrationBuilder.DropColumn(
                name: "img3",
                table: "Phong");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "239320f9-5599-43f8-a73e-043e8ebf19fc", "22ce28e8-3dff-4dec-af16-b0afbf70462a", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c7a36344-3e0b-44b1-a1ed-70d7c2a0781b", "17836e19-b4b3-458d-ba25-04292970be29", "client", "client" });
        }
    }
}
