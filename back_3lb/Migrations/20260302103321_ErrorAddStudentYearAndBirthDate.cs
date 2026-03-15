using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace back_3lb.Migrations
{
    /// <inheritdoc />
    public partial class ErrorAddStudentYearAndBirthDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Students",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Students");
        }
    }
}
