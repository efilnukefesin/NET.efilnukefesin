using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets
{
    internal class ClassB
    {
        private ITestService service;
        public ClassB(ITestService Service)
        {
            this.service = Service;
        }
    }
}
