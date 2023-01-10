namespace CarsNeuralKNN.Encoders
{
    public interface IDataClassifier
    {
        public double[] getMinMax(ICollection<double[]> trainSet);

        public double[] getMinMaxWithFilters(ICollection<double[]> trainSet);
    }
}