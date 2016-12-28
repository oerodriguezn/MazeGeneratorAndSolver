using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MazeGeneratorAndSolver.Library.Services;
using MazeGeneratorAndSolver.Dtos;
using System.Linq;

namespace MazeGeneratorAndSolver.Library.UnitTest
{
    [TestClass]
    public class ExitWayServiceTest
    {
        [TestMethod]
        public void CreateEntryAndExitWayPathTestOk()
        {
            var randomGeneratorService = new RandomGeneratorService();
            //moveMazeCellService Should be  mock instead the concrete class. No time to Mock
            var moveMazeCellService = new MoveMazeCellService(randomGeneratorService);
            var mazeSetupService = new MazeSetupService();
            var generatedMaze = mazeSetupService.InitializeMaze(10, 10);

            var exitWayServiceTest = new ExitWayService(moveMazeCellService, randomGeneratorService);

            exitWayServiceTest.CreateEntryAndExitWayPath(10, 10, ref generatedMaze);

            var entryCell = (from MazeCell cell in generatedMaze
                                  where cell.Value == (int)MazeCellValueEnum.Entry
                                  select cell).FirstOrDefault();

            var exitCell = (from MazeCell cell in generatedMaze
                            where cell.Value == (int)MazeCellValueEnum.Exit
                            select cell).FirstOrDefault();

            Assert.IsNotNull(entryCell);
            Assert.IsNotNull(exitCell);
            //Check exit and entry not in the same side
            Assert.IsFalse((entryCell.PositionX == 0 && exitCell.PositionX == entryCell.PositionX) || (entryCell.PositionY == 0 && exitCell.PositionY == entryCell.PositionY));
            Assert.IsFalse((entryCell.PositionX == 9 && exitCell.PositionX == entryCell.PositionX) || (entryCell.PositionY == 9 && exitCell.PositionY == entryCell.PositionY));
        }
    }
}
