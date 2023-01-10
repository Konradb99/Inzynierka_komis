namespace CarsNeuralCore.Dto
{
    public class PredictionResultDto
    {
        public string prefferedClass { get; set; }
        public ICollection<CarDto> prefferedCars { get; set; }
    }
}