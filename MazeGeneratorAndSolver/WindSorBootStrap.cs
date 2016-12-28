using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGeneratorAndSolver
{
    class WindSorBootStrap
    {
        public IWindsorContainer CreateBootstrapContainer()
        {
            return new WindsorContainer()
                .Install(
                    FromAssembly.This()
                );
        }
    }
}
