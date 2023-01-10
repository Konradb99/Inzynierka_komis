using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarsNeuralInfrastructure.Migrations
{
    public partial class fixDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Car",
                table: "Car");

            migrationBuilder.RenameTable(
                name: "Car",
                newName: "Cars");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cars",
                table: "Cars",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TestSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "varchar(100)", nullable: false),
                    Model = table.Column<string>(type: "varchar(100)", nullable: false),
                    BodyType = table.Column<string>(type: "varchar(100)", nullable: false),
                    DriveType = table.Column<string>(type: "varchar(100)", nullable: false),
                    GearboxType = table.Column<string>(type: "varchar(100)", nullable: false),
                    FuelType = table.Column<string>(type: "varchar(100)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Distance = table.Column<int>(type: "int", nullable: false),
                    ProductionYear = table.Column<int>(type: "int", nullable: false),
                    Power = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "varchar(100)", nullable: false),
                    Model = table.Column<string>(type: "varchar(100)", nullable: false),
                    BodyType = table.Column<string>(type: "varchar(100)", nullable: false),
                    DriveType = table.Column<string>(type: "varchar(100)", nullable: false),
                    GearboxType = table.Column<string>(type: "varchar(100)", nullable: false),
                    FuelType = table.Column<string>(type: "varchar(100)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Distance = table.Column<int>(type: "int", nullable: false),
                    ProductionYear = table.Column<int>(type: "int", nullable: false),
                    Power = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainSet", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestSet");

            migrationBuilder.DropTable(
                name: "TrainSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cars",
                table: "Cars");

            migrationBuilder.RenameTable(
                name: "Cars",
                newName: "Car");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Car",
                table: "Car",
                column: "Id");
        }
    }
}