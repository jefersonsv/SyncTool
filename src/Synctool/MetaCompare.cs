using System;
using System.Collections.Generic;
using System.Text;

namespace Synctool
{
    internal class MetaCompare : IEqualityComparer<MetaFileInfo>
    {
        public MetaCompare() { }

        public bool Equals(MetaFileInfo f1, MetaFileInfo f2)
        {
            return (f1.RelativeName == f2.RelativeName &&
                    f1.FileInfo.Name == f2.FileInfo.Name &&
                    f1.FileInfo.Length == f2.FileInfo.Length);
        }

        // Return a hash that reflects the comparison criteria. According to the   
        // rules for IEqualityComparer<T>, if Equals is true, then the hash codes must  
        // also be equal. Because equality as defined here is a simple value equality, not  
        // reference identity, it is possible that two or more objects will produce the same  
        // hash code.  
        public int GetHashCode(MetaFileInfo fi)
        {
            string s = String.Format("{0}{1}", fi.RelativeName, fi.FileInfo.Length);
            return s.GetHashCode();
        }
    }
}
