using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsNeuralNetworkAccurancyTester.Services
{
    public interface IAccurancyTesterService
    {
        public Task getAccurancy();
    }
}
