using System;
using System.IO;
using System.Linq;
using Troschuetz.Random.Generators;

namespace MHW_Randomizer
{
    public class MonsterRandomizer
    {
        public static void Randomize()
        {
            if (ViewModels.Settings.RandomMonsterAttackStatus || ViewModels.Settings.RandomMonsterElement)
                RandomAttackDebuffs();
        }

        private static void RandomAttackDebuffs()
        {
            Files[] colFiles = ChunkOTF.files.Values.Where(o => o.Name.Contains(".col") && o.EntireName.Contains("collision\\em") && !o.Name.Contains("_") && !o.EntireName.Contains("shell")).ToArray();
            colFiles = colFiles.OrderBy(x => x.Name).ToArray();
            //"atk" string represented in bytes
            byte[] atkBytes = new byte[] { 65, 84, 75, 0 };
            string[] statusNames = new string[] { "Poison", "Deadly Poison", "Paralysis", "Sleep", "Blast", "Slime", "Stun", "Miasma", "Bleed" };
            string[] elementNames = new string[] { "None", "Thunder", "Water", "Ice", "Fire", "Dragon" };

            NR3Generator statusRandom = new NR3Generator(ViewModels.Randomizer.Seed);
            NR3Generator elementRandom = new NR3Generator(ViewModels.Randomizer.Seed);
            File.Create(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\Monster Attack Log.txt").Dispose();
            using (StreamWriter file = File.AppendText(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\Monster Attack Log.txt"))
            {
                foreach (Files col in colFiles)
                {
                    if (!ViewModels.Settings.IncludeSmallMonsterDebuffs && col.Name.Contains("ems") || col.Name == "ems062.col")
                        continue;
                    string[] fathernodes = col.EntireName.Split('\\');
                    file.WriteLine("Monster: " + QuestData.MonsterNames[Array.IndexOf(QuestData.MonsterEmNumber, fathernodes[2] + "_" + fathernodes[3]) + 1]);
                    var colBytes = col.Extract();
                    //Find where the attack stats are
                    int attackIndex = colBytes.BMHIndexOf(atkBytes);
                    var atk2List = StructTools.RawDeserialize<MonsterAtkStructs.Atk2>(colBytes, attackIndex + 16);

                    int[] status = new int[statusRandom.Next(1, 5)];
                    for (int n = 0; n < status.Length; n++)
                    {
                        status[n] = statusRandom.Next(n == 0 ? 1 : 0, 9);
                    }
                    uint elementIndex = elementRandom.NextUInt(1, 6);
                    foreach (var attack in atk2List)
                    {
                        #region Element

                        bool loggedAttackIndex = false;
                        bool giveRandomElement = !ViewModels.Settings.OnlyChangeExistingElement && 30 >= elementRandom.Next(101);
                        bool changeExistingElement = ViewModels.Settings.OnlyChangeExistingElement && attack.Element_Id != 0;
                        if (ViewModels.Settings.RandomMonsterElement && (giveRandomElement || changeExistingElement))
                        {
                            attack.Element_Id = elementIndex;
                            if (attack.Element_Id != 0)
                            {
                                file.WriteLine("\tAttack Index: " + attack.Index);
                                if (ViewModels.Settings.IncreaseElementPower)
                                    attack.Element_Dmg = elementRandom.Next(20, 50);
                                else
                                    attack.Element_Dmg = elementRandom.Next(10, 30);
                                file.WriteLine("\t   " + elementNames[attack.Element_Id] + " Damage: " + attack.Element_Dmg);
                                loggedAttackIndex = true;
                            }
                            else
                                attack.Element_Dmg = 0;

                            //Chose a new element if having a different one per attack
                            if (ViewModels.Settings.EachAttackDifferentElement)
                                elementIndex = elementRandom.NextUInt(1, 6);
                        }

                        #endregion
                        #region Status

                        if (!ViewModels.Settings.RandomMonsterAttackStatus)
                            continue;
                        //30% chance to give a random status/change the status
                        if (30 <= statusRandom.Next(101))
                        {
                            //Clear all the statuses if its not within the 30% chance
                            for (int a = 0; a < 9; a++)
                            {
                                //Clear status if stun is the same as one randomly chosen so there isn't alot of stun
                                if (status.Contains(6) || a != 6)
                                    attack.Statuses[a] = 0;
                            }
                            continue;
                        }

                        //poison, deadly poison, para, sleep, blast, slime, stun, miasma, bleed
                        for (int s = 0; s < 9; s++)
                        {
                            //Only check whole array if randomizing multiple statuses
                            bool giveRandomStatus = !ViewModels.Settings.OnlyChangeExistingStatus && (status[0] == s || (ViewModels.Settings.MultipleStatusesPerAttack && status.Contains(s)));
                            //attack.Statuses.All(o => o == 0) checks if the array is all 0s
                            bool changeExistingStatus = (ViewModels.Settings.OnlyChangeExistingStatus && !attack.Statuses!.All(o => o == 0) && status[0] == s) || (ViewModels.Settings.MultipleStatusesPerAttack && !attack.Statuses!.All(o => o == 0) && status.Contains(s));
                            //Don't let kestodons have blast or slime as it can break the first quest
                            bool kestodonBlast = col.Name == "ems051.col" && (s == 4 || s == 5);
                            if ((giveRandomStatus || changeExistingStatus) && !kestodonBlast)
                            {
                                if (!loggedAttackIndex)
                                {
                                    file.WriteLine("\tAttack Index: " + attack.Index);
                                    //Make it so it doesn't write this multiple times
                                    loggedAttackIndex = true;
                                }
                                //Bias towards lower numbers
                                int roll1 = statusRandom.Next(10, 101);
                                int roll2 = statusRandom.Next(10, 101);
                                int min = Math.Min(roll1, roll2);

                                //Random chance to inflict it
                                attack.Statuses[s] = min;

                                file.WriteLine("\t   " + statusNames[s] + " Chance: " + attack.Statuses[s] + "%");
                            }
                            //Don't remove stun (6)
                            else if (s != 6)
                                attack.Statuses[s] = 0;
                        }
                        if (ViewModels.Settings.EachAttackDifferentStatus)
                            for (int n = 0; n < status.Length; n++)
                            {
                                status[n] = statusRandom.Next(9);
                            }

                        #endregion
                    }
                    file.WriteLine();

                    byte[] randomizedBytes = StructTools.RawSerialize(atk2List);
                    Array.Copy(randomizedBytes, 0, colBytes, attackIndex + 16, randomizedBytes.Length);
                    Directory.CreateDirectory(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + col.EntireName.Truncate(col.EntireName.Length - 10));
                    File.WriteAllBytes(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + col.EntireName, colBytes);
                }
            }
        }

    }
}
