namespace MazeGeneratorAndSolver.Contracts
{
    public interface IMaze
    {
        IMazeCell[,] Body { get; set; }
    }
}
