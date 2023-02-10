using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarsNeuralInfrastructure.Migrations
{
    public partial class removePower : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Power",
                table: "TrainSet");

            migrationBuilder.DropColumn(
                name: "Power",
                table: "TestSet");

            migrationBuilder.DropColumn(
                name: "Power",
                table: "Cars");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Power",
                table: "TrainSet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Power",
                table: "TestSet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Power",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}