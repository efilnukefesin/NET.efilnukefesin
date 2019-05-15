using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Helpers.Assets
{
    internal class TestClass
    {
        private string someString;
        public string SomeString
        {
            get { return this.someString; }
        }

        public TestClass(string SomeString)
        {
            this.someString = SomeString;
        }
    }
}
