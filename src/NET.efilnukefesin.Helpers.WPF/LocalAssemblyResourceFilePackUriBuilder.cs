using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Helpers.WPF
{
    public class LocalAssemblyResourceFilePackUriBuilder : PackUriBuilder
    {
        #region Properties

        #region AbsolutePath
        public override string AbsolutePath
        {
            get
            {
                return $"{this.Scheme}://application:,,,{this.RelativePath}";
            }
        }
        #endregion AbsolutePath

        #endregion Properties

        #region Construction

        public LocalAssemblyResourceFilePackUriBuilder(string path) : base(path) { }

        #endregion Construction
    }
}
