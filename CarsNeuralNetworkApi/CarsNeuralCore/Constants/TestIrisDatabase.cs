namespace CarsNeuralCore.Constants
{
    public static class TestIrisDatabase
    {
        public static List<List<double>> trainData = new List<List<double>>()
        {
            new List<double>{ 6.3, 2.9, 5.6, 1.8, 1, 0, 0 },
            new List<double>{ 6.9, 3.1, 4.9, 1.5, 0, 1, 0 },
            new List<double>{ 4.6, 3.4, 1.4, 0.3, 0, 0, 1 },
            new List<double>{ 7.2, 3.6, 6.1, 2.5, 1, 0, 0 },
            new List<double>{ 4.7, 3.2, 1.3, 0.2, 0, 0, 1 },
            new List<double>{ 4.9, 3, 1.4, 0.2, 0, 0, 1 },
            new List<double>{ 7.6, 3, 6.6, 2.1, 1, 0, 0 },
            new List<double>{ 4.9, 2.4, 3.3, 1, 0, 1, 0 },
            new List<double>{ 5.4, 3.9, 1.7, 0.4, 0, 0, 1 },
            new List<double>{ 4.9, 3.1, 1.5, 0.1, 0, 0, 1 },
            new List<double>{ 5, 3.6, 1.4, 0.2, 0, 0, 1 },
            new List<double>{ 6.4, 3.2, 4.5, 1.5, 0, 1, 0 },
            new List<double>{ 4.4, 2.9, 1.4, 0.2, 0, 0, 1 },
            new List<double>{ 6.3, 3.3, 6, 2.5, 1, 0, 0 },
            new List<double>{ 6.3, 3.3, 6, 2.5, 1, 0, 0 },
            new List<double>{ 5.8, 2.7, 5.1, 1.9, 1, 0, 0 },
            new List<double>{ 5.2, 2.7, 3.9, 1.4, 0, 1, 0 },
            new List<double>{ 7, 3.2, 4.7, 1.4, 0, 1, 0 },
            new List<double>{ 6.5, 2.8, 4.6, 1.5, 0, 1, 0 },
            new List<double>{ 4.9, 2.5, 4.5, 1.7, 1, 0, 0 },
            new List<double>{ 5.7, 2.8, 4.5, 1.3, 0, 1, 0 },
            new List<double>{ 5, 3.4, 1.5, 0.2, 0, 0, 1 },
            new List<double>{ 5, 3.4, 1.5, 0.2, 0, 0, 1 },
            new List<double>{ 6.5, 3, 5.8, 2.2, 1, 0, 0 },
            new List<double>{ 5.5, 2.3, 4, 1.3, 0, 1, 0 },
            new List<double>{ 6.7, 2.5, 5.8, 1.8, 1, 0, 0 },
        };

        public static List<List<double>> testData = new List<List<double>>()
        {
            new List<double>{ 4.6, 3.1, 1.5, 0.2 },// 0, 0, 1 },
            new List<double>{ 7.1, 3, 5.9, 2.1 },// 1, 0, 0 },
            new List<double>{ 6.3, 3.3, 4.7, 1.6 },// 0, 1, 0 },
            new List<double>{ 6.6, 2.9, 4.6, 1.3 },// 0, 1, 0 },
            new List<double> { 7.3, 2.9, 6.3, 1.8 },// 1, 0, 0 },
        };

        public static double[][] trainDataArray = new double[24][]
        {
             new double[] { 6.3, 2.9, 5.6, 1.8, 1, 0, 0 },
              new double[] { 6.9, 3.1, 4.9, 1.5, 0, 1, 0 },
              new double[] { 4.6, 3.4, 1.4, 0.3, 0, 0, 1 },
              new double[] { 7.2, 3.6, 6.1, 2.5, 1, 0, 0 },
              new double[] { 4.7, 3.2, 1.3, 0.2, 0, 0, 1 },
              new double[] { 4.9, 3, 1.4, 0.2, 0, 0, 1 },
              new double[] { 7.6, 3, 6.6, 2.1, 1, 0, 0 },
              new double[] { 4.9, 2.4, 3.3, 1, 0, 1, 0 },
              new double[] { 5.4, 3.9, 1.7, 0.4, 0, 0, 1 },
              new double[] { 4.9, 3.1, 1.5, 0.1, 0, 0, 1 },
              new double[] { 5, 3.6, 1.4, 0.2, 0, 0, 1 },
              new double[] { 6.4, 3.2, 4.5, 1.5, 0, 1, 0 },
              new double[] { 4.4, 2.9, 1.4, 0.2, 0, 0, 1 },
              new double[] { 5.8, 2.7, 5.1, 1.9, 1, 0, 0 },
              new double[] { 6.3, 3.3, 6, 2.5, 1, 0, 0 },
              new double[] { 5.2, 2.7, 3.9, 1.4, 0, 1, 0 },
              new double[] { 7, 3.2, 4.7, 1.4, 0, 1, 0 },
              new double[] { 6.5, 2.8, 4.6, 1.5, 0, 1, 0 },
              new double[] { 4.9, 2.5, 4.5, 1.7, 1, 0, 0 },
              new double[] { 5.7, 2.8, 4.5, 1.3, 0, 1, 0 },
              new double[] { 5, 3.4, 1.5, 0.2, 0, 0, 1 },
              new double[] { 6.5, 3, 5.8, 2.2, 1, 0, 0 },
              new double[] { 5.5, 2.3, 4, 1.3, 0, 1, 0 },
              new double[] { 6.7, 2.5, 5.8, 1.8, 1, 0, 0 },
        };

        public static double[][] testDataArray = new double[6][]
        {
            new double[] { 4.6, 3.1, 1.5, 0.2 },// 0, 0, 1 };
            new double[] { 7.1, 3, 5.9, 2.1 },// 1, 0, 0 };
            new double[] { 5.1, 3.5, 1.4, 0.2 },// 0, 0, 1 };
            new double[] { 6.3, 3.3, 4.7, 1.6 },// 0, 1, 0 };
            new double[] { 6.6, 2.9, 4.6, 1.3 },// 0, 1, 0 };
            new double[] { 7.3, 2.9, 6.3, 1.8 },// 1, 0, 0 };
        };

        public static int GetTrainListCount()
        {
            return trainData.Count();
        }

        public static int GetTestDataCount()
        {
            return testData.Count();
        }
    }
}