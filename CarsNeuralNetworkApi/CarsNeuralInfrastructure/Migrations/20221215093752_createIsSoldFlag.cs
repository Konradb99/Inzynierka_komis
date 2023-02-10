using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarsNeuralInfrastructure.Migrations
{
    public partial class createIsSoldFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSold",
                table: "Cars",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSold",
                table: "Cars");
        }
    }
}
