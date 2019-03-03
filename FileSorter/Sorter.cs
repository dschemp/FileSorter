using System;
using System.IO;
using System.Linq;

namespace FileSorter
{
    public class Sorter
    {
        #region Variables

        public bool Debug { get; set; }
        public int MaxOriginalFileNameLength { get; set; } = 16;

        private readonly string[] _exemptExtensions = { "exe", "dll" };

        private readonly DirectoryInfo _dir;
        private readonly DateTime _olderThan;

        #endregion

        #region Ctors

        public Sorter(string dirPath) : this(new DirectoryInfo(dirPath), DateTime.Now.AddMonths(-1)) { }

        public Sorter(string dirPath, DateTime dateTime) : this(new DirectoryInfo(dirPath), dateTime) { }

        public Sorter(DirectoryInfo dir) : this(dir, DateTime.Now.AddMonths(-1)) { }

        public Sorter(DirectoryInfo dir, DateTime olderThan)
        {
            _dir = dir;
            _olderThan = olderThan;
        }

        #endregion

        #region Wrapper-Methods

        public void CopySort(bool @override = true, Func<FileInfo, string> fileNameMaker = null)
        {
            Sort(File.Copy, @override, fileNameMaker);
        }

        public void MoveSort(bool @override = true, Func<FileInfo, string> fileNameMaker = null)
        {
            Sort(File.Move, @override, fileNameMaker);
        }

        #endregion

        private void Sort(Action<string, string> copyMoveFunc, bool @override = true, Func<FileInfo, string> fileNameMaker = null)
        {
            var files = _dir.GetFiles().Where(x => _exemptExtensions.All(ex => !Path.GetExtension(x.Name).Contains(ex))).ToList(); // All files that are not exempt from the copy process
            foreach (var file in files)
            {
                // Check if valid for copy / move
                if (file.LastAccessTime > _olderThan)
                    continue;

                var name = "";
                // Create new filename
                if (fileNameMaker == null)
                {
                    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.Name);
                    var substring = fileNameWithoutExtension.Substring(0, Math.Min(fileNameWithoutExtension.Length, MaxOriginalFileNameLength));
                    name = substring + "_" + file.CreationTime.ToString("yyyyMMddHHmmss") + file.Extension;
                }
                var newName = (fileNameMaker != null) ? fileNameMaker.Invoke(file) : name;

                if (Debug)
                    Console.WriteLine(file.Name + " -> " + newName);

                // Directory management
                var path = file.DirectoryName + "/" + file.Extension;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                // Copy
                var newFilePath = Path.Combine(path, newName);

                if (@override)
                {
                    if (Debug)
                        Console.WriteLine("Deleting: " + newFilePath);

                    File.Delete(newFilePath);
                }

                if (File.Exists(newFilePath))
                    continue;

                if (Debug)
                    Console.WriteLine(file.FullName + " -> " + newFilePath);

                copyMoveFunc.Invoke(file.FullName, newFilePath);
            }
        }
    }
}