using MazeGeneratorAndSolver.Contracts;
using MazeGeneratorAndSolver.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGeneratorAndSolver
{
    public class DrawMazeService : IDrawMazeService
    {
        private readonly IMazeOutputWriter _writer;
        public DrawMazeService(IMazeOutputWriter mazeOutputWriter)
        {
            _writer = mazeOutputWriter;
        }
        public void DrawMaze(IMaze generatedMaze, bool DrawCorrectWay)
        {
            for (int y = 0; y < generatedMaze.Body.GetLength(1); y++)
            {
                for (int x = 0; x < generatedMaze.Body.GetLength(0); x++)
                {
                    switch ((MazeCellValueEnum)generatedMaze.Body[x, y].Value)
                    {
                        case MazeCellValueEnum.Empty:
                        case MazeCellValueEnum.NotDefined:
                            _writer.WriteSpace();
                            break;
                        case MazeCellValueEnum.Wall:
                            _writer.WriteWall();
                            break;
                        case MazeCellValueEnum.WallBorder:
                            _writer.WriteWallBorder();
                            break;
                        case MazeCellValueEnum.Entry:
                            _writer.WriteEntry();
                            break;
                        case MazeCellValueEnum.Exit:
                            _writer.WriteExit();
                            break;
                        case MazeCellValueEnum.ExitWay:
                            if (DrawCorrectWay)
                                _writer.WriteCorrectWay();
                            else
                                _writer.WriteSpace();
                            break;
                    }
                }
                _writer.WriteNewLine();
            }
        }
    }
}
