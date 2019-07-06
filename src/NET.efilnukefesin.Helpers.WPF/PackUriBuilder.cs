using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Helpers.WPF
{
    public abstract class PackUriBuilder
    {
        #region Properties

        private UriKind uriKind;

        #region uriKindTranslator
        protected static Dictionary<UriKind, Func<PackUriBuilder, Uri>> uriKindTranslator = new Dictionary<UriKind, Func<PackUriBuilder, Uri>>
        {
            {UriKind.Absolute, builder => new Uri(builder.ToString(), UriKind.Absolute)},
            {UriKind.Relative, builder => new Uri(builder.RelativePath, UriKind.Relative)}
        };
        #endregion uriKindTranslator

        protected static readonly Dictionary<UriKind, Func<PackUriBuilder, string>> uriToStringTranslator = new Dictionary<UriKind, System.Func<PackUriBuilder, string>>
        {
            [UriKind.Absolute] = builder => builder.AbsolutePath,
            [UriKind.Relative] = builder => builder.RelativePath,
        };

        public string Scheme => "pack";

        public string RelativePath { get; private set; }

        public abstract string AbsolutePath { get; }

        public Uri ToUri() => this;

        #endregion Properties

        #region Construction

        protected PackUriBuilder(string path)
        {
            if (!path.StartsWith("/"))
            {
                throw new ArgumentException(paramName: nameof(path), message: @"Path must start with ""/"".");
            }
            this.RelativePath = path;
            this.uriKind = UriKind.Absolute;
        }

        #endregion Construction

        #region Methods

        #region ToString
        public override string ToString() => uriToStringTranslator[this.uriKind](this);
        #endregion ToString

        #region Relative
        public PackUriBuilder Relative()
        {
            this.uriKind = UriKind.Relative;
            return this;
        }
        #endregion Relative

        #region Absolute
        public PackUriBuilder Absolute()
        {
            this.uriKind = UriKind.Absolute;
            return this;
        }
        #endregion Absolute

        #region LocalAssemblyResourceFile
        public static LocalAssemblyResourceFilePackUriBuilder LocalAssemblyResourceFile(string path)
        {
            return new LocalAssemblyResourceFilePackUriBuilder(path);
        }
        #endregion LocalAssemblyResourceFile

        #region ReferencedAssemblyResourceFile
        public static ReferencedAssemblyResourceFilePackUriBuilder ReferencedAssemblyResourceFile(string path, string assemblyShortName)
        {
            return new ReferencedAssemblyResourceFilePackUriBuilder(path, assemblyShortName);
        }
        #endregion ReferencedAssemblyResourceFile

        #region ContentFile
        public static ContentFilePackUriBuilder ContentFile(string path)
        {
            return new ContentFilePackUriBuilder(path);
        }
        #endregion ContentFile

        #region SiteOfOrigin
        public static SiteOfOriginPackUriBuilder SiteOfOrigin(string path)
        {
            return new SiteOfOriginPackUriBuilder(path);
        }
        #endregion SiteOfOrigin

        #endregion Methods

        #region Operators

        public static implicit operator string(PackUriBuilder builder) => builder.ToString();

        public static implicit operator Uri(PackUriBuilder builder) => uriKindTranslator[builder.uriKind].Invoke(builder);

        #endregion Operators
    }
}
