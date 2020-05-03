using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetUsageFinder
{
    internal class StageDependencyWindow : DependencyWindow
    {
        public StageDependencyWindow()
        {
            _findMode = FindModeEnum.Stage;
        }
    }
}
