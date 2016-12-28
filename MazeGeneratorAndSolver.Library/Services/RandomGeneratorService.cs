using MazeGeneratorAndSolver.Library.InternalInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGeneratorAndSolver.Library.Services
{
    public class RandomGeneratorService : IRandomGeneratorService
    {
        static Random _RandomGenerator;

        public RandomGeneratorService()
        {
            if (_RandomGenerator == null)
                _RandomGenerator = new Random(DateTime.Now.Millisecond);
        }
        public Random randomGenerator
        {
            get { return _RandomGenerator; }
        }
    }
}
