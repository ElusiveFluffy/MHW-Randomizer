using System;
using System.IO;
using System.Linq;
using Troschuetz.Random.Generators;

namespace MHW_Randomizer
{
    public class MonsterRandomizer
    {
        public static void Randomizer()
        {
            if (IoC.Settings.RandomMonsterAttackStatus || IoC.Settings.RandomMonsterElement)
                RandomAttackDebuffs();
        }

        private static void RandomAttackDebuffs()
        {
            Files[] colFiles = ChunkOTF.files.Values.Where(o => o.Name.Contains(".col") && o.EntireName.Contains("collision\\em") && !o.Name.Contains("_") && !o.EntireName.Contains("shell")).ToArray();
            //"atk" string represented in bytes
            byte[] atkBytes = new byte[] { 65, 84, 75, 0 };
            string[] statusNames = new string[] { "Poison", "Deadly Poison", "Paralysis", "Sleep", "Blast", "Slime", "Stun", "Miasma", "Bleed" };
            string[] elementNames = new string[] { "None", "Thunder", "Water", "Ice", "Fire", "Dragon" };

            NR3Generator statusRandom = new NR3Generator(IoC.Randomizer.Seed);
            NR3Generator elementRandom = new NR3Generator(IoC.Randomizer.Seed);
            File.Create(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Monster Attack Log.txt").Dispose();
            using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Monster Attack Log.txt"))
            {
                foreach (Files col in colFiles)
                {
                    if (col.Name.Contains("ems"))
                        continue;
                    string[] fathernodes = col.EntireName.Split('\\');
                    file.WriteLine("Monster: " + QuestData.MonsterNames[Array.IndexOf(QuestData.MonsterEmNumber, col.Name.Truncate(col.Name.Length - 4) + "_" + fathernodes[3]) + 1]);
                    var colBytes = col.Extract();
                    //Find where the attack stats are
                    int attackIndex = colBytes.BMHIndexOf(atkBytes);
                    var atk2List = StructTools.RawDeserialize<MonsterAtkStructs.Atk2>(colBytes, attackIndex + 16);

                    int[] status = new int[statusRandom.Next(1, 5)];
                    for (int n = 0; n < status.Length; n++)
                    {
                        status[n] = statusRandom.Next(9);
                    }
                    uint elementIndex = elementRandom.NextUInt(6);
                    foreach (var attack in atk2List)
                    {
                        #region Element

                        bool randomEle = false;
                        if (IoC.Settings.RandomMonsterElement && 30 >= elementRandom.Next(101))
                        {
                            file.WriteLine("Attack Index: " + attack.Index);
                            attack.Element_Id = elementIndex;
                            if (elementIndex != 0)
                            {
                                if (IoC.Settings.IncreaseElementPower)
                                    attack.Element_Dmg = elementRandom.Next(20, 50);
                                else
                                    attack.Element_Dmg = elementRandom.Next(10, 30);
                                file.WriteLine("   " + elementNames[elementIndex] + " Damage: " + attack.Element_Dmg);
                            }
                            else
                                attack.Element_Dmg = 0;
                            randomEle = true;
                        }

                        #endregion
                        #region Status

                        if (!IoC.Settings.RandomMonsterAttackStatus)
                            continue;
                        //30% chance to give a random status/change the status
                        if (30 <= statusRandom.Next(101))
                        {
                            for (int a = 0; a < 9; a++)
                            {
                                //Clear status if stun is the same as one randomly chosen so there isn't alot of stun
                                if (status.Contains(6) || a != 6)
                                    attack.Statuses[a] = 0;
                            }
                            continue;
                        }
                        if (!randomEle)
                            file.WriteLine("Attack Index: " + attack.Index);

                        //poison, deadly poison, para, sleep, blast, slime, stun, miasma, bleed
                        for (int a = 0; a < 9; a++)
                        {
                            //Only check whole array if randomizing multiple statuses
                            if (status[0] == a || (status.Contains(a) && IoC.Settings.MultipleStatusesPerAttack))
                            {
                                //Bias towards lower numbers
                                int roll1 = statusRandom.Next(25, 101);
                                int roll2 = statusRandom.Next(25, 101);
                                int min = Math.Min(roll1, roll2);

                                //Random chance to inflict it
                                attack.Statuses[a] = min;

                                file.WriteLine("   " + statusNames[a] + " Chance: " + attack.Statuses[a] + "%");
                            }
                            //Don't remove stun (6)
                            else if (a != 6)
                                attack.Statuses[a] = 0;
                        }
                        if (IoC.Settings.EachAttackDifferentStatus)
                            for (int n = 0; n < status.Length; n++)
                            {
                                status[n] = statusRandom.Next(9);
                            }

                        #endregion
                    }
                    file.WriteLine();

                    byte[] randomizedBytes = StructTools.RawSerialize(atk2List);
                    Array.Copy(randomizedBytes, 0, colBytes, attackIndex + 16, randomizedBytes.Length);
                    Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + col.EntireName.Truncate(col.EntireName.Length - 9));
                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + col.EntireName, colBytes);
                }
            }
        }

    }
}
