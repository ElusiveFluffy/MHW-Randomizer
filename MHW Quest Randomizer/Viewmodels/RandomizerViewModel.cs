using MHW_Randomizer.Crypto;
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
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MHW_Randomizer
{
    public class RandomizerViewModel : BaseViewModel
    {
        public List<string> MissingMIBFiles;

        public ICommand OpenCommand { get; set; }
        public ICommand RandomizeCommand { get; set; }
        public ICommand CreditsCommand { get; set; }

        public string MonsterIcon { get; set; }


        public RandomizerViewModel()
        {
            OpenCommand = new RelayCommand(OpenFolder);
            RandomizeCommand = new RelayCommand(Randomize);
            CreditsCommand = new RelayCommand(Credits);

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

            if (!string.IsNullOrEmpty(IoC.Settings.ChunkFolderPath))
                FolderBrowser.RootFolder = IoC.Settings.ChunkFolderPath;

            if (Directory.GetFiles(IoC.Settings.ChunkFolderPath, "chunk*.bin", SearchOption.TopDirectoryOnly).ToArray().Length == 10)
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
        }

        public void Credits()
        {
            //Message Window
            MessageWindow message = new MessageWindow("Credits\nThank you to:\nAradi147 for the source code for the quest editor,\nSynthlight for the 010 templates which helped me serialize and deserialize a lot of the files, and also for their editor to help check values," +
                "\nFusionR for the source code for cirilla to be able to edit the GMD text files,\nMHVuze for the source code of WorldChunkTool without it I wouldn't have been able to have it read the chunk files for a more streamlined process,\neveryone else who worked on these programs,\nand the MHW modding discord and modding wiki." +
                "\n\nPackages used:\nBetter Folder Browser by WillyKimura\nFody Property Changed by Simon Cropp\nNewtonsoft.Json by James Newton-King\nNinject by Ninject Project Contributors\nTroschuetz.Random by Stefan Troschütz and Alessio Parma\nCRC32.NET by Force")
            {
                Width = 500,
                Height = 370,

                Owner = MainWindow.window,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner
            };
            message.ShowDialog();
        }

        public BetterFolderBrowser FolderBrowser = new BetterFolderBrowser();
        public bool SaveIsEnabled { get; set; }

        public uint Seed;


        public void OpenFolder()
        {
            FolderBrowser.Title = "Select Chunks Folder";
            FolderBrowser.Multiselect = false;

            NativeWindow win32Parent = new NativeWindow();
            win32Parent.AssignHandle(new WindowInteropHelper(MainWindow.window).Handle);
            DialogResult result = FolderBrowser.ShowDialog(win32Parent);

            if (!(result == DialogResult.OK && Directory.GetFiles(FolderBrowser.SelectedPath, "chunk*.bin", SearchOption.TopDirectoryOnly).ToArray().Length == 10))
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
                MessageWindow message = new MessageWindow("Error occured while extracting skipped. Please try again later.\n" + ex.StackTrace);

                message.Owner = MainWindow.window;
                message.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
                message.ShowDialog();
            }
        }


        public void Randomize()
        {
            Seed = TMath.Seed();

            //Dictionary<int, string> armourNames = JsonConvert.DeserializeObject<Dictionary<int, string>>(Encoding.UTF8.GetString(Properties.Resources.eng_otomo_weaponData));
            //Dictionary<int, string> armour = new Dictionary<int, string>();
            //int index = 0;
            //string previous = "";
            //for (int i = 0; i < armourNames.Count; i++)
            //{
            //    if (armourNames.ElementAt(i).Value.Length < 40 && armourNames.ElementAt(i).Value != previous)
            //    {
            //        armour[index] = armourNames.ElementAt(i).Value;
            //        previous = armourNames.ElementAt(i).Value;
            //        index++;
            //    }
            //    else
            //    {
            //        previous = "";
            //    }
            //}
            //using (StreamWriter file = File.CreateText(IoC.Settings.ChunkFolderPath + @"\randomized\otomo_weapon_Data.json"))
            //{
            //    JsonSerializer serializer = new JsonSerializer
            //    {
            //        Formatting = Formatting.Indented,
            //        DefaultValueHandling = DefaultValueHandling.Ignore
            //    };
            //    //serialize objects directly into file stream
            //    serializer.Serialize(file, armour);
            //}

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
            using (StreamWriter file = File.AppendText(IoC.Settings.ChunkFolderPath + @"\randomized\Seed.txt"))
            {
                file.WriteLine("Seed: " + Seed.ToString());
            }

            QuestRandomizer questRandomizer = new QuestRandomizer();
            questRandomizer.Randomize();

            //Shuffle recipes
            RecipeRandomizer.RandomizeRecipes();

            ShopRandomizer.Randomize();

            MiscRandomizer.Randomize();

            //Message Window
            MessageWindow message = new MessageWindow("");

            if (MissingMIBFiles.Count == 0)
                message = new MessageWindow("Successfully Randomized");
            else
                message = new MessageWindow("Successfully Randomized but Missing Some Files: " + string.Join(", ", MissingMIBFiles));

            message.Owner = MainWindow.window;
            message.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            message.ShowDialog();
        }

    }
}
