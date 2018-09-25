using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Synctool
{
    public static class PathHelper
    {
        public static (string, string) SplitPathFromPattern(string pathFullyQualified)
        {
            if (Path.IsPathFullyQualified(pathFullyQualified))
            {
                var lastSegment = Path.GetFileName(Path.GetFileName(pathFullyQualified));
                if (Path.GetInvalidFileNameChars().Any(s => lastSegment.Contains(s)))
                {
                    return (lastSegment, pathFullyQualified.Substring(0, pathFullyQualified.Length - lastSegment.Length));
                }
                else
                {
                    return (null, pathFullyQualified);
                }
            }
            else
            {
                return (pathFullyQualified, null);
            }
        }
    }
}
