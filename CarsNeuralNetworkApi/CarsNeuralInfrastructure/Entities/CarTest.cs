using CarsNeuralInfrastructure.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsNeuralInfrastructure.Models
{
    public class CarTest : BaseEntity
    {
        [Column(TypeName = "varchar(100)")]
        public string Brand { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Model { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string BodyType { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string DriveType { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string GearboxType { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string FuelType { get; set; }

        public int Price { get; set; }
        public int Distance { get; set; }
        public int ProductionYear { get; set; }
        public double Capacity { get; set; }

        [JsonConstructor]
        public CarTest(string brand, string model, string bodyType, string driveType, string gearboxType, string fuelType, int price, int distance, int productionYear, double capacity)
        {
            Brand = brand;
            Model = model;
            BodyType = bodyType;
            DriveType = driveType;
            GearboxType = gearboxType;
            FuelType = fuelType;
            Price = price;
            Distance = distance;
            ProductionYear = productionYear;
            Capacity = capacity;
        }
    }
}