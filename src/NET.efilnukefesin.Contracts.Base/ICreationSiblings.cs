using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.Base
{
    public interface ICreationSiblings
    {
        IBaseObject CreationPredecessor { get; set; }
        IBaseObject CreationSucessor { get; set; }
    }
}
