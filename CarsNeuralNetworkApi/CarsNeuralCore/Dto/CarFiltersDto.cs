namespace CarsNeuralCore.Dto
{
    public class CarFiltersDto
    {
        public string? Brand { get; set; } = null;
        public string? Model { get; set; } = null;
        public string? BodyType { get; set; } = null;
        public string? DriveType { get; set; } = null;
        public string? GearboxType { get; set; } = null;
        public string? FuelType { get; set; } = null;
        public string? PriceMin { get; set; } = null;
        public string? PriceMax { get; set; } = null;
        public string? DistanceMin { get; set; } = null;
        public string? DistanceMax { get; set; } = null;
        public string? ProductionYearMin { get; set; } = null;
        public string? ProductionYearMax { get; set; } = null;
        public string? CapacityMin { get; set; } = null;
        public string? CapacityMax { get; set; } = null;
    }
}