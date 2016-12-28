using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGeneratorAndSolver
{
    public class WindSorConsoleAppInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<MazeGeneratorAndSolver.IMazeGeneratorApp>().ImplementedBy<MazeGeneratorAndSolver.MazeGeneratorConsoleApp>());
            container.Register(Component.For<MazeGeneratorAndSolver.Contracts.IMazeOutputWriter>().ImplementedBy<MazeGeneratorAndSolver.MazeOutputWriter>());
            container.Register(Component.For<MazeGeneratorAndSolver.IDrawMazeService>().ImplementedBy<MazeGeneratorAndSolver.DrawMazeService>());
        }
    }
}
