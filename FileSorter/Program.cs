using System;
using CommandLine;

namespace FileSorter
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                    var s = new Sorter(o.FilePath, DateTime.Now.AddDays(-1 * o.DaysToKeep)) { Debug = o.Debug, MaxOriginalFileNameLength = o.MaxFileNameLength };
                    if (o.Move)
                        s.MoveSort(o.Override);
                    else
                        s.CopySort(o.Override);
                });
        }
    }
}
