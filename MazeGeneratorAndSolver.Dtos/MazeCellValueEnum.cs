using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGeneratorAndSolver.Dtos
{
    public enum MazeCellValueEnum
    {
        InvalidMark = -1,
        NotDefined = 0,
        Empty = 1,
        Wall = 2,
        WallBorder = 3,
        Entry = 4,
        Exit = 5,
        ExitWay = 6
    }
}
