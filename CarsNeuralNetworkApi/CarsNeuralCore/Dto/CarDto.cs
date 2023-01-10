namespace CarsNeuralCore.Dto
{
    public class CarDto
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? BodyType { get; set; }
        public string? DriveType { get; set; }
        public string? GearboxType { get; set; }
        public string? FuelType { get; set; }
        public int? Price { get; set; }
        public int? Distance { get; set; }
        public int? ProductionYear { get; set; }
        public double? Capacity { get; set; }
        public bool? IsSold { get; set; }
    }
}