using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExistingPropertiesWithSquareFeet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        UPDATE Properties 
        SET SquareFeet = CASE 
            WHEN Title LIKE '%Apartment%' THEN 1200
            WHEN Title LIKE '%Studio%' THEN 800
            WHEN Title LIKE '%Penthouse%' THEN 1800
            WHEN Title LIKE '%Family%' THEN 2000
            WHEN Title LIKE '%House%' THEN 1800
            WHEN Title LIKE '%Unit%' THEN 1000
            ELSE 1500
        END
        WHERE SquareFeet = 0
    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
