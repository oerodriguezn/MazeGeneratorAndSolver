using MazeGeneratorAndSolver.Contracts;
using MazeGeneratorAndSolver.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGeneratorAndSolver
{
    class MazeOutputWriter : IMazeOutputWriter
    {
        public void WriteNewLine()
        {
            Console.WriteLine();
        }

        public void WriteWall()
        {
            Console.Write("#");
        }

        public void WriteWallBorder()
        {
            Console.Write("#");
        }

        public void WriteSpace()
        {
            Console.Write(" ");
        }

        public void WriteEntry()
        {
            Console.Write("E");
        }

        public void WriteExit()
        {
            Console.Write("S");
        }

        public void WriteCorrectWay()
        {
            Console.Write(".");
        }

    }
}
