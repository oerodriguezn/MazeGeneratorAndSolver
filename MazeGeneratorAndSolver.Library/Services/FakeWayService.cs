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
    public class FakeWayService : IFakeWayService
    {
        private readonly IMoveMazeCellService _moveMazeCellService;
        private readonly IRandomGeneratorService _randomGeneratorService;
        public FakeWayService(IMoveMazeCellService moveMazeCellService, IRandomGeneratorService randomGeneratorService)
        {
            _moveMazeCellService = moveMazeCellService;
            _randomGeneratorService = randomGeneratorService;
        }
        public void CreateMazeFakeWays(ref IMazeCell[,] generatedMaze)
        {
            var exitWayCells = (from MazeCell cell in generatedMaze
                                where cell.Value == (int)MazeCellValueEnum.ExitWay
                                select cell);

            if (exitWayCells.Any())
            {
                int newFakeWaysCount = exitWayCells.Count() / 4;
                var randomExitWayCells = exitWayCells.OrderBy(x => Guid.NewGuid()).Take(newFakeWaysCount);
                foreach (var randomCell in randomExitWayCells)
                {
                    randomCell.LastDirectionMovement = (int)_moveMazeCellService.NextRandomDirectionWall(randomCell, generatedMaze, _moveMazeCellService.CalculateOppositeDirection((Direction)randomCell.LastDirectionMovement),false);
                    var nextCell = _moveMazeCellService.MoveToNextCell(randomCell, (Direction)randomCell.LastDirectionMovement);
                    MazeCellValueEnum validateNextCell =_moveMazeCellService.ValidateAndCalculateCellMovementType(nextCell, generatedMaze.GetLength(0), generatedMaze.GetLength(1), randomCell.PositionX, randomCell.PositionY, _moveMazeCellService.CalculateOppositeDirection((Direction)randomCell.LastDirectionMovement), MazeCellValueEnum.Empty,generatedMaze);
                    if (validateNextCell != MazeCellValueEnum.InvalidMark)
                    {
                        MazeCell currentCell = new MazeCell();
                        currentCell.PositionX = nextCell.PositionX;
                        currentCell.PositionY = nextCell.PositionY;
                        currentCell.LastDirectionMovement = randomCell.LastDirectionMovement;
                        generatedMaze[nextCell.PositionX, nextCell.PositionY].Value = (int)validateNextCell;

                        int wayLenght = _randomGeneratorService.randomGenerator.Next(5, 30);
                        for (int i = 0; i < wayLenght; i++)
                        {
                            nextCell = _moveMazeCellService.MoveToNextCell(currentCell, _moveMazeCellService.NextRandomDirectionWall(currentCell, generatedMaze, _moveMazeCellService.CalculateOppositeDirection((Direction)currentCell.LastDirectionMovement),false));
                            validateNextCell = _moveMazeCellService.ValidateAndCalculateCellMovementType(nextCell, generatedMaze.GetLength(0), generatedMaze.GetLength(1), randomCell.PositionX, randomCell.PositionY, _moveMazeCellService.CalculateOppositeDirection((Direction)randomCell.LastDirectionMovement), MazeCellValueEnum.Empty, generatedMaze);
                            if (validateNextCell != MazeCellValueEnum.InvalidMark)
                            {
                                currentCell.PositionX = nextCell.PositionX;
                                currentCell.PositionY = nextCell.PositionY;
                                currentCell.LastDirectionMovement = randomCell.LastDirectionMovement;
                                generatedMaze[nextCell.PositionX, nextCell.PositionY].Value = (int)validateNextCell;
                            }
                        }
                    }

                }
            }

        }

    }
}
