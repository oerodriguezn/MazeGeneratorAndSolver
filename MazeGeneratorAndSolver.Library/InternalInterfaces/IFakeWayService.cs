using MazeGeneratorAndSolver.Contracts;
using MazeGeneratorAndSolver.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGeneratorAndSolver.Library.InternalInterfaces
{
    public interface IFakeWayService
    {
        void CreateMazeFakeWays(ref IMazeCell[,] generatedMaze);
    }
}
