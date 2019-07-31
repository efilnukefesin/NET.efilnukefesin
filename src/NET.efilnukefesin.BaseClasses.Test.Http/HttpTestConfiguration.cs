using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NET.efilnukefesin.BaseClasses.Test.Http
{
    public class HttpTestConfiguration
    {
        #region Properties

        public string SolutionRelativePath { get; set; } = null;
        public string ApplicationBasePath { get; set; } = null;
        public string SolutionName { get; set; } = null;
        public string Environment { get; set; } = "Development";

        #endregion Properties
    }
}
