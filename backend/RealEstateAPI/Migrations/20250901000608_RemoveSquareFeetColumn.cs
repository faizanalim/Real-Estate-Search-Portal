using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSquareFeetColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
             name: "SquareFeet",
             table: "Properties");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
    name: "SquareFeet",
    table: "Properties",
    type: "int",
    nullable: false,
    defaultValue: 0);
        }
    }
}
