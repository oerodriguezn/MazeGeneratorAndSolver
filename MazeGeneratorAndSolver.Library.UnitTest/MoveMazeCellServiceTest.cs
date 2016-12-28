using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MazeGeneratorAndSolver.Dtos;
using MazeGeneratorAndSolver.Contracts;
using MazeGeneratorAndSolver.Library.Services;

namespace MazeGeneratorAndSolver.Library.UnitTest
{
    [TestClass]
    public class MoveMazeCellServiceTest
    {
        [TestMethod]
        public void MarkMovementMazeTestExitOk()
        {
            var randomGeneratorService = new RandomGeneratorService();
            var mazeSetupService = new MazeSetupService();
            var moveMazeCellService = new MoveMazeCellService(randomGeneratorService);
            var generatedMaze = mazeSetupService.InitializeMaze(10, 10);

            MazeCellValueEnum expectec = MazeCellValueEnum.Exit;
            MazeCell nextCell = new MazeCell(){PositionX = 0, PositionY = 0, LastDirectionMovement = (int)Direction.N};


            var result = moveMazeCellService.ValidateAndCalculateCellMovementType(nextCell, 10, 10, 9, 9, Direction.S, MazeCellValueEnum.ExitWay, generatedMaze);

            Assert.IsTrue(expectec == result);
        }

        [TestMethod]
        public void MarkMovementMazeTestExitKO()
        {
            var randomGeneratorService = new RandomGeneratorService();
            var mazeSetupService = new MazeSetupService();
            var moveMazeCellService = new MoveMazeCellService(randomGeneratorService);
            var generatedMaze = mazeSetupService.InitializeMaze(10, 10);

            MazeCellValueEnum expectec = MazeCellValueEnum.InvalidMark;
            MazeCell nextCell = new MazeCell() { PositionX = 9, PositionY = 9, LastDirectionMovement = (int)Direction.S };


            var result = moveMazeCellService.ValidateAndCalculateCellMovementType(nextCell, 10, 10, 9, 9, Direction.S, MazeCellValueEnum.ExitWay, generatedMaze);

            Assert.IsTrue(expectec == result);
        }

        [TestMethod]
        public void MarkMovementMazeTestInvalidMarkKO()
        {
            var randomGeneratorService = new RandomGeneratorService();
            var mazeSetupService = new MazeSetupService();
            var moveMazeCellService = new MoveMazeCellService(randomGeneratorService);
            var generatedMaze = mazeSetupService.InitializeMaze(10, 10);

            MazeCellValueEnum expectec = MazeCellValueEnum.InvalidMark;
            MazeCell nextCell = new MazeCell() { PositionX = -1, PositionY = -1, LastDirectionMovement = (int)Direction.S };


            var result = moveMazeCellService.ValidateAndCalculateCellMovementType(nextCell, 10, 10, 9, 9, Direction.S, MazeCellValueEnum.ExitWay, generatedMaze);

            Assert.IsTrue(expectec == result);
        }

        [TestMethod]
        public void MarkMovementMazeTestCorrectWayOk()
        {
            var randomGeneratorService = new RandomGeneratorService();
            var mazeSetupService = new MazeSetupService();
            var moveMazeCellService = new MoveMazeCellService(randomGeneratorService);
            var generatedMaze = mazeSetupService.InitializeMaze(10, 10);

            MazeCellValueEnum expectec = MazeCellValueEnum.ExitWay;
            MazeCell nextCell = new MazeCell() { PositionX = 1, PositionY = 1, LastDirectionMovement = (int)Direction.N };


            var result = moveMazeCellService.ValidateAndCalculateCellMovementType(nextCell, 10, 10, 9, 9, Direction.S, MazeCellValueEnum.ExitWay, generatedMaze);

            Assert.IsTrue(expectec == result);
        }

        [TestMethod]
        public void MarkMovementMazeTestWhiteSpaceOk()
        {
            var randomGeneratorService = new RandomGeneratorService();
            var mazeSetupService = new MazeSetupService();
            var moveMazeCellService = new MoveMazeCellService(randomGeneratorService);
            var generatedMaze = mazeSetupService.InitializeMaze(10, 10);

            MazeCellValueEnum expectec = MazeCellValueEnum.Empty;
            MazeCell nextCell = new MazeCell() { PositionX = 1, PositionY = 1, LastDirectionMovement = (int)Direction.S };


            var result = moveMazeCellService.ValidateAndCalculateCellMovementType(nextCell, 10, 10, 9, 9, Direction.S, MazeCellValueEnum.Empty, generatedMaze);

            Assert.IsTrue(expectec == result);
        }
        [TestMethod]
        public void MarkMovementMazeTestWhiteSpaceKo()
        {
            var randomGeneratorService = new RandomGeneratorService();
            var mazeSetupService = new MazeSetupService();
            var moveMazeCellService = new MoveMazeCellService(randomGeneratorService);
            var generatedMaze = mazeSetupService.InitializeMaze(10, 10);

            MazeCellValueEnum expectec = MazeCellValueEnum.InvalidMark;
            MazeCell nextCell = new MazeCell() { PositionX = 1, PositionY = 1, LastDirectionMovement = (int)Direction.S };
            generatedMaze[1, 1].Value = (int)MazeCellValueEnum.ExitWay;

            var result = moveMazeCellService.ValidateAndCalculateCellMovementType(nextCell, 10, 10, 9, 9, Direction.S, MazeCellValueEnum.Empty, generatedMaze);

            Assert.IsTrue(expectec == result);
        }

