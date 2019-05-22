using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.Base.Assets
{
    [DataContract]
    internal class ABitComplexClass
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Text { get; set; }

        public ABitComplexClass(Guid id, string text)
        {
            this.Id = id;
            this.Text = text;
        }

    }
}
