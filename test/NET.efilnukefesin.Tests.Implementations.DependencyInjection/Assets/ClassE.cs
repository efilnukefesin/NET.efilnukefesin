using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets
{
    internal class ClassE
    {
        private IRegularParameterlessService service;
        public IRegularParameterlessService Service { get { return this.service; } }

        public string SomeString { get; set; }
        public int TheNumber { get; set; }
        public ComplexClass Complex { get; set; }
        public HttpMessageHandler Handler { get; set; }

        public ClassE(IRegularParameterlessService Service, string SomeString, int TheNumber, ComplexClass complexClass = null, HttpMessageHandler handler = null)
        {
            this.service = Service;
            this.SomeString = SomeString;
            this.TheNumber = TheNumber;
            this.Complex = complexClass;
            this.Handler = handler;
        }
    }
}
