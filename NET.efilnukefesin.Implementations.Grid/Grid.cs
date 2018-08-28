﻿using NET.efilnukefesin.Contracts.Base;
using NET.efilnukefesin.Contracts.Grid;
using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Implementations.Grid
{
    public class Grid<T> : IGrid<T>
    {
        #region Properties

        private ISize size;

        #endregion Properties

        #region Construction

        public Grid(int Width, int Height)
        {
            this.size = new Size(Width, Height);
        }

        public Grid(ISize size)
        {
            this.size = size;
        }

        #endregion Construction

        #region Methods

        #endregion Methods

        #region Events

        #endregion Events
    }
}
