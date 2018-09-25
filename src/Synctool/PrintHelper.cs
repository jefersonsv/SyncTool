using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Text;
using Console = Colorful.Console;
using Colorful;

namespace Synctool
{
    public static class PrintHelper
    {
        public static bool Silent;

        public static void Print(string msg)
        {
            if (!Silent)
                Console.Write(msg);
        }

        public static void Print(StyledString msg, Color color)
        {
            if (!Silent)
                Console.WriteLine(msg, color);
        }

        public static void Print(string msg, Color color)
        {
            if (!Silent)
                Console.WriteFormatted(msg, color);
        }

        public static void PrintLine(string msg)
        {
            if (!Silent)
                Console.WriteLine(msg);
        }

        public static void PrintLine(string msg, Color color)
        {
            if (!Silent)
                Console.WriteLineFormatted(msg, color);
        }
    }
}
