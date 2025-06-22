using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace core_service.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrencySimbol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image_url",
                table: "currency");

            migrationBuilder.AddColumn<string>(
                name: "simbol",
                table: "currency",
                type: "character varying(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "simbol",
                table: "currency");

            migrationBuilder.AddColumn<string>(
                name: "image_url",
                table: "currency",
                type: "character varying(2048)",
                maxLength: 2048,
                nullable: true);
        }
    }
}
