using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Helpers.WPF
{
    public class LocalAssemblyResourceFilePackUriBuilder : PackUriBuilder
    {
        public LocalAssemblyResourceFilePackUriBuilder(string path)
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