        [TestMethod]
        public void CalculateOppositeDirectionTestOk()
        {
            var randomGeneratorService = new RandomGeneratorService();
            var moveMazeCellService = new MoveMazeCellService(randomGeneratorService);

            Assert.IsTrue(moveMazeCellService.CalculateOppositeDirection(Direction.N) == Direction.S);
            Assert.IsTrue(moveMazeCellService.CalculateOppositeDirection(Direction.S) == Direction.N);
            Assert.IsTrue(moveMazeCellService.CalculateOppositeDirection(Direction.E) == Direction.W);
            Assert.IsTrue(moveMazeCellService.CalculateOppositeDirection(Direction.W) == Direction.E);
        }

        [TestMethod]
        public void MoveForwardCellTestOk()
        {
            var randomGeneratorService = new RandomGeneratorService();
            var moveMazeCellService = new MoveMazeCellService(randomGeneratorService);

            MazeCell cellToTest = new MazeCell() { PositionX = 5, PositionY = 5, LastDirectionMovement = (int)Direction.S };

            var resultCell = moveMazeCellService.MoveToNextCell(cellToTest, Direction.S);
            Assert.IsTrue(resultCell.PositionY == cellToTest.PositionY + 1 && resultCell.PositionX == cellToTest.PositionX);

            cellToTest.LastDirectionMovement = (int)Direction.N;
            resultCell = moveMazeCellService.MoveToNextCell(cellToTest, Direction.N);
            Assert.IsTrue(resultCell.PositionY == cellToTest.PositionY -1 && resultCell.PositionX == cellToTest.PositionX);

            cellToTest.LastDirectionMovement = (int)Direction.W;
            resultCell = moveMazeCellService.MoveToNextCell(cellToTest, Direction.W);
            Assert.IsTrue(resultCell.PositionY == cellToTest.PositionY  && resultCell.PositionX == cellToTest.PositionX-1);

            cellToTest.LastDirectionMovement = (int)Direction.E;
            resultCell = moveMazeCellService.MoveToNextCell(cellToTest, Direction.E);
            Assert.IsTrue(resultCell.PositionY == cellToTest.PositionY && resultCell.PositionX == cellToTest.PositionX  +1);
        }

        [TestMethod]
        public void NextRandomDirectionWallTestOk()
        {
            var randomGeneratorService = new RandomGeneratorService();
            var moveMazeCellService = new MoveMazeCellService(randomGeneratorService);
            var mazeSetupService = new MazeSetupService();
            var generatedMaze = mazeSetupService.InitializeMaze(10, 10);

            MazeCell cellToTest = new MazeCell() { PositionX = 5, PositionY = 5, LastDirectionMovement = (int)Direction.S };

            var resultDirection = moveMazeCellService.NextRandomDirectionWall(cellToTest, generatedMaze,Direction.N,false);
            Assert.IsTrue(resultDirection != Direction.N);

            cellToTest.PositionX = 1;
            cellToTest.PositionY = 1;
            resultDirection = moveMazeCellService.NextRandomDirectionWall(cellToTest, generatedMaze, Direction.S,false);
            Assert.IsTrue(resultDirection == Direction.E);

            cellToTest.PositionX = 8;
            cellToTest.PositionY = 8;
            resultDirection = moveMazeCellService.NextRandomDirectionWall(cellToTest, generatedMaze, Direction.N, false);
            Assert.IsTrue(resultDirection == Direction.W);

            cellToTest.PositionX = 1;
            cellToTest.PositionY = 8;
            resultDirection = moveMazeCellService.NextRandomDirectionWall(cellToTest, generatedMaze, Direction.E, false);
            Assert.IsTrue(resultDirection == Direction.N);

            cellToTest.PositionX = 8;
            cellToTest.PositionY = 1;
            resultDirection = moveMazeCellService.NextRandomDirectionWall(cellToTest, generatedMaze, Direction.W, false);
            Assert.IsTrue(resultDirection == Direction.S);
        }

    }
}
