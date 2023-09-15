using MHW_Randomizer.Crypto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
        public int PSpawnIndex;
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
        private NR3Generator PickSupplyID;

        #endregion

        private int[] LowRankMonsterIDs;
        private int[] MonsterIDs;

        private GMD StoryTargetText;

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
            PSpawnIndex = data2[27];
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
        }

        public void Randomize()
        {
            QuestData.MonsterMapSobjCount = new int[102, 43];
            SobjFilesCache = ChunkOTF.files.Values.Where(o => o.EntireName.Contains(@"\enemy\boss\em") && !QuestData.BadSobjs.Contains(o.Name)).ToArray();
            SobjFilesBigMCache = SobjFilesCache.Where(o => !o.Name.Contains("em101_00_st101")).ToArray();
            File.Create(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Quest Log.txt").Dispose();

            r = new XorShift128Generator(IoC.Randomizer.Seed);
            PickSobj = new NR3Generator(IoC.Randomizer.Seed);
            PickIcon = new NR3Generator(IoC.Randomizer.Seed);
            PickMap = new NR3Generator(IoC.Randomizer.Seed);
            PickSize = new NR3Generator(IoC.Randomizer.Seed);
            PickSupplyID = new NR3Generator(IoC.Randomizer.Seed);

            Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\quest\enemy\boss\");
            Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\text\quest");

            IoC.Randomizer.MissingMIBFiles = new List<string>();

            StoryTargetText = new GMD(ChunkOTF.files["storyTarget_eng.gmd"].Extract());

            if (IoC.Settings.RandomSupplyBox || IoC.Settings.RandomSupplyBoxItems)
                GetSupplyIDs();

            if (IoC.Settings.ExtraSupplyBoxes > 0)
                AddSupplyBoxIDs();

            using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Quest Log.txt"))
            {
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
                    RandomizeQuests(false, false, file, multiObjQuests, null, false, false, true);
                }

                if (IoC.Settings.RandomizeMultiMon)
                {
                    file.WriteLine("\n\n---------------------------------------------------------------------------");
                    file.WriteLine("                   Low/High Rank Multi-Monster Quests                      ");
                    file.WriteLine("---------------------------------------------------------------------------");
                    string[] multiMonQuests = QuestData.HuntMultiMonster;
                    if (!IoC.Settings.DontRandomizeSlay)
                        multiMonQuests = multiMonQuests.Concat(QuestData.SlayMultiMonster).ToArray();
                    RandomizeQuests(false, false, file, multiMonQuests, null, false, false, true);
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
                        RandomizeQuests(false, true, file, QuestData.IBHuntMultiObjective, null, false, false, true);
                    }

                    if (IoC.Settings.RandomizeMultiMon)
                    {
                        file.WriteLine("\n\n---------------------------------------------------------------------------");
                        file.WriteLine("                       Iceborne Multi-Monster Quests                       ");
                        file.WriteLine("---------------------------------------------------------------------------");
                        string[] multiMonQuests = QuestData.IBHuntMultiMonster;
                        if (!IoC.Settings.DontRandomizeSlay)
                            multiMonQuests = multiMonQuests.Concat(QuestData.IBSlayMultiMonster).ToArray();
                        RandomizeQuests(false, true, file, multiMonQuests, null, false, false, true);
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

            EditAlnks();
            EditMaps();
        }

        private void RandomizeQuests(bool isStoryQuest, bool iceborne, StreamWriter file, string[] Quests = null, Dictionary<string, StoryQuestData> StoryQuests = null, bool captureQuest = false, bool duplicateMonQuest = false, bool multiMonQuest = false)
        {
            GMD GMDFile;
            int[] monsterFor00103 = new int[2];
            foreach (string questNumber in isStoryQuest ? StoryQuests.Keys.ToArray() : Quests)
            {

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

                bool isLowRank = RankIndex == 0;

                int[] currentRankMonsterIDs;
                if (captureQuest)
                    currentRankMonsterIDs = isLowRank ? LowRankMonsterIDs.Where(o => !QuestData.ElderDragonIDs.Contains(o)).ToArray() : MonsterIDs.Where(o => !QuestData.ElderDragonIDs.Contains(o)).ToArray();
                else
                {
                    currentRankMonsterIDs = isLowRank ? LowRankMonsterIDs : MonsterIDs;
                    if (questNumber == "00301")
                        currentRankMonsterIDs = currentRankMonsterIDs.Where(o => o != 33).ToArray();
                }

                //Pick Random Map
                if (IoC.Settings.RandomMaps && (!isStoryQuest || StoryQuests[questNumber].CanRandomizeMap))
                {
                    //if (iceborne)
                    //{
                    //    MapIDIndex = Array.IndexOf(QuestData.MapIDs, QuestData.TestMaps[PickMap.Next(QuestData.TestMaps.Length)]);
                    //    PSpawnIndex = 0;
                    //}
                    //else

                    if (IoC.Settings.IncludeArenaMap)
                    {
                        MapIDIndex = QuestData.ValidArenaMapIndexes[PickMap.Next(7 + (Convert.ToInt32(iceborne) * 6))];
                        if (QuestData.ArenaMaps.Contains(QuestData.MapIDs[MapIDIndex]))
                            PSpawnIndex = 0;
                    }
                    else
                        MapIDIndex = QuestData.ValidMapIndexes[PickMap.Next(5 + (Convert.ToInt32(iceborne) * 2))];
                }

                if (iceborne && MapIDIndex == 3)
                    //Remove alatreon as a possible monster if map is coral highlands as it causes a blinding white light effect on that map
                    currentRankMonsterIDs = currentRankMonsterIDs.Where(o => o != 87).ToArray();

                //Write quest info to log
                file.WriteLine("\n---------------------- " + "Quest: " + QuestData.QuestName[questNumber == "66859" || questNumber == "66860" ? (questNumber == "66859" ? 64802 : 66835) : int.Parse(questNumber)] + ", Quest ID: " + QIDText + ", Map: " + QuestData.MapNames[MapIDIndex] + " ------------------------------");

                byte[] fsm = null;

                #region Monster

                //Loop to go through each monster
                for (int m = 0; m < 7; m++)
                {
                    //Should remove all unneeded monsters if is in a arena
                    if (IoC.Settings.RandomMaps && !IoC.Settings.AllMonstersInArena && m != 0 && QuestData.ArenaMaps.Contains(QuestData.MapIDs[MapIDIndex]) && (RemoveMultiMonster(multiMonQuest, m) || (duplicateMonQuest && m >= int.Parse(MObjC1Text))) && (!isStoryQuest || StoryQuests[questNumber].CanRandomizeMap))
                        MID[m] = 0;

                    //If there is no monster at that index skip unless the option for two monster quest is true
                    if (MID[m] == 0 && !(IoC.Settings.TwoMonsterQuests && m == 1))
                        continue;

                    //If Zorah Magdaros quest only randomize the second monster (First is Zorah)
                    //if ((questNumber == "00401" && m == 0) || (questNumber == "00504" && m == 0))
                    //    continue;

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

                    int RandomMonsterIndex = r.Next(currentRankMonsterIDs.Length);

                    int oldMonsterID = MID[m] - 1;

                    //Randomizes it to be the same as the first monster if its a kill duplicate quest and is a low enough monster slot value
                    if (m < int.Parse(MObjC1Text) && m != 0 && duplicateMonQuest)
                    {
                        MID[m] = MID[0];
                        RandomMonsterIndex = Array.IndexOf(currentRankMonsterIDs, MID[0] - 1);
                    }
                    //Pick another monster if the monster is a duplicate to another one in the quest
                    else if (!IoC.Settings.DuplicateMonster || (isStoryQuest && !StoryQuests[questNumber].CanRandomizeMap))
                    {
                        int FoundMonsterIndex = Array.IndexOf(MID, currentRankMonsterIDs[RandomMonsterIndex] + 1);
                        while (FoundMonsterIndex != -1 && FoundMonsterIndex < m)
                        {
                            RandomMonsterIndex = r.Next(currentRankMonsterIDs.Length);
                            FoundMonsterIndex = Array.IndexOf(MID, currentRankMonsterIDs[RandomMonsterIndex] + 1);
                        }
                    }

                    //Causes too many issue when replacing the Rathian in "The Best Kind of Quest", so hard code it to always be Rathian
                    if (questNumber == "00301" && m == 2)
                    {
                        RandomMonsterIndex = Array.IndexOf(currentRankMonsterIDs, 9);
                        //clear fsm so it doesn't make one as its not needed
                        fsm = null;
                    }

                    if (questNumber == "00103")
                        RandomMonsterIndex = monsterFor00103[m];

                    //Witcher crosser over quest unbeatable if monster 3 isn't leshen
                    if (questNumber == "50910" && m == 3)
                    {
                        currentRankMonsterIDs = currentRankMonsterIDs.Append(23).ToArray();
                        RandomMonsterIndex = currentRankMonsterIDs.Length - 1;
                    }

                    //Set the monster to the randomized monster
                    MID[m] = currentRankMonsterIDs[RandomMonsterIndex] + 1;

                    //For setting quest 00103's monster to the same as 00102 (are linked)
                    if (questNumber == "00102")
                        monsterFor00103[m] = RandomMonsterIndex;

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

                    //Pick random sobj
                    if ((m == 1 && IoC.Settings.TwoMonsterQuests && (!isStoryQuest || StoryQuests[questNumber].CanRandomizeMap || oldMonsterID == -1)) || (IoC.Settings.RandomSobj && (!isStoryQuest || StoryQuests[questNumber].CanRandomizeMap)))
                    {
                        Files[] SobjFiles;
                        if (currentRankMonsterIDs[RandomMonsterIndex] == 26)
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
                        if (currentRankMonsterIDs[RandomMonsterIndex] == 26 && oldMSobj.Contains("em101_00_st101"))
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
                    MSobj[m] = QuestData.MonsterMapSobjCount[currentRankMonsterIDs[RandomMonsterIndex], MapIDIndex];

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
                            sobj[j + 13] = (byte)currentRankMonsterIDs[RandomMonsterIndex];
                            break;
                        }
                        else
                            CDCount = 0;
                    }
                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\quest\enemy\boss\" + QuestData.MonsterStageEmNumber[currentRankMonsterIDs[RandomMonsterIndex]] + QuestData.MapIDs[MapIDIndex] + "_" + MSobj[m].ToString("00") + ".sobj", sobj);

                    #endregion

                    QuestData.MonsterMapSobjCount[currentRankMonsterIDs[RandomMonsterIndex], MapIDIndex]++;

                    bool changeIcon = true;
                    if (isStoryQuest)
                        changeIcon = StoryQuests[questNumber].ChangeQuestIcon;

                    if (IoC.Settings.RandomIcons && m < 5 && MonIcons[m] != 127)
                        MonIcons[m] = PickIcon.Next(QuestData.IconList.Length - 1);
                    else if ((m < 5 && MonIcons[m] != 127 && changeIcon) || (IoC.Settings.TwoMonsterQuests && m == 1 && changeIcon))
                        MonIcons[m] = Array.IndexOf(QuestData.IconList, QuestData.MonsterNames[currentRankMonsterIDs[RandomMonsterIndex] + 1]);

                    //Write monster info to the log
                    file.WriteLine("Monster " + (m + 1).ToString() + ": " + QuestData.MonsterNames[currentRankMonsterIDs[RandomMonsterIndex] + 1] + "\tSobj File Name: " + QuestData.MonsterStageEmNumber[currentRankMonsterIDs[RandomMonsterIndex]] + QuestData.MapIDs[MapIDIndex] + "_" + MSobj[m].ToString("00") + ".sobj" + "\tOld sobj File Name: " + oldMSobj
                       + (IoC.Settings.RandomIcons && m < 5 && MonIcons[m] != 127 ? "\tIcon: " + QuestData.IconList[MonIcons[m]] : ""));

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
                        if (questNumber == "01404" && MID[0] != 80)
                        {
                            value = "Slay " + QuestData.MonsterNames[MID[0]];
                            StoryTargetText.Entries[560].Value = "(Don't need to load Dragonrazer)";
                        }
                        else if (StoryQuests[questNumber].MultiObjectiveHunt)
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

                #region Save mib File

                data = null;
                //data3 = null;
                data4 = null;
                cipher = new Cipher(key);
                int MapID = MapIDIndex;
                if (StarsIndex < 11)
                {
                    for (int i = 0; i < QuestData.ForbiddenMapIDs.Length; i++)
                    {
                        if (QuestData.ForbiddenMapIDs[i] == QuestData.MapIDs[MapID])
                        {
                            MessageBox.Show("THIS MAP IS ILLEGAL FOR LOW AND HIGH RANK QUESTS");
                            return;
                        }
                    }
                    for (int i = 0; i < 7; i++)
                    {
                        if (MID[i] > 61)
                        {
                            MessageBox.Show("Monster #" + (i + 1).ToString() + " IS ILLEGAL FOR LOW AND HIGH RANK QUESTS");
                            return;
                        }
                    }
                }
                data = ChunkOTF.files["questData_" + questNumber + ".mib"].Extract();
                WriteData = cipher.Decipher(data);
                for (int i = 4; i < WriteData.Length; i++)
                    data3[i - 4] = WriteData[i];
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
                for (int i = 0; i < QuestData.MapIDs.Length; i++)
                {
                    if (MapIDIndex == i)
                    {
                        buffer = BitConverter.GetBytes(QuestData.MapIDs[i]);
                        data3[23] = buffer[0];
                        data3[24] = buffer[1];
                        data3[25] = buffer[2];
                        data3[26] = buffer[3];
                    }
                }
                buffer = BitConverter.GetBytes(Convert.ToByte(PSpawnIndex));
                data3[27] = buffer[0];
                if (FSpawnIsChecked == true)
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
                for (int i = 0; i < QuestData.ObjectiveIDs.Length; i++)
                {
                    if (MObjT1Index == i)
                    {
                        buffer = BitConverter.GetBytes(QuestData.ObjectiveIDs[i]);
                        data3[83] = buffer[0];
                    }
                }
                if (MultiMon1IsChecked == true)
                    data3[84] = 04;
                else data3[84] = 0;
                buffer = BitConverter.GetBytes(Convert.ToUInt16(MObjID1));
                data3[87] = buffer[0];
                data3[88] = buffer[1];
                buffer = BitConverter.GetBytes(Convert.ToUInt16(MObjC1Text));
                data3[89] = buffer[0];
                data3[90] = buffer[1];
                for (int i = 0; i < QuestData.ObjectiveIDs.Length; i++)
                {
                    if (MObjT2Index == i)
                    {
                        buffer = BitConverter.GetBytes(QuestData.ObjectiveIDs[i]);
                        data3[91] = buffer[0];
                    }
                }
                if (MultiMon2IsChecked == true)
                    data3[92] = 04;
                else data3[92] = 0;
                buffer = BitConverter.GetBytes(Convert.ToUInt16(MObjID2));
                data3[95] = buffer[0];
                data3[96] = buffer[1];
                buffer = BitConverter.GetBytes(Convert.ToUInt16(MObjC2Text));
                data3[97] = buffer[0];
                data3[98] = buffer[1];
                if (MultiOjectiveIsChecked == true)
                    data3[99] = 2;
                else data3[99] = 1;
                for (int i = 0; i < QuestData.ObjectiveIDs.Length; i++)
                {
                    if (SObjT1Index == i)
                    {
                        buffer = BitConverter.GetBytes(QuestData.ObjectiveIDs[i]);
                        data3[100] = buffer[0];
                    }
                }
                if (SObj1MMIsChecked == true)
                    data3[100] = 04;
                else data3[100] = 0;
                buffer = BitConverter.GetBytes(Convert.ToUInt16(SObjID1Index));
                data3[104] = buffer[0];
                data3[105] = buffer[1];
                buffer = BitConverter.GetBytes(Convert.ToUInt16(SObjC1Text));
                data3[106] = buffer[0];
                data3[107] = buffer[1];
                for (int i = 0; i < QuestData.ObjectiveIDs.Length; i++)
                {
                    if (SObjT2Index == i)
                    {
                        buffer = BitConverter.GetBytes(QuestData.ObjectiveIDs[i]);
                        data3[108] = buffer[0];
                    }
                }
                if (SObj2MMIsChecked == true)
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
                    if (Tempered[i] == true)
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

                for (int i = 4; i < WriteData.Length; i++)
                    WriteData[i] = data3[i - 4];
                data4 = cipher.Encipher(WriteData);

                File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\quest\questData_" + questNumber + ".mib", data4);

                #endregion

            }

            //Override the zone file with a different one as a dummy to remove the softlocking cutscene
            if (iceborne && isStoryQuest)
            {
                Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\quest\q01601\zone");
                File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\quest\q01601\zone\01601_qtev.zon", ChunkOTF.files["01602_qtev.zon"].Extract());
            }
        }

        /// <summary>
        /// Check all the conditions for multi monster quests. Return true if it should remove the <paramref name="m"/> monster
        /// </summary>
        /// <param name="multiMonster">Is it a multi monster quest</param>
        /// <param name="m">current monster index</param>
        /// <returns></returns>
        private bool RemoveMultiMonster(bool multiMonster, int m)
        {
            //Check if its not a multi monster quest first
            if (!multiMonster)
                return true;

            //Only include 2 monsters if multi objective
            if (MultiOjectiveIsChecked)
                //(only need to check if != 1 because already check if != 0)
                return m != 1;

            //Only include required monsters if multi monster
            return MultiMon1IsChecked && m >= int.Parse(MObjC1Text);
        }

        //Supply item randomization step 3
        private static void RandomizeSupplyItems()
        {
            Dictionary<ushort, string> itemPool = JsonConvert.DeserializeObject<Dictionary<ushort, string>>(Encoding.UTF8.GetString(Properties.Resources.SupplyItems)).ToDictionary(x => x.Key, x => x.Value);
            NR3Generator r = new NR3Generator(IoC.Randomizer.Seed);

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
                        if (items[i].Item_Count > 0)
                        {
                            items[i].Item_Id = itemPool.ElementAt(r.Next(itemPool.Count)).Key;
                            file.WriteLine("Item " + i + ": " + itemPool[items[i].Item_Id]);
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
            NR3Generator dice = new NR3Generator(IoC.Randomizer.Seed);

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
                        //Pick two random numbers for the item count and pick the lowest
                        int roll1 = dice.Next(1, 31);
                        int roll2 = dice.Next(1, 31);
                        ushort lowest = (ushort)Math.Min(roll1, roll2);
                        items.Add(new SuppItems { Item_Count = lowest });
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

        private static void EditMaps()
        {
            if (IoC.Settings.RandomMaps || IoC.Settings.RandomSobj)
            {
                #region Rotten Vale Blockade

                byte[] levelObjects = ChunkOTF.files["st104_gm.sobj"].Extract();

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

                Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\stage\st104\common\set");
                File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\stage\st104\common\set\st104_gm.sobj", levelObjects);

                #endregion

                #region Elder Recess Blockade

                levelObjects = ChunkOTF.files["st105_gm.sobj"].Extract();

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

                Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\stage\st105\common\set");
                File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\stage\st105\common\set\st105_gm.sobj", levelObjects);

                #endregion

                #region Hoarfrost Reach Blockade

                levelObjects = ChunkOTF.files["st108_gm.sobj"].Extract();

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

                Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\stage\st108\common\set");
                File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\stage\st108\common\set\st108_gm.sobj", levelObjects);

                #endregion

                #region Guiding Lands Blockade

                //Unlock the blocked off areas in the guiding lands
                if (IoC.Settings.RandomizeIBQuests)
                {
                    //Unlock the blocked off areas
                    byte[] unblockBytes = ChunkOTF.files["st109_v03.sdl"].Extract();
                    Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\stage\st109\common\sdl");
                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\stage\st109\common\sdl\st109_v01.sdl", unblockBytes);
                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\stage\st109\common\sdl\st109_v02.sdl", unblockBytes);
                    //Do the same for bake files
                    unblockBytes = ChunkOTF.files["st109_v03_bake.sdl"].Extract();
                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\stage\st109\common\sdl\st109_v01_bake.sdl", unblockBytes);
                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\stage\st109\common\sdl\st109_v02_bake.sdl", unblockBytes);

                    //Light
                    unblockBytes = ChunkOTF.files["LL_st109_area_v03.llsd"].Extract();
                    Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\stage\st109\common\light");
                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\stage\st109\common\light\LL_st109_area_v01.llsd", unblockBytes);
                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\stage\st109\common\light\LL_st109_area_v02.llsd", unblockBytes);

                    unblockBytes = ChunkOTF.files["LL_st109_v03.llk"].Extract();
                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\stage\st109\common\light\LL_st109_v01.llk", unblockBytes);
                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\stage\st109\common\light\LL_st109_v02.llk", unblockBytes);

                    //etc
                    unblockBytes = ChunkOTF.files["st109_v03.bkipr"].Extract();
                    Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\stage\st109\common\etc");
                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\stage\st109\common\etc\st109_v01.bkipr", unblockBytes);
                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\stage\st109\common\etc\st109_v02.bkipr", unblockBytes);
                    unblockBytes = ChunkOTF.files["st109_v03.umbra"].Extract();
                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\stage\st109\common\etc\st109_v01.umbra", unblockBytes);
                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\stage\st109\common\etc\st109_v02.umbra", unblockBytes);


                    //Remove blocked off area collision by replacing it with a different one
                    unblockBytes = ChunkOTF.files["st109_F_col_add.sbc"].Extract();
                    Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\stage\st109\st109_V\col");
                    Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\stage\st109\st109_W\col");
                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\stage\st109\st109_V\col\st109_V_col.sbc", unblockBytes);
                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\stage\st109\st109_W\col\st109_W_col.sbc", unblockBytes);
                }

                #endregion
            }
        }

        private static void EditAlnks()
        {
            Files[] alnkFiles = ChunkOTF.files.Values.Where(o => o.Name.Contains("alnk")).ToArray();
            alnkFiles = alnkFiles.OrderBy(x => x.Name).ToArray();
            for (int a = 0; a < alnkFiles.Length; a++)
            {
                Files alnk = alnkFiles[a];
                if (alnk.Name.Contains("ems"))
                    continue;

                byte[] alnkBytes = alnk.Extract();
                byte[] newAlnk = new byte[44];
                Array.Copy(alnkBytes, newAlnk, 44);
                newAlnk[40] = 7;
                if (QuestData.IsGroundMonster[a])
                {
                    newAlnk = newAlnk.Concat(Properties.Resources.m101_Ground).ToArray();
                    newAlnk = newAlnk.Concat(Properties.Resources.m102_Ground).ToArray();
                    newAlnk = newAlnk.Concat(Properties.Resources.m103_Ground).ToArray();
                    newAlnk = newAlnk.Concat(Properties.Resources.m104_Ground).ToArray();
                    newAlnk = newAlnk.Concat(Properties.Resources.m105_Ground).ToArray();
                    newAlnk = newAlnk.Concat(Properties.Resources.m108_Ground).ToArray();
                    newAlnk = newAlnk.Concat(Properties.Resources.m109_Ground).ToArray();
                }
                else
                {
                    newAlnk = newAlnk.Concat(Properties.Resources.m101_Flying).ToArray();
                    newAlnk = newAlnk.Concat(Properties.Resources.m102_Flying).ToArray();
                    newAlnk = newAlnk.Concat(Properties.Resources.m103_Flying).ToArray();
                    newAlnk = newAlnk.Concat(Properties.Resources.m104_Flying).ToArray();
                    newAlnk = newAlnk.Concat(Properties.Resources.m105_Flying).ToArray();
                    newAlnk = newAlnk.Concat(Properties.Resources.m108_Flying).ToArray();
                    newAlnk = newAlnk.Concat(Properties.Resources.m109_Flying).ToArray();
                }

                Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + alnk.EntireName.Truncate(alnk.EntireName.Length - 14));
                File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + alnk.EntireName, newAlnk);
                if (alnk.Name.Contains("em018"))
                {
                    //Edit the monster ID
                    newAlnk[8] = 99;
                    Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"/em/em018/05/data");
                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"/em/em018/05/data/em018.dtt_alnk", newAlnk);
                }

                if (alnk.Name.Contains("em023"))
                {
                    //Edit the monster ID
                    newAlnk[8] = 92;
                    Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"/em/em023/05/data");
                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"/em/em023/05/data/em023.dtt_alnk", newAlnk);
                }
            }
            //Add custom think table for Shara to transform
            if (IoC.Settings.IncludeShara)
            {
                //Add custom think file
                Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"/em/em126/00/data");
                File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"/em/em126/00/data/em126_00.thk", Properties.Resources.em126_00thk);
            }
        }
    }
}
