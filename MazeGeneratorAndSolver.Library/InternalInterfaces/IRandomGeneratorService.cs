using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGeneratorAndSolver.Library.InternalInterfaces
{
    public interface IRandomGeneratorService
    {
        Random randomGenerator { get; }
    }
}
