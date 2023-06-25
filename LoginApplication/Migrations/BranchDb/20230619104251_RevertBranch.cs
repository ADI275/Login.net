using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginApplication.Migrations.BranchDb
{
    /// <inheritdoc />
    public partial class RevertBranch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameChangeDate",
                table: "Branches");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NameChangeDate",
                table: "Branches",
                type: "datetime2",
                nullable: true);
        }
    }
}
