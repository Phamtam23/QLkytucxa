using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Quanlykytucxa.Migrations
{
    public partial class AddDatayc1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7b7845bf-80ca-4e91-8411-2c0794c5b8f9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9f271b13-a1d9-4d89-9f39-64939d268bf9");

            migrationBuilder.AddColumn<DateTime>(
                name: "Ngaygui",
                table: "YeuCauSuaChua",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "da2068b6-9815-4931-b603-599b35340c14", "af0f5a5c-b659-4d0d-87b5-f4f9eac9b244", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1259b379-0e49-4c72-b070-2fdf39caef58", "55f0bd43-8b32-4b1e-9c75-b8e59ffab998", "client", "client" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1259b379-0e49-4c72-b070-2fdf39caef58");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "da2068b6-9815-4931-b603-599b35340c14");

            migrationBuilder.DropColumn(
                name: "Ngaygui",
                table: "YeuCauSuaChua");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7b7845bf-80ca-4e91-8411-2c0794c5b8f9", "3dfacc30-c136-4f1b-9852-57a396f50d27", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9f271b13-a1d9-4d89-9f39-64939d268bf9", "64f358d0-68f3-4ad6-92cb-f74e076f1c81", "client", "client" });
        }
    }
}
