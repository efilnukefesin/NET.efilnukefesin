using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Implementations.Base
{
    public class Size : ISize
    {
        #region Properties

        public int Width { get; set; }
        public int Height { get; set; }

        #endregion Properties

        #region Construction

        public Size(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
        }

        #endregion Construction

        #region Methods

        #endregion Methods

        #region Events

        #endregion Events
    }
}
