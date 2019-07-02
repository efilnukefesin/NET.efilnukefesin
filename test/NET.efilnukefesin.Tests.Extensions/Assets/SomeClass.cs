using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Extensions.Assets
{
    internal class SomeClass
    {
        public int SomeNumber { get; set; }

        public SomeClass()
        {
            Random random = new Random();
            this.SomeNumber = random.Next(255);
        }
    }
}
