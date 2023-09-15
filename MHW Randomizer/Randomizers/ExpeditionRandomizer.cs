using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Troschuetz.Random.Generators;

namespace MHW_Randomizer
{
    public class ExpeditionRandomizer
    {
        public static void Randomize()
        {
            byte[] expeditionSpawnBytes = ChunkOTF.files["discoverEmSet.dems"].Extract();
            List<ExpeditionSpawn> expeditionSpawn = StructTools.RawDeserialize<ExpeditionSpawn>(expeditionSpawnBytes, 10);

            List<ExpeditionSpawn> newChances = new List<ExpeditionSpawn>();
            for (uint chances = 0; chances < 87; chances++)
                newChances.Add(new ExpeditionSpawn { MonsterID = chances });

            NR3Generator monsterPicker = new NR3Generator(IoC.Randomizer.Seed);
            NR3Generator indexPicker = new NR3Generator(IoC.Randomizer.Seed);

            File.Create(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Expedition Log.txt").Dispose();
            using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Expedition Log.txt"))
            {
                for (int rank = 0; rank < 3; rank++)
                {
                    for (int map = 0; map < 6; map++)
                    {
                        if ((rank == 0 && map > 3) || (rank == 1 && map > 4))
                            continue;

                        List<uint> rollChances = new List<uint> { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 10, 10, 10, 10, 10 };
                        for (int chanceRoll = 0; chanceRoll < 15; chanceRoll++)
                        {
                            int index = indexPicker.Next(rollChances.Count());
                            switch (rank)
                            {
                                case 0:
                                    {
                                        int monsterID = ExpeditionData.UsualLowRankMonster[monsterPicker.Next(ExpeditionData.UsualLowRankMonster.Length)];
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
                                case 1:
                                    {
                                        int monsterID = ExpeditionData.UsualHighRankMonster[monsterPicker.Next(ExpeditionData.UsualHighRankMonster.Length)];
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
                                case 2:
                                    {
                                        int monsterID = ExpeditionData.UsualMasterRankMonster[monsterPicker.Next(ExpeditionData.UsualMasterRankMonster.Length)];
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

                foreach (ExpeditionSpawn spawn in newChances)
                {
                    if (QuestData.MonsterNames[spawn.MonsterID + 1].Contains("[s]") || QuestData.MonsterNames[spawn.MonsterID + 1] == "NON-VALID")
                        continue;

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

                byte[] editedSpawnBytes = StructTools.RawSerialize<ExpeditionSpawn>(newChances);
                Array.Copy(editedSpawnBytes, 0, expeditionSpawnBytes, 10, editedSpawnBytes.Length);

                Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\quest\enemy");
                File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\quest\enemy\discoverEmSet.dems", expeditionSpawnBytes);
            }

            //CSV
            File.Create(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Expedition Log.csv").Dispose();
            using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Expedition Log.csv"))
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
    }
}
