using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MazeGeneratorAndSolver.Contracts;
using Moq;
using MazeGeneratorAndSolver.Dtos;
using System.Linq;

namespace MazeGeneratorAndSolver.UI.UnitTest
{
    [TestClass]
    public class DrawMazeServiceUnitTest
    {
        [TestMethod]
        public void DrawMazeTestNoCorrectWayOK()
        {
            var mazeOutputWriterMock = setupMock();

            DrawMazeService serviceToTest = new DrawMazeService(mazeOutputWriterMock.Object);
            var generatedMaze = MazeHelper.InitializeMaze(10, 10);

            serviceToTest.DrawMaze(generatedMaze, false);

            mazeOutputWriterMock.Verify(m => m.WriteNewLine(), Times.Exactly(10));

            var borderWalls = (from MazeCell cell in generatedMaze.Body
                         where cell.Value == (int)MazeCellValueEnum.WallBorder
                                   select cell).Count();

            mazeOutputWriterMock.Verify(m => m.WriteWallBorder(), Times.Exactly(borderWalls));
            mazeOutputWriterMock.Verify(m => m.WriteEntry(), Times.Once);
            mazeOutputWriterMock.Verify(m => m.WriteExit(), Times.Once);
            mazeOutputWriterMock.Verify(m => m.WriteCorrectWay(), Times.Never);
        }

        [TestMethod]
        public void DrawMazeTestWithCorrectWayOK()
        {
            var mazeOutputWriterMock = setupMock();

            DrawMazeService serviceToTest = new DrawMazeService(mazeOutputWriterMock.Object);
            var generatedMaze = MazeHelper.InitializeMaze(10, 10);

            serviceToTest.DrawMaze(generatedMaze, true);

            mazeOutputWriterMock.Verify(m => m.WriteNewLine(), Times.Exactly(10));

            var borderWalls = (from MazeCell cell in generatedMaze.Body
                               where cell.Value == (int)MazeCellValueEnum.WallBorder
                               select cell).Count();

            mazeOutputWriterMock.Verify(m => m.WriteWallBorder(), Times.Exactly(borderWalls));
            mazeOutputWriterMock.Verify(m => m.WriteEntry(), Times.Once);
            mazeOutputWriterMock.Verify(m => m.WriteExit(), Times.Once);
            mazeOutputWriterMock.Verify(m => m.WriteCorrectWay(), Times.Once);
        }

        private static Mock<IMazeOutputWriter> setupMock()
        {
            var mazeOutputWriterMock = new Mock<IMazeOutputWriter>();
            mazeOutputWriterMock.Setup(m => m.WriteNewLine());
            mazeOutputWriterMock.Setup(m => m.WriteWall());
            mazeOutputWriterMock.Setup(m => m.WriteWallBorder());
            mazeOutputWriterMock.Setup(m => m.WriteSpace());
            mazeOutputWriterMock.Setup(m => m.WriteEntry());
            mazeOutputWriterMock.Setup(m => m.WriteExit());
            mazeOutputWriterMock.Setup(m => m.WriteCorrectWay());
            return mazeOutputWriterMock;
        }
    }
}
