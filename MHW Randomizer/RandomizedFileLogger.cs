using System;
using System.IO;

namespace MHW_Randomizer
{
    public class RandomizedFileLogger
    {
        private static FileSystemWatcher Watcher;
        public static void SetupWatcher()
        {
            Watcher = new FileSystemWatcher(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder);

            Watcher.NotifyFilter = NotifyFilters.CreationTime
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Size;

            Watcher.Changed += OnChanged;
            Watcher.Created += OnCreated;
            Watcher.Error += OnError;

            Watcher.IncludeSubdirectories = true;
            Watcher.EnableRaisingEvents = true;
        }

        public static void DisposeWatcher()
        {
            Watcher.Dispose();
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            //Make sure its not one of the log text files or a folder (file exists will filter out folders)
            if (e.ChangeType == WatcherChangeTypes.Changed && File.Exists(e.FullPath) && Path.GetExtension(e.FullPath) != ".txt" && Path.GetExtension(e.FullPath) != ".csv")
                //Add it to the file list
                IoC.Randomizer.RandomizedFiles.Add(e.FullPath.Replace(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder, ""));
        }

        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            //Add it to the file list only if its a file and isn't one of the log text files
            if (!File.GetAttributes(e.FullPath).HasFlag(FileAttributes.Directory) && Path.GetExtension(e.FullPath) != ".txt" && Path.GetExtension(e.FullPath) != ".csv")
                IoC.Randomizer.RandomizedFiles.Add(e.FullPath.Replace(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder, ""));
        }

        private static void OnError(object sender, ErrorEventArgs e) =>
            PrintException(e.GetException());

        private static void PrintException(Exception ex)
        {
            if (ex != null)
            {
                MessageWindow message = new MessageWindow("Error Logging File:\n" + ex.Message);

                message.Owner = MainWindow.window;
                message.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
                message.ShowDialog();
            }
        }
    }
}
