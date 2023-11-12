using MHW_Randomizer.Crypto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using Troschuetz.Random.Generators;

namespace MHW_Randomizer
{
    public class QuestRandomizer
    {
        public UInt64 Inspectingint32 = 0;
        public Int16 Inspectingbyte = 0;


        #region Settings

        #region Common and Objectives

        public int StarsIndex;
        public int RankIndex;
        public int MapIDIndex;
        public int PlayerSpawnIndex;
        public int TimeIndex;
        public int WeatherIndex;
        public int HRReqIndex;
        /// <summary>
        /// 1st objective type
        /// </summary>
        public int MObjT1Index;
        /// <summary>
        /// 2nd objective type
        /// </summary>
        public int MObjT2Index;
        /// <summary>
        /// Target Monster 1
        /// </summary>
        public int MObjID1;
        /// <summary>
        /// Target Monster 2
        /// </summary>
        public int MObjID2;
        public int SObjT1Index;
        public int SObjT2Index;
        public int SObjID1Index;
        public int SObjID2Index;
        public int BGMIndex;
        public int QCMusicIndex;
        public int QTypeIndex;

        public bool FSpawnIsChecked;
        public bool MultiMon1IsChecked;
        public bool MultiMon2IsChecked;
        public bool MultiOjectiveIsChecked;
        public bool SObj1MMIsChecked;
        public bool SObj2MMIsChecked;
        public bool ATFlagIsChecked;
        public bool PSGearIsChecked;

        public string QIDText;
        public uint RewardMoney;
        public uint PenaltyMoney;
        public string TimerText;
        public string MObjC1Text;
        public string MObjC2Text;
        public string SObjC1Text;
        public string SObjC2Text;
        public string RRemIDText;
        public string S1RRemIDText;
        public string S2RRemIDText;
        public string SRemIDText;
        public string HRpointText;

        #endregion

        #region Monsters

        //All Indexes
        /// <summary>
        /// Monster ID
        /// </summary>
        public int[] MID = new int[7];
        /// <summary>
        /// Monster HP
        /// </summary>
        public int[] MHtP = new int[7];
        /// <summary>
        /// Monster Attack
        /// </summary>
        public int[] MAtk = new int[7];
        /// <summary>
        /// Monster Defence
        /// </summary>
        public int[] MDef = new int[7];
        /// <summary>
        /// Monster Hp/Attack Roll
        /// </summary>
        public int[] MHAR = new int[7];
        /// <summary>
        /// Monster Size Table
        /// </summary>
        public int[] MSeT = new int[7];
        /// <summary>
        /// Monster Part HP
        /// </summary>
        public int[] MPHP = new int[7];
        /// <summary>
        /// Monster Base Status
        /// </summary>
        public int[] MBSt = new int[7];
        /// <summary>
        /// Monster Status Buildup
        /// </summary>
        public int[] MStB = new int[7];
        /// <summary>
        /// Monster Base Stun
        /// </summary>
        public int[] MBKO = new int[7];
        /// <summary>
        /// Monster Base Exhaust
        /// </summary>
        public int[] MBEx = new int[7];
        /// <summary>
        /// Monster Base Mount
        /// </summary>
        public int[] MBMo = new int[7];
        /// <summary>
        /// Monster Sequential Rules
        /// </summary>
        public int[] MSSpw = new int[5];
        /// <summary>
        /// Quest Icons
        /// </summary>
        public int[] MonIcons = new int[5];

        //These ones a value though
        public int[] MonsterSize = new int[7];
        public int[] MSobj = new int[7];

        public bool[] Tempered = new bool[7];

        //Small Monsters
        public int sMsobj;
        public int sMHPIndex;
        public int sMAtIndex;
        public int sMDeIndex;
        public string MPModText;

        #endregion

        #region Spawn, Map Icons, and Arena

        public byte[] SpawnChances = new byte[7];
        public int[] MapIcons = new int[51];
        public int[] SmallMonIcons = new int[5];

        public int SetID;
        public int NumberOfPlayers;

        public int[] ArenaTimers = new int[3];
        public int FenceCooldown;
        public int FenceUptime;
        public bool FenceSwitch;

        #endregion
        #endregion

        private readonly string key = "TZNgJfzyD2WKiuV4SglmI6oN5jP2hhRJcBwzUooyfIUTM4ptDYGjuRTP";
        private Cipher cipher;
        public byte[] data;
        public byte[] data2 = new byte[1100];
        public byte[] data3 = new byte[1100];
        public byte[] data4;
        public byte[] ReadData;
        public byte[] WriteData;

        public string CurrentMibFile;
        public int TotalMibFiles { get; set; }
        public int TotalSobjFiles { get; set; }

        private Files[] SobjFilesCache;
        private Files[] SobjFilesBigMCache;

        private static List<string> SupplyBoxIDs = new List<string>();

        #region Random Number Generators

        private XorShift128Generator r;
        private NR3Generator PickSobj;
        private NR3Generator PickIcon;
        private NR3Generator PickMap;
        private NR3Generator PickSize;
        private NR3Generator PickFenceTime;
        private NR3Generator PickSupplyID;

        #endregion

        private int[] LowRankMonsterIDs;
        private int[] MonsterIDs;

        private GMD StoryTargetText;

        private byte[] XenoMapWarpBytes = ChunkOTF.files["00991.sobjl"].Extract();
        private List<int> ValidMapIndexes = new List<int>();

        public void OpenMIBFIle(byte[] mibFile)
        {
            cipher = new Cipher(key);

            data = mibFile;
            ReadData = cipher.Decipher(data);
            for (int i = 4; i < ReadData.Length; i++)
                data2[i - 4] = ReadData[i];
            Int32 RV = 0;
            #region Common and Objectives
            QIDText = BitConverter.ToInt32(data2, 6).ToString();
            StarsIndex = data2[10];
            RankIndex = data2[19];
            RV = BitConverter.ToInt32(data2, 23);
            MapIDIndex = Array.IndexOf(QuestData.MapIDs, RV);
            PlayerSpawnIndex = data2[27];
            FSpawnIsChecked = data2[31] == 0;
            TimeIndex = data2[39];
            WeatherIndex = data2[43];
            RewardMoney = BitConverter.ToUInt32(data2, 51);
            PenaltyMoney = BitConverter.ToUInt32(data2, 55);
            TimerText = BitConverter.ToUInt32(data2, 63).ToString();
            for (int i = 0; i < 5; i++)
                MonIcons[i] = BitConverter.ToUInt16(data2, 68 + 2 * i);
            HRReqIndex = data2[78];

            //Main Objective 1
            MObjT1Index = Array.IndexOf(QuestData.ObjectiveIDs, data2[83]);
            MultiMon1IsChecked = data2[84] == 04;
            MObjID1 = BitConverter.ToUInt16(data2, 87);
            MObjC1Text = BitConverter.ToUInt16(data2, 89).ToString();
            //Main Objective 2
            MObjT2Index = Array.IndexOf(QuestData.ObjectiveIDs, data2[91]);
            MultiMon2IsChecked = data2[92] == 04;
            MObjID2 = BitConverter.ToUInt16(data2, 95);
            MObjC2Text = BitConverter.ToUInt16(data2, 97).ToString();
            MultiOjectiveIsChecked = data2[99] != 1;

            //Sub Objective 1
            SObjT1Index = Array.IndexOf(QuestData.ObjectiveIDs, data2[100]);
            SObj1MMIsChecked = data2[101] == 04;
            SObjID1Index = BitConverter.ToUInt16(data2, 104);
            SObjC1Text = BitConverter.ToUInt16(data2, 106).ToString();
            //Sub Objective 2
            SObjT2Index = Array.IndexOf(QuestData.ObjectiveIDs, data2[108]);
            SObj2MMIsChecked = data2[109] == 04;
            SObjID2Index = BitConverter.ToUInt16(data2, 112);
            SObjC2Text = BitConverter.ToUInt16(data2, 114).ToString();

            BGMIndex = data2[120];
            QCMusicIndex = data2[124];
            QTypeIndex = Array.IndexOf(QuestData.QuestTypeIDs, data2[128]);
            ATFlagIsChecked = data2[130] == 2 || data2[130] == 3;
            PSGearIsChecked = data2[130] == 1 || data2[130] == 3;

            //Rem Stuff
            RRemIDText = BitConverter.ToUInt32(data2, 132).ToString();
            S1RRemIDText = BitConverter.ToUInt32(data2, 136).ToString();
            S2RRemIDText = BitConverter.ToUInt32(data2, 140).ToString();
            SRemIDText = BitConverter.ToUInt32(data2, 144).ToString();

            HRpointText = BitConverter.ToUInt32(data2, 160).ToString();
            #endregion
            #region Monsters
            //Monster Stats
            for (int i = 0; i < 7; i++)
            {
                MID[i] = BitConverter.ToInt32(data2, 172 + 65 * i) + 1;
                MSobj[i] = BitConverter.ToInt32(data2, 176 + 65 * i);
                Tempered[i] = data2[184 + 65 * i] == 1;
                MHtP[i] = BitConverter.ToInt32(data2, 185 + 65 * i);
                MAtk[i] = BitConverter.ToInt32(data2, 189 + 65 * i);
                MDef[i] = BitConverter.ToInt32(data2, 193 + 65 * i);
                MHAR[i] = BitConverter.ToInt32(data2, 197 + 65 * i);
                MSeT[i] = BitConverter.ToInt32(data2, 205 + 65 * i);
                MPHP[i] = BitConverter.ToInt32(data2, 213 + 65 * i);
                MBSt[i] = BitConverter.ToInt32(data2, 217 + 65 * i);
                MStB[i] = BitConverter.ToInt32(data2, 221 + 65 * i);
                MBKO[i] = BitConverter.ToInt32(data2, 225 + 65 * i);
                MBEx[i] = BitConverter.ToInt32(data2, 229 + 65 * i);
                MBMo[i] = BitConverter.ToInt32(data2, 233 + 65 * i);
                MonsterSize[i] = BitConverter.ToInt32(data2, 201 + 65 * i);
            }
            sMsobj = BitConverter.ToInt32(data2, 627);
            sMHPIndex = BitConverter.ToInt32(data2, 631);
            sMAtIndex = BitConverter.ToInt32(data2, 635);
            sMDeIndex = BitConverter.ToInt32(data2, 639);
            MPModText = BitConverter.ToInt32(data2, 644).ToString();
            #endregion
            #region Spawn, Map Icons, and Arena
            for (int i = 0; i < 5; i++)
                MSSpw[i] = data2[652 + 4 * i];
            for (int i = 0; i < SpawnChances.Length; i++)
                SpawnChances[i] = data2[672 + (4 * i)];
            for (int i = 0; i < MapIcons.Length; i++)
                MapIcons[i] = BitConverter.ToInt32(data2, 704 + (4 * i));
            for (int i = 0; i < 5; i++)
            {
                //Set it to 127 to be able to differenciate it between there being a icon and not being a icon as there is a icon at index 0
                //The flag at this offset used in this if is 0 if there is no icon, and 1 if there is a icon
                if (data2[908 + (4 * i)] == 0)
                    SmallMonIcons[i] = 127;
                else 
                    SmallMonIcons[i] = data2[928 + (4 * i)];
            }
            SetID = BitConverter.ToInt32(data2, 948);
            RV = BitConverter.ToInt32(data2, 952);
            if (RV == 0)
                NumberOfPlayers = 4;
            else 
                NumberOfPlayers = RV;
            for (int i = 0; i < 3; i++)
                ArenaTimers[i] = BitConverter.ToInt32(data2, 956 + (4 * i));

            FenceSwitch = data2[980] == 128;
            FenceCooldown = BitConverter.ToInt32(data2, 988);
            FenceUptime = BitConverter.ToInt32(data2, 992);
            #endregion
        }

