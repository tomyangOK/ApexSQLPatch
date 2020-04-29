using System;

namespace ApexSQLPatch.Core
{
    public class ConsoleGui
    {
        public static void WriteGui()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("  +-------------------------------------------------+");
            Console.WriteLine("  |                                                 |");
            Console.WriteLine("  |          ApexSQL DBA 2019.02.1245 Patch         |");
            Console.WriteLine("  |                                                 |");
            Console.WriteLine("  |         支持ApexSQL Log、ApexSQL Recover        |");
            Console.WriteLine("  |                                                 |");
            Console.WriteLine("  +-------------------------------------------------+");
            Console.WriteLine();
        }

        public static void WriteLine(string text = null)
        {
            Console.WriteLine(string.IsNullOrEmpty(text) ? null : $"  {text}");
        }

        public static void Write(string text = null)
        {
            Console.Write(string.IsNullOrEmpty(text) ? null : $"  {text}");
        }
    }
}
