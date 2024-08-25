using System.IO;

namespace MHW_Randomizer
{
    public class Maps
    {
        public static void Edit()
        {
            #region Rotten Vale Blockade

            byte[] levelObjects = GameFiles.GetFile("\\stage\\st104\\common\\set\\st104_gm.sobj");

            //Raise blockade in lowest part of the rotten vale
            levelObjects[4487] = 0x00;
            levelObjects[4488] = 0xB4;
            levelObjects[4489] = 0x12;
            levelObjects[4490] = 0xC6;

            //Raise the first blockade in lower part  of the rotten vale
            levelObjects[26691] = 0xD5;
            levelObjects[26692] = 0xC9;
            levelObjects[26693] = 0xEC;
            levelObjects[26694] = 0xC5;

            Directory.CreateDirectory(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\stage\st104\common\set");
            GameFiles.WriteAndLogFile(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\stage\st104\common\set\st104_gm.sobj", levelObjects);

            #endregion

            #region Elder Recess Blockade

            levelObjects = GameFiles.GetFile("\\stage\\st105\\common\\set\\st105_gm.sobj");

            //Raise the lava wall blockade
            levelObjects[0x9AAA] = 0x00;
            levelObjects[0x9AAB] = 0x00;
            levelObjects[0x9AAC] = 0x00;
            levelObjects[0x9AAD] = 0x00;

            //Lower the crystals blocking the crawl space to the camp
            levelObjects[0x9C64] = 0x00;
            levelObjects[0x9C65] = 0x00;
            levelObjects[0x9C66] = 0xAF;
            levelObjects[0x9C67] = 0xC4;

            //Raise the crystal blockade where nergigante would be
            levelObjects[0x9FE7] = 0x00;
            levelObjects[0x9FE8] = 0x00;
            levelObjects[0x9FE9] = 0xAF;
            levelObjects[0x9FEA] = 0x44;

            Directory.CreateDirectory(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\stage\st105\common\set");
            GameFiles.WriteAndLogFile(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\stage\st105\common\set\st105_gm.sobj", levelObjects);

            #endregion

            #region Hoarfrost Reach Blockade

            if (ViewModels.Settings.RandomizeIBQuests || ViewModels.Settings.RandomizeIceborneExpeditions)
            {
                levelObjects = GameFiles.GetFile("\\stage\\st108\\common\\set\\st108_gm.sobj");

                //Rocks to the left area
                levelObjects[0xF2A9] = 0x00;
                levelObjects[0xF2AA] = 0x40;
                levelObjects[0xF2AB] = 0x9C;
                levelObjects[0xF2AC] = 0xC5;

                levelObjects[0xF508] = 0x00;
                levelObjects[0xF509] = 0x80;
                levelObjects[0xF50A] = 0x3B;
                levelObjects[0xF50B] = 0xC5;

                levelObjects[0xF767] = 0x00;
                levelObjects[0xF768] = 0x00;
                levelObjects[0xF769] = 0x16;
                levelObjects[0xF76A] = 0xC5;

                levelObjects[0xFE6A] = 0x00;
                levelObjects[0xFE6B] = 0xE0;
                levelObjects[0xFE6C] = 0xAB;
                levelObjects[0xFE6D] = 0xC5;

                //Crawl space near the rocks in the left area
                levelObjects[0x10637] = 0x00;
                levelObjects[0x10638] = 0x00;
                levelObjects[0x10639] = 0xFA;
                levelObjects[0x1063A] = 0x44;

                //Rock to the back left of the map
                levelObjects[0xF9C6] = 0x00;
                levelObjects[0xF9C7] = 0x40;
                levelObjects[0xF9C8] = 0x9C;
                levelObjects[0xF9C9] = 0xC5;

                levelObjects[0xFC18] = 0x00;
                levelObjects[0xFC19] = 0x40;
                levelObjects[0xFC1A] = 0x9C;
                levelObjects[0xFC1B] = 0xC5;

                levelObjects[0x10837] = 0x00;
                levelObjects[0x10838] = 0x40;
                levelObjects[0x10839] = 0x9C;
                levelObjects[0x1083A] = 0xC5;

                //Logs in the middle of the map
                levelObjects[0x1043E] = 0x00;
                levelObjects[0x1043F] = 0x40;
                levelObjects[0x10440] = 0x9C;
                levelObjects[0x10441] = 0xC5;

                //Ice to the back right of the map
                levelObjects[0x10217] = 0x00;
                levelObjects[0x10218] = 0x00;
                levelObjects[0x10219] = 0x7A;
                levelObjects[0x1021A] = 0xC5;

                //Giant Crystal at back of the map
                levelObjects[0x10031] = 0x00;
                levelObjects[0x10032] = 0x40;
                levelObjects[0x10033] = 0x9C;
                levelObjects[0x10034] = 0xC5;

                Directory.CreateDirectory(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\stage\st108\common\set");
                GameFiles.WriteAndLogFile(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\stage\st108\common\set\st108_gm.sobj", levelObjects);
            }

            #endregion

            #region Guiding Lands Blockade

            //Unlock the blocked off areas in the guiding lands
            if (ViewModels.Settings.RandomizeIBQuests)
            {
                //Unlock the blocked off areas
                byte[] unblockBytes = GameFiles.GetFile(@"\stage\st109\common\sdl\st109_v03.sdl");
                Directory.CreateDirectory(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\stage\st109\common\sdl");
                GameFiles.WriteAndLogFile(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\stage\st109\common\sdl\st109_v01.sdl", unblockBytes);
                GameFiles.WriteAndLogFile(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\stage\st109\common\sdl\st109_v02.sdl", unblockBytes);
                //Do the same for bake files
                unblockBytes = GameFiles.GetFile(@"\stage\st109\common\sdl\st109_v03_bake.sdl");
                GameFiles.WriteAndLogFile(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\stage\st109\common\sdl\st109_v01_bake.sdl", unblockBytes);
                GameFiles.WriteAndLogFile(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\stage\st109\common\sdl\st109_v02_bake.sdl", unblockBytes);

                //Light
                unblockBytes = GameFiles.GetFile(@"\stage\st109\common\light\LL_st109_area_v03.llsd");
                Directory.CreateDirectory(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\stage\st109\common\light");
                GameFiles.WriteAndLogFile(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\stage\st109\common\light\LL_st109_area_v01.llsd", unblockBytes);
                GameFiles.WriteAndLogFile(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\stage\st109\common\light\LL_st109_area_v02.llsd", unblockBytes);

                unblockBytes = GameFiles.GetFile(@"\stage\st109\common\light\LL_st109_v03.llk");
                GameFiles.WriteAndLogFile(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\stage\st109\common\light\LL_st109_v01.llk", unblockBytes);
                GameFiles.WriteAndLogFile(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\stage\st109\common\light\LL_st109_v02.llk", unblockBytes);

                //etc
                unblockBytes = GameFiles.GetFile(@"\stage\st109\common\etc\st109_v03.bkipr");
                Directory.CreateDirectory(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\stage\st109\common\etc");
                GameFiles.WriteAndLogFile(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\stage\st109\common\etc\st109_v01.bkipr", unblockBytes);
                GameFiles.WriteAndLogFile(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\stage\st109\common\etc\st109_v02.bkipr", unblockBytes);
                unblockBytes = GameFiles.GetFile(@"\stage\st109\common\etc\st109_v03.umbra");
                GameFiles.WriteAndLogFile(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\stage\st109\common\etc\st109_v01.umbra", unblockBytes);
                GameFiles.WriteAndLogFile(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\stage\st109\common\etc\st109_v02.umbra", unblockBytes);


                //Remove blocked off area collision by replacing it with a different one
                unblockBytes = GameFiles.GetFile(@"\stage\st109\st109_F\col\st109_F_col_add.sbc");
                Directory.CreateDirectory(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\stage\st109\st109_V\col");
                Directory.CreateDirectory(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\stage\st109\st109_W\col");
                GameFiles.WriteAndLogFile(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\stage\st109\st109_V\col\st109_V_col.sbc", unblockBytes);
                GameFiles.WriteAndLogFile(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\stage\st109\st109_W\col\st109_W_col.sbc", unblockBytes);
            }

            #endregion

        }

        /// <summary>
        /// Always raise this since otherwise if the monster isn't teostra in the quest Teostra the Infernal it won't be removed for some reason
        /// </summary>
        public static void RaiseLavaWall()
        {
            byte[] levelObjects = GameFiles.GetFile(@"\stage\st105\common\set\st105_gm.sobj");

            //Raise the lava wall blockade
            levelObjects[0x9AAA] = 0x00;
            levelObjects[0x9AAB] = 0x00;
            levelObjects[0x9AAC] = 0x00;
            levelObjects[0x9AAD] = 0x00;

            Directory.CreateDirectory(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\stage\st105\common\set");
            GameFiles.WriteAndLogFile(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\stage\st105\common\set\st105_gm.sobj", levelObjects);
        }
    }
}
