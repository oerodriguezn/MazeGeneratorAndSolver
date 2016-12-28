using MazeGeneratorAndSolver.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGeneratorAndSolver
{
    public interface IDrawMazeService
    {
        void DrawMaze(IMaze generatedMaze, bool DrawCorrectWay);
    }
}
