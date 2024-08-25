using MHW_Randomizer.Crypto;
using System;
using System.Collections.Generic;
using System.IO;
using Troschuetz.Random.Generators;

namespace MHW_Randomizer
{
    public static class MiscRandomizer
    {
        /// <summary>
        /// First number is the colour index, second is the red, green, and blue values
        /// </summary>
        private static List<byte[]>? Colours;

        public static void Randomize()
        {
            if (ViewModels.Settings.RandomScoutflyColour)
                RandomizeScoutFlies();

            if (ViewModels.Settings.FasterKinsects)
                AddKinsectSpeed();
            if (ViewModels.Settings.UnknownMonsterIcons)
                HideMonsters();
        }

        private static void HideMonsters()
        {
            GMD monsterNames = new GMD(GameFiles.GetFile(@"\common\text\em_names_eng.gmd"));
            for (int entry = 0; entry < monsterNames.Entries.Count; entry++)
            {
                monsterNames.Entries[entry].Value = "???";
            }
            Directory.CreateDirectory(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\common\text\");
            monsterNames.Save(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\common\text\em_names_eng.gmd");

            Directory.CreateDirectory(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\ui\common\tex\");
            GameFiles.WriteAndLogFile(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\ui\common\tex\cmn_micon00_ID.tex", Properties.Resources.cmn_micon00_ID);
            GameFiles.WriteAndLogFile(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\ui\common\tex\cmn_micon01_ID.tex", Properties.Resources.cmn_micon01_ID);
            GameFiles.WriteAndLogFile(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\ui\common\tex\cmn_micon00M_ID.tex", Properties.Resources.cmn_micon00M_ID);
        }

        private static void RandomizeScoutFlies()
        {
            Colours = new List<byte[]> {
                                                new byte[] { 147, 71, 213 }, //Purple
                                                new byte[] { 180, 39, 125 }, //Pinky Purple
                                                new byte[] { 227, 38, 190 }, //Pink
                                                new byte[] { 38, 87, 227 }, //Blue
                                                new byte[] { 34, 55, 128 }, //Dark Blue
                                                new byte[] { 38, 206, 227 }, //Light Blue
                                                new byte[] { 182, 255, 248 }, //Pastel Light Blue
                                                new byte[] { 33, 148, 0 }, //Dark Green
                                                new byte[] { 129, 238, 53 }, //Green
                                                new byte[] { 70, 214, 124 }, //Cyan Green
                                                new byte[] { 215, 241, 112 }, //Light Green
                                                new byte[] { 255, 190, 123 }, //DarkTan
                                                new byte[] { 227, 160, 38 }, //Orange
                                                new byte[] { 218, 127, 18 }, //Darker Orange
                                                new byte[] { 227, 38, 21 }, //Red
                                                new byte[] { 169, 0, 33 }, //Dark Red
                                                new byte[] { 254, 231, 21 }, //Yellow
                                            };
            byte[] scoutflyBytes = GameFiles.GetFile(@"\common\guide_insect\gi_param.gip");

            NR3Generator r = new NR3Generator(ViewModels.Randomizer.Seed);
            //Scoutfly Colour
            //Normal Monster/Tracking Anything
            scoutflyBytes = scoutflyBytes.RandomColourPicker(220, r, 240);
            //Elder Monster
            scoutflyBytes = scoutflyBytes.RandomColourPicker(232, r, 244);
            //Tempered Monster
            scoutflyBytes = scoutflyBytes.RandomColourPicker(236, r, 248);

            if (ViewModels.Settings.DifferentTrackScoutflyColour)
            {
                //Trace Colour
                //Normal Monster Trace
                scoutflyBytes = scoutflyBytes.RandomColourPicker(240, r);
                //Elder Monster Trace
                scoutflyBytes = scoutflyBytes.RandomColourPicker(244, r);
                //Tempered Monster Trace
                scoutflyBytes = scoutflyBytes.RandomColourPicker(248, r);
            }

            Directory.CreateDirectory(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\common\guide_insect\");
            GameFiles.WriteAndLogFile(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\common\guide_insect\gi_param.gip", scoutflyBytes);
        }

        public static void AddKinsectSpeed()
        {
            byte[] insectBytes;
            //If the file has been randomized then just open the randomized one
            if (File.Exists(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\common\equip\rod_insect.rod_inse"))
                insectBytes = File.ReadAllBytes(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\common\equip\rod_insect.rod_inse");
            else
            {
                Directory.CreateDirectory(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\common\equip\");
                insectBytes = GameFiles.GetFile(@"\common\equip\rod_insect.rod_inse");
            }

            Cipher cipher = new Cipher("SFghFQVFJycHnypExurPwut98ZZq1cwvm7lpDpASeP4biRhstQgULzlb");
            insectBytes = cipher.Decipher(insectBytes);

            List<RecipeStructs.KinsectTree> insects = StructTools.RawDeserialize<RecipeStructs.KinsectTree>(insectBytes, 10);

            foreach (RecipeStructs.KinsectTree insect in insects)
            {
                //If the speed is less than 3 then set it to 3
                if (insect.Speed < 3)
                    insect.Speed = 3;
            }

            //Length is 10 bytes for the header, another 10 for the ending bytes
            Array.Copy(StructTools.RawSerialize(insects), 0, insectBytes, 10, insectBytes.Length - 20);
            insectBytes = cipher.Encipher(insectBytes);

            GameFiles.WriteAndLogFile(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\common\equip\rod_insect.rod_inse", insectBytes);
        }

        private static byte[] RandomColourPicker(this byte[] fileBytes, uint offset, NR3Generator r, uint traceOffset = 0)
        {
            int index = r.Next(Colours.Count);
            for (int i = 0; i < 3; i++)
            {
                fileBytes[offset + i] = ViewModels.Settings.CompletelyRandomScoutflyColour ? (byte)r.Next(256) : Colours[index][i];
                if (traceOffset != 0)
                    fileBytes[traceOffset + i] = fileBytes[offset + i];
                
            }
            Colours.RemoveAt(index);
            return fileBytes;
        }
    }
}
