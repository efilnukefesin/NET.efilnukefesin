using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Helpers.WPF
{
    public class SiteOfOriginPackUriBuilder : PackUriBuilder
    {
        #region Properties

        #region AbsolutePath
        public override string AbsolutePath
        {
            get
            {
                return $"{this.Scheme}://siteoforigin:,,,{this.RelativePath}";
            }
        }
        #endregion AbsolutePath

        #endregion Properties

        #region Construction

        public SiteOfOriginPackUriBuilder(string path) : base(path) { }

        #endregion Construction
    }
}
