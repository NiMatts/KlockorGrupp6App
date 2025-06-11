using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KlockorGrupp6App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class testrelationer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clocks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clocks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Clocks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Clocks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Clocks",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserID",
                table: "Clocks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Clocks_CreatedByUserID",
                table: "Clocks",
                column: "CreatedByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Clocks_AspNetUsers_CreatedByUserID",
                table: "Clocks",
                column: "CreatedByUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clocks_AspNetUsers_CreatedByUserID",
                table: "Clocks");

            migrationBuilder.DropIndex(
                name: "IX_Clocks_CreatedByUserID",
                table: "Clocks");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                table: "Clocks");

            migrationBuilder.InsertData(
                table: "Clocks",
                columns: new[] { "Id", "Brand", "Model", "Price", "Year" },
                values: new object[,]
                {
                    { 1, "Rolex", "Submariner", 98000m, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Omega", "Speedmaster", 67000m, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Tag Heuer", "Carrera", 42000m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Seiko", "Presage", 8900m, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Casio", "G-Shock", 1500m, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }
    }
}
