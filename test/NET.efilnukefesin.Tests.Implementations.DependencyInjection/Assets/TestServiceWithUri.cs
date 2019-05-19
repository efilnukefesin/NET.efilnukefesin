using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets
{
    internal class TestServiceWithUri : ITestService
    {
        #region Properties
        public string SomeString { get; private set; }
        public Uri SomeUri { get; private set; }
        #endregion Properties

        #region Construction
        public TestServiceWithUri(Uri SomeUri, string SomeString)
        {
            this.SomeUri = SomeUri;
            this.SomeString = SomeString;
        }
        #endregion Construction
    }
}
