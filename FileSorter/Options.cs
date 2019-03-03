using CommandLine;

namespace FileSorter
{
    public class Options
    {
        [Option('d', "debug", Required = false, HelpText = "Shows you verbose messages")]
        public bool Debug { get; set; }

        [Option(Default = false, HelpText = "Move all files instead of copying them into the new directory")]
        public bool Move { get; set; }

        [Option('p', "path", Required = true, HelpText = "Set the path to the directory to be sorted")]
        public string FilePath { get; set; }

        [Option('l', "maxlength", Default = 16, Required = false, HelpText = "Sets the length of the original filename to be prepended on the new filename")]
        public int MaxFileNameLength { get; set; }

        [Option('o', "override", Default = false, HelpText = "Override files that already exists when copying / moving")]
        public bool Override { get; set; }

        [Option('k', "keep", Default = 14, HelpText = "Sets the days a file has to be older than to be copied / moved")]
        public int DaysToKeep { get; set; }
    }
}