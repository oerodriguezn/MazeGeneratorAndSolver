using MazeGeneratorAndSolver.Contracts;
using MazeGeneratorAndSolver.Dtos;
using MazeGeneratorAndSolver.Library.InternalInterfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGeneratorAndSolver.Library
{
    public class MazeGenerator: IMazeGenerator
    {
        private readonly IExitWayService _exitWayService;
        private readonly IFakeWayService _fakeWayService;
        private readonly IMazeSetupService _mazeSetupService;
        public MazeGenerator(IExitWayService exitWayService, IFakeWayService fakeWayService, IMazeSetupService mazeSetupService)
        {
            _exitWayService = exitWayService;
            _fakeWayService = fakeWayService;
            _mazeSetupService = mazeSetupService;
        }
        public IMaze GenerateMaze(int HorizontalSize, int VerticalSize)
        {
            IMazeCell[,] generatedMaze = null;
            int emptyWayCellsCount = 0;
            do
            {
                generatedMaze = _mazeSetupService.InitializeMaze(HorizontalSize, VerticalSize);

                _exitWayService.CreateEntryAndExitWayPath(HorizontalSize, VerticalSize, ref generatedMaze);

                _fakeWayService.CreateMazeFakeWays(ref generatedMaze);

                emptyWayCellsCount = (from MazeCell cell in generatedMaze
                                      where cell.Value == (int)MazeCellValueEnum.Empty
                                      select cell).Count();

            } while (emptyWayCellsCount < HorizontalSize && emptyWayCellsCount < VerticalSize);
            return new Maze { Body = generatedMaze };
        }
    }
}
