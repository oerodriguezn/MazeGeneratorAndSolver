using MazeGeneratorAndSolver.Contracts;
using MazeGeneratorAndSolver.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGeneratorAndSolver
{
    public interface IMazeGeneratorApp
    {
        void ExecuteApplication();
    }
    public class MazeGeneratorConsoleApp : IMazeGeneratorApp
    {
        private readonly IMazeGenerator _iMazeGenerator;
        private  readonly IDrawMazeService _iDrawMazeService;
        public MazeGeneratorConsoleApp(IMazeGenerator mazeGenerator, IDrawMazeService drawMazeService)
        {
            _iMazeGenerator = mazeGenerator;
            _iDrawMazeService = drawMazeService;
        }
        public void ExecuteApplication()
        {
            int verticalLenght = 0;
            int horizontalLenght = 0;

            ShowMazeAppHeaderAndGetInput(ref verticalLenght, ref horizontalLenght);
            string menuOption = "G";
            IMaze generatedMaze = null;
            do
            {
                switch (menuOption)
                {
                    case "G":
                        generatedMaze = _iMazeGenerator.GenerateMaze(horizontalLenght, verticalLenght);
                        _iDrawMazeService.DrawMaze( generatedMaze, false);
                        break;
                    case "S":
                        _iDrawMazeService.DrawMaze( generatedMaze, true);
                        break;
                }

                Console.WriteLine("Press S to show the correct way of the maze");
                Console.WriteLine("Press G to generate a new Maze with same dimentions.");
                Console.WriteLine("Press any other key to exit.");
                menuOption = Console.ReadLine().ToUpper();
            } while (menuOption == "G" || menuOption == "S");
        }

        

        private void ShowMazeAppHeaderAndGetInput(ref int verticalLenght, ref int horizontalLenght)
        {
            Console.WriteLine("Maze Generator Console App Test program");
            do
            {
                Console.WriteLine("Please enter horizontal maze lenght:");
                string horizontalLenghtInput = Console.ReadLine();
                horizontalLenght = isMazeLenghtValid(horizontalLenghtInput);
            } while (horizontalLenght == 0);

            do
            {
                Console.WriteLine("Please enter Vertical maze lenght:");
                string verticalLenghtInput = Console.ReadLine();
                verticalLenght = isMazeLenghtValid(verticalLenghtInput);
            } while (verticalLenght == 0);
        }

        private int isMazeLenghtValid(string mazeLenghtInput)
        {
            int number = 0;
            if (int.TryParse(mazeLenghtInput,out number))
            {
                if (!(number > 10 && number <= 10000))
                {
                    Console.WriteLine("The Maze Lenght must be greater than 10 and lower than 10.000");
                    number = 0;
                }
                   
            }
            else{
                Console.WriteLine("The input should be a interger number");
            }
                return number;
        }

        
    }
}
