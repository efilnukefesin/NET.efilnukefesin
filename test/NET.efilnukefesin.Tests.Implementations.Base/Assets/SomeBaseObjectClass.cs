using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.Base.Assets
{
    class SomeBaseObjectClass : BaseObject
    {
        [DataMember]
        public string TestString { get; set; } = "Hello World";

        protected override void dispose()
        {
            
        }
    }
}
