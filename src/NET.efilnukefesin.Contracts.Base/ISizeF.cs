using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.Base
{
    public interface ISizeF
    {
        #region Properties

        float Width { get; set; }
        float Height { get; set; }
        float AspectRatio { get; }

        #endregion Properties

        #region Methods

        ISizeF Half();

        #endregion Methods
    }
}
