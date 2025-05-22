using Microsoft.EntityFrameworkCore.Migrations;

namespace Quanlykytucxa.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "239320f9-5599-43f8-a73e-043e8ebf19fc", "22ce28e8-3dff-4dec-af16-b0afbf70462a", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c7a36344-3e0b-44b1-a1ed-70d7c2a0781b", "17836e19-b4b3-458d-ba25-04292970be29", "client", "client" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "239320f9-5599-43f8-a73e-043e8ebf19fc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7a36344-3e0b-44b1-a1ed-70d7c2a0781b");
        }
    }
}
