using MHW_Randomizer.Crypto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Troschuetz.Random.Generators;

namespace MHW_Randomizer
{
    public class RecipeRandomizer
    {
        //Easy to use ID
        public enum WeaponCategory { Greatsword, Sword_and_Shield, Dual_Blades, LongSword, Hammer, Hunting_Horn, Lance, GunLance, Switch_Axe, Charge_Blade, Insect_Glaive, Bow, Heavy_Bowgun, Light_Bowgun }

        public static string[] Element = { "None", "Fire", "Water", "Ice", "Thunder", "Dragon", "Poison", "Paralysis", "Sleep", "Blast" };

        //For checking and shuffling recipes
        public static void RandomizeRecipes()
        {
            #region Armour

            if (IoC.Settings.ShuffleArmourRecipes || IoC.Settings.ShuffleCharms)
            {
                File.Create(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Armour Log.txt").Dispose();
                using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Armour Log.txt"))
                {
                    file.WriteLine("---------------------------------------------------------------------------");
                    file.WriteLine("                              Armour Recipes                               ");
                    file.WriteLine("---------------------------------------------------------------------------");
                }

                byte[] recipeBytes = ChunkOTF.files["armor.eq_crt"].Extract();
                byte[] header = new byte[10];
                Array.Copy(recipeBytes, header, 10);
                List<RecipeStructs.Armour> recipeList = StructTools.RawDeserialize<RecipeStructs.Armour>(recipeBytes, 10);

                NR3Generator r = new NR3Generator(IoC.Randomizer.Seed);


                List<int> validIDs = new List<int>();

                if (IoC.Settings.ShuffleCharms)
                {
                    //0-103 charms
                    validIDs.AddRange(Enumerable.Range(0, 104));

                    ShuffleArmourRecipes(recipeList, r, validIDs);
                }

                if (IoC.Settings.ShuffleArmourRecipes)
                {
                    for (int loop = 1; loop < 6; loop++)
                    {
                        for (int rank = 0; rank < 3; rank++)
                        {
                            validIDs = new List<int>();

                            switch (loop)
                            {
                                case 1:
                                    {
                                        //104-724 head
                                        //104-134 low rank (first two are the iron ore ones)
                                        //136-230 232-239, 246-249, 252, 264-270, 275-282, 308-312 High rank
                                        //318-371, 384-409, 412-413, 419-430, 434-451, 454-457, 459-464, 467-468, 470-473, 476-495, 497-499, 523-524, 528, 530-533 Master Rank
                                        switch (rank)
                                        {
                                            case 0:
                                                {
                                                    if (IoC.Settings.ShuffleIronOre)
                                                        validIDs.AddRange(Enumerable.Range(104, 31));
                                                    else
                                                        validIDs.AddRange(Enumerable.Range(106, 29));
                                                    break;
                                                }
                                            case 1:
                                                {
                                                    validIDs.AddRange(Enumerable.Range(136, 95));
                                                    validIDs.AddRange(Enumerable.Range(232, 8));
                                                    validIDs.AddRange(Enumerable.Range(246, 4));
                                                    validIDs.Add(252);
                                                    validIDs.AddRange(Enumerable.Range(264, 7));
                                                    validIDs.AddRange(Enumerable.Range(275, 8));
                                                    validIDs.AddRange(Enumerable.Range(308, 5));
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    validIDs.AddRange(Enumerable.Range(318, 54));
                                                    validIDs.AddRange(Enumerable.Range(384, 26));
                                                    validIDs.AddRange(Enumerable.Range(412, 2));
                                                    validIDs.AddRange(Enumerable.Range(419, 12));
                                                    validIDs.AddRange(Enumerable.Range(434, 18));
                                                    validIDs.AddRange(Enumerable.Range(454, 4));
                                                    validIDs.AddRange(Enumerable.Range(459, 6));
                                                    validIDs.AddRange(Enumerable.Range(467, 2));
                                                    validIDs.AddRange(Enumerable.Range(470, 4));
                                                    validIDs.AddRange(Enumerable.Range(476, 20));
                                                    validIDs.AddRange(Enumerable.Range(497, 3));
                                                    validIDs.AddRange(Enumerable.Range(523, 2));
                                                    validIDs.Add(528);
                                                    validIDs.AddRange(Enumerable.Range(530, 4));
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                case 2:
                                    {
                                        //725-1303 chest
                                        //725-754 Low rank (first two are the iron ore ones)
                                        //755-850, 857-858, 863, 875-881, 886-889, 908-910 High rank
                                        //918-969, 978-1003, 1006-1007, 1010-1021, 1024-1041, 1044-1053, 1056-1061, 1064-1083, 1104-1105, 1109, 1111-1114 Master rank
                                        switch (rank)
                                        {
                                            case 0:
                                                {
                                                    if (IoC.Settings.ShuffleIronOre)
                                                        validIDs.AddRange(Enumerable.Range(725, 30));
                                                    else
                                                        validIDs.AddRange(Enumerable.Range(727, 28));
                                                    break;
                                                }
                                            case 1:
                                                {
                                                    validIDs.AddRange(Enumerable.Range(755, 96));
                                                    validIDs.AddRange(Enumerable.Range(857, 2));
                                                    validIDs.Add(863);
                                                    validIDs.AddRange(Enumerable.Range(875, 7));
                                                    validIDs.AddRange(Enumerable.Range(886, 4));
                                                    validIDs.AddRange(Enumerable.Range(908, 3));
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    validIDs.AddRange(Enumerable.Range(918, 52));
                                                    validIDs.AddRange(Enumerable.Range(978, 26));
                                                    validIDs.AddRange(Enumerable.Range(1006, 2));
                                                    validIDs.AddRange(Enumerable.Range(1010, 12));
                                                    validIDs.AddRange(Enumerable.Range(1024, 18));
                                                    validIDs.AddRange(Enumerable.Range(1044, 10));
                                                    validIDs.AddRange(Enumerable.Range(1056, 6));
                                                    validIDs.AddRange(Enumerable.Range(1064, 20));
                                                    validIDs.AddRange(Enumerable.Range(1104, 2));
                                                    validIDs.Add(1109);
                                                    validIDs.AddRange(Enumerable.Range(1111, 4));
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                case 3:
                                    {
                                        //1304-1880 arms
                                        //1304-1334 Low rank (first two are the iron ore ones)
                                        //1335-1430, 1437-1438, 1443, 1455-1461, 1466-1469, 1488-1490 High rank
                                        //1498-1547, 1557-1582, 1585-1586, 1589-1600, 1603-1620, 1623-1632, 1635-1640, 1643-1662, 1683-1684, 1688, 1690-1693 Master rank
                                        switch (rank)
                                        {
                                            case 0:
                                                {
                                                    if (IoC.Settings.ShuffleIronOre)
                                                        validIDs.AddRange(Enumerable.Range(1304, 31));
                                                    else
                                                        validIDs.AddRange(Enumerable.Range(1306, 29));
                                                    break;
                                                }
                                            case 1:
                                                {
                                                    validIDs.AddRange(Enumerable.Range(1335, 96));
                                                    validIDs.AddRange(Enumerable.Range(1437, 2));
                                                    validIDs.Add(1443);
                                                    validIDs.AddRange(Enumerable.Range(1455, 7));
                                                    validIDs.AddRange(Enumerable.Range(1466, 4));
                                                    validIDs.AddRange(Enumerable.Range(1488, 3));
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    validIDs.AddRange(Enumerable.Range(1498, 50));
                                                    validIDs.AddRange(Enumerable.Range(1557, 26));
                                                    validIDs.AddRange(Enumerable.Range(1585, 2));
                                                    validIDs.AddRange(Enumerable.Range(1589, 12));
                                                    validIDs.AddRange(Enumerable.Range(1603, 18));
                                                    validIDs.AddRange(Enumerable.Range(1623, 10));
                                                    validIDs.AddRange(Enumerable.Range(1635, 6));
                                                    validIDs.AddRange(Enumerable.Range(1643, 20));
                                                    validIDs.AddRange(Enumerable.Range(1683, 2));
                                                    validIDs.Add(1688);
                                                    validIDs.AddRange(Enumerable.Range(1690, 4));
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                case 4:
                                    {
                                        //1881-2451 waist
                                        //1881-1910 Low rank (first two are the iron ore ones)
                                        //1911-2004, 2011-2012, 2017, 2029-2035, 2040-2043, 2062-2064 High rank
                                        //2072-2119, 2128-2153, 2156-2157, 2160-2171, 2174-2191, 2194-2203, 2206-2211, 2214-2233, 2254-2255, 2259, 2261-2264 Master rank
                                        switch (rank)
                                        {
                                            case 0:
                                                {
                                                    if (IoC.Settings.ShuffleIronOre)
                                                        validIDs.AddRange(Enumerable.Range(1881, 30));
                                                    else
                                                        validIDs.AddRange(Enumerable.Range(1883, 28));
                                                    break;
                                                }
                                            case 1:
                                                {
                                                    validIDs.AddRange(Enumerable.Range(1911, 94));
                                                    validIDs.AddRange(Enumerable.Range(2011, 2));
                                                    validIDs.Add(2017);
                                                    validIDs.AddRange(Enumerable.Range(2029, 7));
                                                    validIDs.AddRange(Enumerable.Range(2040, 4));
                                                    validIDs.AddRange(Enumerable.Range(2062, 3));
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    validIDs.AddRange(Enumerable.Range(2072, 48));
                                                    validIDs.AddRange(Enumerable.Range(2128, 26));
                                                    validIDs.AddRange(Enumerable.Range(2156, 2));
                                                    validIDs.AddRange(Enumerable.Range(2160, 12));
                                                    validIDs.AddRange(Enumerable.Range(2174, 18));
                                                    validIDs.AddRange(Enumerable.Range(2194, 10));
                                                    validIDs.AddRange(Enumerable.Range(2206, 6));
                                                    validIDs.AddRange(Enumerable.Range(2214, 20));
                                                    validIDs.AddRange(Enumerable.Range(2254, 2));
                                                    validIDs.Add(2259);
                                                    validIDs.AddRange(Enumerable.Range(2261, 4));
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                case 5:
                                    {
                                        //2452-3028 legs
                                        //2452-2482 Low rank (first two are the iron ore ones)
                                        //2483-2578, 2585-2586, 2591, 2603-2609, 2614-2617, 2636-2638 High rank
                                        //2646-2695, 2705-2730, 2733-2734, 2737-2748, 2751-2768, 2771-2780, 2783-2788, 2791-2810, 2831-2832, 2836, 2838-2841 Master rank
                                        switch (rank)
                                        {
                                            case 0:
                                                {
                                                    if (IoC.Settings.ShuffleIronOre)
                                                        validIDs.AddRange(Enumerable.Range(2452, 31));
                                                    else
                                                        validIDs.AddRange(Enumerable.Range(2454, 29));
                                                    break;
                                                }
                                            case 1:
                                                {
                                                    validIDs.AddRange(Enumerable.Range(2483, 96));
                                                    validIDs.AddRange(Enumerable.Range(2585, 2));
                                                    validIDs.Add(2591);
                                                    validIDs.AddRange(Enumerable.Range(2603, 7));
                                                    validIDs.AddRange(Enumerable.Range(2614, 4));
                                                    validIDs.AddRange(Enumerable.Range(2636, 3));
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    validIDs.AddRange(Enumerable.Range(2646, 50));
                                                    validIDs.AddRange(Enumerable.Range(2705, 26));
                                                    validIDs.AddRange(Enumerable.Range(2733, 2));
                                                    validIDs.AddRange(Enumerable.Range(2737, 12));
                                                    validIDs.AddRange(Enumerable.Range(2751, 18));
                                                    validIDs.AddRange(Enumerable.Range(2771, 10));
                                                    validIDs.AddRange(Enumerable.Range(2783, 6));
                                                    validIDs.AddRange(Enumerable.Range(2791, 20));
                                                    validIDs.AddRange(Enumerable.Range(2831, 2));
                                                    validIDs.Add(2836);
                                                    validIDs.AddRange(Enumerable.Range(2838, 4));
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                            }

                            ShuffleArmourRecipes(recipeList, r, validIDs);

                        }
                    }
                }

                byte[] randomizedBytes = StructTools.RawSerialize(recipeList);
                randomizedBytes = header.Concat(randomizedBytes).ToArray();
                Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\equip\");
                File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\equip\armor.eq_crt", randomizedBytes);
            }

            if (IoC.Settings.RandomArmourSkill || IoC.Settings.RandomArmourSkillLevels || IoC.Settings.ShuffleArmourSetBonus || IoC.Settings.RandomArmourDecoSlots || IoC.Settings.RandomDecoSlotSize || IoC.Settings.GiveCharmDecoSlot 
                || IoC.Settings.RandomArmourIconColour || IoC.Settings.RandomCharmSkill || IoC.Settings.RandomCharmSkillLevels)
            {
                byte[] datBytes = ChunkOTF.files["armor.am_dat"].Extract();
                List<RecipeStructs.ArmourDat> datList = StructTools.RawDeserialize<RecipeStructs.ArmourDat>(datBytes, 10);

                RandomizeArmourDat(datList);

                byte[] randomizedDatBytes = StructTools.RawSerialize(datList);
                Array.Copy(randomizedDatBytes, 0, datBytes, 10, randomizedDatBytes.Length);
                Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\equip\");
                File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\equip\armor.am_dat", datBytes);
            }

            #endregion

            #region Weapons

            if (IoC.Settings.ShuffleWeaponRecipes || IoC.Settings.ShuffleWeaponOrder || IoC.Settings.RandomWeaponElement || IoC.Settings.RandomWeaponIconColour || IoC.Settings.RandomWeaponDecoSlots || IoC.Settings.RandomWeaponDecoSlotSize)
            {
                File.Create(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Weapon Log.txt").Dispose();
                using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Weapon Log.txt"))
                {
                    file.Write("---------------------------------------------------------------------------");
                    file.Write("\n                              Weapon Recipes                               ");
                    file.Write("\n---------------------------------------------------------------------------");
                }


                byte[] recipeBytes = ChunkOTF.files["weapon.eq_cus"].Extract();
                byte[] weaponCraftBytes = ChunkOTF.files["weapon.eq_crt"].Extract();
                List<RecipeStructs.Weapon> recipeList = StructTools.RawDeserialize<RecipeStructs.Weapon>(recipeBytes, 10);
                List<RecipeStructs.Weapon> recipeNonRandomList = StructTools.RawDeserialize<RecipeStructs.Weapon>(recipeBytes, 10);
                byte[] treeBytes;
                List<RecipeStructs.WeaponTree> weaponTree = new List<RecipeStructs.WeaponTree>();
                List<RecipeStructs.WeaponTree> weaponNonRandomTree = new List<RecipeStructs.WeaponTree>();
                List<RecipeStructs.RangedWeaponTree> rangedWeaponTree = new List<RecipeStructs.RangedWeaponTree>();
                List<RecipeStructs.RangedWeaponTree> rangedWeaponNonRandomTree = new List<RecipeStructs.RangedWeaponTree>();
                List<RecipeStructs.Armour> weaponCraft = StructTools.RawDeserialize<RecipeStructs.Armour>(weaponCraftBytes, 10);
                Dictionary<int, string> weaponNames;
                NR3Generator r = new NR3Generator(IoC.Randomizer.Seed);

                Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\equip\");
                bool exportWp_dat = IoC.Settings.ShuffleWeaponOrder || IoC.Settings.RandomWeaponElement || IoC.Settings.RandomWeaponIconColour || IoC.Settings.RandomWeaponDecoSlots || IoC.Settings.RandomWeaponDecoSlotSize;
                for (int weapons = 0; weapons < 11; weapons++)
                {
                    switch (weapons)
                    {
                        case 0:
                            {
                                using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Weapon Log.txt"))
                                {
                                    file.WriteLine("\n---------------------------------------------------------------------------");
                                    file.WriteLine("                          Charge Blade Changes                             ");
                                    file.WriteLine("---------------------------------------------------------------------------");
                                }

                                treeBytes = ChunkOTF.files["c_axe.wp_dat"].Extract();
                                weaponNames = JsonConvert.DeserializeObject<Dictionary<int, string>>(Encoding.UTF8.GetString(Properties.Resources.Charge_Blade));
                                weaponTree = StructTools.RawDeserialize<RecipeStructs.WeaponTree>(treeBytes, 10);
                                weaponNonRandomTree = StructTools.RawDeserialize<RecipeStructs.WeaponTree>(treeBytes, 10);
                                int[] badIndex = { 1676, 1728, 1798, 1807, 1814, 1817, 1819, 1826, 1828, 1829, 1831, 1832, 1834, 1836, 1838 };

                                RandomizeWeapon(1672, 1839, badIndex, WeaponCategory.Charge_Blade, weaponCraft, recipeList, recipeNonRandomList, weaponTree, weaponNonRandomTree, weaponNames, r);

                                byte[] randomizedTreeBytes = StructTools.RawSerialize(weaponTree);
                                Array.Copy(randomizedTreeBytes, 0, treeBytes, 10, randomizedTreeBytes.Length);

                                if (exportWp_dat)
                                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\equip\c_axe.wp_dat", treeBytes);

                                break;
                            }
                        case 1:
                            {
                                using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Weapon Log.txt"))
                                {
                                    file.WriteLine("\n---------------------------------------------------------------------------");
                                    file.WriteLine("                           Dual Blades Changes                             ");
                                    file.WriteLine("---------------------------------------------------------------------------");
                                }

                                treeBytes = ChunkOTF.files["w_sword.wp_dat"].Extract();
                                weaponNames = JsonConvert.DeserializeObject<Dictionary<int, string>>(Encoding.UTF8.GetString(Properties.Resources.Dual_Blades));
                                weaponTree = StructTools.RawDeserialize<RecipeStructs.WeaponTree>(treeBytes, 10);
                                weaponNonRandomTree = StructTools.RawDeserialize<RecipeStructs.WeaponTree>(treeBytes, 10);
                                int[] badIndex = { 573, 631, 709, 722, 729, 732, 739, 742, 744, 746, 747, 749, 750, 752, 754, 756, 757, 759 };

                                RandomizeWeapon(569, 760, badIndex, WeaponCategory.Dual_Blades, weaponCraft, recipeList, recipeNonRandomList, weaponTree, weaponNonRandomTree, weaponNames, r);

                                byte[] randomizedTreeBytes = StructTools.RawSerialize(weaponTree);
                                Array.Copy(randomizedTreeBytes, 0, treeBytes, 10, randomizedTreeBytes.Length);

                                if (exportWp_dat)
                                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\equip\w_sword.wp_dat", treeBytes);
                                break;
                            }
                        case 2:
                            {
                                using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Weapon Log.txt"))
                                {
                                    file.WriteLine("\n---------------------------------------------------------------------------");
                                    file.WriteLine("                           Great Sword Changes                             ");
                                    file.WriteLine("---------------------------------------------------------------------------");
                                }

                                treeBytes = ChunkOTF.files["l_sword.wp_dat"].Extract();
                                weaponNames = JsonConvert.DeserializeObject<Dictionary<int, string>>(Encoding.UTF8.GetString(Properties.Resources.Greatsword));
                                weaponTree = StructTools.RawDeserialize<RecipeStructs.WeaponTree>(treeBytes, 10);
                                weaponNonRandomTree = StructTools.RawDeserialize<RecipeStructs.WeaponTree>(treeBytes, 10);
                                int[] badIndex = { 6, 80, 140, 153, 160, 163, 166, 169, 176, 178, 179, 181, 182, 184, 186, 189 };

                                RandomizeWeapon(2, 190, badIndex, WeaponCategory.Greatsword, weaponCraft, recipeList, recipeNonRandomList, weaponTree, weaponNonRandomTree, weaponNames, r);

                                byte[] randomizedTreeBytes = StructTools.RawSerialize(weaponTree);
                                Array.Copy(randomizedTreeBytes, 0, treeBytes, 10, randomizedTreeBytes.Length);

                                if (exportWp_dat)
                                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\equip\l_sword.wp_dat", treeBytes);
                                break;
                            }
                        case 3:
                            {
                                using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Weapon Log.txt"))
                                {
                                    file.WriteLine("\n---------------------------------------------------------------------------");
                                    file.WriteLine("                             GunLance Changes                              ");
                                    file.WriteLine("---------------------------------------------------------------------------");
                                }

                                treeBytes = ChunkOTF.files["g_lance.wp_dat"].Extract();
                                weaponNames = JsonConvert.DeserializeObject<Dictionary<int, string>>(Encoding.UTF8.GetString(Properties.Resources.Gunlance));
                                weaponTree = StructTools.RawDeserialize<RecipeStructs.WeaponTree>(treeBytes, 10);
                                weaponNonRandomTree = StructTools.RawDeserialize<RecipeStructs.WeaponTree>(treeBytes, 10);
                                int[] badIndex = { 1315, 1320, 1382, 1449, 1460, 1467, 1470, 1477, 1479, 1480, 1482, 1483, 1485, 1487, 1489 };

                                RandomizeWeapon(1316, 1490, badIndex, WeaponCategory.GunLance, weaponCraft, recipeList, recipeNonRandomList, weaponTree, weaponNonRandomTree, weaponNames, r);

                                byte[] randomizedTreeBytes = StructTools.RawSerialize(weaponTree);
                                Array.Copy(randomizedTreeBytes, 0, treeBytes, 10, randomizedTreeBytes.Length);

                                if (exportWp_dat)
                                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\equip\g_lance.wp_dat", treeBytes);
                                break;
                            }
                        case 4:
                            {
                                using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Weapon Log.txt"))
                                {
                                    file.WriteLine("\n---------------------------------------------------------------------------");
                                    file.WriteLine("                             Hammer Changes                                ");
                                    file.WriteLine("---------------------------------------------------------------------------");
                                }

                                treeBytes = ChunkOTF.files["hammer.wp_dat"].Extract();
                                weaponNames = JsonConvert.DeserializeObject<Dictionary<int, string>>(Encoding.UTF8.GetString(Properties.Resources.Hammer));
                                weaponTree = StructTools.RawDeserialize<RecipeStructs.WeaponTree>(treeBytes, 10);
                                weaponNonRandomTree = StructTools.RawDeserialize<RecipeStructs.WeaponTree>(treeBytes, 10);
                                int[] badIndex = { 761, 766, 829, 902, 911, 918, 921, 924, 931, 934, 936, 937, 939, 940, 942, 944, 946, 947 };

                                RandomizeWeapon(762, 948, badIndex, WeaponCategory.Hammer, weaponCraft, recipeList, recipeNonRandomList, weaponTree, weaponNonRandomTree, weaponNames, r);

                                byte[] randomizedTreeBytes = StructTools.RawSerialize(weaponTree);
                                Array.Copy(randomizedTreeBytes, 0, treeBytes, 10, randomizedTreeBytes.Length);

                                if (exportWp_dat)
                                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\equip\hammer.wp_dat", treeBytes);
                                break;
                            }
                        case 5:
                            {
                                using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Weapon Log.txt"))
                                {
                                    file.WriteLine("\n---------------------------------------------------------------------------");
                                    file.WriteLine("                          Hunting Horn Changes                             ");
                                    file.WriteLine("---------------------------------------------------------------------------");
                                }

                                treeBytes = ChunkOTF.files["whistle.wp_dat"].Extract();
                                weaponNames = JsonConvert.DeserializeObject<Dictionary<int, string>>(Encoding.UTF8.GetString(Properties.Resources.Hunting_Horn));
                                weaponTree = StructTools.RawDeserialize<RecipeStructs.WeaponTree>(treeBytes, 10);
                                weaponNonRandomTree = StructTools.RawDeserialize<RecipeStructs.WeaponTree>(treeBytes, 10);
                                int[] badIndex = { 949, 954, 1018, 1090, 1101, 1108, 1111, 1118, 1120, 1121, 1123, 1124, 1126, 1128 };

                                RandomizeWeapon(950, 1129, badIndex, WeaponCategory.Hunting_Horn, weaponCraft, recipeList, recipeNonRandomList, weaponTree, weaponNonRandomTree, weaponNames, r);

                                byte[] randomizedTreeBytes = StructTools.RawSerialize(weaponTree);
                                Array.Copy(randomizedTreeBytes, 0, treeBytes, 10, randomizedTreeBytes.Length);

                                if (exportWp_dat)
                                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\equip\whistle.wp_dat", treeBytes);
                                break;
                            }
                        case 6:
                            {
                                using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Weapon Log.txt"))
                                {
                                    file.WriteLine("\n---------------------------------------------------------------------------");
                                    file.WriteLine("                          Insect Glaive Changes                            ");
                                    file.WriteLine("---------------------------------------------------------------------------");
                                }

                                treeBytes = ChunkOTF.files["rod.wp_dat"].Extract();
                                weaponNames = JsonConvert.DeserializeObject<Dictionary<int, string>>(Encoding.UTF8.GetString(Properties.Resources.Insect_Glaive));
                                weaponTree = StructTools.RawDeserialize<RecipeStructs.WeaponTree>(treeBytes, 10);
                                weaponNonRandomTree = StructTools.RawDeserialize<RecipeStructs.WeaponTree>(treeBytes, 10);
                                int[] badIndex = { 1840, 1845, 1914, 1979, 1989, 1996, 2006, 2009, 2011, 2012, 2014, 2015, 2017, 2019 };

                                RandomizeWeapon(1841, 2020, badIndex, WeaponCategory.Insect_Glaive, weaponCraft, recipeList, recipeNonRandomList, weaponTree, weaponNonRandomTree, weaponNames, r);

                                byte[] randomizedTreeBytes = StructTools.RawSerialize(weaponTree);
                                Array.Copy(randomizedTreeBytes, 0, treeBytes, 10, randomizedTreeBytes.Length);

                                if (exportWp_dat)
                                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\equip\rod.wp_dat", treeBytes);
                                break;
                            }
                        case 7:
                            {
                                using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Weapon Log.txt"))
                                {
                                    file.WriteLine("\n---------------------------------------------------------------------------");
                                    file.WriteLine("                              Lance Changes                                ");
                                    file.WriteLine("---------------------------------------------------------------------------");
                                }

                                treeBytes = ChunkOTF.files["lance.wp_dat"].Extract();
                                weaponNames = JsonConvert.DeserializeObject<Dictionary<int, string>>(Encoding.UTF8.GetString(Properties.Resources.Lance));
                                weaponTree = StructTools.RawDeserialize<RecipeStructs.WeaponTree>(treeBytes, 10);
                                weaponNonRandomTree = StructTools.RawDeserialize<RecipeStructs.WeaponTree>(treeBytes, 10);
                                int[] badIndex = { 1130, 1135, 1204, 1263, 1277, 1284, 1287, 1290, 1297, 1300, 1303, 1306, 1308, 1309, 1311, 1313 };

                                RandomizeWeapon(1131, 1314, badIndex, WeaponCategory.Lance, weaponCraft, recipeList, recipeNonRandomList, weaponTree, weaponNonRandomTree, weaponNames, r);

                                byte[] randomizedTreeBytes = StructTools.RawSerialize(weaponTree);
                                Array.Copy(randomizedTreeBytes, 0, treeBytes, 10, randomizedTreeBytes.Length);

                                if (exportWp_dat)
                                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\equip\lance.wp_dat", treeBytes);
                                break;
                            }
                        case 8:
                            {
                                using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Weapon Log.txt"))
                                {
                                    file.WriteLine("\n---------------------------------------------------------------------------");
                                    file.WriteLine("                            Longsword Changes                              ");
                                    file.WriteLine("---------------------------------------------------------------------------");
                                }

                                treeBytes = ChunkOTF.files["tachi.wp_dat"].Extract();
                                weaponNames = JsonConvert.DeserializeObject<Dictionary<int, string>>(Encoding.UTF8.GetString(Properties.Resources.Longsword));
                                weaponTree = StructTools.RawDeserialize<RecipeStructs.WeaponTree>(treeBytes, 10);
                                weaponNonRandomTree = StructTools.RawDeserialize<RecipeStructs.WeaponTree>(treeBytes, 10);
                                int[] badIndex = { 191, 196, 255, 327, 338, 345, 348, 351, 354, 361, 363, 364, 366, 367, 369, 371, 373 };

                                RandomizeWeapon(192, 374, badIndex, WeaponCategory.LongSword, weaponCraft, recipeList, recipeNonRandomList, weaponTree, weaponNonRandomTree, weaponNames, r);

                                byte[] randomizedTreeBytes = StructTools.RawSerialize(weaponTree);
                                Array.Copy(randomizedTreeBytes, 0, treeBytes, 10, randomizedTreeBytes.Length);

                                if (exportWp_dat)
                                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\equip\tachi.wp_dat", treeBytes);
                                break;
                            }
                        case 9:
                            {
                                using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Weapon Log.txt"))
                                {
                                    file.WriteLine("\n---------------------------------------------------------------------------");
                                    file.WriteLine("                            Switch Axe Changes                             ");
                                    file.WriteLine("---------------------------------------------------------------------------");
                                }

                                treeBytes = ChunkOTF.files["s_axe.wp_dat"].Extract();
                                weaponNames = JsonConvert.DeserializeObject<Dictionary<int, string>>(Encoding.UTF8.GetString(Properties.Resources.Switch_Axe));
                                weaponTree = StructTools.RawDeserialize<RecipeStructs.WeaponTree>(treeBytes, 10);
                                weaponNonRandomTree = StructTools.RawDeserialize<RecipeStructs.WeaponTree>(treeBytes, 10);
                                int[] badIndex = { 1491, 1496, 1566, 1630, 1642, 1649, 1652, 1659, 1661, 1662, 1664, 1665, 1667, 1669 };

                                RandomizeWeapon(1492, 1670, badIndex, WeaponCategory.Switch_Axe, weaponCraft, recipeList, recipeNonRandomList, weaponTree, weaponNonRandomTree, weaponNames, r);

                                byte[] randomizedTreeBytes = StructTools.RawSerialize(weaponTree);
                                Array.Copy(randomizedTreeBytes, 0, treeBytes, 10, randomizedTreeBytes.Length);

                                if (exportWp_dat)
                                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\equip\s_axe.wp_dat", treeBytes);
                                break;
                            }
                        case 10:
                            {
                                using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Weapon Log.txt"))
                                {
                                    file.WriteLine("\n---------------------------------------------------------------------------");
                                    file.WriteLine("                        Sword and Shield Changes                           ");
                                    file.WriteLine("---------------------------------------------------------------------------");
                                }

                                treeBytes = ChunkOTF.files["sword.wp_dat"].Extract();
                                weaponNames = JsonConvert.DeserializeObject<Dictionary<int, string>>(Encoding.UTF8.GetString(Properties.Resources.Sword_and_Shield));
                                weaponTree = StructTools.RawDeserialize<RecipeStructs.WeaponTree>(treeBytes, 10);
                                weaponNonRandomTree = StructTools.RawDeserialize<RecipeStructs.WeaponTree>(treeBytes, 10);
                                int[] badIndex = { 375, 380, 462, 525, 536, 543, 546, 553, 556, 558, 559, 561, 562, 564, 566 };

                                RandomizeWeapon(376, 567, badIndex, WeaponCategory.Sword_and_Shield, weaponCraft, recipeList, recipeNonRandomList, weaponTree, weaponNonRandomTree, weaponNames, r);

                                byte[] randomizedTreeBytes = StructTools.RawSerialize(weaponTree);
                                Array.Copy(randomizedTreeBytes, 0, treeBytes, 10, randomizedTreeBytes.Length);

                                if (exportWp_dat)
                                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\equip\sword.wp_dat", treeBytes);
                                break;
                            }
                    }

                }

                for (int rangedWeapons = 0; rangedWeapons < 3; rangedWeapons++)
                {
                    switch (rangedWeapons)
                    {
                        //Bow
                        case 0:
                            {
                                using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Weapon Log.txt"))
                                {
                                    file.WriteLine("\n---------------------------------------------------------------------------");
                                    file.WriteLine("                               Bow Changes                                 ");
                                    file.WriteLine("---------------------------------------------------------------------------");
                                }

                                treeBytes = ChunkOTF.files["bow.wp_dat_g"].Extract();
                                weaponNames = JsonConvert.DeserializeObject<Dictionary<int, string>>(Encoding.UTF8.GetString(Properties.Resources.Heavy_Bowgun));
                                rangedWeaponTree = StructTools.RawDeserialize<RecipeStructs.RangedWeaponTree>(treeBytes, 10);
                                rangedWeaponNonRandomTree = StructTools.RawDeserialize<RecipeStructs.RangedWeaponTree>(treeBytes, 10);
                                int[] badIndex = { 2368, 2373, 2441, 2501, 2507, 2514, 2517, 2520, 2521, 2522, 2523, 2530, 2532, 2533, 2535, 2536, 2538, 2540, 2542 };

                                RandomizeRangedWeapon(2369, 2543, badIndex, WeaponCategory.Bow, weaponCraft, recipeList, recipeNonRandomList, rangedWeaponTree, rangedWeaponNonRandomTree, weaponNames, r);

                                if (exportWp_dat)
                                {
                                    byte[] randomizedTreeBytes = StructTools.RawSerialize(rangedWeaponTree);
                                    Array.Copy(randomizedTreeBytes, 0, treeBytes, 10, randomizedTreeBytes.Length);
                                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\equip\bow.wp_dat_g", treeBytes);
                                }
                                break;
                            }
                        case 1:
                            {
                                using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Weapon Log.txt"))
                                {
                                    file.WriteLine("\n---------------------------------------------------------------------------");
                                    file.WriteLine("                           Heavy Bowgun Changes                            ");
                                    file.WriteLine("---------------------------------------------------------------------------");
                                }

                                treeBytes = ChunkOTF.files["hbg.wp_dat_g"].Extract();
                                weaponNames = JsonConvert.DeserializeObject<Dictionary<int, string>>(Encoding.UTF8.GetString(Properties.Resources.Heavy_Bowgun));
                                rangedWeaponTree = StructTools.RawDeserialize<RecipeStructs.RangedWeaponTree>(treeBytes, 10);
                                rangedWeaponNonRandomTree = StructTools.RawDeserialize<RecipeStructs.RangedWeaponTree>(treeBytes, 10);
                                int[] badIndex = { 2202, 2207, 2271, 2325, 2339, 2346, 2349, 2356, 2358, 2359, 2361, 2362, 2364, 2366 };

                                RandomizeRangedWeapon(2203, 2367, badIndex, WeaponCategory.Heavy_Bowgun, weaponCraft, recipeList, recipeNonRandomList, rangedWeaponTree, rangedWeaponNonRandomTree, weaponNames, r);

                                if (exportWp_dat)
                                {
                                    byte[] randomizedTreeBytes = StructTools.RawSerialize(rangedWeaponTree);
                                    Array.Copy(randomizedTreeBytes, 0, treeBytes, 10, randomizedTreeBytes.Length);
                                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\equip\hbg.wp_dat_g", treeBytes);
                                }
                                break;
                            }
                        case 2:
                            {
                                using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Weapon Log.txt"))
                                {
                                    file.WriteLine("\n---------------------------------------------------------------------------");
                                    file.WriteLine("                           Light Bowgun Changes                            ");
                                    file.WriteLine("---------------------------------------------------------------------------");
                                }

                                treeBytes = ChunkOTF.files["lbg.wp_dat_g"].Extract();
                                weaponNames = JsonConvert.DeserializeObject<Dictionary<int, string>>(Encoding.UTF8.GetString(Properties.Resources.Light_Bowgun));
                                rangedWeaponTree = StructTools.RawDeserialize<RecipeStructs.RangedWeaponTree>(treeBytes, 10);
                                rangedWeaponNonRandomTree = StructTools.RawDeserialize<RecipeStructs.RangedWeaponTree>(treeBytes, 10);
                                int[] badIndex = { 2021, 2026, 2094, 2153, 2168, 2175, 2178, 2181, 2188, 2190, 2191, 2193, 2194, 2196, 2198, 2199, 2200 };

                                RandomizeRangedWeapon(2022, 2201, badIndex, WeaponCategory.Heavy_Bowgun, weaponCraft, recipeList, recipeNonRandomList, rangedWeaponTree, rangedWeaponNonRandomTree, weaponNames, r);

                                if (exportWp_dat)
                                {
                                    byte[] randomizedTreeBytes = StructTools.RawSerialize(rangedWeaponTree);
                                    Array.Copy(randomizedTreeBytes, 0, treeBytes, 10, randomizedTreeBytes.Length);
                                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\equip\lbg.wp_dat_g", treeBytes);
                                }
                                break;
                            }
                    }
                }

                byte[] randomizedBytes = StructTools.RawSerialize(recipeList);
                Array.Copy(randomizedBytes, 0, recipeBytes, 10, randomizedBytes.Length);
                byte[] randomizedCraftBytes = StructTools.RawSerialize(weaponCraft);
                Array.Copy(randomizedCraftBytes, 0, weaponCraftBytes, 10, randomizedCraftBytes.Length);

                if (IoC.Settings.ShuffleWeaponRecipes || IoC.Settings.ShuffleWeaponOrder)
                {
                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\equip\weapon.eq_cus", recipeBytes);
                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\equip\weapon.eq_crt", weaponCraftBytes);
                }

            }

            #endregion

            #region Shuffle Kinsects

            if (IoC.Settings.ShuffleKinsectRecipes || IoC.Settings.ShuffleKinsectOrder || IoC.Settings.RandomKinsectType || IoC.Settings.RandomKinsectDust)
            {
                File.Create(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Kinsect Log.txt").Dispose();
                using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Kinsect Log.txt"))
                {
                    file.Write("---------------------------------------------------------------------------");
                    file.Write("\n                              Kinsect Recipes                              ");
                    file.Write("\n---------------------------------------------------------------------------");
                }

                byte[] recipeBytes = ChunkOTF.files["insect.eq_cus"].Extract();
                byte[] treeBytes = ChunkOTF.files["rod_insect.rod_inse"].Extract();

                Cipher cipher = new Cipher("SFghFQVFJycHnypExurPwut98ZZq1cwvm7lpDpASeP4biRhstQgULzlb");
                treeBytes = cipher.Decipher(treeBytes);

                byte[] header = new byte[10];
                byte[] treeHeader = new byte[10];
                byte[] treeEnd = new byte[10];
                Array.Copy(recipeBytes, header, 10);
                Array.Copy(treeBytes, treeHeader, 10);
                Array.Copy(treeBytes, treeBytes.Length - 11, treeEnd, 0, 10);
                List<RecipeStructs.Weapon> recipeList = StructTools.RawDeserialize<RecipeStructs.Weapon>(recipeBytes, 10);
                List<RecipeStructs.Weapon> recipeNonRandomList = StructTools.RawDeserialize<RecipeStructs.Weapon>(recipeBytes, 10);
                List<RecipeStructs.KinsectTree> weaponTree = StructTools.RawDeserialize<RecipeStructs.KinsectTree>(treeBytes, 10);
                List<RecipeStructs.KinsectTree> weaponNonRandomTree = StructTools.RawDeserialize<RecipeStructs.KinsectTree>(treeBytes, 10);

                NR3Generator r = new NR3Generator(IoC.Randomizer.Seed);

                bool changeKinsectStats = IoC.Settings.RandomKinsectType || IoC.Settings.RandomKinsectDust || IoC.Settings.RandomKinsectIconColour;
                if (changeKinsectStats)
                {
                    NR3Generator ir = new NR3Generator(IoC.Randomizer.Seed);
                    for (int i = 0; i < weaponTree.Count; i++)
                    {
                        if (IoC.Settings.RandomKinsectType)
                        {
                            int type = ir.Next(0, 2);
                            weaponTree[i].Attack_Type = (byte)type;
                        }
                        if (IoC.Settings.RandomKinsectDust)
                        {
                            int dust = ir.Next(0, 4);
                            weaponTree[i].Dust_Effect = (ushort)dust;
                        }
                        if (IoC.Settings.RandomKinsectIconColour)
                        {
                            weaponTree[i].Rarity = (byte)ir.Next(12);
                        }
                    }
                }
                if (IoC.Settings.ShuffleKinsectRecipes || IoC.Settings.ShuffleKinsectOrder)
                {
                    List<int> validIndexs = new List<int>();
                    for (int rank = 0; rank < 3; rank++)
                    {
                        validIndexs = new List<int>();
                        switch (rank)
                        {
                            //(Don't touch bugs that are bought)
                            //2-6, 19-21, 34-36, 53-57, 70-72, 85-87 Low rank
                            case 0:
                                {
                                    validIndexs.AddRange(Enumerable.Range(2, 5));
                                    validIndexs.AddRange(Enumerable.Range(19, 3));
                                    validIndexs.AddRange(Enumerable.Range(34, 3));
                                    validIndexs.AddRange(Enumerable.Range(53, 5));
                                    validIndexs.AddRange(Enumerable.Range(70, 3));
                                    validIndexs.AddRange(Enumerable.Range(85, 3));
                                    break;
                                }
                            //7-9, 13-15, 22-24, 28-30, 37-39, 43-45, 58-60, 64-66, 73-75, 79-81, 88-90, 94-96, 104 High rank
                            case 1:
                                {
                                    validIndexs.AddRange(Enumerable.Range(7, 3));
                                    validIndexs.AddRange(Enumerable.Range(13, 3));
                                    validIndexs.AddRange(Enumerable.Range(22, 3));
                                    validIndexs.AddRange(Enumerable.Range(28, 3));
                                    validIndexs.AddRange(Enumerable.Range(37, 3));
                                    validIndexs.AddRange(Enumerable.Range(43, 3));
                                    validIndexs.AddRange(Enumerable.Range(58, 3));
                                    validIndexs.AddRange(Enumerable.Range(64, 3));
                                    validIndexs.AddRange(Enumerable.Range(73, 3));
                                    validIndexs.AddRange(Enumerable.Range(79, 3));
                                    validIndexs.AddRange(Enumerable.Range(88, 3));
                                    validIndexs.AddRange(Enumerable.Range(94, 3));
                                    break;
                                }
                            //10-12, 16-18, 25-27, 31-33, 40-42, 46-51, 61-63, 67-69, 76-78, 82-84, 91-93, 97-102, 105 Master rank
                            case 2:
                                {
                                    validIndexs.AddRange(Enumerable.Range(10, 3));
                                    validIndexs.AddRange(Enumerable.Range(16, 3));
                                    validIndexs.AddRange(Enumerable.Range(25, 3));
                                    validIndexs.AddRange(Enumerable.Range(31, 3));
                                    validIndexs.AddRange(Enumerable.Range(40, 3));
                                    validIndexs.AddRange(Enumerable.Range(46, 6));
                                    validIndexs.AddRange(Enumerable.Range(61, 3));
                                    validIndexs.AddRange(Enumerable.Range(67, 3));
                                    validIndexs.AddRange(Enumerable.Range(76, 3));
                                    validIndexs.AddRange(Enumerable.Range(82, 3));
                                    validIndexs.AddRange(Enumerable.Range(91, 3));
                                    validIndexs.AddRange(Enumerable.Range(97, 6));
                                    break;
                                }
                        }

                        ShuffleWeaponRecipes(recipeList, r, validIndexs, IoC.Settings.ShuffleKinsectOrder, IoC.Settings.ShuffleKinsectRecipes);
                    }
                }

                Dictionary<int, string> insectNames = JsonConvert.DeserializeObject<Dictionary<int, string>>(Encoding.UTF8.GetString(Properties.Resources.eng_insectData));
                LogKinsectShuffle(recipeList, recipeNonRandomList, weaponTree, weaponNonRandomTree, insectNames);

                byte[] randomizedBytes = StructTools.RawSerialize(recipeList);
                byte[] randomizedTreeBytes = StructTools.RawSerialize(weaponTree);
                randomizedBytes = header.Concat(randomizedBytes).ToArray();
                randomizedTreeBytes = treeHeader.Concat(randomizedTreeBytes).Concat(treeEnd).ToArray();

                randomizedTreeBytes = cipher.Encipher(randomizedTreeBytes);

                Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\equip\");
                File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\equip\insect.eq_cus", randomizedBytes);
                if (IoC.Settings.ShuffleKinsectOrder || changeKinsectStats)
                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\equip\rod_insect.rod_inse", randomizedTreeBytes);
            }
            #endregion

            #region Palicos

            Dictionary<int, string> palicoWeaponNames = JsonConvert.DeserializeObject<Dictionary<int, string>>(Encoding.UTF8.GetString(Properties.Resources.otomo_weaponData));
            if (IoC.Settings.ShufflePalicoArmour || IoC.Settings.ShufflePalicoWeapons)
            {
                File.Create(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Palico Log.txt").Dispose();
                byte[] recipeBytes = ChunkOTF.files["ot_equip.eq_crt"].Extract();
                List<RecipeStructs.Armour> recipeList = StructTools.RawDeserialize<RecipeStructs.Armour>(recipeBytes, 10);
                NR3Generator r = new NR3Generator(IoC.Randomizer.Seed);
                List<RecipeStructs.Armour> validLowRank = new List<RecipeStructs.Armour>();
                List<RecipeStructs.Armour> validHighRank = new List<RecipeStructs.Armour>();
                List<RecipeStructs.Armour> validMasterRank = new List<RecipeStructs.Armour>();


                if (IoC.Settings.ShufflePalicoArmour)
                {
                    int[] badIDs = { 51, 61, 126, 145, 146, 147, 149, 151, 154, 156, 158, 159, 160, 212, 213, 223, 288, 307, 308, 309, 311, 313, 316, 320, 321, 322 };
                    Dictionary<int, string> armourNames = JsonConvert.DeserializeObject<Dictionary<int, string>>(Encoding.UTF8.GetString(Properties.Resources.otomo_armourData));
                    using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Palico Log.txt"))
                    {
                        file.WriteLine("---------------------------------------------------------------------------");
                        file.WriteLine("                          Palico Armour Recipes                            ");
                        file.WriteLine("---------------------------------------------------------------------------");
                    }
                    List<RecipeStructs.Armour> armourList = recipeList.GetRange(160, recipeList.Count - 160);
                    foreach (RecipeStructs.Armour armour in armourList)
                    {
                        if (badIDs.Contains(armour.Equipment_Index_Raw))
                            continue;
                        switch (armour.Item_Rank)
                        {
                            case 0:
                                {
                                    validLowRank.Add(armour);
                                    break;
                                }
                            case 1:
                                {
                                    validHighRank.Add(armour);
                                    break;
                                }
                            case 2:
                                {
                                    validMasterRank.Add(armour);
                                    break;
                                }
                        }
                    }
                    PalicoShuffle(r, armourNames, validLowRank);
                    PalicoShuffle(r, armourNames, validHighRank);
                    PalicoShuffle(r, armourNames, validMasterRank);
                }

                if (IoC.Settings.ShufflePalicoWeapons)
                {
                    validLowRank = new List<RecipeStructs.Armour>();
                    validHighRank = new List<RecipeStructs.Armour>();
                    validMasterRank = new List<RecipeStructs.Armour>();
                    int[] badIDs = { 50, 54, 55, 60, 130, 149, 150, 151, 153, 155, 158, 162, 163, 164 };
                    using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Palico Log.txt"))
                    {
                        file.WriteLine("---------------------------------------------------------------------------");
                        file.WriteLine("                          Palico Weapon Recipes                            ");
                        file.WriteLine("---------------------------------------------------------------------------");
                    }
                    List<RecipeStructs.Armour> weaponList = recipeList.GetRange(0, 160);
                    foreach (RecipeStructs.Armour armour in weaponList)
                    {
                        if (badIDs.Contains(armour.Equipment_Index_Raw))
                            continue;
                        switch (armour.Item_Rank)
                        {
                            case 0:
                                {
                                    validLowRank.Add(armour);
                                    break;
                                }
                            case 1:
                                {
                                    validHighRank.Add(armour);
                                    break;
                                }
                            case 2:
                                {
                                    validMasterRank.Add(armour);
                                    break;
                                }
                        }
                    }
                    PalicoShuffle(r, palicoWeaponNames, validLowRank);
                    PalicoShuffle(r, palicoWeaponNames, validHighRank);
                    PalicoShuffle(r, palicoWeaponNames, validMasterRank);
                }

                byte[] randomizedBytes = StructTools.RawSerialize(recipeList);
                Array.Copy(randomizedBytes, 0, recipeBytes, 10, randomizedBytes.Length);

                Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\equip\");
                File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\equip\ot_equip.eq_crt", recipeBytes);
            }

            if (IoC.Settings.RandomPalicoWeaponElement || IoC.Settings.RandomPalicoWeaponType || IoC.Settings.RandomPalicoWeaponColour)
            {
                if (!(IoC.Settings.ShufflePalicoArmour || IoC.Settings.ShufflePalicoWeapons))
                    File.Create(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Palico Log.txt").Dispose();

                using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Palico Log.txt"))
                {
                    file.WriteLine("---------------------------------------------------------------------------");
                    file.WriteLine("                          Palico Weapon Element                            ");
                    file.WriteLine("---------------------------------------------------------------------------");
                }

                int[] badIDs = { 50, 54, 55, 60, 130, 149, 150, 151, 153, 155, 158, 162, 163, 164 };
                byte[] weaponBytes = ChunkOTF.files["otomoWeapon.owp_dat"].Extract();
                Cipher cipher = new Cipher("FZoS8QLyOyeFmkdrz73P9Fh2N4NcTwy3QQPjc1YRII5KWovK6yFuU8SL");
                weaponBytes = cipher.Decipher(weaponBytes);

                List<PalicoWeapon> palicoWeapons = StructTools.RawDeserialize<PalicoWeapon>(weaponBytes, 10);

                NR3Generator re = new NR3Generator(IoC.Randomizer.Seed);
                NR3Generator dice = new NR3Generator(IoC.Randomizer.Seed);

                //Elements multiplied by 10
                //Fire = 1
                //Water = 2
                //Ice = 3
                //Thunder = 4
                //Dragon = 5

                //Not multiplied by 10
                //Poison = 6
                //Paralysis = 7
                //Sleep = 8
                //Blast = 9
                using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Palico Log.txt"))
                {
                    foreach (var weapon in palicoWeapons)
                    {
                        if (badIDs.Contains((int)weapon.Index))
                            continue;
                        string logText = "Weapon: " + palicoWeaponNames[(int)weapon.Index];
                        if (IoC.Settings.RandomPalicoWeaponElement)
                        {
                            weapon.Element = (byte)re.Next(10);
                            if (weapon.Element == 0)
                                weapon.Elemental_Damage = 0;
                            else if (weapon.Element < 6)
                            {
                                int roll1 = dice.Next(6 + (weapon.Rarity * 2), 22 + (weapon.Rarity * 3));
                                int roll2 = dice.Next(6 + (weapon.Rarity * 2), 22 + (weapon.Rarity * 3));
                                ushort lowest = (ushort)Math.Min(roll1, roll2);
                                weapon.Elemental_Damage = lowest;
                                logText += "\n\tElement: " + Element[weapon.Element];
                                logText += "\n\tElement Power: " + (weapon.Elemental_Damage * 10);
                            }
                            else
                            {
                                int roll1 = dice.Next(35 + (weapon.Rarity * 2), 50 + (weapon.Rarity * 4));
                                int roll2 = dice.Next(35 + (weapon.Rarity * 2), 50 + (weapon.Rarity * 4));
                                ushort lowest = (ushort)Math.Min(roll1, roll2);
                                weapon.Elemental_Damage = lowest;
                                logText += "\n\tElement: " + Element[weapon.Element];
                                logText += "\n\tElement Power: " + weapon.Elemental_Damage;
                            }
                        }
                        if (IoC.Settings.RandomPalicoWeaponType)
                        {
                            weapon.Attack_Type = (ushort)re.Next(2);
                            logText += "\n\tAttack Type: " + (weapon.Attack_Type == 0 ? "Severing" : "Blunt");
                        }
                        if (IoC.Settings.RandomPalicoWeaponColour)
                        {
                            weapon.Rarity = (byte)re.Next(12);
                            logText += "\n\tRarity (Colour): " + weapon.Rarity;
                        }
                        if (logText.Contains("\n"))
                            file.WriteLine(logText);
                    }
                }
                byte[] randomizedBytes = StructTools.RawSerialize(palicoWeapons);
                Array.Copy(randomizedBytes, 0, weaponBytes, 10, randomizedBytes.Length);
                weaponBytes = cipher.Encipher(weaponBytes);

                Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\equip\");
                File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\equip\otomoWeapon.owp_dat", weaponBytes);

            }

            #endregion
        }

        private static void PalicoShuffle(NR3Generator r, Dictionary<int, string> armourNames, List<RecipeStructs.Armour> armourList)
        {
            Dictionary<int, string> itemNames = JsonConvert.DeserializeObject<Dictionary<int, string>>(Encoding.UTF8.GetString(Properties.Resources.eng_itemData));
            for (int c = armourList.Count - 1; c >= 0; c--)
            {
                armourList[c].Needed_Item_Id_to_Unlock = 1;
                armourList[c].Story_Unlock = 0;
                if (c == 0)
                    break;

                // Pick a random index 
                // from 0 to i 
                int j = r.Next(0, c + 1);

                // Swap arr[i] with the 
                // element at random index
                ushort temp = armourList[c].Equipment_Index_Raw;
                armourList[c].Equipment_Index_Raw = armourList[j].Equipment_Index_Raw;
                armourList[j].Equipment_Index_Raw = temp;


                using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Palico Log.txt"))
                {
                    //Log
                    string recipe = armourNames[armourList[c].Equipment_Index_Raw] + " recipe:\t"
                        + (armourNames[armourList[c].Equipment_Index_Raw].Length < 16 ? "\t" : "")
                        + "Mat1 = " + itemNames[armourList[c].Mat_1_Id * 2]
                        + (itemNames[armourList[c].Mat_1_Id * 2].Length <= 16 ? "\t" : "")
                        + (armourList[c].Mat_2_Id != 0 ? "\t Mat2 = " + itemNames[armourList[c].Mat_2_Id * 2] : "")
                        + (itemNames[armourList[c].Mat_2_Id * 2].Length < 16 ? "\t" : "")
                        + (armourList[c].Mat_3_Id != 0 ? "\t Mat3 = " + itemNames[armourList[c].Mat_3_Id * 2] : "")
                        + (itemNames[armourList[j].Mat_3_Id * 2].Length < 16 ? "\t" : "")
                        + (armourList[c].Mat_4_Id != 0 ? "\t Mat4 = " + itemNames[armourList[c].Mat_4_Id * 2] : "");
                    file.WriteLine(recipe);
                }


            }
        }


        #region Randomize Weapon and Armour Functions

        private static void RandomizeWeapon(int firstIndex, int lastIndex, int[] badIndex, WeaponCategory equipCategory, List<RecipeStructs.Armour> weaponCraft, List<RecipeStructs.Weapon> recipeList, List<RecipeStructs.Weapon> recipeNonRandomList,
            List<RecipeStructs.WeaponTree> weaponTree, List<RecipeStructs.WeaponTree> weaponNonRandomTree, Dictionary<int, string> weaponNames, NR3Generator r)
        {
            List<int> validLowRankIndexs = new List<int>();
            List<int> validHighRankIndexs = new List<int>();
            List<int> validMasterRankIndexs = new List<int>();
            for (int indexRange = firstIndex; indexRange < lastIndex + 1; indexRange++)
            {
                if (badIndex.Contains(indexRange))
                    continue;
                switch (recipeList[indexRange].Item_Rank)
                {
                    case 0:
                        {
                            validLowRankIndexs.Add(indexRange);
                            break;
                        }
                    case 1:
                        {
                            validHighRankIndexs.Add(indexRange);
                            break;
                        }
                    case 2:
                        {
                            validMasterRankIndexs.Add(indexRange);
                            break;
                        }
                }
            }

            ShuffleWeaponRecipes(recipeList, r, validLowRankIndexs, IoC.Settings.ShuffleWeaponOrder, IoC.Settings.ShuffleWeaponRecipes);
            ShuffleWeaponRecipes(recipeList, r, validHighRankIndexs, IoC.Settings.ShuffleWeaponOrder, IoC.Settings.ShuffleWeaponRecipes);
            ShuffleWeaponRecipes(recipeList, r, validMasterRankIndexs, IoC.Settings.ShuffleWeaponOrder, IoC.Settings.ShuffleWeaponRecipes);

            LogWeaponShuffle(firstIndex, lastIndex, (int)equipCategory, badIndex, weaponCraft, recipeList, recipeNonRandomList, weaponTree, weaponNonRandomTree, weaponNames);
        }

        private static void RandomizeRangedWeapon(int firstIndex, int lastIndex, int[] badIndex, WeaponCategory equipCategory, List<RecipeStructs.Armour> weaponCraft, List<RecipeStructs.Weapon> recipeList, List<RecipeStructs.Weapon> recipeNonRandomList,
            List<RecipeStructs.RangedWeaponTree> weaponTree, List<RecipeStructs.RangedWeaponTree> weaponNonRandomTree, Dictionary<int, string> weaponNames, NR3Generator r)
        {
            List<int> validLowRankIndexs = new List<int>();
            List<int> validHighRankIndexs = new List<int>();
            List<int> validMasterRankIndexs = new List<int>();
            for (int indexRange = firstIndex; indexRange < lastIndex + 1; indexRange++)
            {
                if (badIndex.Contains(indexRange))
                    continue;
                switch (recipeList[indexRange].Item_Rank)
                {
                    case 0:
                        {
                            validLowRankIndexs.Add(indexRange);
                            break;
                        }
                    case 1:
                        {
                            validHighRankIndexs.Add(indexRange);
                            break;
                        }
                    case 2:
                        {
                            validMasterRankIndexs.Add(indexRange);
                            break;
                        }
                }
            }

            ShuffleWeaponRecipes(recipeList, r, validLowRankIndexs, IoC.Settings.ShuffleWeaponOrder, IoC.Settings.ShuffleWeaponRecipes);
            ShuffleWeaponRecipes(recipeList, r, validHighRankIndexs, IoC.Settings.ShuffleWeaponOrder, IoC.Settings.ShuffleWeaponRecipes);
            ShuffleWeaponRecipes(recipeList, r, validMasterRankIndexs, IoC.Settings.ShuffleWeaponOrder, IoC.Settings.ShuffleWeaponRecipes);

            LogRangedWeaponShuffle(firstIndex, lastIndex, (int)equipCategory, badIndex, weaponCraft, recipeList, recipeNonRandomList, weaponTree, weaponNonRandomTree, weaponNames);
        }

        private static void ShuffleWeaponRecipes(List<RecipeStructs.Weapon> recipeList, NR3Generator r, List<int> validIDs, bool shuffleOrder, bool shuffleRecipe)
        {
            if (shuffleOrder)
            {
                for (int c = validIDs.Count - 1; c >= 0; c--)
                {
                    recipeList[validIDs[c]].Needed_Item_Id_to_Unlock = 1;
                    recipeList[validIDs[c]].Story_Unlock = 0;
                    if (c == 0)
                        break;

                    // Pick a random index 
                    // from 0 to i 
                    int j = r.Next(0, c + 1);

                    // Swap arr[i] with the 
                    // element at random index
                    ushort temp = recipeList[validIDs[c]].Equipment_Index_Raw;
                    recipeList[validIDs[c]].Equipment_Index_Raw = recipeList[validIDs[j]].Equipment_Index_Raw;
                    recipeList[validIDs[j]].Equipment_Index_Raw = temp;

                }
            }
            if (shuffleRecipe)
            {
                for (int c = validIDs.Count - 1; c >= 0; c--)
                {
                    recipeList[validIDs[c]].Needed_Item_Id_to_Unlock = 1;
                    recipeList[validIDs[c]].Story_Unlock = 0;
                    if (c == 0)
                        break;

                    // Pick a random index 
                    // from 0 to i 
                    int j = r.Next(0, c + 1);

                    // Swap arr[i] with the 
                    // element at random index
                    ushort tempMat1ID = recipeList[validIDs[c]].Mat_1_Id;
                    byte tempMat1Count = recipeList[validIDs[c]].Mat_1_Count;
                    ushort tempMat2ID = recipeList[validIDs[c]].Mat_2_Id;
                    byte tempMat2Count = recipeList[validIDs[c]].Mat_2_Count;
                    ushort tempMat3ID = recipeList[validIDs[c]].Mat_3_Id;
                    byte tempMat3Count = recipeList[validIDs[c]].Mat_3_Count;
                    ushort tempMat4ID = recipeList[validIDs[c]].Mat_4_Id;
                    byte tempMat4Count = recipeList[validIDs[c]].Mat_4_Count;


                    recipeList[validIDs[c]].Mat_1_Id = recipeList[validIDs[j]].Mat_1_Id;
                    recipeList[validIDs[c]].Mat_1_Count = recipeList[validIDs[j]].Mat_1_Count;
                    recipeList[validIDs[c]].Mat_2_Id = recipeList[validIDs[j]].Mat_2_Id;
                    recipeList[validIDs[c]].Mat_2_Count = recipeList[validIDs[j]].Mat_2_Count;
                    recipeList[validIDs[c]].Mat_3_Id = recipeList[validIDs[j]].Mat_3_Id;
                    recipeList[validIDs[c]].Mat_3_Count = recipeList[validIDs[j]].Mat_3_Count;
                    recipeList[validIDs[c]].Mat_4_Id = recipeList[validIDs[j]].Mat_4_Id;
                    recipeList[validIDs[c]].Mat_4_Count = recipeList[validIDs[j]].Mat_4_Count;

                    recipeList[validIDs[j]].Mat_1_Id = tempMat1ID;
                    recipeList[validIDs[j]].Mat_1_Count = tempMat1Count;
                    recipeList[validIDs[j]].Mat_2_Id = tempMat2ID;
                    recipeList[validIDs[j]].Mat_2_Count = tempMat2Count;
                    recipeList[validIDs[j]].Mat_3_Id = tempMat3ID;
                    recipeList[validIDs[j]].Mat_3_Count = tempMat3Count;
                    recipeList[validIDs[j]].Mat_4_Id = tempMat4ID;
                    recipeList[validIDs[j]].Mat_4_Count = tempMat4Count;

                }
            }
        }


        private static void LogWeaponShuffle(int firstIndex, int lastIndex, int equipCategory, int[] badIndex, List<RecipeStructs.Armour> weaponCraft, List<RecipeStructs.Weapon> recipeList, List<RecipeStructs.Weapon> recipeNonRandomList, List<RecipeStructs.WeaponTree> weaponTree, List<RecipeStructs.WeaponTree> weaponNonRandomTree,
                                      Dictionary<int, string> weaponNames)
        {
            Dictionary<int, string> itemNames = JsonConvert.DeserializeObject<Dictionary<int, string>>(Encoding.UTF8.GetString(Properties.Resources.eng_itemData));

            if (IoC.Settings.ShuffleWeaponRecipes)
            {
                using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Weapon Log.txt"))
                {
                    file.WriteLine("\n");

                    List<RecipeStructs.Weapon> sortedList = recipeList.OrderBy(o => recipeNonRandomList.IndexOf(recipeNonRandomList.FirstOrDefault(p => p.Equipment_Index_Raw == o.Equipment_Index_Raw && o.Equipment_Category_Raw == equipCategory))).ToList();

                    for (int p = firstIndex; p < lastIndex + 1; p++)
                    {
                        if (badIndex.Contains(p))
                            continue;
                        //Log
                        string recipe = weaponNames[recipeList[p].Equipment_Index_Raw] + " recipe:\t"
                            + (weaponNames[recipeNonRandomList[p].Equipment_Index_Raw].Length < 16 ? "\t" : "")
                            + "Mat1 = " + itemNames[recipeList[p].Mat_1_Id * 2]
                            + (itemNames[recipeList[p].Mat_1_Id * 2].Length <= 16 ? "\t" : "")
                            + (recipeList[p].Mat_2_Id != 0 ? "\t Mat2 = " + itemNames[recipeList[p].Mat_2_Id * 2] : "")
                            + (itemNames[recipeList[p].Mat_2_Id * 2].Length < 16 ? "\t" : "")
                            + (recipeList[p].Mat_3_Id != 0 ? "\t Mat3 = " + itemNames[recipeList[p].Mat_3_Id * 2] : "")
                            + (itemNames[recipeList[p].Mat_3_Id * 2].Length < 16 ? "\t" : "")
                            + (recipeList[p].Mat_4_Id != 0 ? "\t Mat4 = " + itemNames[recipeList[p].Mat_4_Id * 2] : "");
                        file.WriteLine(recipe);
                    }
                }
            }

            using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Weapon Log.txt"))
            {
                file.WriteLine("\n");
                NR3Generator r = new NR3Generator(IoC.Randomizer.Seed);
                NR3Generator re = new NR3Generator(IoC.Randomizer.Seed);
                NR3Generator dice = new NR3Generator(IoC.Randomizer.Seed);
                for (int p = firstIndex; p < lastIndex + 1; p++)
                {
                    var weaponTreePos = weaponTree.FirstOrDefault(o => o.Index == recipeList[p].Equipment_Index_Raw);
                    string log = "";
                    if (IoC.Settings.ShuffleWeaponOrder)
                    {
                        var wep = weaponCraft.FirstOrDefault(o => o.Equipment_Index_Raw == recipeNonRandomList[p].Equipment_Index_Raw && o.Equipment_Category_Raw == recipeList[p].Equipment_Category_Raw);
                        if (wep != null && !badIndex.Contains(p))
                        {
                            wep.Equipment_Index_Raw = recipeList[p].Equipment_Index_Raw;
                            wep.Needed_Item_Id_to_Unlock = 1;
                        }

                        //Fix position
                        var positionNonRandomTree = weaponNonRandomTree.FirstOrDefault(o => o.Index == recipeNonRandomList[p].Equipment_Index_Raw);
                        weaponTreePos.Tree_Id = positionNonRandomTree.Tree_Id;
                        weaponTreePos.Tree_Position = positionNonRandomTree.Tree_Position;

                        //31 18
                        //Log
                        string original = "\nOriginal Weapon: " + weaponNames[recipeNonRandomList[p].Equipment_Index_Raw];
                        if (original.Length < 32)
                            original = original + "\t";
                        log += original + "\tShuffled Weapon: " + weaponNames[recipeList[p].Equipment_Index_Raw];
                    }
                    if (IoC.Settings.RandomWeaponElement)
                    {
                        weaponTreePos.Element = (byte)re.Next(10);
                        if (weaponTreePos.Element == 0)
                            weaponTreePos.Element_Damage = 0;
                        else
                        {
                            int roll1 = dice.Next(6 + (weaponTreePos.Rarity * 2), 22 + (weaponTreePos.Rarity * 3));
                            int roll2 = dice.Next(6 + (weaponTreePos.Rarity * 2), 22 + (weaponTreePos.Rarity * 3));
                            ushort lowest = (ushort)Math.Min(roll1, roll2);
                            weaponTreePos.Element_Damage = lowest;
                            log += "\n\tElement: " + Element[weaponTreePos.Element];
                            log += "\n\tElement Power: " + (weaponTreePos.Element_Damage * 10);
                        }
                        weaponTreePos.Element_Hidden = 0;
                        weaponTreePos.Element_Hidden_Damage = 0;
                    }

                    #region Weapon Slot Count

                    //Armour Decoration Slot Count
                    if (IoC.Settings.RandomWeaponDecoSlots)
                    {
                        int[] weightTable = { 50 - weaponTreePos.Rarity * 4, 40 - weaponTreePos.Rarity * 3, 10 + (weaponTreePos.Rarity - 1) * 2, 10 + (int)Math.Round(((double)weaponTreePos.Rarity - 1) * 1.5) };

                        byte slotCount = (byte)r.Next(weightTable.Sum());
                        byte Smallestslot = 0;

                        int cumulative_weight = 0;
                        for (byte i = 0; i < 4; i++)
                        {
                            cumulative_weight += weightTable[i];
                            if (slotCount < cumulative_weight)
                            {
                                Smallestslot = i;
                                break;
                            }
                        }
                        weaponTreePos.Slot_Count = Smallestslot;
                        if (Smallestslot != 0)
                            log += "\n\tDeco Slot Count: " + Smallestslot;

                        switch (Smallestslot)
                        {
                            case 0:
                                {
                                    weaponTreePos.Slot_1_Size = 0;
                                    weaponTreePos.Slot_2_Size = 0;
                                    weaponTreePos.Slot_3_Size = 0;
                                    break;
                                }
                            case 1:
                                {
                                    if (weaponTreePos.Slot_1_Size == 0)
                                        weaponTreePos.Slot_1_Size = 1;

                                    weaponTreePos.Slot_2_Size = 0;
                                    weaponTreePos.Slot_3_Size = 0;

                                    if (!IoC.Settings.RandomWeaponDecoSlotSize)
                                        log += "\n\tSlot 1 Size: " + weaponTreePos.Slot_1_Size;
                                    break;
                                }
                            case 2:
                                {
                                    if (weaponTreePos.Slot_1_Size == 0)
                                        weaponTreePos.Slot_1_Size = 1;

                                    if (weaponTreePos.Slot_2_Size == 0)
                                        weaponTreePos.Slot_2_Size = 1;

                                    weaponTreePos.Slot_3_Size = 0;

                                    if (!IoC.Settings.RandomWeaponDecoSlotSize)
                                    {
                                        log += "\n\tSlot 1 Size: " + weaponTreePos.Slot_1_Size;
                                        log += "\n\tSlot 2 Size: " + weaponTreePos.Slot_2_Size;
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    if (weaponTreePos.Slot_1_Size == 0)
                                        weaponTreePos.Slot_1_Size = 1;

                                    if (weaponTreePos.Slot_2_Size == 0)
                                        weaponTreePos.Slot_2_Size = 1;

                                    if (weaponTreePos.Slot_3_Size == 0)
                                        weaponTreePos.Slot_3_Size = 1;

                                    if (!IoC.Settings.RandomWeaponDecoSlotSize)
                                    {
                                        log += "\n\tSlot 1 Size: " + weaponTreePos.Slot_1_Size;
                                        log += "\n\tSlot 2 Size: " + weaponTreePos.Slot_2_Size;
                                        log += "\n\tSlot 3 Size: " + weaponTreePos.Slot_3_Size;
                                    }
                                    break;
                                }
                        }
                    }

                    #endregion

                    #region Weapon Slot Size

                    //Weapon Decoration Slot Size
                    if (IoC.Settings.RandomWeaponDecoSlotSize)
                    {
                        int[] weightTable = { 50 - weaponTreePos.Rarity * 4, 40 - weaponTreePos.Rarity * 3, 10 + (weaponTreePos.Rarity - 1) * 2, 10 + (int)Math.Round(((double)weaponTreePos.Rarity - 1) * 1.5) };

                        int cumulative_weight;
                        switch (weaponTreePos.Slot_Count)
                        {
                            case 0:
                                {
                                    break;
                                }
                            case 1:
                                {
                                    byte slotCountTes = (byte)r.Next(weightTable.Sum());
                                    cumulative_weight = 0;
                                    for (byte i = 0; i < 4; i++)
                                    {
                                        cumulative_weight += weightTable[i];
                                        if (slotCountTes < cumulative_weight)
                                        {
                                            weaponTreePos.Slot_1_Size = (byte)(i + 1);
                                            log += "\n\tSlot 1 Size: " + weaponTreePos.Slot_1_Size;
                                            break;
                                        }
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    byte slotCountTes = (byte)r.Next(weightTable.Sum());
                                    cumulative_weight = 0;
                                    for (byte i = 0; i < 4; i++)
                                    {
                                        cumulative_weight += weightTable[i];
                                        if (slotCountTes < cumulative_weight)
                                        {
                                            weaponTreePos.Slot_1_Size = (byte)(i + 1);
                                            break;
                                        }
                                    }

                                    slotCountTes = (byte)r.Next(weightTable.Sum());
                                    cumulative_weight = 0;
                                    for (byte i = 0; i < 4; i++)
                                    {
                                        cumulative_weight += weightTable[i];
                                        if (slotCountTes < cumulative_weight)
                                        {
                                            weaponTreePos.Slot_2_Size = (byte)(i + 1);
                                            break;
                                        }
                                    }
                                    log += "\n\tSlot 1 Size: " + weaponTreePos.Slot_1_Size;
                                    log += "\n\tSlot 2 Size: " + weaponTreePos.Slot_2_Size;
                                    break;
                                }
                            case 3:
                                {
                                    byte slotCountTes = (byte)r.Next(weightTable.Sum());
                                    cumulative_weight = 0;
                                    for (byte i = 0; i < 4; i++)
                                    {
                                        cumulative_weight += weightTable[i];
                                        if (slotCountTes < cumulative_weight)
                                        {
                                            weaponTreePos.Slot_1_Size = (byte)(i + 1);
                                            break;
                                        }
                                    }

                                    slotCountTes = (byte)r.Next(weightTable.Sum());
                                    cumulative_weight = 0;
                                    for (byte i = 0; i < 4; i++)
                                    {
                                        cumulative_weight += weightTable[i];
                                        if (slotCountTes < cumulative_weight)
                                        {
                                            weaponTreePos.Slot_2_Size = (byte)(i + 1);
                                            break;
                                        }
                                    }

                                    slotCountTes = (byte)r.Next(weightTable.Sum());
                                    cumulative_weight = 0;
                                    for (byte i = 0; i < 4; i++)
                                    {
                                        cumulative_weight += weightTable[i];
                                        if (slotCountTes < cumulative_weight)
                                        {
                                            weaponTreePos.Slot_3_Size = (byte)(i + 1);
                                            break;
                                        }
                                    }
                                    log += "\n\tSlot 1 Size: " + weaponTreePos.Slot_1_Size;
                                    log += "\n\tSlot 2 Size: " + weaponTreePos.Slot_2_Size;
                                    log += "\n\tSlot 3 Size: " + weaponTreePos.Slot_3_Size;
                                    break;
                                }
                        }
                    }

                    #endregion

                    //Change icon colour
                    if (IoC.Settings.RandomWeaponIconColour)
                    {
                        weaponTreePos.Rarity = (byte)r.Next(12);
                        log += "\n\tRarity (Colour): " + weaponTreePos.Rarity;
                    }

                    if (!string.IsNullOrEmpty(log))
                        file.WriteLine("Weapon: " + weaponNames[recipeList[p].Equipment_Index_Raw] + log);
                }
            }
        }

        private static void LogRangedWeaponShuffle(int firstIndex, int lastIndex, int equipCategory, int[] badIndex, List<RecipeStructs.Armour> weaponCraft, List<RecipeStructs.Weapon> recipeList, List<RecipeStructs.Weapon> recipeNonRandomList, List<RecipeStructs.RangedWeaponTree> weaponTree, List<RecipeStructs.RangedWeaponTree> weaponNonRandomTree,
                                      Dictionary<int, string> weaponNames)
        {
            Dictionary<int, string> itemNames = JsonConvert.DeserializeObject<Dictionary<int, string>>(Encoding.UTF8.GetString(Properties.Resources.eng_itemData));

            if (IoC.Settings.ShuffleWeaponRecipes)
            {
                using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Weapon Log.txt"))
                {
                    file.WriteLine("\n");

                    List<RecipeStructs.Weapon> sortedList = recipeList.OrderBy(o => recipeNonRandomList.IndexOf(recipeNonRandomList.FirstOrDefault(p => p.Equipment_Index_Raw == o.Equipment_Index_Raw && o.Equipment_Category_Raw == equipCategory))).ToList();

                    for (int p = firstIndex; p < lastIndex + 1; p++)
                    {
                        if (badIndex.Contains(p))
                            continue;
                        //Log
                        string recipe = weaponNames[recipeList[p].Equipment_Index_Raw] + " recipe:\t"
                            + (weaponNames[recipeNonRandomList[p].Equipment_Index_Raw].Length < 16 ? "\t" : "")
                            + "Mat1 = " + itemNames[recipeList[p].Mat_1_Id * 2]
                            + (itemNames[recipeList[p].Mat_1_Id * 2].Length <= 16 ? "\t" : "")
                            + (recipeList[p].Mat_2_Id != 0 ? "\t Mat2 = " + itemNames[recipeList[p].Mat_2_Id * 2] : "")
                            + (itemNames[recipeList[p].Mat_2_Id * 2].Length < 16 ? "\t" : "")
                            + (recipeList[p].Mat_3_Id != 0 ? "\t Mat3 = " + itemNames[recipeList[p].Mat_3_Id * 2] : "")
                            + (itemNames[recipeList[p].Mat_3_Id * 2].Length < 16 ? "\t" : "")
                            + (recipeList[p].Mat_4_Id != 0 ? "\t Mat4 = " + itemNames[recipeList[p].Mat_4_Id * 2] : "");
                        file.WriteLine(recipe);
                    }
                }
            }

            using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Weapon Log.txt"))
            {
                file.WriteLine("\n");
                NR3Generator r = new NR3Generator(IoC.Randomizer.Seed);
                NR3Generator re = new NR3Generator(IoC.Randomizer.Seed);
                NR3Generator dice = new NR3Generator(IoC.Randomizer.Seed);
                for (int p = firstIndex; p < lastIndex + 1; p++)
                {
                    var weaponTreePos = weaponTree.FirstOrDefault(o => o.Index == recipeList[p].Equipment_Index_Raw);
                    string log = "";
                    if (IoC.Settings.ShuffleWeaponOrder)
                    {
                        var wep = weaponCraft.FirstOrDefault(o => o.Equipment_Index_Raw == recipeNonRandomList[p].Equipment_Index_Raw && o.Equipment_Category_Raw == recipeList[p].Equipment_Category_Raw);
                        if (wep != null && !badIndex.Contains(p))
                        {
                            wep.Equipment_Index_Raw = recipeList[p].Equipment_Index_Raw;
                            wep.Needed_Item_Id_to_Unlock = 1;
                        }

                        //Fix position
                        var positionNonRandomTree = weaponNonRandomTree.FirstOrDefault(o => o.Index == recipeNonRandomList[p].Equipment_Index_Raw);
                        weaponTreePos.Tree_Id = positionNonRandomTree.Tree_Id;
                        weaponTreePos.Tree_Position = positionNonRandomTree.Tree_Position;

                        //31 18
                        //Log
                        string original = "\nOriginal Weapon: " + weaponNames[recipeNonRandomList[p].Equipment_Index_Raw];
                        if (original.Length < 32)
                            original = original + "\t";
                        log += original + "\tShuffled Weapon: " + weaponNames[recipeList[p].Equipment_Index_Raw];
                    }
                    if (IoC.Settings.RandomWeaponElement)
                    {
                        weaponTreePos.Element = (byte)re.Next(10);
                        if (weaponTreePos.Element == 0)
                            weaponTreePos.Element_Damage = 0;
                        else
                        {
                            int roll1 = dice.Next(6 + (weaponTreePos.Rarity * 2), 22 + (weaponTreePos.Rarity * 3));
                            int roll2 = dice.Next(6 + (weaponTreePos.Rarity * 2), 22 + (weaponTreePos.Rarity * 3));
                            ushort lowest = (ushort)Math.Min(roll1, roll2);
                            weaponTreePos.Element_Damage = lowest;
                            log += "\n\tElement: " + Element[weaponTreePos.Element];
                            log += "\n\tElement Power: " + (weaponTreePos.Element_Damage * 10);
                        }
                        weaponTreePos.Element_Hidden = 0;
                        weaponTreePos.Element_Hidden_Damage = 0;
                    }

                    #region Weapon Slot Count

                    //Armour Decoration Slot Count
                    if (IoC.Settings.RandomWeaponDecoSlots)
                    {
                        int[] weightTable = { 50 - weaponTreePos.Rarity * 4, 40 - weaponTreePos.Rarity * 3, 10 + (weaponTreePos.Rarity - 1) * 2, 10 + (int)Math.Round(((double)weaponTreePos.Rarity - 1) * 1.5) };

                        byte slotCount = (byte)r.Next(weightTable.Sum());
                        byte Smallestslot = 0;

                        int cumulative_weight = 0;
                        for (byte i = 0; i < 4; i++)
                        {
                            cumulative_weight += weightTable[i];
                            if (slotCount < cumulative_weight)
                            {
                                Smallestslot = i;
                                break;
                            }
                        }
                        weaponTreePos.Slot_Count = Smallestslot;
                        if (Smallestslot != 0)
                            log += "\n\tDeco Slot Count: " + Smallestslot;

                        switch (Smallestslot)
                        {
                            case 0:
                                {
                                    weaponTreePos.Slot_1_Size = 0;
                                    weaponTreePos.Slot_2_Size = 0;
                                    weaponTreePos.Slot_3_Size = 0;
                                    break;
                                }
                            case 1:
                                {
                                    if (weaponTreePos.Slot_1_Size == 0)
                                        weaponTreePos.Slot_1_Size = 1;

                                    weaponTreePos.Slot_2_Size = 0;
                                    weaponTreePos.Slot_3_Size = 0;

                                    if (!IoC.Settings.RandomWeaponDecoSlotSize)
                                        log += "\n\tSlot 1 Size: " + weaponTreePos.Slot_1_Size;
                                    break;
                                }
                            case 2:
                                {
                                    if (weaponTreePos.Slot_1_Size == 0)
                                        weaponTreePos.Slot_1_Size = 1;

                                    if (weaponTreePos.Slot_2_Size == 0)
                                        weaponTreePos.Slot_2_Size = 1;

                                    weaponTreePos.Slot_3_Size = 0;

                                    if (!IoC.Settings.RandomWeaponDecoSlotSize)
                                    {
                                        log += "\n\tSlot 1 Size: " + weaponTreePos.Slot_1_Size;
                                        log += "\n\tSlot 2 Size: " + weaponTreePos.Slot_2_Size;
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    if (weaponTreePos.Slot_1_Size == 0)
                                        weaponTreePos.Slot_1_Size = 1;

                                    if (weaponTreePos.Slot_2_Size == 0)
                                        weaponTreePos.Slot_2_Size = 1;

                                    if (weaponTreePos.Slot_3_Size == 0)
                                        weaponTreePos.Slot_3_Size = 1;

                                    if (!IoC.Settings.RandomWeaponDecoSlotSize)
                                    {
                                        log += "\n\tSlot 1 Size: " + weaponTreePos.Slot_1_Size;
                                        log += "\n\tSlot 2 Size: " + weaponTreePos.Slot_2_Size;
                                        log += "\n\tSlot 3 Size: " + weaponTreePos.Slot_3_Size;
                                    }
                                    break;
                                }
                        }
                    }

                    #endregion

                    #region Weapon Slot Size

                    //Weapon Decoration Slot Size
                    if (IoC.Settings.RandomWeaponDecoSlotSize)
                    {
                        int[] weightTable = { 50 - weaponTreePos.Rarity * 4, 40 - weaponTreePos.Rarity * 3, 10 + (weaponTreePos.Rarity - 1) * 2, 10 + (int)Math.Round(((double)weaponTreePos.Rarity - 1) * 1.5) };

                        int cumulative_weight;
                        switch (weaponTreePos.Slot_Count)
                        {
                            case 0:
                                {
                                    break;
                                }
                            case 1:
                                {
                                    byte slotCountTes = (byte)r.Next(weightTable.Sum());
                                    cumulative_weight = 0;
                                    for (byte i = 0; i < 4; i++)
                                    {
                                        cumulative_weight += weightTable[i];
                                        if (slotCountTes < cumulative_weight)
                                        {
                                            weaponTreePos.Slot_1_Size = (byte)(i + 1);
                                            log += "\n\tSlot 1 Size: " + weaponTreePos.Slot_1_Size;
                                            break;
                                        }
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    byte slotCountTes = (byte)r.Next(weightTable.Sum());
                                    cumulative_weight = 0;
                                    for (byte i = 0; i < 4; i++)
                                    {
                                        cumulative_weight += weightTable[i];
                                        if (slotCountTes < cumulative_weight)
                                        {
                                            weaponTreePos.Slot_1_Size = (byte)(i + 1);
                                            break;
                                        }
                                    }

                                    slotCountTes = (byte)r.Next(weightTable.Sum());
                                    cumulative_weight = 0;
                                    for (byte i = 0; i < 4; i++)
                                    {
                                        cumulative_weight += weightTable[i];
                                        if (slotCountTes < cumulative_weight)
                                        {
                                            weaponTreePos.Slot_2_Size = (byte)(i + 1);
                                            break;
                                        }
                                    }
                                    log += "\n\tSlot 1 Size: " + weaponTreePos.Slot_1_Size;
                                    log += "\n\tSlot 2 Size: " + weaponTreePos.Slot_2_Size;
                                    break;
                                }
                            case 3:
                                {
                                    byte slotCountTes = (byte)r.Next(weightTable.Sum());
                                    cumulative_weight = 0;
                                    for (byte i = 0; i < 4; i++)
                                    {
                                        cumulative_weight += weightTable[i];
                                        if (slotCountTes < cumulative_weight)
                                        {
                                            weaponTreePos.Slot_1_Size = (byte)(i + 1);
                                            break;
                                        }
                                    }

                                    slotCountTes = (byte)r.Next(weightTable.Sum());
                                    cumulative_weight = 0;
                                    for (byte i = 0; i < 4; i++)
                                    {
                                        cumulative_weight += weightTable[i];
                                        if (slotCountTes < cumulative_weight)
                                        {
                                            weaponTreePos.Slot_2_Size = (byte)(i + 1);
                                            break;
                                        }
                                    }

                                    slotCountTes = (byte)r.Next(weightTable.Sum());
                                    cumulative_weight = 0;
                                    for (byte i = 0; i < 4; i++)
                                    {
                                        cumulative_weight += weightTable[i];
                                        if (slotCountTes < cumulative_weight)
                                        {
                                            weaponTreePos.Slot_3_Size = (byte)(i + 1);
                                            break;
                                        }
                                    }
                                    log += "\n\tSlot 1 Size: " + weaponTreePos.Slot_1_Size;
                                    log += "\n\tSlot 2 Size: " + weaponTreePos.Slot_2_Size;
                                    log += "\n\tSlot 3 Size: " + weaponTreePos.Slot_3_Size;
                                    break;
                                }
                        }
                    }

                    #endregion

                    //Change icon colour
                    if (IoC.Settings.RandomWeaponIconColour)
                    {
                        weaponTreePos.Rarity = (byte)r.Next(12);
                        log += "\n\tRarity (Colour): " + weaponTreePos.Rarity;
                    }

                    if (!string.IsNullOrEmpty(log))
                        file.WriteLine("Weapon: " + weaponNames[recipeList[p].Equipment_Index_Raw] + log);

                }
            }
        }

        private static void LogKinsectShuffle(List<RecipeStructs.Weapon> recipeList, List<RecipeStructs.Weapon> recipeNonRandomList, List<RecipeStructs.KinsectTree> weaponTree, List<RecipeStructs.KinsectTree> weaponNonRandomTree,
                                      Dictionary<int, string> weaponNames)
        {
            Dictionary<int, string> itemNames = JsonConvert.DeserializeObject<Dictionary<int, string>>(Encoding.UTF8.GetString(Properties.Resources.eng_itemData));

            if (IoC.Settings.ShuffleKinsectRecipes)
            {
                using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Kinsect Log.txt"))
                {
                    file.WriteLine("\n");

                    List<RecipeStructs.Weapon> sortedList = recipeList.OrderBy(o => recipeNonRandomList.IndexOf(recipeNonRandomList.FirstOrDefault(p => p.Equipment_Index_Raw == o.Equipment_Index_Raw))).ToList();

                    for (int p = 2; p < sortedList.Count; p++)
                    {
                        if (p == 52 || p == 103)
                            continue;
                        //Log
                        string recipe = weaponNames[sortedList[p].Equipment_Index_Raw] + " recipe:\t"
                            + (weaponNames[recipeNonRandomList[p].Equipment_Index_Raw].Length < 16 ? "\t" : "")
                            + "Mat1 = " + itemNames[sortedList[p].Mat_1_Id * 2]
                            + (itemNames[sortedList[p].Mat_1_Id * 2].Length <= 16 ? "\t" : "")
                            + (sortedList[p].Mat_2_Id != 0 ? "\t Mat2 = " + itemNames[sortedList[p].Mat_2_Id * 2] : "")
                            + (itemNames[sortedList[p].Mat_2_Id * 2].Length < 16 ? "\t" : "")
                            + (sortedList[p].Mat_3_Id != 0 ? "\t Mat3 = " + itemNames[sortedList[p].Mat_3_Id * 2] : "")
                            + (itemNames[sortedList[p].Mat_3_Id * 2].Length < 16 ? "\t" : "")
                            + (sortedList[p].Mat_4_Id != 0 ? "\t Mat4 = " + itemNames[sortedList[p].Mat_4_Id * 2] : "");
                        file.WriteLine(recipe);
                    }
                }
            }

            if (IoC.Settings.ShuffleKinsectOrder)
            {
                using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Kinsect Log.txt"))
                {
                    file.WriteLine("\n");
                    for (int p = 2; p < recipeList.Count; p++)
                    {
                        //Fix position
                        var positionNonRandomTree = weaponNonRandomTree.FirstOrDefault(o => o.Index == recipeNonRandomList[p].Equipment_Index_Raw);
                        weaponTree.FirstOrDefault(o => o.Index == recipeList[p].Equipment_Index_Raw).Tree_Position_Id = positionNonRandomTree.Tree_Position_Id;
                        weaponTree.FirstOrDefault(o => o.Index == recipeList[p].Equipment_Index_Raw).Tree_Id = positionNonRandomTree.Tree_Id;
                        weaponTree.FirstOrDefault(o => o.Index == recipeList[p].Equipment_Index_Raw).Tree_Position = positionNonRandomTree.Tree_Position;

                        //31 18
                        //Log
                        string original = "Original Kinsect: " + weaponNames[recipeNonRandomList[p].Equipment_Index_Raw];
                        if (original.Length < 32)
                            original = original + "\t";
                        file.WriteLine(original + "\tShuffled Kinsect: " + weaponNames[recipeList[p].Equipment_Index_Raw]);
                    }

                }
            }
        }

        //Recipe shuffle function
        private static void ShuffleArmourRecipes(List<RecipeStructs.Armour> recipeList, NR3Generator r, List<int> validIDs)
        {
            Dictionary<int, string> itemNames = JsonConvert.DeserializeObject<Dictionary<int, string>>(Encoding.UTF8.GetString(Properties.Resources.eng_itemData));
            Dictionary<int, string> armourNames = JsonConvert.DeserializeObject<Dictionary<int, string>>(Encoding.UTF8.GetString(Properties.Resources.eng_armorData));

            for (int c = validIDs.Count - 1; c >= 0; c--)
            {
                recipeList[validIDs[c]].Needed_Item_Id_to_Unlock = 1;
                recipeList[validIDs[c]].Story_Unlock = 0;
                if (c == 0)
                    break;

                // Pick a random index 
                // from 0 to i 
                int j = r.Next(0, c + 1);

                // Swap arr[i] with the 
                // element at random index
                ushort temp = recipeList[validIDs[c]].Equipment_Index_Raw;
                recipeList[validIDs[c]].Equipment_Index_Raw = recipeList[validIDs[j]].Equipment_Index_Raw;
                recipeList[validIDs[j]].Equipment_Index_Raw = temp;


                using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Armour Log.txt"))
                {
                    //Log
                    string recipe = armourNames[recipeList[validIDs[c]].Equipment_Index_Raw] + " recipe:\t"
                        + (armourNames[recipeList[validIDs[c]].Equipment_Index_Raw].Length < 16 ? "\t" : "")
                        + "Mat1 = " + itemNames[recipeList[validIDs[c]].Mat_1_Id * 2]
                        + (itemNames[recipeList[validIDs[c]].Mat_1_Id * 2].Length <= 16 ? "\t" : "")
                        + (recipeList[validIDs[c]].Mat_2_Id != 0 ? "\t Mat2 = " + itemNames[recipeList[validIDs[c]].Mat_2_Id * 2] : "")
                        + (itemNames[recipeList[validIDs[c]].Mat_2_Id * 2].Length < 16 ? "\t" : "")
                        + (recipeList[validIDs[c]].Mat_3_Id != 0 ? "\t Mat3 = " + itemNames[recipeList[validIDs[c]].Mat_3_Id * 2] : "")
                        + (itemNames[recipeList[validIDs[j]].Mat_3_Id * 2].Length < 16 ? "\t" : "")
                        + (recipeList[validIDs[c]].Mat_4_Id != 0 ? "\t Mat4 = " + itemNames[recipeList[validIDs[c]].Mat_4_Id * 2] : "");
                    file.WriteLine(recipe);
                }


            }
        }

        private static void RandomizeArmourDat(List<RecipeStructs.ArmourDat> armourDats)
        {
            int[] badIndex = {125, 127, 129, 130, 131, 133, 135, 137, 140, 141, 146, 147, 155, 156, 159, 161, 162, 163, 165, 176, 198, 225, 445, 454, 637, 789, 790, 795, 796, 804, 805, 808, 810, 811, 812, 814, 825, 847, 863, 1064, 1073, 1251, 1407, 1408, 1420, 1423, 1424, 1425, 1426, 1428, 1429, 1430, 1432, 1443, 1465, 1481,
                              1681, 1690, 1866, 2019, 2020, 2032, 2035, 2036, 2037, 2038, 2040, 2041, 2042, 2044, 2055, 2077, 2093, 2290, 2299, 2457, 2631, 2632, 2644, 2647, 2648, 2649, 2650, 2652, 2653, 2654, 2656, 2667, 2689, 2705, 2905, 2914, 3090, 3124, 3125, 3126, 3127, 3155, 3156, 3157, 3224, 3291, 3349, 3351, 3368,
                              3369, 3370, 3371, 3372, 3373, 3374, 3375, 3376, 3377, 3378, 3461, 455, 636, 640, 644, 645, 646, 647, 648, 649, 650, 651, 652, 653, 654, 655, 656, 657, 658, 659, 660, 661, 662, 663, 664, 665, 666, 1074, 1250, 1254, 1261, 1262, 1263, 1264, 1265, 1266, 1267, 1268, 1269, 1270, 1271, 1272, 1273, 
                              1274, 1275, 1276, 1277, 1278, 1279, 1280, 1281, 1282, 1283, 1691, 1865, 1869, 1876, 1877, 1878, 1879, 1880, 1881, 1882, 1883, 1884, 1885, 1886, 1887, 1888, 1889, 1890, 1891, 1892, 1893, 1894, 1895, 1896, 1897, 1898, 2300, 2474, 2478, 2485, 2486, 2487, 2488, 2489, 2490, 2491, 2492, 2493, 
                              2494, 2495, 2496, 2497, 2498, 2499, 2500, 2501, 2502, 2503, 2504, 2505, 2506, 2507, 2915, 3089, 3093, 3100, 3101, 3102, 3103, 3104, 3105, 3106, 3107, 3108, 3109, 3110, 3111, 3112, 3113, 3114, 3115, 3116, 3117, 3118, 3119, 3120, 3121, 3122, 3463, 3464, 3465, 3466, 3467, 3468, 3469, 3470, 
                              3471, 3472, 3473, 3474, 3475, 3476, 3477, 3478, 3479};

            NR3Generator r = new NR3Generator(IoC.Randomizer.Seed);

            Dictionary<uint, string> skillNames = JsonConvert.DeserializeObject<Dictionary<uint, string>>(Encoding.UTF8.GetString(Properties.Resources.eng_skillData));
            Dictionary<uint, string> fullArmourList = JsonConvert.DeserializeObject<Dictionary<uint, string>>(Encoding.UTF8.GetString(Properties.Resources.eng_armorData));

            File.Create(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Armour Data Log.txt").Dispose();
            using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Armour Data Log.txt"))
            {
                file.Write("---------------------------------------------------------------------------");
                file.Write("\n                              Armour Data                                 ");
                file.Write("\n---------------------------------------------------------------------------\n");
            }
            //Armour Set Bonus
            if (IoC.Settings.ShuffleArmourSetBonus)
            {
                //Broken (Freezes after being in the armour crafting menu for too long)
                //if (IoC.Settings.RandomArmourSetBonus)
                //{
                //    for (int s = 0; s < armourDats.Count; s++)
                //    {
                //        if (badIndex.Contains(s))
                //            continue;

                //        if (armourDats[s].Set_Skill_1 != 0)
                //        {
                //            int index = r.Next(ItemData.ArmourSetSkills.Length);
                //            armourDats[s].Set_Skill_1 = ItemData.ArmourSetSkills[index];
                //        }
                //    }
                //}
                //else if (IoC.Settings.CompletelyRandomArmourSetBonus)
                //{
                //    for (int s = 0; s < armourDats.Count; s++)
                //    {
                //        if (badIndex.Contains(s))
                //            continue;

                //        //First pick if will get bonus or not and scale chance of getting one up with rank
                //        int[] weightTable = { 90 - armourDats[s].Rarity * 4, 10 + (armourDats[s].Rarity - 1) * 3};
                //        byte bonusChance = (byte)r.Next(weightTable.Sum());
                //        bool giveSetBonus = false;

                //        int cumulative_weight = 0;
                //        for (byte i = 0; i < 2; i++)
                //        {
                //            cumulative_weight += weightTable[i];
                //            if (bonusChance < cumulative_weight)
                //            {
                //                giveSetBonus = Convert.ToBoolean(i);
                //                break;
                //            }
                //        }

                //        if (giveSetBonus)
                //        {
                //            int index = r.Next(ItemData.ArmourSetSkills.Length);
                //            armourDats[s].Set_Skill_1 = ItemData.ArmourSetSkills[index];
                //        }
                //    }

                //}
                if (IoC.Settings.NonRankSpecificSetBonusShuffle)
                {
                    var dats = armourDats.Where(o => !badIndex.Contains((int)o.Index) && o.Set_Skill_1 > 0).ToList();
                    ShuffleSetBonus(r, dats);
                }
                else
                {
                    var datsLow = armourDats.Where(o => !badIndex.Contains((int)o.Index) && o.Set_Skill_1 > 0 && o.Rarity < 4).ToList();
                    var datsHigh = armourDats.Where(o => !badIndex.Contains((int)o.Index) && o.Set_Skill_1 > 0 && o.Rarity > 3 && o.Rarity <= 8).ToList();
                    //var datsMaster = armourDats.Where(o => !badIndex.Contains((int)o.Index) && o.Set_Skill_1 > 0 && o.Rarity > 8).ToList();
                    ShuffleSetBonus(r, datsLow);
                    ShuffleSetBonus(r, datsHigh);
                    //ShuffleDat(r, datsMaster);
                }
            }
            using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Armour Data Log.txt"))
            {
                NR3Generator skillR = new NR3Generator(IoC.Randomizer.Seed);
                NR3Generator skillLevelR = new NR3Generator(IoC.Randomizer.Seed);
                NR3Generator decoR = new NR3Generator(IoC.Randomizer.Seed);
                NR3Generator decoSizeR = new NR3Generator(IoC.Randomizer.Seed);

                float[] skillWeightTable = { 45, 43.5f, 9.1f, 1.4f, 1 };
                for (int c = 0; c < armourDats.Count; c++)
                {
                    if (badIndex.Contains(c) || fullArmourList[armourDats[c].Index].Contains("Layered"))
                        continue;

                    string logText = "";

                    logText = "Equipment: " + fullArmourList[armourDats[c].Index];

                    #region Armour Skills

                    //Armour Skill
                    if ((IoC.Settings.RandomArmourSkill && armourDats[c].Equip_Slot != 5) || (IoC.Settings.RandomCharmSkill && armourDats[c].Equip_Slot == 5))
                    {
                        if (armourDats[c].Skill_1 != 0)
                        {
                            int index = skillR.Next(ItemData.ArmourSkills.Length);
                            armourDats[c].Skill_1 = ItemData.ArmourSkills[index];
                            logText += "\n\tSkill 1: " + skillNames[(uint)armourDats[c].Skill_1 * 3];
                        }
                        if (armourDats[c].Skill_2 != 0)
                        {
                            int index = skillR.Next(ItemData.ArmourSkills.Length);
                            armourDats[c].Skill_2 = ItemData.ArmourSkills[index];
                            logText += "\n\tSkill 2: " + skillNames[(uint)armourDats[c].Skill_2 * 3];
                        }
                    }

                    if ((IoC.Settings.RandomArmourSkillLevels && armourDats[c].Equip_Slot != 5) || (IoC.Settings.RandomCharmSkillLevels && armourDats[c].Equip_Slot == 5))
                    {
                        if (armourDats[c].Skill_1 != 0)
                        {
                            float slotCount = (float)skillLevelR.NextDouble(skillWeightTable.Sum());
                            int smallestLevel = 0;

                            float cumulative_weight = 0;
                            for (byte i = 0; i < 5; i++)
                            {
                                cumulative_weight += skillWeightTable[i];
                                if (slotCount < cumulative_weight)
                                {
                                    smallestLevel = i + 1;
                                    break;
                                }
                            }
                            armourDats[c].Skill_1_Level = (byte)smallestLevel;
                            logText += "\n\tSkill 1 Level: " + armourDats[c].Skill_1_Level;
                        }
                        if (armourDats[c].Skill_2 != 0)
                        {
                            float slotCount = (float)skillLevelR.NextDouble(skillWeightTable.Sum());
                            int smallestLevel = 0;

                            float cumulative_weight = 0;
                            for (byte i = 0; i < 5; i++)
                            {
                                cumulative_weight += skillWeightTable[i];
                                if (slotCount < cumulative_weight)
                                {
                                    smallestLevel = i + 1;
                                    break;
                                }
                            }
                            armourDats[c].Skill_2_Level = (byte)smallestLevel;
                            logText += "\n\tSkill 2 Level: " + armourDats[c].Skill_2_Level;
                        }
                    }

                    if (IoC.Settings.ShuffleArmourSetBonus || IoC.Settings.NonRankSpecificSetBonusShuffle)
                    {
                        if (armourDats[c].Set_Skill_1 != 0)
                            logText += "\n\tSet Bonus: " + skillNames[(uint)armourDats[c].Set_Skill_1 * 3];
                    }

                    #endregion

                    #region Decoration Slot Count

                    //Armour Decoration Slot Count
                    if (IoC.Settings.RandomArmourDecoSlots && armourDats[c].Equip_Slot != 5)
                    {
                        int[] weightTable = { 50 - armourDats[c].Rarity * 4, 40 - armourDats[c].Rarity * 3, 10 + (armourDats[c].Rarity - 1) * 2, 10 + (int)Math.Round(((double)armourDats[c].Rarity - 1) * 1.5) };

                        byte slotCount = (byte)decoR.Next(weightTable.Sum());
                        byte Smallestslot = 0;

                        int cumulative_weight = 0;
                        for (byte i = 0; i < 4; i++)
                        {
                            cumulative_weight += weightTable[i];
                            if (slotCount < cumulative_weight)
                            {
                                Smallestslot = i;
                                break;
                            }
                        }
                        armourDats[c].Slot_Count = Smallestslot;
                        if (Smallestslot != 0)
                            logText += "\n\tDeco Slot Count: " + Smallestslot;

                        switch (Smallestslot)
                        {
                            case 0:
                                {
                                    armourDats[c].Slot_1_Size = 0;
                                    armourDats[c].Slot_2_Size = 0;
                                    armourDats[c].Slot_3_Size = 0;
                                    break;
                                }
                            case 1:
                                {
                                    if (armourDats[c].Slot_1_Size == 0)
                                        armourDats[c].Slot_1_Size = 1;

                                    armourDats[c].Slot_2_Size = 0;
                                    armourDats[c].Slot_3_Size = 0;

                                    if (!IoC.Settings.RandomDecoSlotSize)
                                        logText += "\n\tSlot 1 Size: " + armourDats[c].Slot_1_Size;
                                    break;
                                }
                            case 2:
                                {
                                    if (armourDats[c].Slot_1_Size == 0)
                                        armourDats[c].Slot_1_Size = 1;

                                    if (armourDats[c].Slot_2_Size == 0)
                                        armourDats[c].Slot_2_Size = 1;

                                    armourDats[c].Slot_3_Size = 0;

                                    if (!IoC.Settings.RandomDecoSlotSize)
                                    {
                                        logText += "\n\tSlot 1 Size: " + armourDats[c].Slot_1_Size;
                                        logText += "\n\tSlot 2 Size: " + armourDats[c].Slot_2_Size;
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    if (armourDats[c].Slot_1_Size == 0)
                                        armourDats[c].Slot_1_Size = 1;

                                    if (armourDats[c].Slot_2_Size == 0)
                                        armourDats[c].Slot_2_Size = 1;

                                    if (armourDats[c].Slot_3_Size == 0)
                                        armourDats[c].Slot_3_Size = 1;

                                    if (!IoC.Settings.RandomDecoSlotSize)
                                    {
                                        logText += "\n\tSlot 1 Size: " + armourDats[c].Slot_1_Size;
                                        logText += "\n\tSlot 2 Size: " + armourDats[c].Slot_2_Size;
                                        logText += "\n\tSlot 3 Size: " + armourDats[c].Slot_3_Size;
                                    }
                                    break;
                                }
                        }
                    }

                    if (IoC.Settings.GiveCharmDecoSlot && armourDats[c].Equip_Slot == 5)
                    {
                        armourDats[c].Slot_Count = 1;
                        logText += "\n\tDeco Slot Count: 1";
                        armourDats[c].Slot_1_Size = 1;
                        if (!IoC.Settings.RandomDecoSlotSize)
                            logText += "\n\tSlot 1 Size: " + armourDats[c].Slot_1_Size;
                    }

                    #endregion

                    #region Decoration Slot Size

                    //Armour Decoration Slot Size
                    if (IoC.Settings.RandomDecoSlotSize)
                    {
                        int[] weightTable = { 50 - armourDats[c].Rarity * 4, 40 - armourDats[c].Rarity * 3, 10 + (armourDats[c].Rarity - 1) * 2, 10 + (int)Math.Round(((double)armourDats[c].Rarity - 1) * 1.5) };

                        int cumulative_weight;
                        switch (armourDats[c].Slot_Count)
                        {
                            case 0:
                                {
                                    break;
                                }
                            case 1:
                                {
                                    byte slotCountTes = (byte)decoSizeR.Next(weightTable.Sum());
                                    cumulative_weight = 0;
                                    for (byte i = 0; i < 4; i++)
                                    {
                                        cumulative_weight += weightTable[i];
                                        if (slotCountTes < cumulative_weight)
                                        {
                                            armourDats[c].Slot_1_Size = (byte)(i + 1);
                                            logText += "\n\tSlot 1 Size: " + armourDats[c].Slot_1_Size;
                                            break;
                                        }
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    byte slotCountTes = (byte)decoSizeR.Next(weightTable.Sum());
                                    cumulative_weight = 0;
                                    for (byte i = 0; i < 4; i++)
                                    {
                                        cumulative_weight += weightTable[i];
                                        if (slotCountTes < cumulative_weight)
                                        {
                                            armourDats[c].Slot_1_Size = (byte)(i + 1);
                                            break;
                                        }
                                    }

                                    slotCountTes = (byte)decoSizeR.Next(weightTable.Sum());
                                    cumulative_weight = 0;
                                    for (byte i = 0; i < 4; i++)
                                    {
                                        cumulative_weight += weightTable[i];
                                        if (slotCountTes < cumulative_weight)
                                        {
                                            armourDats[c].Slot_2_Size = (byte)(i + 1);
                                            break;
                                        }
                                    }
                                    logText += "\n\tSlot 1 Size: " + armourDats[c].Slot_1_Size;
                                    logText += "\n\tSlot 2 Size: " + armourDats[c].Slot_2_Size;
                                    break;
                                }
                            case 3:
                                {
                                    byte slotCountTes = (byte)decoSizeR.Next(weightTable.Sum());
                                    cumulative_weight = 0;
                                    for (byte i = 0; i < 4; i++)
                                    {
                                        cumulative_weight += weightTable[i];
                                        if (slotCountTes < cumulative_weight)
                                        {
                                            armourDats[c].Slot_1_Size = (byte)(i + 1);
                                            break;
                                        }
                                    }

                                    slotCountTes = (byte)decoSizeR.Next(weightTable.Sum());
                                    cumulative_weight = 0;
                                    for (byte i = 0; i < 4; i++)
                                    {
                                        cumulative_weight += weightTable[i];
                                        if (slotCountTes < cumulative_weight)
                                        {
                                            armourDats[c].Slot_2_Size = (byte)(i + 1);
                                            break;
                                        }
                                    }

                                    slotCountTes = (byte)decoSizeR.Next(weightTable.Sum());
                                    cumulative_weight = 0;
                                    for (byte i = 0; i < 4; i++)
                                    {
                                        cumulative_weight += weightTable[i];
                                        if (slotCountTes < cumulative_weight)
                                        {
                                            armourDats[c].Slot_3_Size = (byte)(i + 1);
                                            break;
                                        }
                                    }
                                    logText += "\n\tSlot 1 Size: " + armourDats[c].Slot_1_Size;
                                    logText += "\n\tSlot 2 Size: " + armourDats[c].Slot_2_Size;
                                    logText += "\n\tSlot 3 Size: " + armourDats[c].Slot_3_Size;
                                    break;
                                }
                        }
                    }

                    #endregion

                    //Armour Icon Colour
                    if (IoC.Settings.RandomArmourIconColour)
                    {
                        armourDats[c].Rarity = (byte)r.Next(12);
                        logText += "\n\tRarity(Colour): " + armourDats[c].Rarity;
                    }
                    if (logText.Contains("\n"))
                        file.WriteLine(logText);
                }
            }
        }

        private static void ShuffleSetBonus(NR3Generator r, List<RecipeStructs.ArmourDat> dats)
        {
            for (int c = dats.Count - 1; c >= 0; c--)
            {
                // Pick a random index 
                // from 0 to i 
                int j = r.Next(0, c + 1);

                // Swap arr[i] with the 
                // element at random index
                ushort temp = dats[c].Set_Skill_1;
                dats[c].Set_Skill_1 = dats[j].Set_Skill_1;
                dats[j].Set_Skill_1 = temp;

            }
        }

        #endregion
    }
}
