using MazeGeneratorAndSolver.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGeneratorAndSolver.Dtos
{
    public class Maze : IMaze
    {
        public IMazeCell[,] Body { get; set; }
    }
}
