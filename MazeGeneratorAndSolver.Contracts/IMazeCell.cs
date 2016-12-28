
namespace MazeGeneratorAndSolver.Contracts
{
    public interface IMazeCell
    {
        int PositionX { get; set; }
        int PositionY { get; set; }
        int Value { get; set; }
    }
}
