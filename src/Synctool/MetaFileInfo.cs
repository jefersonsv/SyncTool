using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Synctool
{
    internal class MetaFileInfo : IEquatable<MetaFileInfo>
    {
        public readonly string RelativeName;
        public MetaFileInfo(string rootPath, FileInfo fi)
        {
            this.RelativeName = fi.FullName.Substring(rootPath.Length);
            this.FileInfo = fi;
        }

        public FileInfo FileInfo { get; set; }

        public bool Equals(MetaFileInfo other)
        {
            return (this.RelativeName == other.RelativeName
                 && this.FileInfo.Length == other.FileInfo.Length);
        }

    }
}
