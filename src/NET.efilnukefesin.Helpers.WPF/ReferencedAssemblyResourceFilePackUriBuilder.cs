using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Helpers.WPF
{
    public class ReferencedAssemblyResourceFilePackUriBuilder : PackUriBuilder
    {
        private string _assemblyShortName;
        private string _version;
        private string _publicKey;

        public ReferencedAssemblyResourceFilePackUriBuilder(string path, string assemblyShortName)
        : base(path)
        {
            _assemblyShortName = assemblyShortName;
        }

        public ReferencedAssemblyResourceFilePackUriBuilder Version(string version)
        {
            _version = version;
            return this;
        }

        public ReferencedAssemblyResourceFilePackUriBuilder PublicKey(string publicKey)
        {
            _publicKey = publicKey;
            return this;
        }

        public override string AbsolutePath
        {
            get
            {
                var version = string.IsNullOrEmpty(_version) ? string.Empty : $";{_version}";
                var publicKey = string.IsNullOrEmpty(_publicKey) ? string.Empty : $";{_publicKey}";
                return $"{Scheme}://application:,,,/{_assemblyShortName}{version}{publicKey};component{RelativePath}";
            }
        }
    }
}
