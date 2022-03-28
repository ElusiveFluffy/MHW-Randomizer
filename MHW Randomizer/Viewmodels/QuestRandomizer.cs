using MHW_Randomizer.Crypto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public bool MObj1MMIsChecked;
        public bool MObj2MMIsChecked;
        public bool MultiOIsChecked;
        public bool SObj1MMIsChecked;
        public bool SObj2MMIsChecked;
        public bool ATFlagIsChecked;
        public bool PSGearIsChecked;

        public string QIDText;
        public string RewardText;
        public string PenaltyText;
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

        public void OpenMIBFIle(byte[] mibFile)
        {
            cipher = new Cipher(key);

            data = mibFile;
            ReadData = cipher.Decipher(data);
            for (int i = 4; i < ReadData.Length; i++)
                data2[i - 4] = ReadData[i];
            Int32 RV = 0;
            #region Common and Objectives
            QIDText = BitConverter.ToInt32(new byte[] { data2[6], data2[7], data2[8], data2[9] }, 0).ToString();
            StarsIndex = data2[10];
            RankIndex = data2[19];
            RV = BitConverter.ToInt32(new byte[] { data2[23], data2[24], data2[25], data2[26] }, 0);
            for (int i = 0; i < QuestData.MapIDs.Length; i++)
                if (RV == QuestData.MapIDs[i])
                    MapIDIndex = i;
            PSpawnIndex = data2[27];
            if (data2[31] == 0)
                FSpawnIsChecked = true;
            else FSpawnIsChecked = false;
            TimeIndex = data2[39];
            WeatherIndex = data2[43];
            RewardText = BitConverter.ToUInt32(new byte[] { data2[51], data2[52], data2[53], data2[54] }, 0).ToString();
            PenaltyText = BitConverter.ToUInt32(new byte[] { data2[55], data2[56], data2[57], data2[58] }, 0).ToString();
            TimerText = BitConverter.ToUInt32(new byte[] { data2[63], data2[64], data2[65], data2[66] }, 0).ToString();
            for (int i = 0; i < 5; i++)
                MonIcons[i] = BitConverter.ToUInt16(new byte[] { data2[68 + 2 * i], data2[69 + 2 * i] }, 0);
            HRReqIndex = data2[78];
            for (int i = 0; i < QuestData.ObjectiveIDs.Length; i++)
                if (data2[83] == QuestData.ObjectiveIDs[i])
                    MObjT1Index = i;
            if (data2[84] == 04)
                MObj1MMIsChecked = true;
            else MObj1MMIsChecked = false;
            MObjID1 = BitConverter.ToUInt16(new byte[] { data2[87], data2[88] }, 0);
            MObjC1Text = BitConverter.ToUInt16(new byte[] { data2[89], data2[90] }, 0).ToString();
            for (int i = 0; i < QuestData.ObjectiveIDs.Length; i++)
                if (data2[91] == QuestData.ObjectiveIDs[i])
                    MObjT2Index = i;
            if (data2[92] == 04)
                MObj2MMIsChecked = true;
            else MObj2MMIsChecked = false;
            MObjID2 = BitConverter.ToUInt16(new byte[] { data2[95], data2[96] }, 0);
            MObjC2Text = BitConverter.ToUInt16(new byte[] { data2[97], data2[98] }, 0).ToString();
            if (data2[99] == 1)
                MultiOIsChecked = false;
            else MultiOIsChecked = true;
            for (int i = 0; i < QuestData.ObjectiveIDs.Length; i++)
                if (data2[100] == QuestData.ObjectiveIDs[i])
                    SObjT1Index = i;
            if (data2[101] == 04)
                SObj1MMIsChecked = true;
            else SObj1MMIsChecked = false;
            SObjID1Index = BitConverter.ToUInt16(new byte[] { data2[104], data2[105] }, 0);
            SObjC1Text = BitConverter.ToUInt16(new byte[] { data2[106], data2[107] }, 0).ToString();
            for (int i = 0; i < QuestData.ObjectiveIDs.Length; i++)
                if (data2[108] == QuestData.ObjectiveIDs[i])
                    SObjT2Index = i;
            if (data2[109] == 04)
                SObj2MMIsChecked = true;
            else SObj2MMIsChecked = false;
            SObjID2Index = BitConverter.ToUInt16(new byte[] { data2[112], data2[113] }, 0);
            SObjC2Text = BitConverter.ToUInt16(new byte[] { data2[114], data2[115] }, 0).ToString();
            BGMIndex = data2[120];
            QCMusicIndex = data2[124];
            for (int i = 0; i < QuestData.QuestTypeIDs.Length; i++)
                if (data2[128] == QuestData.QuestTypeIDs[i])
                    QTypeIndex = i;
            if (data2[130] == 0)
            {
                ATFlagIsChecked = false;
                PSGearIsChecked = false;
            }
            if (data2[130] == 1)
            {
                ATFlagIsChecked = false;
                PSGearIsChecked = true;
            }
            if (data2[130] == 2)
            {
                ATFlagIsChecked = true;
                PSGearIsChecked = false;
            }
            if (data2[130] == 3)
            {
                ATFlagIsChecked = true;
                PSGearIsChecked = true;
            }
            RRemIDText = BitConverter.ToUInt32(new byte[] { data2[132], data2[133], data2[134], data2[135] }, 0).ToString();
            S1RRemIDText = BitConverter.ToUInt32(new byte[] { data2[136], data2[137], data2[138], data2[139] }, 0).ToString();
            S2RRemIDText = BitConverter.ToUInt32(new byte[] { data2[140], data2[141], data2[142], data2[143] }, 0).ToString();
            SRemIDText = BitConverter.ToUInt32(new byte[] { data2[144], data2[145], data2[146], data2[147] }, 0).ToString();
            HRpointText = BitConverter.ToUInt32(new byte[] { data2[160], data2[161], data2[162], data2[163] }, 0).ToString();
            #endregion
            #region Monsters
            for (int i = 0; i < 7; i++)
            {
                if (BitConverter.ToUInt32(new byte[] { data2[172 + 65 * i], data2[173 + 65 * i], data2[174 + 65 * i], data2[175 + 65 * i] }, 0) == 4294967295)
                    MID[i] = 0;
                else MID[i] = BitConverter.ToInt32(new byte[] { data2[172 + 65 * i], data2[173 + 65 * i], data2[174 + 65 * i], data2[175 + 65 * i] }, 0) + 1;
                MSobj[i] = BitConverter.ToInt32(new byte[] { data2[176 + 65 * i], data2[177 + 65 * i], data2[178 + 65 * i], data2[179 + 65 * i] }, 0);
                if (data2[184 + 65 * i] == 0)
                    Tempered[i] = false;
                else if (data2[184 + 65 * i] == 1)
                    Tempered[i] = true;
                MHtP[i] = BitConverter.ToInt32(new byte[] { data2[185 + 65 * i], data2[186 + 65 * i], data2[187 + 65 * i], data2[188 + 65 * i] }, 0);
                MAtk[i] = BitConverter.ToInt32(new byte[] { data2[189 + 65 * i], data2[190 + 65 * i], data2[191 + 65 * i], data2[192 + 65 * i] }, 0);
                MDef[i] = BitConverter.ToInt32(new byte[] { data2[193 + 65 * i], data2[194 + 65 * i], data2[195 + 65 * i], data2[196 + 65 * i] }, 0);
                MHAR[i] = BitConverter.ToInt32(new byte[] { data2[197 + 65 * i], data2[198 + 65 * i], data2[199 + 65 * i], data2[200 + 65 * i] }, 0);
                MSeT[i] = BitConverter.ToInt32(new byte[] { data2[205 + 65 * i], data2[206 + 65 * i], data2[207 + 65 * i], data2[208 + 65 * i] }, 0);
                MPHP[i] = BitConverter.ToInt32(new byte[] { data2[213 + 65 * i], data2[214 + 65 * i], data2[215 + 65 * i], data2[216 + 65 * i] }, 0);
                MBSt[i] = BitConverter.ToInt32(new byte[] { data2[217 + 65 * i], data2[218 + 65 * i], data2[219 + 65 * i], data2[220 + 65 * i] }, 0);
                MStB[i] = BitConverter.ToInt32(new byte[] { data2[221 + 65 * i], data2[222 + 65 * i], data2[223 + 65 * i], data2[224 + 65 * i] }, 0);
                MBKO[i] = BitConverter.ToInt32(new byte[] { data2[225 + 65 * i], data2[226 + 65 * i], data2[227 + 65 * i], data2[228 + 65 * i] }, 0);
                MBEx[i] = BitConverter.ToInt32(new byte[] { data2[229 + 65 * i], data2[230 + 65 * i], data2[231 + 65 * i], data2[232 + 65 * i] }, 0);
                MBMo[i] = BitConverter.ToInt32(new byte[] { data2[233 + 65 * i], data2[234 + 65 * i], data2[235 + 65 * i], data2[236 + 65 * i] }, 0);
                MonsterSize[i] = BitConverter.ToInt32(new byte[] { data2[201 + 65 * i], data2[202 + 65 * i], data2[203 + 65 * i], data2[204 + 65 * i] }, 0);
            }
            sMsobj = BitConverter.ToInt32(new byte[] { data2[627], data2[628], data2[629], data2[630] }, 0);
            sMHPIndex = BitConverter.ToInt32(new byte[] { data2[631], data2[632], data2[633], data2[634] }, 0);
            sMAtIndex = BitConverter.ToInt32(new byte[] { data2[635], data2[636], data2[637], data2[638] }, 0);
            sMDeIndex = BitConverter.ToInt32(new byte[] { data2[639], data2[640], data2[641], data2[642] }, 0);
            MPModText = BitConverter.ToInt32(new byte[] { data2[644], data2[645], data2[646], data2[647] }, 0).ToString();
            #endregion
        }

        public void Randomize()
        {
            QuestData.MonsterMapSobjCount = new int[69, 43];
            SobjFilesCache = ChunkOTF.files.Values.Where(o => o.EntireName.Contains(@"\enemy\boss\em") && !o.Name.Contains("em102_00_st101_61.sobj")).ToArray();
            SobjFilesBigMCache = SobjFilesCache.Where(o => !o.Name.Contains("em101_00_st101")).ToArray();
            File.Create(IoC.Settings.ChunkFolderPath + @"\randomized\QuestLog.txt").Dispose();

            NR3Generator r = new NR3Generator(IoC.Randomizer.Seed);
            NR3Generator pickSobj = new NR3Generator(IoC.Randomizer.Seed);
            NR3Generator pickIcon = new NR3Generator(IoC.Randomizer.Seed);
            NR3Generator pickMap = new NR3Generator(IoC.Randomizer.Seed);
            NR3Generator pickSize = new NR3Generator(IoC.Randomizer.Seed);

            #region Quests

            int[] monsterIDs = QuestData.BigMonsterIDs;
            string[] avaliableEmNumbers = QuestData.MonsterEmNumber;

            if (IoC.Settings.IncludeLeshen)
            {
                monsterIDs = monsterIDs.Append(23).ToArray();
                monsterIDs = monsterIDs.Append(51).ToArray();
            }
            if (IoC.Settings.IncludeXenojiiva)
                monsterIDs = monsterIDs.Append(26).ToArray();
            if (IoC.Settings.IncludeBehemoth)
                monsterIDs = monsterIDs.Append(15).ToArray();

            Directory.CreateDirectory(IoC.Settings.ChunkFolderPath + @"\randomized\quest\enemy\boss\");

            IoC.Randomizer.MissingMIBFiles = new List<string>();

            //Create Non-Story Quest List
            string[] quests = QuestData.BigMonsterHuntQuests;

            bool RandomizingIB = false;

            //Add Relevent Quests to the List
            if (!IoC.Settings.DontRandomizeSlay)
                quests = quests.Concat(QuestData.BigMonsterSlayQuests).ToArray();
            if (!IoC.Settings.DontRandomizeCapture)
                quests = quests.Concat(QuestData.BigMonsterCaptureQuests).ToArray();
            if (IoC.Settings.RandomizeMultiObj)
            {
                quests = quests.Concat(QuestData.HuntMultiObjective).ToArray();
                if (!IoC.Settings.DontRandomizeSlay)
                    quests = quests.Concat(QuestData.SlayMultiObjective).ToArray();
            }
            if (IoC.Settings.RandomizeMultiMon)
            {
                quests = quests.Concat(QuestData.HuntMultiMonster).ToArray();
                if (!IoC.Settings.DontRandomizeSlay)
                    quests = quests.Concat(QuestData.SlayMultiMonster).ToArray();
            }
            if (IoC.Settings.RandomizeDuplicate)
            {
                quests = quests.Concat(QuestData.HuntDuplicate).ToArray();
                if (!IoC.Settings.DontRandomizeSlay)
                    quests = quests.Concat(QuestData.SlayDuplicate).ToArray();
            }

            //Create Story Quest List
            Dictionary<string, StoryQuestData> storyQuests = QuestData.StoryHuntQuest;
            if (!IoC.Settings.DontRandomizeSlay)
            {
                storyQuests = storyQuests.Concat(QuestData.StorySlayQuest).ToDictionary(x => x.Key, x => x.Value);
                if (IoC.Settings.RandomizeIBQuests)
                    storyQuests = storyQuests.Concat(QuestData.IBStorySlayQuest).ToDictionary(x => x.Key, x => x.Value);
            }
            if (IoC.Settings.RandomizeIBQuests)
                storyQuests = storyQuests.Concat(QuestData.IBStoryHuntQuest).ToDictionary(x => x.Key, x => x.Value);

            GMD GMDFile;
            int monsterFor00103 = 0;

            GMD storyTargetText = new GMD(ChunkOTF.files["storyTarget_eng.gmd"].ChunkState.ExtractItem(ChunkOTF.files["storyTarget_eng.gmd"]));

            for (int dlc = -1; dlc <= Convert.ToInt32(IoC.Settings.RandomizeIBQuests); dlc++)
            {
                using (StreamWriter file = File.AppendText(IoC.Settings.ChunkFolderPath + @"\randomized\QuestLog.txt"))
                {
                    if (dlc == -1)
                    {
                        file.WriteLine("---------------------------------------------------------------------------");
                        file.WriteLine("              Tweaked Story Quests (Some IB quests in here)                ");
                        file.WriteLine("---------------------------------------------------------------------------");
                    }
                    if (dlc == 0)
                    {
                        file.WriteLine("\n\n---------------------------------------------------------------------------");
                        file.WriteLine("                           Low/High Rank Quests                            ");
                        file.WriteLine("---------------------------------------------------------------------------");
                    }
                }
                foreach (string questNumber in dlc == -1 ? storyQuests.Keys.ToArray() : quests)
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

                    OpenMIBFIle(ChunkOTF.files["questData_" + questNumber + ".mib"].ChunkState.ExtractItem(ChunkOTF.files["questData_" + questNumber + ".mib"]));

                    //Pick Random Map
                    if (IoC.Settings.RandomMaps && dlc != -1)
                    {
                        MapIDIndex = QuestData.ValidMapIndexes[pickMap.Next(5 + (Convert.ToInt32(RandomizingIB) * 2))];
                    }

                    using (StreamWriter file = File.AppendText(IoC.Settings.ChunkFolderPath + @"\randomized\QuestLog.txt"))
                    {
                        file.WriteLine("\n---------------------- " + "Quest: " + QuestData.QuestName[questNumber == "66859" || questNumber == "66860" ? (questNumber == "66859" ? 64802 : 66835) : int.Parse(questNumber)] + ", Quest ID: " + QIDText + ", Map: " + QuestData.MapNames[MapIDIndex] + " ------------------------------");
                    }

                    bool isDuplicateMonQuest = QuestData.HuntDuplicate.Contains(questNumber) || QuestData.SlayDuplicate.Contains(questNumber) || QuestData.IBHuntDuplicate.Contains(questNumber);
                    //Loop to go through each monster
                    for (int m = 0; m < 7; m++)
                    {
                        //If there is no monster at that index skip
                        if (MID[m] == 0 && !(IoC.Settings.TwoMonsterQuests && m == 1 && !isDuplicateMonQuest))
                            continue;
                        //If Zorah Magdaros quest only randomize the second monster (First is Zorah)
                        if ((questNumber == "00401" && m == 0) || (questNumber == "00504" && m == 0))
                            continue;

                        byte[] fsm = null;
                        if (ChunkOTF.files.ContainsKey(@"\quest\q" + questNumber + @"\fsm\em\" + avaliableEmNumbers[MID[m] - 1].Truncate(avaliableEmNumbers[MID[m] - 1].Length - 3) + ".fsm") && dlc == -1)
                            fsm = ChunkOTF.files[@"\quest\q" + questNumber + @"\fsm\em\" + avaliableEmNumbers[MID[m] - 1].Truncate(avaliableEmNumbers[MID[m] - 1].Length - 3) + ".fsm"].ChunkState.ExtractItem(ChunkOTF.files[@"\quest\q" + questNumber + @"\fsm\em\" + avaliableEmNumbers[MID[m] - 1].Truncate(avaliableEmNumbers[MID[m] - 1].Length - 3) + ".fsm"]);

                        //Pick a random size percent between range if both aren't 100
                        if (IoC.Settings.MonsterMinSize != 100 && IoC.Settings.MonsterMaxSize != 100)
                            MonsterSize[m] = pickSize.Next(IoC.Settings.MonsterMinSize, IoC.Settings.MonsterMaxSize + 1);

                        int RandomMonsterIndex = r.Next(monsterIDs.Length);

                        int oldMonsterID = MID[m] - 1;
                        //Pick another monster if the monster is a duplicate to another one in the quest
                        if (!IoC.Settings.DuplicateMonster)
                        {
                            int FoundMonsterIndex = Array.IndexOf(MID, monsterIDs[RandomMonsterIndex] + 1);
                            while (FoundMonsterIndex != -1 && FoundMonsterIndex < m)
                            {
                                RandomMonsterIndex = r.Next(monsterIDs.Length);
                                FoundMonsterIndex = Array.IndexOf(MID, monsterIDs[RandomMonsterIndex] + 1);
                            }
                        }

                        //Randomizes it to be the same as the first monster if its a kill duplicate quest and is a low enough monster slot value
                        if (m < int.Parse(MObjC1Text) && m != 0 && (QuestData.HuntDuplicate.Contains(questNumber) || QuestData.SlayDuplicate.Contains(questNumber) || QuestData.IBHuntDuplicate.Contains(questNumber)))
                        {
                            MID[m] = MID[0];
                            RandomMonsterIndex = Array.IndexOf(monsterIDs, MID[0] - 1);
                        }

                        if (questNumber == "00103")
                            RandomMonsterIndex = monsterFor00103;
                        
                        MID[m] = monsterIDs[RandomMonsterIndex] + 1;

                        if (questNumber == "00102")
                            monsterFor00103 = RandomMonsterIndex;

                        if (dlc == -1 && !storyQuests[questNumber].ChangeObjective)
                        { }
                        else
                        {
                            if (m == 0)
                                MObjID1 = monsterIDs[RandomMonsterIndex];
                            else if (m == 1)
                                MObjID2 = monsterIDs[RandomMonsterIndex];
                        }

                        if (fsm != null)
                        {
                            Directory.CreateDirectory(IoC.Settings.ChunkFolderPath + @"\randomized\quest\q" + questNumber + @"\fsm\em\");
                            File.WriteAllBytes(IoC.Settings.ChunkFolderPath + @"\randomized\quest\q" + questNumber + @"\fsm\em\" + avaliableEmNumbers[MID[m] - 1].Truncate(avaliableEmNumbers[MID[m] - 1].Length - 3) + ".fsm", fsm);
                        }

                        #region sobj Randomization

                        string oldMSobj = "";
                        byte[] sobj;

                        //Pick random sobj
                        if ((m == 1 && IoC.Settings.TwoMonsterQuests && (dlc != -1 || storyQuests[questNumber].CanRandomizeMap)) || (IoC.Settings.RandomSobj && (dlc != -1 || storyQuests[questNumber].CanRandomizeMap)))
                        {
                            Files[] SobjFiles;
                            if (monsterIDs[RandomMonsterIndex] == 26)
                                SobjFiles = SobjFilesBigMCache.Where(o => o.Name.Contains("st" + QuestData.MapIDs[MapIDIndex])).ToArray();
                            else
                                SobjFiles = SobjFilesCache.Where(o => o.Name.Contains("st" + QuestData.MapIDs[MapIDIndex])).ToArray();
                            int sobjIndex = pickSobj.Next(SobjFiles.Length);

                            sobj = SobjFiles[sobjIndex].ChunkState.ExtractItem(SobjFiles[sobjIndex]);
                            oldMSobj = SobjFiles[sobjIndex].Name;

                        }
                        //Else use old monsters one
                        else
                        {
                            oldMSobj = avaliableEmNumbers[oldMonsterID] + QuestData.MapIDs[MapIDIndex] + "_" + MSobj[m].ToString("00") + ".sobj";
                            if (monsterIDs[RandomMonsterIndex] == 26 && oldMSobj.Contains("em101_00_st101"))
                            {
                                Files[] SobjFiles;
                                SobjFiles = SobjFilesBigMCache;
                                int sobjIndex = pickSobj.Next(SobjFiles.Length);

                                sobj = ChunkOTF.files[SobjFiles[sobjIndex].Name].ChunkState.ExtractItem(ChunkOTF.files[SobjFiles[sobjIndex].Name]);
                                oldMSobj = SobjFiles[sobjIndex].Name;
                            }
                            else
                                sobj = ChunkOTF.files[oldMSobj].ChunkState.ExtractItem(ChunkOTF.files[oldMSobj]);
                        }
                        MSobj[m] = QuestData.MonsterMapSobjCount[RandomMonsterIndex, MapIDIndex];

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
                                sobj[j + 13] = (byte)monsterIDs[RandomMonsterIndex];
                                break;
                            }
                            else
                                CDCount = 0;
                        }
                        File.WriteAllBytes(IoC.Settings.ChunkFolderPath + @"\randomized\quest\enemy\boss\" + avaliableEmNumbers[monsterIDs[RandomMonsterIndex]] + QuestData.MapIDs[MapIDIndex] + "_" + MSobj[m].ToString("00") + ".sobj", sobj);

                        #endregion

                        QuestData.MonsterMapSobjCount[RandomMonsterIndex, MapIDIndex]++;

                        bool changeIcon = true;
                        if (dlc == -1)
                            changeIcon = storyQuests[questNumber].ChangeQuestIcon;

                        if (IoC.Settings.RandomIcons && m < 5 && MonIcons[m] != 127)
                            MonIcons[m] = pickIcon.Next(QuestData.IconList.Length - 1);
                        else if ((m < 5 && MonIcons[m] != 127 && changeIcon) || (IoC.Settings.TwoMonsterQuests && m == 1))
                            MonIcons[m] = Array.IndexOf(QuestData.IconList, QuestData.MonsterNames[monsterIDs[RandomMonsterIndex] + 1]);

                        using (StreamWriter file = File.AppendText(IoC.Settings.ChunkFolderPath + @"\randomized\QuestLog.txt"))
                        {
                            file.WriteLine("Monster " + (m + 1).ToString() + ": " + QuestData.MonsterNames[monsterIDs[RandomMonsterIndex] + 1] + "\tSobj File Name: " + avaliableEmNumbers[monsterIDs[RandomMonsterIndex]] + QuestData.MapIDs[MapIDIndex] + "_" + MSobj[m].ToString("00") + ".sobj" + "\tOld sobj File Name: " + oldMSobj
                               + (IoC.Settings.RandomIcons && m < 5 && MonIcons[m] != 127 ? "\tIcon: " + QuestData.IconList[MonIcons[m]] : ""));
                        }

                    }

                    if (IoC.Settings.TwoMonsterQuests)
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


                        MultiOIsChecked = true;
                        MObjT2Index = MObjT1Index;
                        MObjC2Text = "1";
                    }

                    bool changeText = true;
                    if (dlc == -1)
                        changeText = storyQuests[questNumber].ChangeQuestBookObjText;

                    #region GMD

                    if (dlc == -1)
                    {
                        for (int i = 0; i < storyQuests[questNumber].QuestObjTextIndexs.Length; i++)
                        {
                            int entryIndex = storyQuests[questNumber].QuestObjTextIndexs[i];
                            string value = "";
                            if (storyQuests[questNumber].MultiObjectiveHunt)
                                value = storyTargetText.Entries[entryIndex].Value.Replace(QuestData.MonsterNames.FirstOrDefault(o => storyTargetText.Entries[entryIndex].Value.Contains(o)), IoC.Settings.RandomIcons ? "???" : QuestData.MonsterNames[MID[i]]);
                            else
                                value = storyTargetText.Entries[entryIndex].Value.Replace(QuestData.MonsterNames.FirstOrDefault(o => storyTargetText.Entries[entryIndex].Value.Contains(o, StringComparison.OrdinalIgnoreCase)), IoC.Settings.RandomIcons ? "???" : QuestData.MonsterNames[MID[0]]);

                            storyTargetText.Entries[entryIndex].Value = value;
                        }
                    }

                    if (!(QuestData.HuntMultiMonster.Contains(questNumber) || QuestData.HuntMultiObjective.Contains(questNumber) || QuestData.IBHuntMultiMonster.Contains(questNumber) || QuestData.IBHuntMultiObjective.Contains(questNumber) ||
                        QuestData.SlayMultiMonster.Contains(questNumber) || QuestData.SlayMultiObjective.Contains(questNumber) || QuestData.IBSlayMultiMonster.Contains(questNumber)) && changeText )
                    {
                        GMDFile = new GMD(ChunkOTF.files["q" + questNumber + "_eng.gmd"].ChunkState.ExtractItem(ChunkOTF.files["q" + questNumber + "_eng.gmd"]));
                        if (IoC.Settings.TwoMonsterQuests)
                            GMDFile.Entries[1].Value = "Hunt all target monsters";
                        else if (questNumber == "66801" || questNumber == "66803")
                            GMDFile.Entries[1].Value = "Hunt 2 " + (IoC.Settings.RandomIcons ? "???" : QuestData.MonsterNames[MID[0]]);
                        else
                            GMDFile.Entries[1].Value = GMDFile.Entries[1].Value.Replace(QuestData.MonsterNames.FirstOrDefault(o => GMDFile.Entries[1].Value.Contains(o)), IoC.Settings.RandomIcons ? "???" : QuestData.MonsterNames[MID[0]]);

                        Directory.CreateDirectory(IoC.Settings.ChunkFolderPath + @"\randomized\common\text\quest");
                        GMDFile.Save(IoC.Settings.ChunkFolderPath + @"\randomized\common\text\quest\" + GMDFile.Filename + "_eng.gmd");
                    }
                    else if (questNumber == "67103" || questNumber == "66864")
                    {
                        GMDFile = new GMD(ChunkOTF.files["q" + questNumber + "_eng.gmd"].ChunkState.ExtractItem(ChunkOTF.files["q" + questNumber + "_eng.gmd"]));
                        GMDFile.Entries[1].Value = "Hunt all target monsters";
                        Directory.CreateDirectory(IoC.Settings.ChunkFolderPath + @"\randomized\common\text\quest");
                        GMDFile.Save(IoC.Settings.ChunkFolderPath + @"\randomized\common\text\quest\" + GMDFile.Filename + "_eng.gmd");
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
                    data = ChunkOTF.files["questData_" + questNumber + ".mib"].ChunkState.ExtractItem(ChunkOTF.files["questData_" + questNumber + ".mib"]);
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
                    buffer = BitConverter.GetBytes(Convert.ToInt32(RewardText));
                    data3[51] = buffer[0];
                    data3[52] = buffer[1];
                    data3[53] = buffer[2];
                    data3[54] = buffer[3];
                    buffer = BitConverter.GetBytes(Convert.ToInt32(PenaltyText));
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
                    if (MObj1MMIsChecked == true)
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
                    if (MObj2MMIsChecked == true)
                        data3[92] = 04;
                    else data3[92] = 0;
                    buffer = BitConverter.GetBytes(Convert.ToUInt16(MObjID2));
                    data3[95] = buffer[0];
                    data3[96] = buffer[1];
                    buffer = BitConverter.GetBytes(Convert.ToUInt16(MObjC2Text));
                    data3[97] = buffer[0];
                    data3[98] = buffer[1];
                    if (MultiOIsChecked == true)
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

                    File.WriteAllBytes(IoC.Settings.ChunkFolderPath + @"\randomized\quest\questData_" + questNumber + ".mib", data4);

                    #endregion

                }

                //Refresh lists with iceborne
                if (!RandomizingIB && IoC.Settings.RandomizeIBQuests && dlc != -1)
                {
                    quests = QuestData.IBBigMonsterHuntQuests;
                    if (!IoC.Settings.DontRandomizeSlay)
                        quests = quests.Concat(QuestData.IBBigMonsterSlayQuests).ToArray();
                    if (!IoC.Settings.DontRandomizeCapture)
                        quests = quests.Concat(QuestData.IBBigMonsterCaptureQuests).ToArray();
                    if (IoC.Settings.RandomizeMultiObj)
                        quests = quests.Concat(QuestData.IBHuntMultiObjective).ToArray();
                    if (IoC.Settings.RandomizeMultiMon)
                    {
                        quests = quests.Concat(QuestData.IBHuntMultiMonster).ToArray();
                        if (!IoC.Settings.DontRandomizeSlay)
                            quests = quests.Concat(QuestData.IBSlayMultiMonster).ToArray();
                    }
                    if (IoC.Settings.RandomizeDuplicate)
                        quests = quests.Concat(QuestData.IBHuntDuplicate).ToArray();

                    monsterIDs = QuestData.BigMonsterIDsIB;
                    if (IoC.Settings.IceborneOnlyMonsters)
                        monsterIDs = monsterIDs.Where(mon => !QuestData.BigMonsterIDs.Contains(mon)).ToArray();
                    if (IoC.Settings.IncludeHighRankOnly)
                        monsterIDs = monsterIDs.Concat(QuestData.HighRankOnlyMonsters).ToArray();
                    if (IoC.Settings.IncludeFatalis)
                        monsterIDs = monsterIDs.Append(101).ToArray();


                    using (StreamWriter file = File.AppendText(IoC.Settings.ChunkFolderPath + @"\randomized\QuestLog.txt"))
                    {
                        file.WriteLine("\n\n---------------------------------------------------------------------------");
                        file.WriteLine("                              Iceborne Quests                              ");
                        file.WriteLine("---------------------------------------------------------------------------");
                    }
                }
                if (dlc == 0)
                    RandomizingIB = true;

            }

            storyTargetText.Save(IoC.Settings.ChunkFolderPath + @"\randomized\common\text\storyTarget_eng.gmd");

            #endregion
        }
    }
}
