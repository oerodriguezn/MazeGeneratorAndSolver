using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGeneratorAndSolver.Contracts
{
    public interface IMazeGenerator
    {
        IMaze GenerateMaze(int HorizontalSize, int VerticalSize);
    }
}
