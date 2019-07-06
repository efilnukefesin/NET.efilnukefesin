using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Helpers.WPF
{
    public class ContentFilePackUriBuilder : PackUriBuilder
    {
        public ContentFilePackUriBuilder(string path)
    : base(path) { }

        public override string AbsolutePath
        {
            get
            {
                return $"{Scheme}://application:,,,{RelativePath}";
            }
        }
    }
}
