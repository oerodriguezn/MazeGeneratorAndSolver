using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGeneratorAndSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var IocContainerCreator = new WindSorBootStrap();
            var container = IocContainerCreator.CreateBootstrapContainer();
            var shell = container.Resolve<IMazeGeneratorApp>();
            shell.ExecuteApplication();
        }
    }
}
