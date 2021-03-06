﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets
{
    internal class TestService : ITestService
    {
        #region Properties
        public string SomeString { get; private set; }
        #endregion Properties

        #region Construction
        public TestService(string SomeString)
        {
            this.SomeString = SomeString;
        }
        #endregion Construction
    }
}
