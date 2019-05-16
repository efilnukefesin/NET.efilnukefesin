using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets
{
    internal class ClassA
    {
        private ITestService service;
        public ITestService Service { get { return this.service; } }

        private IRegularParameterlessService regularParameterlessService;
        public ClassA(ITestService Service, IRegularParameterlessService regularParameterlessService)
        {
            this.service = Service;
            this.regularParameterlessService = regularParameterlessService;
        }
    }
}
