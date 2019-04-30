using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace NET.efilnukefesin.Contracts.Xml
{
    public interface ILoadFromXml
    {
        void LoadFromXml(XElement element);
    }
}
