using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KlockorGrupp6App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Newaddingbro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Clocks",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Clocks",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");
        }
    }
}
