using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DotNet_8_Identity_Auth.Migrations
{
    /// <inheritdoc />
    public partial class AddFullNameToAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9af24ae-9735-43ba-b667-c2d36c9c72aa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e01c4262-f8ae-4c20-a58d-cd4675a586a7");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "92ea9cdf-c793-43cb-bbc7-71ab17a93dba", null, "Admin", "ADMIN" },
                    { "95eda769-6820-44c7-b832-46e06b06a182", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "92ea9cdf-c793-43cb-bbc7-71ab17a93dba");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "95eda769-6820-44c7-b832-46e06b06a182");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d9af24ae-9735-43ba-b667-c2d36c9c72aa", null, "User", "USER" },
                    { "e01c4262-f8ae-4c20-a58d-cd4675a586a7", null, "Admin", "ADMIN" }
                });
        }
    }
}
