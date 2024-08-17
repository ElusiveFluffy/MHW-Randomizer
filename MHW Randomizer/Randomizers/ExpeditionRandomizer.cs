using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Troschuetz.Random.Generators;

namespace MHW_Randomizer
{
    public class ExpeditionRandomizer
    {
        private static int[] MonsterIDs = new int[] { };
        private static Files[]? SobjFilesCache;
        private static NR3Generator? PickSobj;

        public static void Randomize()
        {
            SobjFilesCache = ChunkOTF.files.Values.Where(o => o.EntireName.Contains(@"\enemy\boss\em") && !QuestData.BadSobjs.Contains(o.Name) && !Regex.IsMatch(o.Name!, @"em\d{3}_\d{2}_st109_(5|6)0.sobj") && !QuestData.BadGuidingSobjs.Contains(o.Name)).ToArray();
            PickSobj = new NR3Generator(ViewModels.Randomizer.Seed);
            //Create Sobj folder
            Directory.CreateDirectory(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\quest\enemy\boss\");

            File.Create(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\Expedition Sobj Log.txt").Dispose();

            //Read in the bytes from the file for the header and transition bytes
            byte[] expeditionSpawnBytes = ChunkOTF.files["discoverEmSet.dems"].Extract();
            List<ExpeditionSpawn> expeditionSpawn = StructTools.RawDeserialize<ExpeditionSpawn>(expeditionSpawnBytes, 10);

            //Create a new list for all the new chances
            List<ExpeditionSpawn> newChances = new List<ExpeditionSpawn>();
            for (uint chances = 0; chances < 101; chances++)
                newChances.Add(new ExpeditionSpawn { MonsterID = chances });

            NR3Generator monsterPicker = new NR3Generator(ViewModels.Randomizer.Seed);
            NR3Generator indexPicker = new NR3Generator(ViewModels.Randomizer.Seed);

            //Loop through all the ranks
            for (int rank = 0; rank < 3; rank++)
            {
                //Check if randomizing expeditions and check currently on the right rank
                if ((ViewModels.Settings.RandomizeExpeditions && rank < 2) || (ViewModels.Settings.RandomizeIceborneExpeditions && rank == 2))
                {
                    switch (rank)
                    {
                        case 0:
                            {
                                //Chose the base set of monsters
                                if (ViewModels.Settings.ExpeditionHighRankInLow)
                                {
                                    MonsterIDs = ExpeditionData.UsualHighRankMonster;

                                    //Add any additional monsters
                                    if (ViewModels.Settings.ExpeditionIncludeNonUsualMonsters)
                                        MonsterIDs = MonsterIDs.Concat(ExpeditionData.UnusedHighRankMonster).ToArray();
                                    if (ViewModels.Settings.ExpeditionIncludeLeshen)
                                    {
                                        MonsterIDs = MonsterIDs.Append(23).ToArray();
                                        MonsterIDs = MonsterIDs.Append(51).ToArray();
                                    }
                                    if (ViewModels.Settings.ExpeditionIncludeBehemoth)
                                        MonsterIDs = MonsterIDs.Append(15).ToArray();
                                }
                                else
                                    MonsterIDs = ExpeditionData.UsualLowRankMonster;

                                if (ViewModels.Settings.ExpeditionIBMonstersInLowRank)
                                {
                                    MonsterIDs = MonsterIDs.Concat(ExpeditionData.MasterRankOnlyMonster).ToArray();
                                    if (ViewModels.Settings.ExpeditionIncludeIBNonUsualMonsters)
                                        MonsterIDs = MonsterIDs.Concat(ExpeditionData.UnusedMasterRankMonster).ToArray();
                                }
                                break;
                            }
                        case 1:
                            {
                                //Chose the base set of monsters
                                if (ViewModels.Settings.ExpeditionHighRankOnlyInHigh)
                                    MonsterIDs = ExpeditionData.HighRankOnlyMonster;
                                else
                                    MonsterIDs = ExpeditionData.UsualHighRankMonster;

                                //Add any additional monsters
                                if (ViewModels.Settings.ExpeditionIncludeNonUsualMonsters)
                                    MonsterIDs = MonsterIDs.Concat(ExpeditionData.UnusedHighRankMonster).ToArray();
                                if (ViewModels.Settings.ExpeditionIncludeLeshen)
                                {
                                    MonsterIDs = MonsterIDs.Append(23).ToArray();
                                    MonsterIDs = MonsterIDs.Append(51).ToArray();
                                }
                                if (ViewModels.Settings.ExpeditionIncludeBehemoth)
                                    MonsterIDs = MonsterIDs.Append(15).ToArray();

                                if (ViewModels.Settings.ExpeditionIBMonstersInHighRank)
                                {
                                    MonsterIDs = MonsterIDs.Concat(ExpeditionData.MasterRankOnlyMonster).ToArray();
                                    if (ViewModels.Settings.ExpeditionIncludeIBNonUsualMonsters)
                                        MonsterIDs = MonsterIDs.Concat(ExpeditionData.UnusedMasterRankMonster).ToArray();
                                }
                                break;
                            }
                        case 2:
                            {
                                //Chose the base set of monsters
                                if (ViewModels.Settings.ExpeditionMasterRankOnlyInMaster)
                                    MonsterIDs = ExpeditionData.MasterRankOnlyMonster;
                                else
                                    MonsterIDs = ExpeditionData.UsualMasterRankMonster;

                                //Add any additional monsters
                                if (ViewModels.Settings.ExpeditionIncludeNonUsualMonsters)
                                    MonsterIDs = MonsterIDs.Concat(ExpeditionData.UnusedHighRankMonster).ToArray();
                                if (ViewModels.Settings.ExpeditionIncludeIBNonUsualMonsters)
                                    MonsterIDs = MonsterIDs.Concat(ExpeditionData.UnusedMasterRankMonster).ToArray();

                                if (ViewModels.Settings.ExpeditionIncludeShara)
                                    MonsterIDs = MonsterIDs.Append(81).ToArray();

                                if (ViewModels.Settings.ExpeditionIncludeFuriousRajang)
                                    MonsterIDs = MonsterIDs.Append(92).ToArray();
                                if (ViewModels.Settings.ExpeditionIncludeAlatreon)
                                    MonsterIDs = MonsterIDs.Append(87).ToArray();

                                if (!ViewModels.Settings.ExpeditionMasterRankOnlyInMaster)
                                {
                                    if (ViewModels.Settings.ExpeditionIncludeLeshen)
                                    {
                                        MonsterIDs = MonsterIDs.Append(23).ToArray();
                                        MonsterIDs = MonsterIDs.Append(51).ToArray();
                                    }
                                    if (ViewModels.Settings.ExpeditionIncludeBehemoth)
                                        MonsterIDs = MonsterIDs.Append(15).ToArray();
                                }
                                break;
                            }
                    }
                    //Loop through every map
                    for (int map = 0; map < 6; map++)
                    {
                        //Skip the maps that aren't used for low and high rank
                        if ((rank == 0 && map > 3) || (rank == 1 && map > 4))
                            continue;

                        List<uint> rollChances = new List<uint> { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 10, 10, 10, 10, 10 };

                        if (rank == 2 && map == 3)
                            //Remove alatreon as a possible monster if map is coral highlands as it causes a blinding white light effect on that map
                            //Remove leshen too as possible monster as they get stuck
                            MonsterIDs = MonsterIDs.Where(o => o != 87 || (o != 23 || o != 51)).ToArray();
                        else if (rank == 2 && map == 4)
                        {
                            if (ViewModels.Settings.ExpeditionIncludeAlatreon)
                                //Add alatreon to the map after
                                MonsterIDs = MonsterIDs.Append(87).ToArray();
                            if (ViewModels.Settings.ExpeditionIncludeLeshen)
                            {
                                //Add the leshen back in
                                MonsterIDs = MonsterIDs.Append(23).ToArray();
                                MonsterIDs = MonsterIDs.Append(51).ToArray();
                            }
                        }
                        else if (rank == 2 && map == 5)
                            //Remove alatreon as a possible monster if map is hoarfrost reach as it causes them to get stuck
                            MonsterIDs = MonsterIDs.Where(o => o != 87).ToArray();

                        for (int chanceRoll = 0; chanceRoll < 15; chanceRoll++)
                        {
                            int index = indexPicker.Next(rollChances.Count());
                            switch (rank)
                            {
                                //Low rank
                                case 0:
                                    {
                                        int monsterID = MonsterIDs[monsterPicker.Next(MonsterIDs.Length)];
                                        if (monsterID < 87)
                                            newChances[monsterID].LowRank.Transitions = expeditionSpawn[monsterID].LowRank.Transitions;
                                        else
                                            newChances[monsterID].LowRank.Transitions = expeditionSpawn[0].LowRank.Transitions;

                                        switch (map)
                                        {
                                            case 0:
                                                {
                                                    newChances[monsterID].LowRank.st101Chance += rollChances[index];
                                                    break;
                                                }
                                            case 1:
                                                {
                                                    newChances[monsterID].LowRank.st102Chance += rollChances[index];
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    newChances[monsterID].LowRank.st103Chance += rollChances[index];
                                                    break;
                                                }
                                            case 3:
                                                {
                                                    newChances[monsterID].LowRank.st104Chance += rollChances[index];
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                //High rank
                                case 1:
                                    {
                                        int monsterID = MonsterIDs[monsterPicker.Next(MonsterIDs.Length)];
                                        if (monsterID < 87)
                                            newChances[monsterID].HighRank.Transitions = expeditionSpawn[monsterID].HighRank.Transitions;
                                        else
                                            newChances[monsterID].HighRank.Transitions = expeditionSpawn[0].HighRank.Transitions;

                                        switch (map)
                                        {
                                            case 0:
                                                {
                                                    newChances[monsterID].HighRank.st101Chance += rollChances[index];
                                                    break;
                                                }
                                            case 1:
                                                {
                                                    newChances[monsterID].HighRank.st102Chance += rollChances[index];
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    newChances[monsterID].HighRank.st103Chance += rollChances[index];
                                                    break;
                                                }
                                            case 3:
                                                {
                                                    newChances[monsterID].HighRank.st104Chance += rollChances[index];
                                                    break;
                                                }
                                            case 4:
                                                {
                                                    newChances[monsterID].HighRank.st105Chance += rollChances[index];
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                //Master rank
                                case 2:
                                    {
                                        int monsterID = MonsterIDs[monsterPicker.Next(MonsterIDs.Length)];
                                        if (monsterID < 87)
                                            newChances[monsterID].MasterRank.Transitions = expeditionSpawn[monsterID].MasterRank.Transitions;
                                        else
                                            newChances[monsterID].MasterRank.Transitions = expeditionSpawn[0].MasterRank.Transitions;

                                        switch (map)
                                        {
                                            case 0:
                                                {
                                                    newChances[monsterID].MasterRank.st101Chance += rollChances[index];
                                                    break;
                                                }
                                            case 1:
                                                {
                                                    newChances[monsterID].MasterRank.st102Chance += rollChances[index];
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    newChances[monsterID].MasterRank.st103Chance += rollChances[index];
                                                    break;
                                                }
                                            case 3:
                                                {
                                                    newChances[monsterID].MasterRank.st104Chance += rollChances[index];
                                                    break;
                                                }
                                            case 4:
                                                {
                                                    newChances[monsterID].MasterRank.st105Chance += rollChances[index];
                                                    break;
                                                }
                                            case 5:
                                                {
                                                    newChances[monsterID].MasterRank.st108Chance += rollChances[index];
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                            }
                            rollChances.RemoveAt(index);
                        }
                    }
                }
            }

            //Log the changes
            File.Create(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\Expedition Log.txt").Dispose();
            using (StreamWriter file = File.AppendText(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\Expedition Log.txt"))
            {
                foreach (ExpeditionSpawn spawn in newChances)
                {
                    if (QuestData.MonsterNames[spawn.MonsterID + 1].Contains("[s]") || QuestData.MonsterNames[spawn.MonsterID + 1] == "NON-VALID")
                        continue;

                    //If not randomizing base game expeditions use original values
                    if (!ViewModels.Settings.RandomizeExpeditions)
                    {
                        if (spawn.MonsterID > 86)
                        {
                            //Low Rank
                            newChances[(int)spawn.MonsterID].LowRank.st101Chance = 0;
                            newChances[(int)spawn.MonsterID].LowRank.st102Chance = 0;
                            newChances[(int)spawn.MonsterID].LowRank.st103Chance = 0;
                            newChances[(int)spawn.MonsterID].LowRank.st104Chance = 0;

                            //High Rank
                            newChances[(int)spawn.MonsterID].HighRank.st101Chance = 0;
                            newChances[(int)spawn.MonsterID].HighRank.st102Chance = 0;
                            newChances[(int)spawn.MonsterID].HighRank.st103Chance = 0;
                            newChances[(int)spawn.MonsterID].HighRank.st104Chance = 0;
                            newChances[(int)spawn.MonsterID].HighRank.st105Chance = 0;
                        }
                        else
                        {
                            //Low rank
                            newChances[(int)spawn.MonsterID].LowRank.st101Chance = expeditionSpawn[(int)spawn.MonsterID].LowRank.st101Chance;
                            newChances[(int)spawn.MonsterID].LowRank.st102Chance = expeditionSpawn[(int)spawn.MonsterID].LowRank.st102Chance;
                            newChances[(int)spawn.MonsterID].LowRank.st103Chance = expeditionSpawn[(int)spawn.MonsterID].LowRank.st103Chance;
                            newChances[(int)spawn.MonsterID].LowRank.st104Chance = expeditionSpawn[(int)spawn.MonsterID].LowRank.st104Chance;

                            //High Rank
                            newChances[(int)spawn.MonsterID].HighRank.st101Chance = expeditionSpawn[(int)spawn.MonsterID].HighRank.st101Chance;
                            newChances[(int)spawn.MonsterID].HighRank.st102Chance = expeditionSpawn[(int)spawn.MonsterID].HighRank.st102Chance;
                            newChances[(int)spawn.MonsterID].HighRank.st103Chance = expeditionSpawn[(int)spawn.MonsterID].HighRank.st103Chance;
                            newChances[(int)spawn.MonsterID].HighRank.st104Chance = expeditionSpawn[(int)spawn.MonsterID].HighRank.st104Chance;
                            newChances[(int)spawn.MonsterID].HighRank.st105Chance = expeditionSpawn[(int)spawn.MonsterID].HighRank.st105Chance;

                        }
                    }

                    //If not randomizing iceborne expeditions use the original values
                    if (!ViewModels.Settings.RandomizeIceborneExpeditions)
                    {
                        if (spawn.MonsterID > 86)
                        {
                            newChances[(int)spawn.MonsterID].MasterRank.st101Chance = 0;
                            newChances[(int)spawn.MonsterID].MasterRank.st102Chance = 0;
                            newChances[(int)spawn.MonsterID].MasterRank.st103Chance = 0;
                            newChances[(int)spawn.MonsterID].MasterRank.st104Chance = 0;
                            newChances[(int)spawn.MonsterID].MasterRank.st105Chance = 0;
                            newChances[(int)spawn.MonsterID].MasterRank.st108Chance = 0;
                        }
                        else
                        {
                            newChances[(int)spawn.MonsterID].MasterRank.st101Chance = expeditionSpawn[(int)spawn.MonsterID].MasterRank.st101Chance;
                            newChances[(int)spawn.MonsterID].MasterRank.st102Chance = expeditionSpawn[(int)spawn.MonsterID].MasterRank.st102Chance;
                            newChances[(int)spawn.MonsterID].MasterRank.st103Chance = expeditionSpawn[(int)spawn.MonsterID].MasterRank.st103Chance;
                            newChances[(int)spawn.MonsterID].MasterRank.st104Chance = expeditionSpawn[(int)spawn.MonsterID].MasterRank.st104Chance;
                            newChances[(int)spawn.MonsterID].MasterRank.st105Chance = expeditionSpawn[(int)spawn.MonsterID].MasterRank.st105Chance;
                            newChances[(int)spawn.MonsterID].MasterRank.st108Chance = expeditionSpawn[(int)spawn.MonsterID].MasterRank.st108Chance;
                        }

                    }


                    //Add the required monsters if they haven't been selected already
                    if (ExpeditionData.LowRankRequiredMonsters.ContainsKey(spawn.MonsterID))
                    {
                        switch (ExpeditionData.LowRankRequiredMonsters[spawn.MonsterID])
                        {
                            case 101:
                                {
                                    if (spawn.LowRank.st101Chance == 0)
                                        spawn.LowRank.st101Chance += 1;
                                    break;
                                }
                            case 102:
                                {
                                    if (spawn.LowRank.st102Chance == 0)
                                        spawn.LowRank.st102Chance += 1;
                                    break;
                                }
                            case 103:
                                {
                                    if (spawn.LowRank.st103Chance == 0)
                                        spawn.LowRank.st103Chance += 1;
                                    break;
                                }
                            case 104:
                                {
                                    if (spawn.LowRank.st104Chance == 0)
                                        spawn.LowRank.st104Chance += 1;
                                    break;
                                }
                        }
                    }
                    if (ExpeditionData.HighRankRequiredMonsters.ContainsKey(spawn.MonsterID))
                    {

                        switch (ExpeditionData.HighRankRequiredMonsters[spawn.MonsterID])
                        {
                            case 101:
                                {
                                    if (spawn.HighRank.st101Chance == 0)
                                        spawn.HighRank.st101Chance += 1;
                                    break;
                                }
                            case 102:
                                {
                                    if (spawn.HighRank.st102Chance == 0)
                                        spawn.HighRank.st102Chance += 1;
                                    break;
                                }
                            case 103:
                                {
                                    if (spawn.HighRank.st103Chance == 0)
                                        spawn.HighRank.st103Chance += 1;
                                    break;
                                }
                            case 104:
                                {
                                    if (spawn.HighRank.st104Chance == 0)
                                        spawn.HighRank.st104Chance += 1;
                                    break;
                                }
                            case 105:
                                {
                                    if (spawn.HighRank.st105Chance == 0)
                                        spawn.HighRank.st105Chance += 1;
                                    break;
                                }
                        }
                    }
                    if (ExpeditionData.MasterRankRequiredMonsters.ContainsKey(spawn.MonsterID))
                    {

                        switch (ExpeditionData.MasterRankRequiredMonsters[spawn.MonsterID])
                        {
                            case 101:
                                {
                                    if (spawn.MasterRank.st101Chance == 0)
                                        spawn.MasterRank.st101Chance += 1;
                                    break;
                                }
                            case 102:
                                {
                                    if (spawn.MasterRank.st102Chance == 0)
                                        spawn.MasterRank.st102Chance += 1;
                                    break;
                                }
                            case 103:
                                {
                                    if (spawn.MasterRank.st103Chance == 0)
                                        spawn.MasterRank.st103Chance += 1;
                                    break;
                                }
                            case 104:
                                {
                                    if (spawn.MasterRank.st104Chance == 0)
                                        spawn.MasterRank.st104Chance += 1;
                                    break;
                                }
                            case 105:
                                {
                                    if (spawn.MasterRank.st105Chance == 0)
                                        spawn.MasterRank.st105Chance += 1;
                                    break;
                                }
                            case 108:
                                {
                                    if (spawn.MasterRank.st108Chance == 0)
                                        spawn.MasterRank.st108Chance += 1;
                                    break;
                                }
                        }
                    }

                    file.WriteLine("-------------------------");
                    file.WriteLine(QuestData.MonsterNames[spawn.MonsterID + 1]);

                    if (spawn.LowRank.Transitions != null)
                    {
                        file.WriteLine("Low Rank");
                        if (spawn.LowRank.st101Chance != 0)
                            file.WriteLine("Ancient Forest Chance: " + spawn.LowRank.st101Chance + "%");
                        if (spawn.LowRank.st102Chance != 0)
                            file.WriteLine("Wildspire Waste Chance: " + spawn.LowRank.st102Chance + "%");
                        if (spawn.LowRank.st103Chance != 0)
                            file.WriteLine("Coral Highlands Chance: " + spawn.LowRank.st103Chance + "%");
                        if (spawn.LowRank.st104Chance != 0)
                            file.WriteLine("Rotten Vale Chance: " + spawn.LowRank.st104Chance + "%");
                    }
                    else
                        spawn.LowRank.Transitions = new uint[14];

                    if (spawn.HighRank.Transitions != null)
                    {
                        file.WriteLine("\nHigh Rank");
                        if (spawn.HighRank.st101Chance != 0)
                            file.WriteLine("Ancient Forest Chance: " + spawn.HighRank.st101Chance + "%");
                        if (spawn.HighRank.st102Chance != 0)
                            file.WriteLine("Wildspire Waste Chance: " + spawn.HighRank.st102Chance + "%");
                        if (spawn.HighRank.st103Chance != 0)
                            file.WriteLine("Coral Highlands Chance: " + spawn.HighRank.st103Chance + "%");
                        if (spawn.HighRank.st104Chance != 0)
                            file.WriteLine("Rotten Vale Chance: " + spawn.HighRank.st104Chance + "%");
                        if (spawn.HighRank.st105Chance != 0)
                            file.WriteLine("Elder Recess Chance: " + spawn.HighRank.st105Chance + "%");
                    }
                    else
                        spawn.HighRank.Transitions = new uint[14];

                    if (spawn.MasterRank.Transitions != null)
                    {
                        file.WriteLine("\nMaster Rank");
                        if (spawn.MasterRank.st101Chance != 0)
                            file.WriteLine("Ancient Forest Chance: " + spawn.MasterRank.st101Chance + "%");
                        if (spawn.MasterRank.st102Chance != 0)
                            file.WriteLine("Wildspire Waste Chance: " + spawn.MasterRank.st102Chance + "%");
                        if (spawn.MasterRank.st103Chance != 0)
                            file.WriteLine("Coral Highlands Chance: " + spawn.MasterRank.st103Chance + "%");
                        if (spawn.MasterRank.st104Chance != 0)
                            file.WriteLine("Rotten Vale Chance: " + spawn.MasterRank.st104Chance + "%");
                        if (spawn.MasterRank.st105Chance != 0)
                            file.WriteLine("Elder Recess Chance: " + spawn.MasterRank.st105Chance + "%");
                        if (spawn.MasterRank.st108Chance != 0)
                            file.WriteLine("Hoarfrost Reach Chance: " + spawn.MasterRank.st108Chance + "%");
                    }
                    else
                        spawn.MasterRank.Transitions = new uint[14];

                    file.WriteLine("");
                }

            }
            //Create the sobjs
            //Check if the base game expeditions need a new sobj, if not then skip to iceborne
            for (int map = (ViewModels.Settings.RandomizeExpeditions || ViewModels.Settings.ExpeditionRandomSobj) ? 1 : 6; map < 7; map++)
            {
                //Skip checking if not randomizing iceborne sobjs
                if (!ViewModels.Settings.ExpeditionRandomIBSobj && map == 6)
                    break;

                using (StreamWriter sobjFile = File.AppendText(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\Expedition Sobj Log.txt"))
                {
                    //Write monster info to the log
                    sobjFile.WriteLine("-----------------------");
                    sobjFile.WriteLine(QuestData.MapNames[map == 6 ? 8 : map] + " Sobjs");
                    sobjFile.WriteLine("-----------------------");
                }
                foreach (ExpeditionSpawn spawn in newChances)
                {
                    bool giveMonsterRandomSobj = false;
                    switch (map)
                    {
                        case 1:
                            {
                                giveMonsterRandomSobj = (ViewModels.Settings.ExpeditionRandomSobj && (newChances[(int)spawn.MonsterID].LowRank.st101Chance > 0 || newChances[(int)spawn.MonsterID].HighRank.st101Chance > 0)) ||
                                                        (ViewModels.Settings.ExpeditionRandomIBSobj && newChances[(int)spawn.MonsterID].MasterRank.st101Chance > 0);
                                break;
                            }
                        case 2:
                            {
                                giveMonsterRandomSobj = (ViewModels.Settings.ExpeditionRandomSobj && (newChances[(int)spawn.MonsterID].LowRank.st102Chance > 0 || newChances[(int)spawn.MonsterID].HighRank.st102Chance > 0)) ||
                                                        (ViewModels.Settings.ExpeditionRandomIBSobj && newChances[(int)spawn.MonsterID].MasterRank.st102Chance > 0);
                                break;
                            }
                        case 3:
                            {
                                giveMonsterRandomSobj = (ViewModels.Settings.ExpeditionRandomSobj && (newChances[(int)spawn.MonsterID].LowRank.st103Chance > 0 || newChances[(int)spawn.MonsterID].HighRank.st103Chance > 0)) ||
                                                        (ViewModels.Settings.ExpeditionRandomIBSobj && newChances[(int)spawn.MonsterID].MasterRank.st103Chance > 0);
                                break;
                            }
                        case 4:
                            {
                                giveMonsterRandomSobj = (ViewModels.Settings.ExpeditionRandomSobj && (newChances[(int)spawn.MonsterID].LowRank.st104Chance > 0 || newChances[(int)spawn.MonsterID].HighRank.st104Chance > 0)) ||
                                                        (ViewModels.Settings.ExpeditionRandomIBSobj && newChances[(int)spawn.MonsterID].MasterRank.st104Chance > 0);
                                break;
                            }
                        case 5:
                            {
                                giveMonsterRandomSobj = (ViewModels.Settings.ExpeditionRandomSobj && newChances[(int)spawn.MonsterID].HighRank.st105Chance > 0) ||
                                                        (ViewModels.Settings.ExpeditionRandomIBSobj && newChances[(int)spawn.MonsterID].MasterRank.st105Chance > 0);
                                break;
                            }
                        case 6:
                            {
                                if (ViewModels.Settings.ExpeditionRandomIBSobj)
                                {
                                    giveMonsterRandomSobj = newChances[(int)spawn.MonsterID].MasterRank.st108Chance > 0;
                                }
                                break;
                            }
                    }
                    if (giveMonsterRandomSobj)
                        //If its 6 set the map index to 8 for hoarfrost reach
                        CreateSobj((int)spawn.MonsterID, map == 6 ? 8 : map);
                }

                using (StreamWriter sobjFile = File.AppendText(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\Expedition Sobj Log.txt"))
                {
                    //Add a blank line inbetween each map
                    sobjFile.WriteLine("");
                }
            }

            //Copy the header and then add the edited spawn bytes to it
            byte[] editedSpawnBytes = StructTools.RawSerialize<ExpeditionSpawn>(newChances);
            byte[] header = new byte[10];
            Array.Copy(expeditionSpawnBytes, header, 10);
            header = header.Concat(editedSpawnBytes).ToArray();

            //Edit the entry count to include all of the monsters
            header[6] = 101;

            //Write the file
            File.WriteAllBytes(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\quest\enemy\discoverEmSet.dems", header);

            //CSV to more easily go through the values
            File.Create(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\Expedition Log.csv").Dispose();
            using (StreamWriter file = File.AppendText(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\Expedition Log.csv"))
            {
                file.WriteLine("ID,Monster,Low Rank Ancient Forest,Low Rank Wildspire Waste,Low Rank Coral Highlands,Low Rank Rotten Vale," +
                    "High Rank Ancient Forest,High Rank Wildspire Waste,High Rank Coral Highlands,High Rank Rotten Vale,High Rank Elder Recess," +
                    "Master Rank Ancient Forest,Master Rank Wildspire Waste,Master Rank Coral Highlands,Master Rank Rotten Vale,Master Rank Elder Recess,Master Rank Hoarfrost Reach");
                foreach (ExpeditionSpawn spawn in newChances)
                {
                    if (QuestData.MonsterNames[spawn.MonsterID + 1].Contains("[s]") || QuestData.MonsterNames[spawn.MonsterID + 1] == "NON-VALID")
                        continue;

                    file.WriteLine(string.Join(",", new string[] { spawn.MonsterID.ToString(), QuestData.MonsterNames[spawn.MonsterID + 1], spawn.LowRank.st101Chance + "%", spawn.LowRank.st102Chance + "%", spawn.LowRank.st103Chance + "%", spawn.LowRank.st104Chance + "%",
                                                                   spawn.HighRank.st101Chance + "%", spawn.HighRank.st102Chance + "%", spawn.HighRank.st103Chance + "%", spawn.HighRank.st104Chance + "%", spawn.HighRank.st105Chance + "%",
                                                                   spawn.MasterRank.st101Chance + "%", spawn.MasterRank.st102Chance + "%", spawn.MasterRank.st103Chance + "%", spawn.MasterRank.st104Chance + "%", spawn.MasterRank.st105Chance + "%", spawn.MasterRank.st108Chance + "%", }));
                }
            }
        }

        private static void CreateSobj(int monsterID, int mapIDIndex)
        {
            //Expeditions default to sobj 00 for spawns usually, if that sobj exists then we know 0 has already been created
            if (File.Exists(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\quest\enemy\boss\" + QuestData.MonsterStageEmNumber[monsterID] + QuestData.MapIDs[mapIDIndex] + "_00.sobj"))
                return;

            string oldMSobj = "";
            byte[] sobj;

            //Pick random sobj
            Files[] SobjFiles;
            SobjFiles = SobjFilesCache!.Where(o => o.Name.Contains("st" + QuestData.MapIDs[mapIDIndex])).ToArray();
            int sobjIndex = PickSobj.Next(SobjFiles.Length);

            sobj = SobjFiles[sobjIndex].Extract();
            oldMSobj = SobjFiles[sobjIndex].Name!;

            //The count of hex byte CD
            int CDCount = 0;
            //Find where monster ID is located
            for (int j = 0; j < sobj.Length; j++)
            {
                if (sobj[j] == 205)
                {
                    CDCount++;
                }
                else if (CDCount > 12)
                {
                    sobj[j + 13] = (byte)monsterID;
                    break;
                }
                else
                    CDCount = 0;
            }
            File.WriteAllBytes(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\quest\enemy\boss\" + QuestData.MonsterStageEmNumber[monsterID] + QuestData.MapIDs[mapIDIndex] + "_00.sobj", sobj);


            using (StreamWriter file = File.AppendText(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\Expedition Sobj Log.txt"))
            {
                //Write monster info to the log
                file.WriteLine("Sobj File Name: " + QuestData.MonsterStageEmNumber[monsterID] + QuestData.MapIDs[mapIDIndex] + "_00.sobj" + "\tOld sobj File Name: " + oldMSobj + "\tMonster: " + QuestData.MonsterNames[monsterID + 1]);
            }
        }
    }
}
