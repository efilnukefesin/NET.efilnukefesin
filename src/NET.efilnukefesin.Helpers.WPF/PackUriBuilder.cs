using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Helpers.WPF
{
    public abstract class PackUriBuilder
    {
        private string _path;
        private UriKind _uriKind;

        protected PackUriBuilder(string path)
        {
            if (!path.StartsWith("/")) throw new ArgumentException(paramName: nameof(path), message: "Path must start with \"/\".");
            _path = path;
            _uriKind = UriKind.Absolute;
        }

        public string Scheme => "pack";

        public string RelativePath => _path;

        public abstract string AbsolutePath { get; }

        public static implicit operator string(PackUriBuilder builder) => builder.ToString();

        public static implicit operator Uri(PackUriBuilder builder)
        {
            switch (builder._uriKind)
            {
                case UriKind.Absolute:
                    return new Uri(builder.ToString(), UriKind.Absolute);
                case UriKind.Relative:
                    return new Uri(builder.RelativePath, UriKind.Relative);
            }
            // todo: I know this needs a better message ;-)
            throw new ArgumentOutOfRangeException("Invalid UriKind.");
        }

        public override string ToString()
        {
            switch (_uriKind)
            {
                case UriKind.Absolute:
                    return AbsolutePath;
                case UriKind.Relative:
                    return RelativePath;
            }
            // todo: I know this needs a better message ;-)
            throw new ArgumentOutOfRangeException("Invalid UriKind.");
        }

        public Uri ToUri() => (Uri)this;

        public PackUriBuilder Relative()
        {
            _uriKind = UriKind.Relative;
            return this;
        }

        public PackUriBuilder Absolute()
        {
            _uriKind = UriKind.Absolute;
            return this;
        }

        public static LocalAssemblyResourceFilePackUriBuilder LocalAssemblyResourceFile(string path)
        {
            return new LocalAssemblyResourceFilePackUriBuilder(path);
        }

        public static ReferencedAssemblyResourceFilePackUriBuilder ReferencedAssemblyResourceFile(string path, string assemblyShortName)
        {
            return new ReferencedAssemblyResourceFilePackUriBuilder(path, assemblyShortName);
        }

        public static ContentFilePackUriBuilder ContentFile(string path)
        {
            return new ContentFilePackUriBuilder(path);
        }

        public static SiteOfOriginPackUriBuilder SiteOfOrigin(string path)
        {
            return new SiteOfOriginPackUriBuilder(path);
        }
    }
}
