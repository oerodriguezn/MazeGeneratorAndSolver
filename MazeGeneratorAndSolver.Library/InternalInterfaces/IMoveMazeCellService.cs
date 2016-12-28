using MazeGeneratorAndSolver.Contracts;
using MazeGeneratorAndSolver.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGeneratorAndSolver.Library.InternalInterfaces
{
    public interface IMoveMazeCellService
    {
        Direction CalculateOppositeDirection(Direction currentDirection);
        MazeCell MoveToNextCell(MazeCell currentCell, Direction directionToMove);

        Direction NextRandomDirectionWall(MazeCell currentCell, IMazeCell[,] generatedMaze, Direction oppositeDirection, bool allowWallBorder);

        MazeCellValueEnum ValidateAndCalculateCellMovementType(MazeCell nextCell, int HorizontalSize, int VerticalSize, int startPositionX, int startPositionY, Direction entrySide, MazeCellValueEnum markType, IMazeCell[,] generatedMaze);
    }
}
