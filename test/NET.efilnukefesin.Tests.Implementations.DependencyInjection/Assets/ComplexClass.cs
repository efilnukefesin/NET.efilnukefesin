using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets
{
    internal class ComplexClass
    {
        public string TheText { get; set; }
        public double TheNumber { get; set; }

        public ComplexClass(string theText, double theNumber)
        {
            TheText = theText;
            TheNumber = theNumber;
        }
    }
}
