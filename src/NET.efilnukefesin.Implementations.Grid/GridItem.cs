using NET.efilnukefesin.Contracts.Grid;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("NET.efilnukefesin.Tests.Implementations.Grid")]
namespace NET.efilnukefesin.Implementations.Grid
{
    internal class GridItem<T> : IGridItem<T>
    {
        #region Properties

        public int X { get; set; }
        public int Y { get; set; }
        public T Data { get; set; }

        #endregion Properties

        #region Construction

        public GridItem(int X, int Y, T Data)
        {
            this.X = X;
            this.Y = Y;
            this.Data = Data;
        }

        #endregion Construction

        #region Methods

        #endregion Methods

        #region Events

        #endregion Events
    }
}
