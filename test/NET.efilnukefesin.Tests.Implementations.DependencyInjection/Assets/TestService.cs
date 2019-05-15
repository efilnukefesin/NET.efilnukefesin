using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets
{
    internal class TestService : ITestService
    {
        public string SomeString { get; private set; }

        public TestService(string SomeString)
        {
            this.SomeString = SomeString;
        }

        
    }
}
