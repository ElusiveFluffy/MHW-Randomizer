using System;
using System.IO;
using System.Windows.Input;
using System.Windows.Forms;
using WK.Libraries.BetterFolderBrowserNS;
using System.Windows.Interop;
using System.Text;
using Troschuetz.Random;
using Troschuetz.Random.Generators;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MHW_Randomizer
{
    public class RandomizerViewModel : BaseViewModel
    {
        public List<string> MissingMIBFiles = new List<string>();

        public ICommand OpenCommand { get; set; }
        public ICommand RandomizeCommand { get; set; }
        public ICommand CreditsCommand { get; set; }

        public ICommand RemoveFilesCommand { get; set; }

        public string MonsterIcon { get; set; }

        public BetterFolderBrowser FolderBrowser = new BetterFolderBrowser();
        public bool OpenFolderIsEnabled { get; set; } = true;
        public bool SaveIsEnabled { get; set; }
        public string RandomizeRootFolder { get; set; }
        public bool Randomizing { get; set; }

        public HashSet<string> RandomizedFiles = new HashSet<string>();

        public uint Seed;


        public RandomizerViewModel()
        {
            OpenCommand = new RelayCommand(OpenFolder);
            RandomizeCommand = new RelayCommand(Randomize);
            CreditsCommand = new RelayCommand(Credits);

            RemoveFilesCommand = new RelayCommand(RemoveOldRandomizedFiles);

            FolderBrowser.RootFolder = AppDomain.CurrentDomain.BaseDirectory;

            NR3Generator r = new NR3Generator(TMath.Seed());
            string[] images = {
                @"\Monster Icons\MHWI_Acidic_Glavenus_Icon.png",
                @"\Monster Icons\MHWI_Anteka_Icon.png",
                @"\Monster Icons\MHWI_Banbaro_Icon.png",
                @"\Monster Icons\MHWI_Barioth_Icon.png",
                @"\Monster Icons\MHWI_Beotodus_Icon.png",
                @"\Monster Icons\MHWI_Blackveil_Vaal_Hazak_Icon.png",
                @"\Monster Icons\MHWI_Brachydios_Icon.png",
                @"\Monster Icons\MHWI_Brute_Tigrex_Icon.png",
                @"\Monster Icons\MHWI_Coral_Pukei-Pukei_Icon.png",
                @"\Monster Icons\MHWI_Ebony_Odogaron_Icon.png",
                @"\Monster Icons\MHWI_Fulgur_Anjanath_Icon.png",
                @"\Monster Icons\MHWI_Glavenus_Icon.png",
                @"\Monster Icons\MHWI_Namielle_Icon.png",
                @"\Monster Icons\MHWI_Nargacuga_Icon.png",
                @"\Monster Icons\MHWI_Nightshade_Paolumu_Icon.png",
                @"\Monster Icons\MHWI_Popo_Icon.png",
                @"\Monster Icons\MHWI_Rajang_Icon.png",
                @"\Monster Icons\MHWI_Ruiner_Nergigante_Icon.png",
                @"\Monster Icons\MHWI_Savage_Deviljho_Icon.png",
                @"\Monster Icons\MHWI_Seething_Bazelgeuse_Icon.png",
                @"\Monster Icons\MHWI_Shara_Ishvalda_Icon.png",
                @"\Monster Icons\MHWI_Shrieking_Legiana_Icon.png",
                @"\Monster Icons\MHWI_Tigrex_Icon.png",
                @"\Monster Icons\MHWI_Velkhana_Icon.png",
                @"\Monster Icons\MHWI_Viper_Tobi-Kadachi_Icon.png",
                @"\Monster Icons\MHWI_Wulg_Icon.png",
                @"\Monster Icons\MHWI_Yian_Garuga_Icon.png",
                @"\Monster Icons\MHWI_Zinogre_Icon..png",
                @"\Monster Icons\MHW_Ancient_Leshen_Icon.png",
                @"\Monster Icons\MHW_Anjanath_Icon.png",
                @"\Monster Icons\MHW_Apceros_Icon.png",
                @"\Monster Icons\MHW_Aptonoth_Icon.png",
                @"\Monster Icons\MHW_Azure_Rathalos_Icon.png",
                @"\Monster Icons\MHW_Barnos_Icon.png",
                @"\Monster Icons\MHW_Barroth_Icon.png",
                @"\Monster Icons\MHW_Bazelgeuse_Icon.png",
                @"\Monster Icons\MHW_Behemoth_Icon.png",
                @"\Monster Icons\MHW_Black_Diablos_Icon.png",
                @"\Monster Icons\MHW_Deviljho_Icon.png",
                @"\Monster Icons\MHW_Diablos_Icon.png",
                @"\Monster Icons\MHW_Dodogama_Icon.png",
                @"\Monster Icons\MHW_Gajalaka_Icon.png",
                @"\Monster Icons\MHW_Gajau_Icon.png",
                @"\Monster Icons\MHW_Gastodon_Icon.png",
                @"\Monster Icons\MHW_Girros_Icon.png",
                @"\Monster Icons\MHW_Great_Girros_Icon.png",
                @"\Monster Icons\MHW_Great_Jagras_Icon.png",
                @"\Monster Icons\MHW_Hornetaur_Icon.png",
                @"\Monster Icons\MHW_Jagras_Icon.png",
                @"\Monster Icons\MHW_Jyuratodus_Icon.png",
                @"\Monster Icons\MHW_Kelbi_Icon.png",
                @"\Monster Icons\MHW_Kestodon_Icon.png",
                @"\Monster Icons\MHW_Kirin_Icon.png",
                @"\Monster Icons\MHW_Kulu-Ya-Ku_Icon.png",
                @"\Monster Icons\MHW_Kulve_Taroth_Icon.png",
                @"\Monster Icons\MHW_Kushala_Daora_Icon.png",
                @"\Monster Icons\MHW_Lavasioth_Icon.png",
                @"\Monster Icons\MHW_Legiana_Icon.png",
                @"\Monster Icons\MHW_Leshen_Icon.png",
                @"\Monster Icons\MHW_Lunastra_Icon.png",
                @"\Monster Icons\MHW_Mernos_Icon.png",
                @"\Monster Icons\MHW_Mosswine_Icon.png",
                @"\Monster Icons\MHW_Nergigante_Icon.png",
                @"\Monster Icons\MHW_Noios_Icon.png",
                @"\Monster Icons\MHW_Odogaron_Icon.png",
                @"\Monster Icons\MHW_Paolumu_Icon.png",
                @"\Monster Icons\MHW_Pink_Rathian_Icon.png",
                @"\Monster Icons\MHW_Pukei-Pukei_Icon.png",
                @"\Monster Icons\MHW_Radobaan_Icon.png",
                @"\Monster Icons\MHW_Raphinos_Icon.png",
                @"\Monster Icons\MHW_Rathalos_Icon.png",
                @"\Monster Icons\MHW_Rathian_Icon.png",
                @"\Monster Icons\MHW_Shamos_Icon.png",
                @"\Monster Icons\MHW_Teostra_Icon.png",
                @"\Monster Icons\MHW_Tobi-Kadachi_Icon.png",
                @"\Monster Icons\MHW_Tzitzi-Ya-Ku_Icon.png",
                @"\Monster Icons\MHW_Uragaan_Icon.png",
                @"\Monster Icons\MHW_Vaal_Hazak_Icon.png",
                @"\Monster Icons\MHW_Vespoid_Icon.png",
                @"\Monster Icons\MHW_Xeno'jiiva_Icon.png",
                @"\Monster Icons\MHW_Zorah_Magdaros_Icon.png"
            };
            MonsterIcon = images[r.Next(images.Length)];

            StartUp();
        }

        public async void StartUp()
        {
            if (!File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}\\oo2core_8_win64.dll"))
            {
                OpenFolderIsEnabled = false;
                await Task.Delay(1);
                //Message Window
                MessageWindow message = new MessageWindow("ERROR: oo2core_8_win64.dll is missing. Can't decompress chunk file.");
                message.Owner = MainWindow.window;
                message.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
                message.ShowDialog();

                return;
            }

            if (string.IsNullOrEmpty(IoC.Settings.ChunkFolderPath))
                return;

            if (!Directory.Exists(IoC.Settings.ChunkFolderPath))
            {
                await Task.Delay(1);
                //Message Window
                MessageWindow message = new MessageWindow("Last used chunk folder doesn't exist");

                message.Owner = MainWindow.window;
                message.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
                message.ShowDialog();
                return;
            }

            if (Directory.GetFiles(IoC.Settings.ChunkFolderPath, "chunk*.bin", SearchOption.TopDirectoryOnly).ToArray().Length >= 10)
            {
                Analyze(IoC.Settings.ChunkFolderPath);
                SaveIsEnabled = true;
            }
            else
            {
                await Task.Delay(1);
                //Message Window
                MessageWindow message = new MessageWindow("Some chunk files are missing from last used chunk folder");

                message.Owner = MainWindow.window;
                message.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
                message.ShowDialog();
            }
            FolderBrowser.RootFolder = IoC.Settings.ChunkFolderPath;
        }

        public void Credits()
        {
            //Message Window
            MessageWindow message = new MessageWindow("Credits\nThank you to:\nAradi147 for the source code for the quest editor,\nSynthlight for the 010 templates which helped me serialize and deserialize a lot of the files, and also for their editor to help check values," +
                "\nFusionR for the source code for cirilla to be able to edit the GMD text files,\nMHVuze for the source code of WorldChunkTool without it I wouldn't have been able to have it read the chunk files for a more streamlined process,\nFandirus for the fix for Raging Brachydios,\nand everyone else who worked on these programs,\nand the Monster Hunter modding discord and MHW modding wiki." +
                "\n\nPackages used:\nBetter Folder Browser by WillyKimura\nFody Property Changed by Simon Cropp\nNewtonsoft.Json by James Newton-King\nNinject by Ninject Project Contributors\nTroschuetz.Random by Stefan Troschütz and Alessio Parma\nCRC32.NET by Force")
            {
                Width = 500,
                Height = 370,

                Owner = MainWindow.window,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner
            };
            message.ShowDialog();
        }

        public void OpenFolder()
        {
            if (!string.IsNullOrWhiteSpace(IoC.Settings.ChunkFolderPath) && Directory.Exists(IoC.Settings.ChunkFolderPath))
                FolderBrowser.RootFolder = IoC.Settings.ChunkFolderPath;
            else
                FolderBrowser.RootFolder = AppDomain.CurrentDomain.BaseDirectory;

            FolderBrowser.Title = "Select Chunks Folder";
            FolderBrowser.Multiselect = false;

            NativeWindow win32Parent = new NativeWindow();
            win32Parent.AssignHandle(new WindowInteropHelper(MainWindow.window).Handle);
            DialogResult result = FolderBrowser.ShowDialog(win32Parent);

            if (!(result == DialogResult.OK && Directory.GetFiles(FolderBrowser.SelectedPath, "chunk*.bin", SearchOption.TopDirectoryOnly).ToArray().Length >= 10))
            {
                if (!string.IsNullOrWhiteSpace(FolderBrowser.SelectedPath) && result == DialogResult.OK)
                {
                    //Message Window
                    MessageWindow message = new MessageWindow("Missing Some chunk files, or selected wrong folder");

                    message.Owner = MainWindow.window;
                    message.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
                    message.ShowDialog();
                }
                return;
            }
            else
                IoC.Settings.ChunkFolderPath = FolderBrowser.SelectedPath;

            Analyze(IoC.Settings.ChunkFolderPath);
            SaveIsEnabled = true;
        }


        static int MagicChunk = 0x00504D43;

        //Grab all the files from the chunks (Getting newest ones)
        public void Analyze(string folderInput)
        {
            try
            {
                string[] chunkFiles = Directory.GetFiles(folderInput, "chunk*.bin", SearchOption.TopDirectoryOnly).CustomSort().ToArray();
                int inputFileMagic;
                using (BinaryReader Reader = new BinaryReader(File.OpenRead(chunkFiles[0]))) inputFileMagic = Reader.ReadInt32();
                if (inputFileMagic == MagicChunk)
                {
                    {
                        foreach (string fileNameEach in chunkFiles)
                        {
                            ChunkOTF cur_chunk = new ChunkOTF();
                            cur_chunk.AnalyzeChunks(fileNameEach);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageWindow message = new MessageWindow("Error occured while analyzing chunks:\n" + ex.Message);

                message.Owner = MainWindow.window;
                message.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
                message.ShowDialog();
            }
        }


        public async void Randomize()
        {
            //Return if cannot pick a randomized folder
            if (!PickRandomizedFolder("Select Folder to Output Randomized Files (Will Also Add a Randomized Folder For the Files)", true))
                return;

            Randomizing = true;
            //Allow the UI to update instead of freezing
            await Task.Delay(20);

            Seed = TMath.Seed();

            if (!string.IsNullOrWhiteSpace(IoC.Settings.UserSeed))
            {
                if (uint.TryParse(IoC.Settings.UserSeed, out _))
                    Seed = Convert.ToUInt32(uint.Parse(IoC.Settings.UserSeed));
                else
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(IoC.Settings.UserSeed);
                    uint total = 1;
                    foreach (byte num in bytes)
                    {
                        total = total * (uint)(num / 2) / 2;
                        if (total == 0)
                            total = 1;
                    }
                    Seed = total;
                }
            }
            using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + RandomizeRootFolder + @"\Seed.txt"))
            {
                file.WriteLine("Seed: " + Seed.ToString());
            }
            if (!string.IsNullOrWhiteSpace(RandomizeRootFolder))
                File.WriteAllText(IoC.Settings.SaveFolderPath + @"\randomized\Installation Instructions.txt",
                                  "Put the \"Randomized Files.json\" file (for when deleting the files with the randomizer), common, em, quest, and/or stage folders (some won't be there depending on what you randomized) into the nativePC folder in the root folder of MHW (if its not there create it and name it exactly like \"nativePC\" (without the quotation marks), its case sensitive)");

            //Clear out all the old randomized files
            RemoveOldRandomizedFiles();

            //Setup the file logger
            RandomizedFileLogger.SetupWatcher();

            //Reset the sobj index list
            QuestData.MonsterMapSobjCount = new int[102, 43];

            QuestRandomizer questRandomizer = new QuestRandomizer();
            if (IoC.Settings.RandomizeQuests)
                questRandomizer.Randomize();
            //Just so you can apply the tweak even without randomizing quests
            if (IoC.Settings.OnePlayerQuests)
                questRandomizer.MakeNonRandomQuests1Player();

            if (IoC.Settings.RandomizeExpeditions || IoC.Settings.RandomizeIceborneExpeditions || IoC.Settings.ExpeditionRandomSobj || IoC.Settings.ExpeditionRandomIBSobj)
                ExpeditionRandomizer.Randomize();

            //If randomizing the quests or expeditions add in edited alnks for all maps and mosters
            if (IoC.Settings.RandomizeQuests || IoC.Settings.RandomizeExpeditions || IoC.Settings.RandomizeIceborneExpeditions)
                Alnk.CreateAlnks();

            //Raise the lava wall to avoid a softlock
            Maps.RaiseLavaWall();

            //Remove the blockades on the maps if using any random spawn files incase a monster spawns behind them
            if (IoC.Settings.RandomSobj || IoC.Settings.RandomizeExpeditions || IoC.Settings.RandomizeIceborneExpeditions)
                Maps.Edit();

            MonsterRandomizer.Randomize();

            //Shuffle recipes
            RecipeRandomizer.RandomizeRecipes();

            ShopRandomizer.Randomize();

            MiscRandomizer.Randomize();

            //Dispose the watcher so it no longer gets fired
            RandomizedFileLogger.DisposeWatcher();
            //Write all the randomized files to a json file after disposing the logger
            using (StreamWriter file = File.CreateText(IoC.Settings.SaveFolderPath + RandomizeRootFolder + @"\Randomized Files.json"))
            {
                JsonSerializer serializer = new JsonSerializer
                {
                    Formatting = Formatting.Indented,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                };
                //serialize objects directly into file stream
                serializer.Serialize(file, RandomizedFiles);
            }
            //Clear out the randomized file set to free up some memory
            RandomizedFiles = new HashSet<string>();

            Randomizing = false;

            //Message Window
            MessageWindow message;

            if (MissingMIBFiles.Count == 0)
                message = new MessageWindow("Successfully Randomized");
            else
                message = new MessageWindow("Successfully Randomized but Missing Some Files: " + string.Join(", ", MissingMIBFiles));

            message.Owner = MainWindow.window;
            message.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            message.ShowDialog();
        }

        /// <summary>
        /// Opens the folder browser to pick a folder then set the folder directory
        /// </summary>
        /// <param name="folderBrowserTitle"></param>
        /// <param name="randomizeButton"></param>
        /// <returns>Returns true if successful</returns>
        private bool PickRandomizedFolder(string folderBrowserTitle, bool randomizeButton = false)
        {
            FolderBrowser = new BetterFolderBrowser();
            if (!string.IsNullOrWhiteSpace(IoC.Settings.SaveFolderPath) && Directory.Exists(IoC.Settings.SaveFolderPath))
                FolderBrowser.RootFolder = IoC.Settings.SaveFolderPath;
            else
                FolderBrowser.RootFolder = AppDomain.CurrentDomain.BaseDirectory;

            FolderBrowser.Title = folderBrowserTitle;
            FolderBrowser.Multiselect = false;

            NativeWindow win32Parent = new NativeWindow();
            //Make it so you can't interact with the randomizer window while the file browser is open
            win32Parent.AssignHandle(new WindowInteropHelper(MainWindow.window).Handle);
            DialogResult result = FolderBrowser.ShowDialog(win32Parent);

            if (!(result == DialogResult.OK && Directory.Exists(FolderBrowser.SelectedPath)))
            {
                if (!Directory.Exists(FolderBrowser.SelectedPath) && result == DialogResult.OK)
                {
                    //Message Window
                    MessageWindow folderMessage = new MessageWindow("Error: Folder Doesn't Exist");

                    folderMessage.Owner = MainWindow.window;
                    folderMessage.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
                    folderMessage.ShowDialog();
                }
                return false;
            }
            else if (!randomizeButton)
            {
                IoC.Settings.SaveFolderPath = FolderBrowser.SelectedPath;
                RandomizeRootFolder = "";
            }
            else if (new DirectoryInfo(FolderBrowser.SelectedPath).Name == "nativePC")
            {
                ChoiceWindow warning = new ChoiceWindow("Warning randomizing directly into the nativePC folder will overide any files that get randomized. Are you sure you want to randomize there?")
                {
                    Owner = MainWindow.window,
                    WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner,
                    Width = 450
                };
                if ((bool)warning.ShowDialog())
                {
                    IoC.Settings.SaveFolderPath = FolderBrowser.SelectedPath;
                    RandomizeRootFolder = "";
                }
                else
                    return false;
            }
            else
            {
                IoC.Settings.SaveFolderPath = FolderBrowser.SelectedPath;
                Directory.CreateDirectory(IoC.Settings.SaveFolderPath + @"\randomized");
                RandomizeRootFolder = @"\randomized";
            }

            return true;
        }

        /// <summary>
        /// Clears out the old randomized files for easy removal of the randomized files, and when randomizing with different settings it clears out the unneeded files
        /// </summary>
        private void RemoveOldRandomizedFiles()
        {
            //Only run this when the remove old files button is pressed
            if (!Randomizing)
            {
                //Only continue if this succeeds
                if (!PickRandomizedFolder("Pick the nativePC folder to remove the randomized files from"))
                    return;

                if (!IoC.Settings.SaveFolderPath.Contains("nativePC"))
                {
                    MessageWindow message = new MessageWindow("Error, nativePC folder wasn't selected!")
                    {
                        Owner = MainWindow.window,
                        WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner
                    };
                    message.ShowDialog();
                    return;
                }
                #region No Randomized File Json

                //Check if the json file exists, if it does just continue out of this to the regular method
                if (!File.Exists(IoC.Settings.SaveFolderPath + @"\Randomized Files.json"))
                {
                    ChoiceWindow warning = new ChoiceWindow("Could not find the \"Randomized Files.json\" file in selected directory. Would you like to remove all possible randomized files? (Could include other mods files)")
                    {
                        Owner = MainWindow.window,
                        WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner,
                        Width = 450
                    };
                    if ((bool)warning.ShowDialog())
                    {
                        try
                        {
                            foreach (int quest in QuestData.QuestName.Keys)
                            {
                                //Format it so it has 0s ahead of the number to make it match the files
                                string fileQuestNumber = quest.ToString("D5");

                                //Delete the quest file
                                if (File.Exists(IoC.Settings.SaveFolderPath + @"\quest\questData_" + fileQuestNumber + ".mib"))
                                    File.Delete(IoC.Settings.SaveFolderPath + @"\quest\questData_" + fileQuestNumber + ".mib");

                                //Delete the folder containing all the fsm files
                                if (Directory.Exists(IoC.Settings.SaveFolderPath + @"\quest\q" + fileQuestNumber + @"\fsm\em"))
                                    Directory.Delete(IoC.Settings.SaveFolderPath + @"\quest\q" + fileQuestNumber + @"\fsm\em", true);

                                //Delete the Xeno map hitching post file or the blank Zorah one
                                if (Directory.Exists(IoC.Settings.SaveFolderPath + @"\quest\q" + fileQuestNumber + @"\set\"))
                                    File.Delete(IoC.Settings.SaveFolderPath + @"\quest\q" + fileQuestNumber + @"\set\" + fileQuestNumber + ".sobjl");

                                //Delete the gmd text files
                                if (File.Exists(IoC.Settings.SaveFolderPath + @"\common\text\quest\q" + fileQuestNumber + "_eng.gmd"))
                                    File.Delete(IoC.Settings.SaveFolderPath + @"\common\text\quest\q" + fileQuestNumber + "_eng.gmd");
                            }

                            HashSet<string> filesToRemove = JsonConvert.DeserializeObject<HashSet<string>>(Encoding.UTF8.GetString(Properties.Resources.FilesToRemove));
                            HashSet<string> foldersToRemove = JsonConvert.DeserializeObject<HashSet<string>>(Encoding.UTF8.GetString(Properties.Resources.FoldersToRemove));

                            //Remove all the potential files
                            foreach (string file in filesToRemove)
                            {
                                if (File.Exists(IoC.Settings.SaveFolderPath + file))
                                    File.Delete(IoC.Settings.SaveFolderPath + file);
                            }

                            //Remove all the potential folders
                            foreach (string folder in foldersToRemove)
                            {
                                if (Directory.Exists(IoC.Settings.SaveFolderPath + folder))
                                    Directory.Delete(IoC.Settings.SaveFolderPath + folder, true);
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageWindow errorMessage = new MessageWindow("Error Deleting Old Files:\n" + ex.Message)
                            {
                                Owner = MainWindow.window,
                                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner
                            };
                            errorMessage.ShowDialog();
                        }

                        DeleteEmptyFolders(IoC.Settings.SaveFolderPath);

                        MessageWindow message = new MessageWindow("Successfully removed previous randomized files")
                        {
                            Owner = MainWindow.window,
                            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner
                        };
                        message.ShowDialog();

                        return;
                    }
                    else
                        return;
                }
            }
            #endregion
            //If only the file doesn't exist than return
            else if (!File.Exists(IoC.Settings.SaveFolderPath + RandomizeRootFolder + @"\Randomized Files.json"))
                return;

            string randomFilesJson = File.ReadAllText(IoC.Settings.SaveFolderPath + RandomizeRootFolder + @"\Randomized Files.json");
            //Check if its a incomplete json due to something like a crash
            if (!randomFilesJson.EndsWith("]"))
                randomFilesJson += "\r\n]";

            //Read in all the files
            RandomizedFiles = JsonConvert.DeserializeObject<HashSet<string>>(randomFilesJson);

            try
            {
                //Loop through and delete all the files
                foreach (string relativeFilePath in RandomizedFiles)
                {
                    if (File.Exists(IoC.Settings.SaveFolderPath + RandomizeRootFolder + relativeFilePath))
                        File.Delete(IoC.Settings.SaveFolderPath + RandomizeRootFolder + relativeFilePath);
                }
            }
            catch (Exception ex)
            {
                MessageWindow message = new MessageWindow("Error Deleting Old Files:\n" + ex.Message)
                {
                    Owner = MainWindow.window,
                    WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner
                };
                message.ShowDialog();
            }

            //Delete the old randomized files json file
            File.Delete(IoC.Settings.SaveFolderPath + RandomizeRootFolder + @"\Randomized Files.json");

            //Clear the hash set
            RandomizedFiles = new HashSet<string>();

            DeleteEmptyFolders(IoC.Settings.SaveFolderPath);
            if (!Randomizing)
            {
                MessageWindow message = new MessageWindow("Successfully removed previous randomized files")
                {
                    Owner = MainWindow.window,
                    WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner
                };
                message.ShowDialog();
            }
        }

        private static void DeleteEmptyFolders(string startLocation)
        {
            foreach (string directory in Directory.GetDirectories(startLocation))
            {
                //Go through to the sub folder
                DeleteEmptyFolders(directory);
                //If it doesn't contain any files or folders, delete the directory
                //Checking for .Any() with enumerate makes it exit the check early on the first file or folder found, rather than getting all files and folders
                if (!Directory.EnumerateFileSystemEntries(directory).Any())
                {
                    Directory.Delete(directory, false);
                }
            }
        }

    }
}
