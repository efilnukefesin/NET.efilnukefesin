using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.Base
{
    public interface ISize
    {
        int Width { get; set; }
        int Height { get; set; }
        float AspectRatio { get; }
    }
}
