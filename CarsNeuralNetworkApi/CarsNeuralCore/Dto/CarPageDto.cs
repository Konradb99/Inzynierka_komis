namespace CarsNeuralCore.Dto
{
    public class CarPageDto
    {
        public int PageNumber { get; set; }
        public ICollection<CarDto> Data { get; set; }
    }
}