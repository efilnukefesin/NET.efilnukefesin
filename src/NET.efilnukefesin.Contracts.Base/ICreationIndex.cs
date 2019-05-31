using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.Base
{
    public interface ICreationIndex
    {
        /// <summary>
        /// in which order of IBaseObjects has this been created?
        /// </summary>
        int CreationIndex { get; }
    }
}
