using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.Grid
{
    public interface IGrid<T>
    {
        #region Properties

        ISize Size { get; }

        #endregion Properties

        #region Indexer

        T this[int x, int y] { get; set; }

        #endregion Indexer

        #region Methods

        void Fill(T value);
        void Clear();
        void CopyFrom(IGrid<T> Source);

        #endregion Methods
    }
}
