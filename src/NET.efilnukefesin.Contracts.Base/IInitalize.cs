using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NET.efilnukefesin.Contracts.Base
{
    public interface IInitalize
    {
        #region Properties

        bool IsInitialized { get; set; }

        #endregion Properties

        #region Methods
        Task<bool> Initialize();
        #endregion Methods
    }
}
