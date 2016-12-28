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
    public class ExitWayService : IExitWayService
    {
        private readonly IMoveMazeCellService _moveMazeCellService;
        private readonly IRandomGeneratorService _randomGeneratorService;
        public ExitWayService(IMoveMazeCellService moveMazeCellService,IRandomGeneratorService randomGeneratorService)
        {
            _moveMazeCellService = moveMazeCellService;
            _randomGeneratorService = randomGeneratorService;
        }
        public void CreateEntryAndExitWayPath(int HorizontalSize, int VerticalSize, ref IMazeCell[,] generatedMaze)
        {
            Direction entrySide = (Direction)_randomGeneratorService.randomGenerator.Next(0, 3);
            int entrySideStartPosition = 1;
            if (entrySide == Direction.N || entrySide == Direction.S)
                entrySideStartPosition = _randomGeneratorService.randomGenerator.Next(HorizontalSize / 3, HorizontalSize - (HorizontalSize / 3));
            else
                entrySideStartPosition = _randomGeneratorService.randomGenerator.Next(VerticalSize / 3, VerticalSize - (VerticalSize / 3));

            MazeCell startPosition = new MazeCell();

            switch (entrySide)
            {
                case Direction.N:
                    startPosition.PositionX = entrySideStartPosition;
                    startPosition.PositionY = 0;
                    break;
                case Direction.E:
                    startPosition.PositionX = HorizontalSize - 1;
                    startPosition.PositionY = entrySideStartPosition;
                    break;
                case Direction.S:
                    startPosition.PositionX = entrySideStartPosition;
                    startPosition.PositionY = VerticalSize - 1;
                    break;
                case Direction.W:
                    startPosition.PositionX = 0;
                    startPosition.PositionY = entrySideStartPosition;
                    break;
            }

            generatedMaze[startPosition.PositionX, startPosition.PositionY].Value = (int)MazeCellValueEnum.Entry;

            MazeCell currentCell = new MazeCell { PositionX = startPosition.PositionX, PositionY = startPosition.PositionY };

            currentCell.LastDirectionMovement = (int)_moveMazeCellService.CalculateOppositeDirection(entrySide);

            MazeCell nextCell = _moveMazeCellService.MoveToNextCell(currentCell, (Direction)currentCell.LastDirectionMovement);
            currentCell.PositionX = nextCell.PositionX;
            currentCell.PositionY = nextCell.PositionY;
            generatedMaze[nextCell.PositionX, nextCell.PositionY].Value = (int)MazeCellValueEnum.ExitWay;

            while (generatedMaze[currentCell.PositionX, currentCell.PositionY].Value != (int)MazeCellValueEnum.Exit)
            {
                nextCell = _moveMazeCellService.MoveToNextCell(currentCell, _moveMazeCellService.NextRandomDirectionWall(currentCell, generatedMaze, _moveMazeCellService.CalculateOppositeDirection((Direction)currentCell.LastDirectionMovement),false));
                MazeCellValueEnum validateMovement = _moveMazeCellService.ValidateAndCalculateCellMovementType(nextCell, HorizontalSize, VerticalSize, startPosition.PositionX, startPosition.PositionY, entrySide, MazeCellValueEnum.ExitWay,generatedMaze);
                if (validateMovement != MazeCellValueEnum.InvalidMark)
                {
                    currentCell.PositionX = nextCell.PositionX;
                    currentCell.PositionY = nextCell.PositionY;
                    currentCell.LastDirectionMovement = nextCell.LastDirectionMovement;
                    generatedMaze[nextCell.PositionX, nextCell.PositionY].Value = (int)validateMovement;

                    if (generatedMaze[currentCell.PositionX, currentCell.PositionY].Value != (int)MazeCellValueEnum.Exit)
                    {
                        nextCell = _moveMazeCellService.MoveToNextCell(nextCell, (Direction)currentCell.LastDirectionMovement);
                        MazeCellValueEnum validateNextMovement = _moveMazeCellService.ValidateAndCalculateCellMovementType(nextCell, HorizontalSize, VerticalSize, startPosition.PositionX, startPosition.PositionY, entrySide, MazeCellValueEnum.ExitWay, generatedMaze);
                        if (validateNextMovement != MazeCellValueEnum.InvalidMark)
                        {
                            currentCell.PositionX = nextCell.PositionX;
                            currentCell.PositionY = nextCell.PositionY;
                            currentCell.LastDirectionMovement = nextCell.LastDirectionMovement;
                            generatedMaze[nextCell.PositionX, nextCell.PositionY].Value = (int)validateNextMovement;
                        }
                    }
                }
            }
        }

    }
}
