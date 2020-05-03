using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetUsageFinder
{
    internal class FileDependencyWindow : DependencyWindow
    {
        public FileDependencyWindow()
        {
            _findMode = FindModeEnum.File;
        }
    }
}
