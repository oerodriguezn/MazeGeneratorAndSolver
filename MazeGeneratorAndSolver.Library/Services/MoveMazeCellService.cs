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
    public class MoveMazeCellService : IMoveMazeCellService
    {
        private readonly IRandomGeneratorService _randomGeneratorService;
        public MoveMazeCellService(IRandomGeneratorService randomGeneratorService)
        {
            _randomGeneratorService = randomGeneratorService;
        }
        public Direction CalculateOppositeDirection(Direction currentDirection)
        {
            Direction OppositeDirection = currentDirection;
            switch (currentDirection)
            {
                case Direction.N:
                    OppositeDirection = Direction.S;
                    break;
                case Direction.E:
                    OppositeDirection = Direction.W;
                    break;
                case Direction.S:
                    OppositeDirection = Direction.N;
                    break;
                case Direction.W:
                    OppositeDirection = Direction.E;
                    break;
            }
            return OppositeDirection;
        }

        public MazeCell MoveToNextCell(MazeCell currentCell, Direction directionToMove)
        {
            MazeCell moveNextCell = new MazeCell();
            switch (directionToMove)
            {
                case Direction.N:
                    moveNextCell.PositionX = currentCell.PositionX;
                    if (currentCell.PositionY > 0)
                        moveNextCell.PositionY = currentCell.PositionY - 1;
                    break;
                case Direction.S:
                    moveNextCell.PositionX = currentCell.PositionX;
                    moveNextCell.PositionY = currentCell.PositionY + 1;
                    break;
                case Direction.W:
                    if (currentCell.PositionX > 0)
                        moveNextCell.PositionX = currentCell.PositionX - 1;
                    moveNextCell.PositionY = currentCell.PositionY;
                    break;
                case Direction.E:
                    moveNextCell.PositionX = currentCell.PositionX + 1;
                    moveNextCell.PositionY = currentCell.PositionY;
                    break;

            }
            moveNextCell.LastDirectionMovement = (int)directionToMove;
            return moveNextCell;
        }

        public Direction NextRandomDirectionWall(MazeCell currentCell, IMazeCell[,] generatedMaze, Direction OppositeDirection, bool allowWallBorder)
        {
            Direction NextDirection;
            int NewPositionY = currentCell.PositionY;
            int NewPositionX = currentCell.PositionX;
            bool triedN = false;
            bool triedS = false;
            bool triedW = false;
            bool triedE = false;
            do
            {
                NewPositionY = currentCell.PositionY;
                NewPositionX = currentCell.PositionX;
                NextDirection = (Direction)_randomGeneratorService.randomGenerator.Next(0, 4);
                switch (NextDirection)
                {
                    case Direction.N:
                        NewPositionY--;
                        triedN = true;
                        break;
                    case Direction.E:
                        NewPositionX++;
                        triedE = true;
                        break;
                    case Direction.S:
                        NewPositionY ++;
                        triedS = true;
                        break;
                    case Direction.W:
                        NewPositionX--;
                        triedW = true;
                        break;
                }
            } while (!(triedN && triedS && triedW && triedE) && (NewPositionY < 0 || NewPositionX < 0 || NewPositionY > generatedMaze.GetLength(1) - 1 || NewPositionX > generatedMaze.GetLength(0) - 1 || NextDirection == OppositeDirection || generatedMaze[NewPositionX, NewPositionY].Value != (int)MazeCellValueEnum.Wall || (generatedMaze[NewPositionX, NewPositionY].Value == (int)MazeCellValueEnum.WallBorder && allowWallBorder)));
            return NextDirection;
        }

        public MazeCellValueEnum ValidateAndCalculateCellMovementType(MazeCell nextCell, int HorizontalSize, int VerticalSize, int startPositionX, int startPositionY, Direction entrySide, MazeCellValueEnum markType, IMazeCell[,] generatedMaze)
        {
            MazeCellValueEnum markToMake = MazeCellValueEnum.InvalidMark;

            if (nextCell.PositionX < HorizontalSize && nextCell.PositionY < VerticalSize && nextCell.PositionX >= 0 && nextCell.PositionY >= 0)
            {
                switch (entrySide)
                {
                    case Direction.N:
                    case Direction.S:
                        if (nextCell.PositionY != startPositionY)
                            markToMake = markType;
                        break;
                    case Direction.E:
                    case Direction.W:
                        if (nextCell.PositionX != startPositionX)
                            markToMake = markType;
                        break;
                }
                if (markToMake != MazeCellValueEnum.InvalidMark)
                {
                    if (generatedMaze[nextCell.PositionX, nextCell.PositionY].Value == (int)MazeCellValueEnum.WallBorder)
                    {
                        if (markType == MazeCellValueEnum.ExitWay)
                            markToMake = MazeCellValueEnum.Exit;
                        else
                            markToMake = MazeCellValueEnum.InvalidMark;
                    }
                    else
                    {
                        if (markType == MazeCellValueEnum.Empty)
                        {
                            if (generatedMaze[nextCell.PositionX, nextCell.PositionY].Value == (int)MazeCellValueEnum.ExitWay)
                                markToMake = MazeCellValueEnum.InvalidMark;
                            else
                            {
                                int NewX = nextCell.PositionX;
                                int NewY = nextCell.PositionY;
                                switch ((Direction)nextCell.LastDirectionMovement)
                                {
                                    case Direction.N:
                                        NewY--;
                                        break;
                                    case Direction.S:
                                        NewY++;
                                        break;
                                    case Direction.E:
                                        NewX++;
                                        break;
                                    case Direction.W:
                                        NewX--;
                                        break;
                                }
                                if (NewX < 0 || NewY < 0 || NewX >= generatedMaze.GetLength(0) || NewY >= generatedMaze.GetLength(1))
                                    markToMake = MazeCellValueEnum.InvalidMark;
                                else
                                    if (generatedMaze[NewX, NewY].Value != (int)MazeCellValueEnum.Wall)
                                        markToMake = MazeCellValueEnum.InvalidMark;

                            }
                        }
                    }
                }
            }
            return markToMake;
        }

    }
}
