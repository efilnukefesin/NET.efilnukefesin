using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Helpers.WPF
{
    public class SiteOfOriginPackUriBuilder : PackUriBuilder
    {
        public SiteOfOriginPackUriBuilder(string path)
    : base(path) { }

        public override string AbsolutePath
        {
            get
            {
                return $"{Scheme}://siteoforigin:,,,{RelativePath}";
            }
        }
    }
}
