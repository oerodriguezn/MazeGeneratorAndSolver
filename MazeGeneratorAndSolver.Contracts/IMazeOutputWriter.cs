using MazeGeneratorAndSolver.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGeneratorAndSolver.Contracts
{
    public interface IMazeOutputWriter
    {
        void WriteNewLine();
        void WriteWall();

        void WriteWallBorder();

        void WriteSpace();

        void WriteCorrectWay();

        void WriteEntry();

        void WriteExit();
    }
}
