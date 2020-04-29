using ApexSQLPatch.Core;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ApexSQLPatch
{
    class Program
    {
        public enum ApexProduct
        {
            Unknown,
            ApexLog,
            ApexRecover,
        }

        static void Main(string[] args)
        {
            ConsoleGui.WriteGui();

            var apexProduct = ApexProduct.Unknown;
            var apexPath = string.Empty;

            ConsoleGui.WriteLine("1. ApexSQL Log");
            ConsoleGui.WriteLine("2. ApexSQL Recover");

            ConsoleGui.WriteLine();
            ConsoleGui.Write("请输入产品序号：");

            var keyInfo = Console.ReadKey();

            ConsoleGui.WriteLine();
            ConsoleGui.Write("请输入产品路径：");

            switch (keyInfo.KeyChar)
            {
                case '1':
                    {
                        apexProduct = ApexProduct.ApexLog;
                        apexPath = Console.ReadLine();
                    }
                    break;
                case '2':
                    {
                        apexProduct = ApexProduct.ApexRecover;
                        apexPath = Console.ReadLine();
                    }
                    break;
            }

            ConsoleGui.WriteLine();

            if (apexProduct != ApexProduct.Unknown)
            {
                try
                {
                    var dirInfo = new DirectoryInfo(apexPath);
                    if (dirInfo.Exists)
                    {
                        if (apexProduct == ApexProduct.ApexLog || apexProduct == ApexProduct.ApexRecover)
                        {
                            var files = dirInfo.GetFiles("ApexSQL.Engine.Communication.dll", SearchOption.TopDirectoryOnly);
                            if (files != null && files.Any())
                            {
                                var filePath = files.First().FullName;
                                var filePathBak = $"{filePath}.bak";

                                if (File.Exists(filePathBak))
                                    File.Delete(filePathBak);

                                files.First().MoveTo($"{filePath}.bak");
                                ReleasePatchFile(filePath);

                                ConsoleGui.Write("成功");
                            }
                        }
                    }
                }
                catch
                {
                    return;
                }
            }

            Console.ReadKey();
        }

        private static void ReleasePatchFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                byte[] buffer = null;

                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ApexSQLPatch.Res.ApexSQL.Engine.Communication.dll"))
                {
                    buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                }

                using (var fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fs.Write(buffer, 0, buffer.Length);
                }
            }
        }
    }
}
