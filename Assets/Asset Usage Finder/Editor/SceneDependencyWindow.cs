using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetUsageFinder
{
    internal class SceneDependencyWindow : DependencyWindow
   {
        public SceneDependencyWindow()
        {
            _findMode = FindModeEnum.Scene;
        }
    }
}
