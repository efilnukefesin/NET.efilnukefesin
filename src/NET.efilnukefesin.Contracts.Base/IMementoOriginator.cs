using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.Base
{
    public interface IMementoOriginator
    {
        void Save();
        void Restore();
        bool DiffersFromMemory();
    }
}
