using System;
using System.IO;
using System.Linq;

namespace MHW_Randomizer
{
    public class GameFiles
    {
        public static bool ChunkFilesLoaded;
        public const string GameFilesPath = "Unrandomized Files (Don't Delete)";
        /// <summary>
        /// Gets a file based on the path from either the chunk files or from the randomizer folder
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        public static byte[] GetFile(string relativePath)
        {
            //Check to see if can load it from the chunk files first
            if (ChunkFilesLoaded)
            {
                Files gameFile;
                if (!ChunkOTF.files.TryGetValue(relativePath.Replace(GameFilesPath, ""), out gameFile))
                    return Array.Empty<byte>();
                
                return gameFile.Extract();
            }
            else
            {
                if (!relativePath.Contains(GameFilesPath))
                    relativePath = GameFilesPath + relativePath;

                if (!File.Exists(relativePath))
                    return Array.Empty<byte>();

                return File.ReadAllBytes(relativePath);
            }
        }

        /// <summary>
        /// Writes the data to the <paramref name="filePath"/>, then logs the files without the file watcher
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="data"></param>
        public static void WriteAndLogFile(string filePath, byte[] data)
        {
            //Write the file to the harddrive
            File.WriteAllBytes(filePath, data);

            //Add it to the file list
            if (ViewModels.Randomizer.RandomizedFiles.Add(filePath.Replace(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder, "")))
            {
                //Add it to the temporary json just incase the randomizer crashes
                using (StreamWriter file = File.AppendText(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\Randomized Files.json"))
                {
                    file.WriteLine("\"" + filePath.Replace(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder, "").Replace(@"\", @"\\") + "\",");
                }
            }
        }

        public static bool ContainsFile(string relativePath)
        {
            if (ChunkFilesLoaded)
            {
                return ChunkOTF.files.ContainsKey(relativePath);
            }
            else
            {
                if (!relativePath.Contains(GameFilesPath))
                    relativePath = GameFilesPath + relativePath;

                return File.Exists(relativePath);
            }
        }

        /// <summary>
        /// Get all files matching a certain extension (don't include the .)
        /// </summary>
        /// <param name="fileExtension"></param>
        /// <param name="searchFolder">The top level folder to start the search from</param>
        /// <returns></returns>
        public static string[] GetAllFilesOfType(string fileExtension, string searchFolder = "", string chunkSearchOverride = "")
        {
            if (ChunkFilesLoaded)
            {
                if (string.IsNullOrEmpty(chunkSearchOverride))
                    return ChunkOTF.files.Values.Where(o => o.Name.Contains("." + fileExtension) && o.EntireName.Contains(searchFolder)).Select(f => f.EntireName).ToArray()!;
                else
                    return ChunkOTF.files.Values.Where(o => o.Name.Contains("." + fileExtension) && o.EntireName.Contains(chunkSearchOverride)).Select(f => f.EntireName).ToArray()!;
            }
            else
                return Directory.EnumerateFiles(GameFilesPath + searchFolder, "*." + fileExtension, SearchOption.AllDirectories).ToArray();
        }
    }
}
