using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.Base
{
    public interface IId
    {
        //TODO: check out NET Standard 3.0 Release version and use Guid again due to serialization trouble
        Guid Id { get; set; }
        //string Id { get; set; }
    }
}
