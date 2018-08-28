using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.Grid
{
    public interface IGridItem<T>
    {
        int X { get; set; }
        int Y { get; set; }
        T Data { get; set; }
    }
}
