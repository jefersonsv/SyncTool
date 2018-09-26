using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Colorful;
using DocoptNet;
using Microsoft.Extensions.FileSystemGlobbing;

namespace Synctool
{
    static class Program
    {
        /// <summary>
        /// <see cref="http://try.docopt.org"/>
        /// <see cref="https://stackoverflow.com/questions/16863371/why-doesnt-my-docopt-option-have-its-default-value"/>
        /// <see cref="https://dmerej.info/blog/post/docopt-v-argparse/"/>
        /// <see cref="https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/how-to-compare-the-contents-of-two-folders-linq"/>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fontArr = ArrayHelper.GetByteArray(assembly.GetManifestResourceStream($"Synctool.Contessa.flf"));
            var usageArr = ArrayHelper.GetByteArray(assembly.GetManifestResourceStream($"Synctool.Usage.txt"));

            try
            {
                var arguments = new Docopt().Apply(Encoding.Default.GetString(usageArr), args, version: "Synctool", exit: false);
                PrintHelper.Silent = arguments["--silent"].IsTrue;

                FigletFont font = FigletFont.Load(fontArr);
                Figlet figlet = new Figlet(font);
                PrintHelper.Print(figlet.ToAscii("Synctool"), Color.Blue);
                PrintHelper.PrintLine("Command line tool that sync files and folders recursively" + Environment.NewLine, Color.White);

                try
                {
                    Run(args, arguments);
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    PrintHelper.Silent = false;
                    PrintHelper.Print(ex.ToString(), Color.Red);
                    Environment.Exit(2);
                }
            }
            catch
            {
                PrintHelper.Silent = false;
                PrintHelper.PrintLine(Encoding.Default.GetString(usageArr));
                PrintHelper.PrintLine(string.Empty);

                PrintHelper.PrintLine("Install/Uninstall tool:");
                PrintHelper.PrintLine($@"    > To install tool from system");
                PrintHelper.PrintLine($@"    dotnet tool install -g synctool", Color.Green);
                PrintHelper.PrintLine(string.Empty);
                PrintHelper.PrintLine($@"    > To uninstall tool from system");
                PrintHelper.PrintLine($@"    dotnet tool uninstall -g synctool", Color.Green);

                Environment.Exit(1);
            }
        }

        public static void Run(string[] afterArgs, IDictionary<string, ValueObject> param)
        {
            if (param["sync"].IsTrue)
            {
                PrintHelper.Print(string.Empty);

                var src = Path.GetFullPath(param["<source-folder>"].ToString()) + @"\";
                var dest = Path.GetFullPath(param["<destiny-folder>"].ToString()) + @"\";

                System.IO.DirectoryInfo srcDir = new System.IO.DirectoryInfo(src);
                System.IO.DirectoryInfo dir2 = new System.IO.DirectoryInfo(dest);

                // Take a snapshot of the file system.  
                IEnumerable<System.IO.FileInfo> list1 = srcDir.GetFiles("*.*", System.IO.SearchOption.AllDirectories);
                IEnumerable<System.IO.FileInfo> list2 = dir2.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

                MetaCompare meta = new MetaCompare();
                var l1 = list1.Select(s => new MetaFileInfo(srcDir.FullName, s));
                var l2 = list2.Select(s => new MetaFileInfo(dir2.FullName, s));

                // Copy files
                var copyFiles = (from file in l1
                                 select file).Except(l2, meta);

                foreach (var v in copyFiles)
                {
                    var to = new FileInfo(v.FileInfo.FullName.Replace(srcDir.FullName, dir2.FullName));
                    if (!to.Directory.Exists)
                        Directory.CreateDirectory(to.DirectoryName);

                    PrintHelper.Print("File copied: ", Color.White);
                    PrintHelper.PrintLine(to.FullName, Color.Gray);

                    File.Copy(v.FileInfo.FullName, to.FullName, true);
                }

                // Delete files
                var deleteList = (from file in l2
                                  select file).Except(l1, meta);

                foreach (var v in deleteList)
                {
                    PrintHelper.Print("File deleted: ", Color.White);
                    PrintHelper.PrintLine(v.FileInfo.FullName, Color.Gray);

                    File.Delete(v.FileInfo.FullName);
                }
            }
            else if (param["version"].IsTrue)
            {
                // https://stackoverflow.com/questions/909555/how-can-i-get-the-assembly-file-version
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

                System.Console.Write(fvi.FileVersion);
            }
        }
    }
}
