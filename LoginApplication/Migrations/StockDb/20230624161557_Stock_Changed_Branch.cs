using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginApplication.Migrations.StockDb
{
    /// <inheritdoc />
    public partial class Stock_Changed_Branch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Stocks");

            migrationBuilder.AddColumn<string>(
                name: "Branch",
                table: "Stocks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Branch",
                table: "Stocks");

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Stocks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
