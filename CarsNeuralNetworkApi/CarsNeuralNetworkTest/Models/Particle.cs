using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsNeuralNetwork.Models
{
    public class Particle
    {
        //public double age; // optional used to determine death-birth

        public Particle(double[] position, double error, double[] velocity,
          double[] bestPosition, double bestError)
        {
            this.Position = new double[position.Length];
            position.CopyTo(this.Position, 0);
            this.Error = error;
            this.Velocity = new double[velocity.Length];
            velocity.CopyTo(this.Velocity, 0);
            this.BestPosition = new double[bestPosition.Length];
            bestPosition.CopyTo(this.BestPosition, 0);
            this.BestError = bestError;

        }

        public double[] Position
        {
            get; set;

        }
        public double Error
        {
            get; set;
        }

        public double[] Velocity
        {
            get; set;
        }

        public double[] BestPosition
        {
            get; set;
        }
        public double BestError
        {
            get; set;
        }
    }
}