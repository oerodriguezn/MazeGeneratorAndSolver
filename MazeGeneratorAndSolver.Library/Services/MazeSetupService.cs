using MazeGeneratorAndSolver.Contracts;
using MazeGeneratorAndSolver.Dtos;
using MazeGeneratorAndSolver.Library.InternalInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGeneratorAndSolver.Library.Services
{
    public class MazeSetupService : IMazeSetupService
    {
        public IMazeCell[,] InitializeMaze(int HorizontalSize, int VerticalSize)
        {
            IMazeCell[,] generatedMaze = new IMazeCell[HorizontalSize, VerticalSize];
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
            return generatedMaze;
        }
    }
}
