using NET.efilnukefesin.Contracts.Grid;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Implementations.Grid
{
    public class GridItem<T> : IGridItem<T>
    {
        #region Properties

        private int x;
        private int y;
        private T data;

        #endregion Properties

        #region Construction

        public GridItem(int X, int Y, T Data)
        {
            this.x = X;
            this.y = Y;
            this.data = Data;
        }

        #endregion Construction

        #region Methods

        #endregion Methods

        #region Events

        #endregion Events
    }
}
