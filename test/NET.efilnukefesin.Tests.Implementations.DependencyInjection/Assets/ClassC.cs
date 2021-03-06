﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets
{
    internal class ClassC
    {
        private IRegularParameterlessService service;
        public IRegularParameterlessService Service { get { return this.service; } }

        public string SomeString { get; set; }
        public int TheNumber { get; set; }

        public ClassC(IRegularParameterlessService Service, string SomeString, int TheNumber)
        {
            this.service = Service;
            this.SomeString = SomeString;
            this.TheNumber = TheNumber;
        }
    }
}
