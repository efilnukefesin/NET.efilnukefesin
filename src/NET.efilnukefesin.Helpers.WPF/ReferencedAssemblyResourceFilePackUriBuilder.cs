using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Helpers.WPF
{
    public class ReferencedAssemblyResourceFilePackUriBuilder : PackUriBuilder
    {
        #region Properties

        private string assemblyShortName;
        private string version;
        private string publicKey;

        #region AbsolutePath
        public override string AbsolutePath
        {
            get
            {
                string version = string.IsNullOrEmpty(this.version) ? string.Empty : $";{this.version}";
                string publicKey = string.IsNullOrEmpty(this.publicKey) ? string.Empty : $";{this.publicKey}";
                return $"{this.Scheme}://application:,,,/{this.assemblyShortName}{version}{publicKey};component{this.RelativePath}";
            }
        }
        #endregion AbsolutePath

        #endregion Properties

        #region Construction

        public ReferencedAssemblyResourceFilePackUriBuilder(string path, string assemblyShortName) : base(path)
        {
            this.assemblyShortName = assemblyShortName;
        }

        #endregion Construction

        #region Methods

        #region Version
        public ReferencedAssemblyResourceFilePackUriBuilder Version(string version)
        {
            this.version = version;
            return this;
        }
        #endregion Version

        #region PublicKey
        public ReferencedAssemblyResourceFilePackUriBuilder PublicKey(string publicKey)
        {
            this.publicKey = publicKey;
            return this;
        }
        #endregion PublicKey

        #endregion Methods
    }
}
