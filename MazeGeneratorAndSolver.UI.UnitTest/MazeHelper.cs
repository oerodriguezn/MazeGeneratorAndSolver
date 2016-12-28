using MazeGeneratorAndSolver.Contracts;
using MazeGeneratorAndSolver.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGeneratorAndSolver.UI.UnitTest
{
    public class MazeHelper
    {
        public static Maze InitializeMaze(int HorizontalSize, int VerticalSize)
        {
            IMazeCell[,] generatedMaze = new MazeCell[HorizontalSize, VerticalSize];
            for (int i = 0; i < HorizontalSize; i++)
                for (int j = 0; j < VerticalSize; j++)
                {
                    generatedMaze[i, j] = new MazeCell();
                    generatedMaze[i, j].Value = (int)MazeCellValueEnum.Wall;
                    generatedMaze[i, j].PositionX = i;
                    generatedMaze[i, j].PositionY = j;

                    if (i == 0)
                        generatedMaze[i, j].Value = (int)MazeCellValueEnum.WallBorder;

                    if (j == 0)
                        generatedMaze[i, j].Value = (int)MazeCellValueEnum.WallBorder;

                    if (i == HorizontalSize - 1)
                        generatedMaze[i, j].Value = (int)MazeCellValueEnum.WallBorder;

                    if (j == VerticalSize - 1)
                        generatedMaze[i, j].Value = (int)MazeCellValueEnum.WallBorder;
                }

            generatedMaze[0, 0].Value = (int)MazeCellValueEnum.Entry;
            generatedMaze[1, 1].Value = (int)MazeCellValueEnum.Empty;
            generatedMaze[2, 2].Value = (int)MazeCellValueEnum.ExitWay;
            generatedMaze[HorizontalSize - 1, VerticalSize-1].Value = (int)MazeCellValueEnum.Exit;
            return new Maze() { Body = generatedMaze };
        }
    }
}
