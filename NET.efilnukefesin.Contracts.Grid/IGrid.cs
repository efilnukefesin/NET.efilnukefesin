using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.Grid
{
    public interface IGrid<T>
    {
        #region Indexer

        T this[int x, int y] { get; set; }

        #endregion Indexer
    }
}
