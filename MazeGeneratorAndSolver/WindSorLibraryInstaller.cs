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
   public class WindSorLibraryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyNamed("MazeGeneratorAndSolver.Library")
                                  .Where(type => type.Namespace.Contains("MazeGeneratorAndSolver"))
                                  .WithService.DefaultInterfaces());
        }
    }
}
