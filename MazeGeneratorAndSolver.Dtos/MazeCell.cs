using MazeGeneratorAndSolver.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGeneratorAndSolver.Dtos
{
    public class MazeCell: IMazeCell
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Value { get; set; }

        public int LastDirectionMovement { get; set; }
    }
}