        public void Randomize()
        {
            SobjFilesCache = ChunkOTF.files.Values.Where(o => o.EntireName.Contains(@"\enemy\boss\em") && !QuestData.BadSobjs.Contains(o.Name) && !Regex.IsMatch(o.Name, @"em\d{3}_\d{2}_st109_(5|6)0.sobj") && !QuestData.BadGuidingSobjs.Contains(o.Name)).ToArray();
            SobjFilesBigMCache = SobjFilesCache.Where(o => !o.Name.Contains("em101_00_st101")).ToArray();
            File.Create(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Quest Log.txt").Dispose();

            r = new XorShift128Generator(IoC.Randomizer.Seed);
            PickSobj = new NR3Generator(IoC.Randomizer.Seed);
            PickIcon = new NR3Generator(IoC.Randomizer.Seed);
            PickMap = new NR3Generator(IoC.Randomizer.Seed);
            PickSize = new NR3Generator(IoC.Randomizer.Seed);
            PickFenceTime = new NR3Generator(IoC.Randomizer.Seed);
            PickSupplyID = new NR3Generator(IoC.Randomizer.Seed);

            Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\quest\enemy\boss\");
            Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\text\quest");

            IoC.Randomizer.MissingMIBFiles = new List<string>();

            StoryTargetText = new GMD(ChunkOTF.files["storyTarget_eng.gmd"].Extract());

            if (IoC.Settings.RandomSupplyBox || IoC.Settings.RandomSupplyBoxItems)
                GetSupplyIDs();

            if (IoC.Settings.ExtraSupplyBoxes > 0)
                AddSupplyBoxIDs();

            //Set the folder to q00991, for some reason this one uses q00804's one
            XenoMapWarpBytes[33] = 0x39;
            XenoMapWarpBytes[34] = 0x39;
            XenoMapWarpBytes[35] = 0x31;

            using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Quest Log.txt"))
            {
                ValidMapIndexes = ValidMapIndexes.Concat(QuestData.ValidMapIndexes).ToList();

                if (IoC.Settings.IncludeArenaMap)
                    ValidMapIndexes = ValidMapIndexes.Concat(QuestData.ValidArenaMapIndexes).ToList();

                if (IoC.Settings.IBMapsInBaseGame)
                    ValidMapIndexes = ValidMapIndexes.Concat(QuestData.ValidIBMapIndexes).ToList();

                //Set all the values to 10
                for (int i = 0; i < QuestData.MonsterChance.Length; i++)
                    QuestData.MonsterChance[i] = 10;
                //Clear the hash set so it doesn't add the chances from the last randomization
                QuestData.MonstersToAddChance = new HashSet<int>();

                //Set up IDs
                LowRankMonsterIDs = QuestData.LowRankBigMonsterIDs;
                MonsterIDs = QuestData.BigMonsterIDs;

                if (IoC.Settings.IncludeLeshen)
                {
                    MonsterIDs = MonsterIDs.Append(23).ToArray();
                    MonsterIDs = MonsterIDs.Append(51).ToArray();
                }
                if (IoC.Settings.IncludeXenojiiva)
                    MonsterIDs = MonsterIDs.Append(26).ToArray();
                if (IoC.Settings.IncludeBehemoth)
                    MonsterIDs = MonsterIDs.Append(15).ToArray();
                if (IoC.Settings.HighRankMonInLowRank)
                    LowRankMonsterIDs = MonsterIDs;

                //Remove Raging Brachydios because gets stuck going to their second phase
                if (IoC.Settings.IBMonstersInLowRank)
                    LowRankMonsterIDs = LowRankMonsterIDs.Concat(QuestData.BigMonsterIDsIB.Where(mon => !QuestData.BigMonsterIDs.Contains(mon) && mon != 96)).ToArray();
                if (IoC.Settings.IBMonstersInHighRank)
                    MonsterIDs = MonsterIDs.Concat(QuestData.BigMonsterIDsIB.Where(mon => !QuestData.BigMonsterIDs.Contains(mon) && mon != 96)).ToArray();

                //Set up the cumulative chance
                QuestData.TotalMonsterChance = LowRankMonsterIDs.Length * 10;

                file.WriteLine("---------------------------------------------------------------------------");
                file.WriteLine("                             Story Hunt Quests                             ");
                file.WriteLine("---------------------------------------------------------------------------");
                RandomizeQuests(true, false, file, null, QuestData.StoryHuntQuest);

                if (!IoC.Settings.DontRandomizeSlay)
                {
                    file.WriteLine("\n\n---------------------------------------------------------------------------");
                    file.WriteLine("                             Story Slay Quests                             ");
                    file.WriteLine("---------------------------------------------------------------------------");
                    RandomizeQuests(true, false, file, null, QuestData.StorySlayQuest);
                }

                file.WriteLine("\n\n---------------------------------------------------------------------------");
                file.WriteLine("                        Low/High Rank Hunt Quests                          ");
                file.WriteLine("---------------------------------------------------------------------------");
                RandomizeQuests(false, false, file, QuestData.BigMonsterHuntQuests);

                if (!IoC.Settings.DontRandomizeSlay)
                {
                    file.WriteLine("\n\n---------------------------------------------------------------------------");
                    file.WriteLine("                        Low/High Rank Slay Quests                          ");
                    file.WriteLine("---------------------------------------------------------------------------");
                    RandomizeQuests(false, false, file, QuestData.BigMonsterSlayQuests);
                }

                if (!IoC.Settings.DontRandomizeCapture)
                {
                    file.WriteLine("\n\n---------------------------------------------------------------------------");
                    file.WriteLine("                      Low/High Rank Capture Quests                         ");
                    file.WriteLine("---------------------------------------------------------------------------");
                    RandomizeQuests(false, false, file, QuestData.BigMonsterCaptureQuests, null, true);
                }

                if (IoC.Settings.RandomizeMultiObj)
                {
                    file.WriteLine("\n\n---------------------------------------------------------------------------");
                    file.WriteLine("                  Low/High Rank Multi-Objective Quests                     ");
                    file.WriteLine("---------------------------------------------------------------------------");
                    string[] multiObjQuests = QuestData.HuntMultiObjective;
                    if (!IoC.Settings.DontRandomizeSlay)
                        multiObjQuests = multiObjQuests.Concat(QuestData.SlayMultiObjective).ToArray();
                    RandomizeQuests(false, false, file, multiObjQuests, null, false, false);
                }

                if (IoC.Settings.RandomizeMultiMon)
                {
                    file.WriteLine("\n\n---------------------------------------------------------------------------");
                    file.WriteLine("                   Low/High Rank Multi-Monster Quests                      ");
                    file.WriteLine("---------------------------------------------------------------------------");
                    string[] multiMonQuests = QuestData.HuntMultiMonster;
                    if (!IoC.Settings.DontRandomizeSlay)
                        multiMonQuests = multiMonQuests.Concat(QuestData.SlayMultiMonster).ToArray();
                    RandomizeQuests(false, false, file, multiMonQuests, null, false, false);
                }

                if (IoC.Settings.RandomizeDuplicate)
                {
                    file.WriteLine("\n\n---------------------------------------------------------------------------");
                    file.WriteLine("                     Low/High Rank Duplicate Quests                        ");
                    file.WriteLine("---------------------------------------------------------------------------");
                    string[] duplicateQuests = QuestData.HuntDuplicate;
                    if (!IoC.Settings.DontRandomizeSlay)
                        duplicateQuests = duplicateQuests.Concat(QuestData.SlayDuplicate).ToArray();
                    RandomizeQuests(false, false, file, duplicateQuests, null, false, true);
                }

                if (IoC.Settings.RandomizeIBQuests)
                {
                    //If the IB maps have been included in the base game don't need to add the iceborne maps here
                    if (!IoC.Settings.IBMapsInBaseGame)
                        ValidMapIndexes = ValidMapIndexes.Concat(QuestData.ValidIBMapIndexes).ToList();

                    //Iceborne story quests
                    MonsterIDs = QuestData.BigMonsterIDsIB;
                    if (IoC.Settings.IceborneOnlyMonsters)
                        MonsterIDs = MonsterIDs.Where(mon => !QuestData.BigMonsterIDs.Contains(mon)).ToArray();
                    if (IoC.Settings.IncludeHighRankOnly)
                        MonsterIDs = MonsterIDs.Concat(QuestData.IBHighRankOnlyMonsters).ToArray();
                    if (IoC.Settings.IncludeShara)
                        MonsterIDs = MonsterIDs.Append(81).ToArray();
                    if (IoC.Settings.IncludeFuriousRajang)
                        MonsterIDs = MonsterIDs.Append(92).ToArray();
                    if (IoC.Settings.IncludeAlatreon)
                        MonsterIDs = MonsterIDs.Append(87).ToArray();
                    //Only add these ones
                    if (IoC.Settings.IncludeLeshen && IoC.Settings.IncludeHighRankOnly)
                    {
                        MonsterIDs = MonsterIDs.Append(23).ToArray();
                        MonsterIDs = MonsterIDs.Append(51).ToArray();
                    }
                    if (IoC.Settings.IncludeLeshen && IoC.Settings.IncludeHighRankOnly)
                        MonsterIDs = MonsterIDs.Append(15).ToArray();
                    //if (IoC.Settings.IncludeFatalis)
                    //    MonsterIDs = MonsterIDs.Append(101).ToArray();

                    file.WriteLine("\n\n---------------------------------------------------------------------------");
                    file.WriteLine("                       Iceborne Story Hunt Quests                          ");
                    file.WriteLine("---------------------------------------------------------------------------");
                    RandomizeQuests(true, true, file, null, QuestData.IBStoryHuntQuest);

                    //Refresh lists with iceborne
                    if (!IoC.Settings.DontRandomizeSlay)
                    {
                        file.WriteLine("\n\n---------------------------------------------------------------------------");
                        file.WriteLine("                       Iceborne Story Slay Quests                          ");
                        file.WriteLine("---------------------------------------------------------------------------");
                        RandomizeQuests(true, true, file, null, QuestData.IBStorySlayQuest);
                    }

                    //Iceborne quests
                    file.WriteLine("\n\n---------------------------------------------------------------------------");
                    file.WriteLine("                           Iceborne Hunt Quests                            ");
                    file.WriteLine("---------------------------------------------------------------------------");
                    RandomizeQuests(false, true, file, QuestData.IBBigMonsterHuntQuests);


                    if (!IoC.Settings.DontRandomizeSlay)
                    {
                        file.WriteLine("\n\n---------------------------------------------------------------------------");
                        file.WriteLine("                           Iceborne Slay Quests                            ");
                        file.WriteLine("---------------------------------------------------------------------------");
                        RandomizeQuests(false, true, file, QuestData.IBBigMonsterSlayQuests);
                    }
                    //Refresh lists with iceborne
                    if (!IoC.Settings.DontRandomizeCapture)
                    {
                        file.WriteLine("\n\n---------------------------------------------------------------------------");
                        file.WriteLine("                          Iceborne Capture Quests                          ");
                        file.WriteLine("---------------------------------------------------------------------------");
                        RandomizeQuests(false, true, file, QuestData.IBBigMonsterCaptureQuests, null, true);
                    }

                    if (IoC.Settings.RandomizeMultiObj)
                    {
                        file.WriteLine("\n\n---------------------------------------------------------------------------");
                        file.WriteLine("                      Iceborne Multi-Objective Quests                      ");
                        file.WriteLine("---------------------------------------------------------------------------");
                        RandomizeQuests(false, true, file, QuestData.IBHuntMultiObjective, null, false, false);
                    }

                    if (IoC.Settings.RandomizeMultiMon)
                    {
                        file.WriteLine("\n\n---------------------------------------------------------------------------");
                        file.WriteLine("                       Iceborne Multi-Monster Quests                       ");
                        file.WriteLine("---------------------------------------------------------------------------");
                        string[] multiMonQuests = QuestData.IBHuntMultiMonster;
                        if (!IoC.Settings.DontRandomizeSlay)
                            multiMonQuests = multiMonQuests.Concat(QuestData.IBSlayMultiMonster).ToArray();
                        RandomizeQuests(false, true, file, multiMonQuests, null, false, false);
                    }
                    if (IoC.Settings.RandomizeDuplicate)
                    {
                        file.WriteLine("\n\n---------------------------------------------------------------------------");
                        file.WriteLine("                         Iceborne Duplicate Quests                         ");
                        file.WriteLine("---------------------------------------------------------------------------");
                        RandomizeQuests(false, true, file, QuestData.IBHuntDuplicate, null, false, true);
                    }
                }
            }

            StoryTargetText.Save(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\text\storyTarget_eng.gmd");

            if (IoC.Settings.RandomSupplyBoxItems)
                RandomizeSupplyItems();

        }

        private void RandomizeQuests(bool isStoryQuest, bool iceborne, StreamWriter file, string[] Quests = null, Dictionary<string, StoryQuestData> StoryQuests = null, bool captureQuest = false, bool duplicateMonQuest = false)
        {
            GMD GMDFile;
            int[] monsterFor00103 = new int[2];
            foreach (string questNumber in isStoryQuest ? StoryQuests.Keys.ToArray() : Quests)
            {
                List<int> previousQuestMonsters = new List<int>();

                if (!ChunkOTF.files.ContainsKey("questData_" + questNumber + ".mib"))
                {
                    if (questNumber == "64802" && ChunkOTF.files.ContainsKey("questData_66859.mib"))
                        continue;
                    else if (questNumber == "66835" && ChunkOTF.files.ContainsKey("questData_66860.mib"))
                        continue;

                    IoC.Randomizer.MissingMIBFiles.Add("questData_" + questNumber + ".mib");
                    continue;
                }

                OpenMIBFIle(ChunkOTF.files["questData_" + questNumber + ".mib"].Extract());

                //If Zorah Magdaros quest set the objective
                if (questNumber == "00401" || questNumber == "00504")
                {
                    //If this setting is false then don't change the Zorah Quests
                    if (!IoC.Settings.RandomizeZorahStoryQuests)
                        continue;

                    //Change the objective
                    MObjT1Index = 5;
                    MObjC1Text = "1";

                    //Remove all the NPCs
                    byte[] zorahSobjl = ChunkOTF.files["00401.sobjl"].Extract();
                    zorahSobjl[8] = 0;
                    Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\quest\q00401\set\");
                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\quest\q00401\set\00401.sobjl", zorahSobjl);
                    zorahSobjl = ChunkOTF.files["00504.sobjl"].Extract();
                    zorahSobjl[8] = 0;
                    Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\quest\q00504\set\");
                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\quest\q00504\set\00504.sobjl", zorahSobjl);

                    //If not randomizing maps then pick a random one here
                    //Pick a random map after creating the zorah sobjl just incase it gets overwritten with the xeno one
                    if (!IoC.Settings.RandomMaps)
                        PickRandomMap(questNumber, duplicateMonQuest);
                }

                if (questNumber == "50802" && IoC.Settings.MakePandorasArena30Minutes)
                    TimerText = "30";

                bool isLowRank = RankIndex == 0;

                int[] currentRankMonsterIDs;
                if (captureQuest)
                    currentRankMonsterIDs = isLowRank ? LowRankMonsterIDs.Where(o => !QuestData.UncaptureableMonsterIDs.Contains(o)).ToArray() : MonsterIDs.Where(o => !QuestData.UncaptureableMonsterIDs.Contains(o)).ToArray();
                else
                {
                    currentRankMonsterIDs = isLowRank ? LowRankMonsterIDs : MonsterIDs;
                    if (questNumber == "00301")
                        currentRankMonsterIDs = currentRankMonsterIDs.Where(o => o != 33).ToArray();
                }

                //Pick Random Map
                if (IoC.Settings.RandomMaps && (!isStoryQuest || StoryQuests[questNumber].CanRandomizeMap))
                {
                    PickRandomMap(questNumber, duplicateMonQuest, captureQuest, iceborne);
                }

                if (MapIDIndex == 3)
                    //Remove alatreon as a possible monster if map is coral highlands as it causes a blinding white light effect on that map
                    //Remove leshen too as possible monster as they get stuck
                    currentRankMonsterIDs = currentRankMonsterIDs.Where(o => o != 87 || (o != 23 || o != 51)).ToArray();
                else if (MapIDIndex == 8)
                    //Remove alatreon as a possible monster if map is hoarfrost reach as it causes them to get stuck
                    currentRankMonsterIDs = currentRankMonsterIDs.Where(o => o != 87).ToArray();

                if (IoC.Settings.OnePlayerQuests)
                    NumberOfPlayers = 1;

                //Just recalculate it each quest just as a failsafe as this doesn't take long
                RecalculateTotalChance(currentRankMonsterIDs);

                //Write quest info to log
                file.WriteLine("\n---------------------- " + "Quest: " + QuestData.QuestName[questNumber == "66859" || questNumber == "66860" ? (questNumber == "66859" ? 64802 : 66835) : int.Parse(questNumber)] + ", Quest ID: " + QIDText + ", Map: " + QuestData.MapNames[MapIDIndex] + " ------------------------------");

                //Write it to the log if the current map is the arena (challenge) map and the fence switch is enabled
                if (MapIDIndex == 12 && FenceSwitch)
                    file.WriteLine("\t\tArena Fence Uptime: " + FenceUptime + " Seconds\t\tArena Fence Cooldown: " + FenceCooldown + " Seconds");

                byte[] fsm = null;

                #region Monster

                //Loop to go through each monster
                for (int m = 0; m < 7; m++)
                {
                    //Should remove all unneeded monsters if is in a arena
                    if (IoC.Settings.RandomMaps && !IoC.Settings.AllMonstersInArena && m != 0 && QuestData.ArenaMaps.Contains(QuestData.MapIDs[MapIDIndex]) && RemoveMultiMonster(MultiMon1IsChecked || MultiOjectiveIsChecked, duplicateMonQuest, m) && (!isStoryQuest || StoryQuests[questNumber].CanRandomizeMap))
                        MID[m] = 0;

                    //If there is no monster at that index skip unless the option for two monster quest is true
                    if (MID[m] == 0 && !(IoC.Settings.TwoMonsterQuests && m == 1))
                        continue;

                    //Clear fsm file
                    fsm = null;

                    bool isVariant = false;
                    if (MID[m] != 0)
                        //Check if its a variant, if so need to add _00
                        isVariant = int.Parse(QuestData.MonsterVariantNumber[MID[m] - 1]) != 0;

                    if (isStoryQuest && StoryQuests[questNumber].CreateFSM && MID[m] != 0 && ChunkOTF.files.ContainsKey(@"\quest\q" + questNumber + @"\fsm\em\" + QuestData.MonsterEmNumber[MID[m] - 1] + (isVariant ? "_00" : "") + ".fsm"))
                        fsm = ChunkOTF.files[@"\quest\q" + questNumber + @"\fsm\em\" + QuestData.MonsterEmNumber[MID[m] - 1] + (isVariant ? "_00" : "") + ".fsm"].Extract();
                    else if (isStoryQuest && StoryQuests[questNumber].CreateFSM && m == 0 && ChunkOTF.files.ContainsKey(@"\quest\q" + questNumber + @"\fsm\em\emXXX_00.fsm"))
                        fsm = ChunkOTF.files[@"\quest\q" + questNumber + @"\fsm\em\emXXX_00.fsm"].Extract();
                    else if (isStoryQuest && m == 0 && ChunkOTF.files.ContainsKey(@"\quest\q" + questNumber + @"\fsm\em\50910_em127_00.fsm"))
                        fsm = ChunkOTF.files[@"\quest\q" + questNumber + @"\fsm\em\50910_em127_00.fsm"].Extract();

                    //Pick a random size percent between range if both aren't 100
                    if (IoC.Settings.MonsterMinSize != 100 && IoC.Settings.MonsterMaxSize != 100)
                        MonsterSize[m] = PickSize.Next(IoC.Settings.MonsterMinSize, IoC.Settings.MonsterMaxSize + 1);

                    int RandomMonsterID = PickMonsterID(currentRankMonsterIDs);

                    //int RandomMonsterIndex = r.Next(currentRankMonsterIDs.Length);

                    int oldMonsterID = MID[m] - 1;

                    //Randomizes it to be the same as the first monster if its a kill duplicate quest and is a low enough monster slot value
                    if (m < int.Parse(MObjC1Text) && m != 0 && duplicateMonQuest)
                    {
                        MID[m] = MID[0];
                        RandomMonsterID = MID[0] - 1;
                    }
                    //Pick another monster if the monster is a duplicate to another one in the quest
                    //Don't allow duplicate monsters on story quests with cutscenes as it will probably mess up the cutscene or have both of them in it
                    else if (!IoC.Settings.DuplicateMonster || (isStoryQuest && !StoryQuests[questNumber].CanRandomizeMap))
                    {
                        int FoundMonsterIndex = Array.IndexOf(MID, RandomMonsterID + 1);
                        while (FoundMonsterIndex != -1 && FoundMonsterIndex < m)
                        {
                            RandomMonsterID = PickMonsterID(currentRankMonsterIDs);
                            FoundMonsterIndex = Array.IndexOf(MID, RandomMonsterID + 1);
                        }
                    }

                    //Causes too many issue when replacing the Rathian in "The Best Kind of Quest", so hard code it to always be Rathian
                    if (questNumber == "00301" && m == 2)
                    {
                        RandomMonsterID = 9;
                        //clear fsm so it doesn't make one as its not needed
                        fsm = null;
                    }

                    if (questNumber == "00103")
                        RandomMonsterID = monsterFor00103[m];

                    //Witcher crosser over quest unbeatable if monster 3 isn't leshen
                    if (questNumber == "50910" && m == 3)
                    {
                        RandomMonsterID = 23;
                    }

                    //Set the monster to the randomized monster
                    MID[m] = RandomMonsterID + 1;

                    //For setting quest 00103's monster to the same as 00102 (are linked)
                    if (questNumber == "00102")
                        monsterFor00103[m] = RandomMonsterID;

                    //Check if the new monster is also a variant
                    isVariant = int.Parse(QuestData.MonsterVariantNumber[MID[m] - 1]) != 0;

                    //Delete the old fsm folders
                    if (Directory.Exists(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\quest\q" + questNumber + @"\fsm\em\") && m == 0)
                        Directory.Delete(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\quest\q" + questNumber + @"\fsm\em\", true);
                    if (fsm != null)
                    {
                        //Create a copy of the fsm file but with the name for the randomized monster
                        Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\quest\q" + questNumber + @"\fsm\em\");
                        if (questNumber != "50910")
                            File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\quest\q" + questNumber + @"\fsm\em\" + QuestData.MonsterEmNumber[MID[m] - 1] + (isVariant ? "_00" : "") + ".fsm", fsm);
                        else
                            File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\quest\q" + questNumber + @"\fsm\em\50910_" + QuestData.MonsterEmNumber[MID[m] - 1] + (isVariant ? "_00" : "") + ".fsm", fsm);
                    }

                    #region sobj Randomization

                    string oldMSobj = "";
                    byte[] sobj;

                    //Pick random sobj (always give the zorah quests a random one)
                    if ((m == 1 && IoC.Settings.TwoMonsterQuests && (!isStoryQuest || StoryQuests[questNumber].CanRandomizeMap || oldMonsterID == -1)) || ((IoC.Settings.RandomSobj || questNumber == "00401" || questNumber == "00504") && (!isStoryQuest || StoryQuests[questNumber].CanRandomizeMap)))
                    {
                        Files[] SobjFiles;
                        //Make it so xeno isn't in great jagras' den
                        if (RandomMonsterID == 26)
                            SobjFiles = SobjFilesBigMCache.Where(o => o.Name.Contains("st" + QuestData.MapIDs[MapIDIndex])).ToArray();
                        else
                            SobjFiles = SobjFilesCache.Where(o => o.Name.Contains("st" + QuestData.MapIDs[MapIDIndex])).ToArray();
                        int sobjIndex = PickSobj.Next(SobjFiles.Length);

                        sobj = SobjFiles[sobjIndex].Extract();
                        oldMSobj = SobjFiles[sobjIndex].Name;

                    }
                    //Else use old monsters one
                    else
                    {
                        oldMSobj = QuestData.MonsterStageEmNumber[oldMonsterID] + QuestData.MapIDs[MapIDIndex] + "_" + MSobj[m].ToString("00") + ".sobj";
                        if (RandomMonsterID == 26 && oldMSobj.Contains("em101_00_st101"))
                        {
                            Files[] SobjFiles;
                            SobjFiles = SobjFilesBigMCache;
                            int sobjIndex = PickSobj.Next(SobjFiles.Length);

                            sobj = ChunkOTF.files[SobjFiles[sobjIndex].Name].Extract();
                            oldMSobj = SobjFiles[sobjIndex].Name;
                        }
                        else
                            sobj = ChunkOTF.files[oldMSobj].Extract();
                    }
                    //Add 1 so the expedition randomizer creates ones for 0, and makes it so the bad sobjs don't get potentially used if they're one used for a story
                    MSobj[m] = QuestData.MonsterMapSobjCount[RandomMonsterID, MapIDIndex] + 1;

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
                            sobj[j + 13] = (byte)RandomMonsterID;
                            break;
                        }
                        else
                            CDCount = 0;
                    }
                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\quest\enemy\boss\" + QuestData.MonsterStageEmNumber[RandomMonsterID] + QuestData.MapIDs[MapIDIndex] + "_" + MSobj[m].ToString("00") + ".sobj", sobj);

                    QuestData.MonsterMapSobjCount[RandomMonsterID, MapIDIndex]++;

                    #endregion

                    bool changeIcon = true;
                    if (isStoryQuest)
                        changeIcon = StoryQuests[questNumber].ChangeQuestIcon;

                    if (IoC.Settings.RandomIcons && m < 5 && MonIcons[m] != 127)
                        MonIcons[m] = PickIcon.Next(QuestData.IconList.Length - 1);
                    else if ((m < 5 && MonIcons[m] != 127 && changeIcon) || (IoC.Settings.TwoMonsterQuests && m == 1 && changeIcon))
                        MonIcons[m] = Array.IndexOf(QuestData.IconList, QuestData.MonsterNames[RandomMonsterID + 1]);

                    //Write monster info to the log
                    file.WriteLine("Monster " + (m + 1).ToString() + ": " + QuestData.MonsterNames[RandomMonsterID + 1] + "\tSobj File Name: " + QuestData.MonsterStageEmNumber[RandomMonsterID] + QuestData.MapIDs[MapIDIndex] + "_" + MSobj[m].ToString("00") + ".sobj" + "\tOld sobj File Name: " + oldMSobj
                       + (IoC.Settings.RandomIcons && m < 5 && MonIcons[m] != 127 ? "\tIcon: " + QuestData.IconList[MonIcons[m]] : ""));

                }

                List<int> IDsToRemove = new List<int>();
                foreach (int id in QuestData.MonstersToAddChance)
                {
                    QuestData.MonsterChance[id] += 1;

                    //If the chance is 10 remove it
                    if (QuestData.MonsterChance[id] == 10)
                        //Add the IDs to a separate list so it doesn't confuse the loop and cause a error
                        IDsToRemove.Add(id);
                }

                //Now remove the IDs so it doesn't cause a error
                foreach (int id in IDsToRemove)
                    QuestData.MonstersToAddChance.Remove(id);

                //Set the first monster's chance to 0 since its guaranteed to be the target one
                QuestData.MonsterChance[MID[0] - 1] = 0;
                QuestData.MonstersToAddChance.Add(MID[0] - 1);

                //Loop through the monsters and set their chances
                for (int monIDChance = 1; monIDChance < 7; monIDChance++)
                {
                    if (MID[monIDChance] == 0)
                        continue;

                    //Since in multi monster quests all monsters are the target ones make their chance 0
                    //Or if its a multiobjective quest can assume the second monster is a target one
                    if (MultiMon1IsChecked || (MultiOjectiveIsChecked && monIDChance == 1))
                    {
                        QuestData.MonsterChance[MID[monIDChance] - 1] = 0;
                        QuestData.MonstersToAddChance.Add(MID[monIDChance] - 1);
                        continue;
                    }

                    //For the non target ones just remove 50% until they are 0
                    if (QuestData.MonsterChance[MID[monIDChance] - 1] < 5)
                    {
                        QuestData.MonsterChance[MID[monIDChance] - 1] = 0;
                        QuestData.MonstersToAddChance.Add(MID[monIDChance] - 1);
                    }
                    else
                    {
                        QuestData.MonsterChance[MID[monIDChance] - 1] -= 5;
                        QuestData.MonstersToAddChance.Add(MID[monIDChance] - 1);
                    }
                }

                #endregion

                if (!isStoryQuest || StoryQuests[questNumber].ChangeObjective)
                {
                    //Change the quest objective
                    MObjID1 = MID[0] - 1;
                    if (MID[1] != 0)
                        MObjID2 = MID[1] - 1;
                }

                if (IoC.Settings.TwoMonsterQuests && !duplicateMonQuest)
                {
                    //Set monster 2's stats to be the same as monster 1 as it doesn't have any stats
                    MHtP[1] = MHtP[0];
                    MAtk[1] = MAtk[0];
                    MDef[1] = MDef[0];
                    MHAR[1] = MHAR[0];
                    MSeT[1] = MSeT[0];
                    MPHP[1] = MPHP[0];
                    MBSt[1] = MBSt[0];
                    MStB[1] = MStB[0];
                    MBKO[1] = MBKO[0];
                    MBEx[1] = MBEx[0];
                    MBMo[1] = MBMo[0];
                    MonsterSize[1] = MonsterSize[0];
                    Tempered[1] = Tempered[0];


                    MultiOjectiveIsChecked = true;
                    MObjT2Index = MObjT1Index;
                    MObjC2Text = "1";
                }

                if (IoC.Settings.RandomSupplyBox)
                    SRemIDText = SupplyBoxIDs[PickSupplyID.Next(SupplyBoxIDs.Count)];

                bool changeText = true;
                if (isStoryQuest)
                    changeText = StoryQuests[questNumber].ChangeQuestBookObjText;

                #region GMD

                if (isStoryQuest)
                {
                    for (int i = 0; i < StoryQuests[questNumber].QuestObjTextIndexs.Length; i++)
                    {
                        int entryIndex = StoryQuests[questNumber].QuestObjTextIndexs[i];
                        string value = "";
                        List<string> textToReplace = QuestData.MonsterNames.Where(o => StoryTargetText.Entries[entryIndex].Value.Contains(o, StringComparison.OrdinalIgnoreCase)).ToList();
                        //Change the Zorah quest text to say hunt the monster
                        if (questNumber == "00401" || questNumber == "00504")
                            StoryTargetText.Entries[entryIndex].Value = "Hunt a " + QuestData.MonsterNames[MID[0]];
                        //If its not lunastra then change the text
                        else if (questNumber == "50802" && MID[0] != 18)
                            StoryTargetText.Entries[entryIndex].Value = "Slay a " + QuestData.MonsterNames[MID[0]];
                        //If its not velkhana then change the text
                        else if (questNumber == "01404" && MID[0] != 80)
                        {
                            StoryTargetText.Entries[entryIndex].Value = "Slay a " + QuestData.MonsterNames[MID[0]];
                            StoryTargetText.Entries[560].Value = "(Don't need to load Dragonrazer)";
                        }

                        if (StoryQuests[questNumber].MultiObjectiveHunt)
                            value = StoryTargetText.Entries[entryIndex].Value.Replace(textToReplace[textToReplace.Count - 1], IoC.Settings.RandomIcons ? "???" : QuestData.MonsterNames[MID[i]]);
                        //add a and to the text if two monster quests is enabled to make it clear you need to kill 2 monsters
                        else if (IoC.Settings.TwoMonsterQuests)
                            value = StoryTargetText.Entries[entryIndex].Value.Replace(textToReplace[textToReplace.Count - 1], IoC.Settings.RandomIcons ? "???" : QuestData.MonsterNames[MID[0]]) + " and " + (IoC.Settings.RandomIcons ? "???" : QuestData.MonsterNames[MID[1]]);
                        else
                            value = StoryTargetText.Entries[entryIndex].Value.Replace(textToReplace[textToReplace.Count - 1], IoC.Settings.RandomIcons ? "???" : QuestData.MonsterNames[MID[0]]);

                        StoryTargetText.Entries[entryIndex].Value = value;
                    }
                }

                if (!(QuestData.HuntMultiMonster.Contains(questNumber) || QuestData.HuntMultiObjective.Contains(questNumber) || QuestData.IBHuntMultiMonster.Contains(questNumber) || QuestData.IBHuntMultiObjective.Contains(questNumber) ||
                    QuestData.SlayMultiMonster.Contains(questNumber) || QuestData.SlayMultiObjective.Contains(questNumber) || QuestData.IBSlayMultiMonster.Contains(questNumber)) && changeText)
                {
                    GMDFile = new GMD(ChunkOTF.files["q" + questNumber + "_eng.gmd"].Extract());
                    if (IoC.Settings.TwoMonsterQuests)
                        GMDFile.Entries[1].Value = "Hunt all target monsters";
                    else if (questNumber == "66801" || questNumber == "66803")
                        GMDFile.Entries[1].Value = "Hunt 2 " + (IoC.Settings.RandomIcons ? "???" : QuestData.MonsterNames[MID[0]]);
                    else
                    {
                        var textToReplace = QuestData.MonsterNames.Where(o => GMDFile.Entries[1].Value.Contains(o)).ToList();
                        GMDFile.Entries[1].Value = GMDFile.Entries[1].Value.Replace(textToReplace[textToReplace.Count - 1], IoC.Settings.RandomIcons ? "???" : QuestData.MonsterNames[MID[0]]);
                    }

                    GMDFile.Save(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\text\quest\" + GMDFile.Filename + "_eng.gmd");
                }
                else if (questNumber == "67103" || questNumber == "66864")
                {
                    GMDFile = new GMD(ChunkOTF.files["q" + questNumber + "_eng.gmd"].Extract());
                    GMDFile.Entries[1].Value = "Hunt all target monsters";
                    GMDFile.Save(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\text\quest\" + GMDFile.Filename + "_eng.gmd");
                }

                #endregion
                SaveMibFile(questNumber);
            }

            //Override the zone file with a different one as a dummy to remove the softlocking cutscene
            if (iceborne && isStoryQuest)
            {
                Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\quest\q01601\zone");
                File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\quest\q01601\zone\01601_qtev.zon", ChunkOTF.files["01602_qtev.zon"].Extract());
            }
        }

        private void SaveMibFile(string questNumber)
        {
            data = null;
            //data3 = null;
            data4 = null;
            cipher = new Cipher(key);
            int MapID = MapIDIndex;
            data = ChunkOTF.files["questData_" + questNumber + ".mib"].Extract();
            WriteData = cipher.Decipher(data);
            data3 = new byte[WriteData.Length - 4];
            Array.Copy(WriteData, 4, data3, 0, WriteData.Length - 4);

            #region Common and Objectives
            byte[] buffer = BitConverter.GetBytes(Convert.ToInt32(QIDText));
            data3[6] = buffer[0];
            data3[7] = buffer[1];
            data3[8] = buffer[2];
            data3[9] = buffer[3];
            buffer = BitConverter.GetBytes(Convert.ToByte(StarsIndex));
            data3[10] = buffer[0];
            buffer = BitConverter.GetBytes(Convert.ToByte(RankIndex));
            data3[19] = buffer[0];

            buffer = BitConverter.GetBytes(QuestData.MapIDs[MapIDIndex]);
            data3[23] = buffer[0];
            data3[24] = buffer[1];
            data3[25] = buffer[2];
            data3[26] = buffer[3];

            buffer = BitConverter.GetBytes(Convert.ToByte(PlayerSpawnIndex));
            data3[27] = buffer[0];
            if (FSpawnIsChecked)
                data3[31] = 0;
            else data3[31] = 1;
            buffer = BitConverter.GetBytes(Convert.ToByte(TimeIndex));
            data3[39] = buffer[0];
            buffer = BitConverter.GetBytes(Convert.ToByte(WeatherIndex));
            data3[43] = buffer[0];
            buffer = BitConverter.GetBytes(RewardMoney);
            data3[51] = buffer[0];
            data3[52] = buffer[1];
            data3[53] = buffer[2];
            data3[54] = buffer[3];
            buffer = BitConverter.GetBytes(PenaltyMoney);
            data3[55] = buffer[0];
            data3[56] = buffer[1];
            data3[57] = buffer[2];
            data3[58] = buffer[3];
            buffer = BitConverter.GetBytes(Convert.ToInt32(TimerText));
            data3[63] = buffer[0];
            data3[64] = buffer[1];
            data3[65] = buffer[2];
            data3[66] = buffer[3];
            for (int i = 0; i < 5; i++)
            {
                buffer = BitConverter.GetBytes(Convert.ToUInt16(MonIcons[i]));
                data3[68 + 2 * i] = buffer[0];
                data3[69 + 2 * i] = buffer[1];
            }
            buffer = BitConverter.GetBytes(Convert.ToByte(HRReqIndex));
            data3[78] = buffer[0];

            //Objective 1
            data3[83] = QuestData.ObjectiveIDs[MObjT1Index];
            if (MultiMon1IsChecked)
                data3[84] = 04;
            else data3[84] = 0;
            buffer = BitConverter.GetBytes(Convert.ToUInt16(MObjID1));
            data3[87] = buffer[0];
            data3[88] = buffer[1];
            buffer = BitConverter.GetBytes(Convert.ToUInt16(MObjC1Text));
            data3[89] = buffer[0];
            data3[90] = buffer[1];

            //Objective 2
            data3[91] = QuestData.ObjectiveIDs[MObjT2Index];
            if (MultiMon2IsChecked)
                data3[92] = 04;
            else data3[92] = 0;
            buffer = BitConverter.GetBytes(Convert.ToUInt16(MObjID2));
            data3[95] = buffer[0];
            data3[96] = buffer[1];
            buffer = BitConverter.GetBytes(Convert.ToUInt16(MObjC2Text));
            data3[97] = buffer[0];
            data3[98] = buffer[1];
            if (MultiOjectiveIsChecked)
                data3[99] = 2;
            else data3[99] = 1;

            //Sub objective 1
            data3[100] = QuestData.ObjectiveIDs[SObjT1Index];

            if (SObj1MMIsChecked)
                data3[101] = 04;
            else data3[101] = 0;
            buffer = BitConverter.GetBytes(Convert.ToUInt16(SObjID1Index));
            data3[104] = buffer[0];
            data3[105] = buffer[1];
            buffer = BitConverter.GetBytes(Convert.ToUInt16(SObjC1Text));
            data3[106] = buffer[0];
            data3[107] = buffer[1];

            //Sub objective 2
            data3[108] = QuestData.ObjectiveIDs[SObjT2Index];
            if (SObj2MMIsChecked)
                data3[109] = 04;
            else data3[109] = 0;
            buffer = BitConverter.GetBytes(Convert.ToUInt16(SObjID2Index));
            data3[112] = buffer[0];
            data3[113] = buffer[1];
            buffer = BitConverter.GetBytes(Convert.ToUInt16(SObjC2Text));
            data3[114] = buffer[0];
            data3[115] = buffer[1];
            buffer = BitConverter.GetBytes(Convert.ToByte(BGMIndex));
            data3[120] = buffer[0];
            buffer = BitConverter.GetBytes(Convert.ToByte(QCMusicIndex));
            data3[124] = buffer[0];
            for (int i = 0; i < QuestData.QuestTypeIDs.Length; i++)
            {
                if (QTypeIndex == i)
                {
                    buffer = BitConverter.GetBytes(QuestData.QuestTypeIDs[i]);
                    data3[128] = buffer[0];
                }
            }
            data3[130] = Convert.ToByte(2 * Convert.ToInt32(ATFlagIsChecked) + Convert.ToInt32(PSGearIsChecked));
            buffer = BitConverter.GetBytes(Convert.ToInt32(RRemIDText));
            data3[132] = buffer[0];
            data3[133] = buffer[1];
            data3[134] = buffer[2];
            data3[135] = buffer[3];
            buffer = BitConverter.GetBytes(Convert.ToInt32(S1RRemIDText));
            data3[136] = buffer[0];
            data3[137] = buffer[1];
            data3[138] = buffer[2];
            data3[139] = buffer[3];
            buffer = BitConverter.GetBytes(Convert.ToInt32(S2RRemIDText));
            data3[140] = buffer[0];
            data3[141] = buffer[1];
            data3[142] = buffer[2];
            data3[143] = buffer[3];
            buffer = BitConverter.GetBytes(Convert.ToInt32(SRemIDText));
            data3[144] = buffer[0];
            data3[145] = buffer[1];
            data3[146] = buffer[2];
            data3[147] = buffer[3];
            buffer = BitConverter.GetBytes(Convert.ToInt32(HRpointText));
            data3[160] = buffer[0];
            data3[161] = buffer[1];
            data3[162] = buffer[2];
            data3[163] = buffer[3];
            #endregion
            #region Monsters
            for (int i = 0; i < 7; i++)
            {
                if (MID[i] == 0)
                {
                    data3[172 + 65 * i] = 255;
                    data3[173 + 65 * i] = 255;
                    data3[174 + 65 * i] = 255;
                    data3[175 + 65 * i] = 255;
                }
                else
                {
                    buffer = BitConverter.GetBytes(Convert.ToInt32(MID[i] - 1));
                    data3[172 + 65 * i] = buffer[0];
                    data3[173 + 65 * i] = buffer[1];
                    data3[174 + 65 * i] = buffer[2];
                    data3[175 + 65 * i] = buffer[3];
                }
                buffer = BitConverter.GetBytes(Convert.ToInt32(MSobj[i]));
                data3[176 + 65 * i] = buffer[0];
                data3[177 + 65 * i] = buffer[1];
                data3[178 + 65 * i] = buffer[2];
                data3[179 + 65 * i] = buffer[3];
                if (Tempered[i])
                    data3[184 + 65 * i] = 1;
                else data3[184 + 65 * i] = 0;
                buffer = BitConverter.GetBytes(Convert.ToInt32(MHtP[i]));
                data3[185 + 65 * i] = buffer[0];
                data3[186 + 65 * i] = buffer[1];
                data3[187 + 65 * i] = buffer[2];
                data3[188 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MAtk[i]));
                data3[189 + 65 * i] = buffer[0];
                data3[190 + 65 * i] = buffer[1];
                data3[191 + 65 * i] = buffer[2];
                data3[192 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MDef[i]));
                data3[193 + 65 * i] = buffer[0];
                data3[194 + 65 * i] = buffer[1];
                data3[195 + 65 * i] = buffer[2];
                data3[196 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MHAR[i]));
                data3[197 + 65 * i] = buffer[0];
                data3[198 + 65 * i] = buffer[1];
                data3[199 + 65 * i] = buffer[2];
                data3[200 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MSeT[i]));
                data3[205 + 65 * i] = buffer[0];
                data3[206 + 65 * i] = buffer[1];
                data3[207 + 65 * i] = buffer[2];
                data3[208 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MPHP[i]));
                data3[213 + 65 * i] = buffer[0];
                data3[214 + 65 * i] = buffer[1];
                data3[215 + 65 * i] = buffer[2];
                data3[216 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MBSt[i]));
                data3[217 + 65 * i] = buffer[0];
                data3[218 + 65 * i] = buffer[1];
                data3[219 + 65 * i] = buffer[2];
                data3[220 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MStB[i]));
                data3[221 + 65 * i] = buffer[0];
                data3[222 + 65 * i] = buffer[1];
                data3[223 + 65 * i] = buffer[2];
                data3[224 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MBKO[i]));
                data3[225 + 65 * i] = buffer[0];
                data3[226 + 65 * i] = buffer[1];
                data3[227 + 65 * i] = buffer[2];
                data3[228 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MBEx[i]));
                data3[229 + 65 * i] = buffer[0];
                data3[230 + 65 * i] = buffer[1];
                data3[231 + 65 * i] = buffer[2];
                data3[232 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MBMo[i]));
                data3[233 + 65 * i] = buffer[0];
                data3[234 + 65 * i] = buffer[1];
                data3[235 + 65 * i] = buffer[2];
                data3[236 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MonsterSize[i]));
                data3[201 + 65 * i] = buffer[0];
                data3[202 + 65 * i] = buffer[1];
                data3[203 + 65 * i] = buffer[2];
                data3[204 + 65 * i] = buffer[3];
            }

            buffer = BitConverter.GetBytes(Convert.ToInt32(sMsobj));
            data3[627] = buffer[0];
            data3[628] = buffer[1];
            data3[629] = buffer[2];
            data3[630] = buffer[3];
            buffer = BitConverter.GetBytes(Convert.ToInt32(sMHPIndex));
            data3[631] = buffer[0];
            data3[632] = buffer[1];
            data3[633] = buffer[2];
            data3[634] = buffer[3];
            buffer = BitConverter.GetBytes(Convert.ToInt32(sMAtIndex));
            data3[635] = buffer[0];
            data3[636] = buffer[1];
            data3[637] = buffer[2];
            data3[638] = buffer[3];
            buffer = BitConverter.GetBytes(Convert.ToInt32(sMDeIndex));
            data3[639] = buffer[0];
            data3[640] = buffer[1];
            data3[641] = buffer[2];
            data3[642] = buffer[3];
            buffer = BitConverter.GetBytes(Convert.ToInt32(MPModText));
            data3[644] = buffer[0];
            data3[645] = buffer[1];
            data3[646] = buffer[2];
            data3[647] = buffer[3];
            #endregion
            #region Spawn, Map Icons, and Arena
            for (int i = 0; i < 5; i++)
                data3[652 + 4 * i] = Convert.ToByte(MSSpw[i]);
            for (int i = 0; i < SpawnChances.Length; i++)
                data3[672 + 4 * i] = SpawnChances[i];
            for (int i = 0; i < MapIcons.Length; i++)
            {
                buffer = BitConverter.GetBytes(MapIcons[i]);
                data3[704 + 4 * i] = buffer[0];
                data3[705 + 4 * i] = buffer[1];
                data3[706 + 4 * i] = buffer[2];
                data3[707 + 4 * i] = buffer[3];
            }
            for (int i = 0; i < 5; i++)
            {
                if (SmallMonIcons[i] == 127)
                {
                    //Set the flag to 0 if there is no icon
                    data3[908 + 4 * i] = 0;
                    data3[928 + 4 * i] = 0;
                }
                else
                {
                    //Set the flag to 1 if there is a icon
                    data3[908 + 4 * i] = 1;
                    data3[928 + 4 * i] = Convert.ToByte(SmallMonIcons[i]);
                }
            }
            buffer = BitConverter.GetBytes(SetID);
            data3[948] = buffer[0];
            data3[949] = buffer[1];
            data3[950] = buffer[2];
            data3[951] = buffer[3];
            if (NumberOfPlayers == 4)
            {
                data3[952] = 0;
                data3[953] = 0;
                data3[954] = 0;
                data3[955] = 0;
            }
            else
            {
                buffer = BitConverter.GetBytes(NumberOfPlayers);
                data3[952] = buffer[0];
                data3[953] = buffer[1];
                data3[954] = buffer[2];
                data3[955] = buffer[3];
            }
            for (int i = 0; i < 3; i++)
            {
                buffer = BitConverter.GetBytes(ArenaTimers[i]);
                data3[956 + 4 * i] = buffer[0];
                data3[957 + 4 * i] = buffer[1];
                data3[958 + 4 * i] = buffer[2];
                data3[959 + 4 * i] = buffer[3];
            }
            data3[980] = FenceSwitch ? (byte)128 : (byte)0;

            buffer = BitConverter.GetBytes(FenceCooldown);
            data3[988] = buffer[0];
            data3[989] = buffer[1];
            data3[990] = buffer[2];
            data3[991] = buffer[3];

            buffer = BitConverter.GetBytes(FenceUptime);
            data3[992] = buffer[0];
            data3[993] = buffer[1];
            data3[994] = buffer[2];
            data3[995] = buffer[3];
            #endregion

            Array.Copy(data3, 0, WriteData, 4, WriteData.Length - 4);
            data4 = cipher.Encipher(WriteData);

            File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\quest\questData_" + questNumber + ".mib", data4);
        }

        private void PickRandomMap(string questNumber, bool duplicateMonQuest, bool captureQuest = false, bool iceborne = false)
        {
            //Make it a copy not a reference
            List<int> currentValidMapIndexs = new List<int>().Concat(ValidMapIndexes).ToList();
            //Don't include the special arena maps if its a capture quest as you can't place traps in them
            if (!captureQuest)
            {
                if (IoC.Settings.IncludeXenoArena)
                    currentValidMapIndexs.Add(QuestData.XenoArena);

                if ((IoC.Settings.IBMapsInBaseGame || iceborne) && IoC.Settings.IncludeIBArenaMaps)
                    currentValidMapIndexs = currentValidMapIndexs.Concat(QuestData.ValidIBArenaMapIndexes).ToList();
            }

            MapIDIndex = currentValidMapIndexs[PickMap.Next(currentValidMapIndexs.Count())];
            if (QuestData.ArenaMaps.Contains(QuestData.MapIDs[MapIDIndex]))
            {
                //Force spawn to be at camp 1 or the game crashes
                PlayerSpawnIndex = 0;

                //If its the special arena set up the fence timers only if there is more than one monster in the arena
                if (MapIDIndex == 12 && (IoC.Settings.AllMonstersInArena || MultiMon1IsChecked || MultiOjectiveIsChecked || duplicateMonQuest))
                {
                    FenceSwitch = true;
                    //Round to the nearest 5
                    FenceUptime = 5 * (int)Math.Round(PickFenceTime.Next(30, 101) / 5.0f);
                    FenceCooldown = FenceUptime * 2;
                }
                //If its xeno's arena add the hitching post
                else if (MapIDIndex == 24)
                {
                    Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\quest\q" + questNumber + @"\set\");
                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\quest\q" + questNumber + @"\set\" + questNumber + ".sobjl", XenoMapWarpBytes);
                }
                //If its Xeno or a iceborne arena then change the bgm to 22 since its the only one with nice music for them
                if (MapIDIndex > 23)
                    BGMIndex = 22;
            }
        }

        /// <summary>
        /// Returns a monster ID from <paramref name="monsterIDs"/> considering their chances. Returns -1 if a monster couldn't be chosen.
        /// Also uses QuestData.CumulativeChance for the selected chance
        /// </summary>
        /// <returns></returns>
        private int PickMonsterID(int[] monsterIDs)
        {
            int selectedChance = r.Next(QuestData.TotalMonsterChance) + 1;
            int addedChance = 0;

            foreach (int id in monsterIDs)
            {
                //Add on this monsters chance
                addedChance += QuestData.MonsterChance[id];

                //Break out of the loop once the monster is chosen
                if (addedChance >= selectedChance)
                {
                    return id;
                }
            }

            //Return -1 if a monster wasn't found
            return -1;
        }

        /// <summary>
        /// Loops through all the <paramref name="monsterIDs"/> and gets the total chance of all of them
        /// </summary>
        private void RecalculateTotalChance(int[] monsterIDs)
        {
            QuestData.TotalMonsterChance = 0;
            foreach (int id in monsterIDs)
            {
                QuestData.TotalMonsterChance += QuestData.MonsterChance[id];
            }
        }

        /// <summary>
        /// Check all the conditions for multi monster quests. Return true if it should remove the <paramref name="m"/> monster
        /// </summary>
        /// <param name="multiMonster">Is it a multi monster quest</param>
        /// <param name="m">current monster index</param>
        /// <returns></returns>
        private bool RemoveMultiMonster(bool multiMonster, bool duplicateMonster, int m)
        {
            //Check if its not a multi monster quest first
            if (!multiMonster && !duplicateMonster)
                return true;

            //Only include 2 monsters if multi objective
            if (MultiOjectiveIsChecked)
                //(only need to check if != 1 because already check if != 0)
                return m != 1;

            //Only include required monsters if multi or duplicate monster
            return (MultiMon1IsChecked || duplicateMonster) && m >= int.Parse(MObjC1Text);
        }

        public void MakeNonRandomQuests1Player()
        {
            foreach (int questNumber in QuestData.QuestName.Keys)
            {
                //Format it so it has 0s ahead of the number to make it match the files
                string fileQuestNumber = questNumber.ToString("D5");

                //If the file has been randomized then skip it
                if (File.Exists(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\quest\questData_" + fileQuestNumber + ".mib"))
                    continue;

                //Continue if the quest doesn't exist
                if (!ChunkOTF.files.ContainsKey("questData_" + fileQuestNumber + ".mib"))
                    continue;

                OpenMIBFIle(ChunkOTF.files["questData_" + fileQuestNumber + ".mib"].Extract());

                NumberOfPlayers = 1;

                SaveMibFile(fileQuestNumber);
            }
        }

        //Supply item randomization step 3
        private static void RandomizeSupplyItems()
        {
            Dictionary<ushort, string> itemPool = JsonConvert.DeserializeObject<Dictionary<ushort, string>>(Encoding.UTF8.GetString(Properties.Resources.SupplyItems)).ToDictionary(x => x.Key, x => x.Value);
            Dictionary<ushort, int> itemLimits = JsonConvert.DeserializeObject<Dictionary<ushort, int>>(Encoding.UTF8.GetString(Properties.Resources.SupplyItemsLimit)).ToDictionary(x => x.Key, x => x.Value);
            XorShift128Generator r = new XorShift128Generator(IoC.Randomizer.Seed);
            XorShift128Generator countRoll = new XorShift128Generator(IoC.Randomizer.Seed);

            Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\quest\supp");
            File.Create(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Supply Log.txt").Dispose();
            using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Supply Log.txt"))
            {
                file.WriteLine("Items 8 and above only appear in the supply box if there is 2 or more players\n");
                foreach (string supp in SupplyBoxIDs)
                {
                    file.WriteLine("Supply Box ID: " + supp);
                    byte[] suppBytes = new byte[0];
                    //If its a custom extra supply box ID read it from the disk
                    if (int.Parse(supp) > 11999)
                        suppBytes = File.ReadAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\quest\supp\" + "suppData_" + supp + ".supp");
                    else
                        suppBytes = ChunkOTF.files["suppData_" + supp + ".supp"].Extract();

                    //Extract the bytes for the items
                    byte[] suppItemBytes = new byte[96];
                    Array.Copy(suppBytes, 14, suppItemBytes, 0, 96);
                    List<SuppItems> items = StructTools.RawDeserialize<SuppItems>(suppItemBytes, 0);

                    //Pick a random item
                    for (int i = 0; i < items.Count; i++)
                    {
                        //If the item count is 0 there is no item in that slot
                        if (items[i].Item_Count > 0)
                        {
                            //Pick one of the supply items
                            items[i].Item_Id = itemPool.ElementAt(r.Next(itemPool.Count)).Key;

                            //Chose a count for it within the limit
                            items[i].Item_Count = (ushort)r.Next(1, itemLimits[items[i].Item_Id] + 1);

                            //Log it
                            file.WriteLine("Item " + i + ": " + itemPool[items[i].Item_Id] + "\tCount: " + items[i].Item_Count);
                        }
                    }

                    suppItemBytes = StructTools.RawSerialize(items);
                    Array.Copy(suppItemBytes, 0, suppBytes, 14, 96);
                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\quest\supp\" + "suppData_" + supp + ".supp", suppBytes);

                    file.Write("\n\n");
                }
            }
        }

        //Supply item randomization step 2
        private static void AddSupplyBoxIDs()
        {
            NR3Generator IDr = new NR3Generator(IoC.Randomizer.Seed);
            NR3Generator amountr = new NR3Generator(IoC.Randomizer.Seed);

            string[] localSuppIDs = SupplyBoxIDs.ToArray();
            Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\quest\supp");

            for (int id = 0; id < IoC.Settings.ExtraSupplyBoxes; id++)
            {
                byte[] suppBytes = ChunkOTF.files["suppData_" + localSuppIDs[IDr.Next(localSuppIDs.Length)] +".supp"].Extract();
                List<SuppItems> items = new List<SuppItems>();

                int singlePlayAmount = amountr.Next(3, 6);
                int multiPlayAmount = amountr.Next(7, 17);
                for (int a = 0; a < 24; a++)
                {
                    if (a < singlePlayAmount || (7 < a && a - 8 < multiPlayAmount))
                    {
                        //Make the count 1 to have it valid for the last part
                        items.Add(new SuppItems { Item_Count = 1 });
                    }
                    else
                        items.Add(new SuppItems());
                }

                Array.Copy(StructTools.RawSerialize(items), 0, suppBytes, 14, 96);
                //Temporarily store the file in unfinished state on disk, rest will be done on picking random item
                File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\quest\supp\" + "suppData_" + (12000 + id) + ".supp", suppBytes);
                SupplyBoxIDs.Add((12000 + id).ToString());
            }
        }

        //Supply item randomization step 1
        private static void GetSupplyIDs()
        {
            string[] suppFiles = ChunkOTF.files.Keys.Where(o => o.Contains(".supp")).ToArray();
            SupplyBoxIDs = new List<string>();
            foreach (string supp in suppFiles)
                SupplyBoxIDs.Add(supp.Split('_', '.')[1]);
        }
    }
}