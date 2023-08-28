using System;
using System.Collections.Generic;

namespace MHW_Randomizer
{
    public class QuestData
    {

        public static Int32[] MapIDs = { 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 200, 201, 202, 203, 301, 302, 303, 305, 306, 307, 311, 400, 401, 403, 405, 406, 407, 408, 409, 410, 411, 412, 413, 414, 415, 416, 417, 501, 502, 503, 504, 505, 506 };
        public static Int32[] ForbiddenMapIDs = { 108, 109, 203, 305, 306, 307, 311, 410, 411, 412, 413, 414, 415, 416, 417, 506 };
        public static byte[] ObjectiveIDs = { 0x00, 0x01, 0x02, 0x11, 0x21, 0x31 };
        public static byte[] QuestTypeIDs = { 0x01, 0x02, 0x04, 0x08, 0x10, 0x20 };

        // 415,
        public static int[] ArenaMaps = { 201, 202, 405, 409, 412, 413, 416, 417 };

        public static int[] ValidMapIndexes = { 1, 2, 3, 4, 5, 8, 9 };

        // 34,
        public static int[] ValidArenaMapIndexes = { 1, 2, 3, 4, 5, 11, 12, 8, 9, 31, 32, 35, 36 };

        public static string[] MapNames = {"Infinity of Nothing", "Ancient Forest", "Wildspire Waste", "Coral Highlands", "Rotten Vale", "Elder Recess" , "Great Ravine" , "Great Ravine (Story Map to Coral Highlands)" , "Hoarfrost Reach", "Guiding Lands", "Infinity of Nothing" ,
            "Special Arena" , "Arena (Challenge)" ,"IB Arena (Seliana Supply Cache)",  "Astera" , "Gathering Hub (Astera)" , "Research Base" , "Seliana", "Gathering Hub (Seliana)", "307 Unconfirmed/To Be Tested","311 Unconfirmed/To Be Tested","Crashes game" , "Ancient Forest (Flooded / Intro)" ,
            "Everstream" , "Confluence of Fates" , "Ancient Forest (Tutorial)" , "Infinity of Nothing" , "Debug Map" , "Caverns of El Dorado" , "Origin Isle (Cutscene)?","Seliana Supply Cache","Origin Isle (Ruiner Nerg Fight)","Origin Isle (Shara Fight)","Ancient Forest (Legiana Track Quest)",
            "Secluded Valley","Alatreon's Stage","Castle Schrade","Living Quarters" , "Private Quarters" , "Private Suite" , "Training Camp" , "Chamber of Five","Seliana House" };

        //Sobj's that spawn the monster out of bounds or don't work properly: em102_00_st101_61.sobj. em118_00_st102_02.sobj (story one?), em103_00_st105_10.sobj (story one too maybe), em042_05_st109_60.sobj (in area that need to do the quest "Across the Lost Path", get the quest after beating the main story,
        //gonna exclude it because could cause a softlock and could be confusing where the monster is), em057_01_st109_00.sobj (in area that need to do the quest "Across the Lost Path"), em113_01_st109_50.sobj (Don't know, seems to be in another area that needs to be unlocked),
        //em057_01_st109_10.sobj (in area that need to do the quest "Across the Lost Path"), em042_00_st109_60.sobj (in area that need to do the quest "Across the Lost Path"), em043_05_st109_50.sobj (Don't know, seems to be in another area that needs to be unlocked)
        public static string[] BadSobjs = { "em102_00_st101_61.sobj", "em118_00_st102_02.sobj", "em103_00_st105_10.sobj", "em011_00_st105_02.sobj", "em103_00_st104_00.sobj", "em127_00_st101_01.sobj" };
        //Possible one em043_05_st109_60.sobj

        public static string[] IconList = { "Anjanath", "Great Jagras", "Pukei-Pukei", "Nergigante", "Xeno'jiiva", "Xeno'jiiva", "Zorah Magdaros", "Kulu-Ya-Ku", "Tzitzi-Ya-Ku", "Jyuratodus", "Tobi-Kadachi", "Paolumu", "Legiana", "Great Girros", "Odogaron", "Radobaan", 
            "Vaal Hazak", "Dodogama", "Kulve Tarroth", "Bazelgeuse", "Behemoth", "Leshen", "Ancient Leshen", "17 EMPTY", "Rathian", "Pink Rathian", "Rathalos", "Azure Rathalos", "Diablos", "Black Diablos", "Kirin", "1F EMPTY", "Kushala Daora", "Lunastra", "Teostra", 
            "Lavasioth", "Deviljho", "Barroth", "Uragaan", "27 EMPTY", "Beotodus", "Coral Pukei-Pukei", "Viper Tobi-Kadachi", "Fulgur Anjanath", "Nightshade Paolumu", "Ebony Odogaron", "Barioth", "Nargacuga", "Skull (Monster near death)", "Hungry/Exhausted (Monster)", 
            "Boaboa", "Gajalaka", "Palico", "Grymalkine", "(Special) Assignment Map icon", "'unknown monster' icon", "UNIDENTIFIED", "UNIDENTIFIED small scoutly icon?", "bigger scoutfly icon", "number 3 as seen when multiple monsters of the same kind appear", "number 4", 
            "number 5", "number 1", "number 2", "Palico/Grymalkine trap marker", "41 EMPTY", "Egg Delivery", "Delivery", "Scarred Yian Garuga", "Banbaro", "Glavenus", "Acidic Glavenus", "Brachydios", "Tigrex", "Velkhana", "Namielle", "Shara Ishvalda", "Shrieking Legiana", 
            "Seething Bazelgeuse", "Savage Deviljho", "Ruiner Nergigante", "Blackveil Vaal Hazak", "Brute Tigrex", "Zinogre", "Stygian Zinogre", "Yian Garuga", "Gold Rathian", "Silver Rathalos", "Aptonoth", "Apceros", "Kelbi 1", "Kelbi 2", "Mosswine", "Hornetaur", 
            "Vespoid", "Gajau", "Jagras", "Mernos", "Kestodon 1", "Kestodon 2", "Raphinos", "Shamos", "Barnos", "Girros", "Popo", "Gastodon", "Noios", "Anteka", "Magmacore ?", "broken pillar", "Barrel 1", "Barrel 2", "Wulg", "Cortos", "Training cart", "73 EMPTY", "74 EMPTY", 
            "75 EMPTY", "76 EMPTY", "77 EMPTY", "Rajang", "Furious Rajang", "Raging Brachydios", "Safi'jiiva", "Alatreon", "Frostfang Barioth", "Fatalis", "No Icon" };

        public string[] QCMusicList = { "Defualt", "Elder Dragon Clear", "Ridge Zorah Clear", "Kulve Taroth", "Final Fantasy", "Leshen", "Ancient Leshen", "AT nergigante", "Resident Evil", "09 unused?", "MR Kulve Taroth", "Alatreon (MHTri clear music)", "Fatalis" };

        public static Dictionary<int, string> QuestName = new Dictionary<int, string>()
        {
            {101, "Jagras of the Ancient Forest"},
            {102, "A Kestodon Kerfuffle"},
            {103, "The Great Jagras Hunt"},
            {151, "Butting Heads with Nature"},
            {152, "A Thicket of Thugs"},
            {153, "Fungal Flexin' in the Ancient Forest"},
            {201, "Bird-Brained Bandit"},
            {205, "Urgent: Pukei-Pukei Hunt"},
            {241, "Learning the Clutch"},
            {251, "The Great Glutton"},
            {252, "Camp Crasher"},
            {261, "Snatch the Snatcher"},
            {262, "The Pain from Gains"},
            {263, "Exterminator of the Waste"},
            {301, "The Best Kind of Quest"},
            {302, "Sinister Shadows in the Swamp"},
            {305, "Flying Sparks: Tobi-Kadachi"},
            {306, "The Encroaching Anjanath"},
            {331, "Special Arena: Pukei-Pukei"},
            {332, "Special Arena: Barroth"},
            {333, "Special Arena: Tobi-Kadachi"},
            {351, "Scatternut Shortage"},
            {352, "The Current Situation"},
            {361, "Mired in the Spire"},
            {362, "The Piscine Problem"},
            {363, "Prickly Predicament"},
            {364, "Gettin' Yolked in the Waste"},
            {365, "Landing the Landslide Wyvern"},
            {401, "One for the History Books"},
            {405, "Ballooning Problems"},
            {407, "Radobaan Roadblock"},
            {408, "Legiana: Embodiment of Elegance"},
            {431, "Special Arena: Anjanath"},
            {432, "Special Arena: Rathian"},
            {433, "Special Arena: Paolumu"},
            {434, "Special Arena: Radobaan"},
            {451, "One Helluva Sinus Infection"},
            {452, "Gettin' Yolked in the Forest"},
            {461, "Royal Relocation"},
            {471, "It's a Crying Shamos"},
            {472, "A Tzitzi for Science"},
            {473, "Sorry You're Not Invited"},
            {474, "What a Bunch of Abalone"},
            {475, "White Monster for a White Coat"},
            {481, "Persistent Pests"},
            {482, "A Rotten Thing To Do"},
            {483, "A Bone to Pick"},
            {484, "On Nightmare's Wings"},
            {501, "Into the Bowels of the Vale"},
            {502, "A Fiery Throne Atop the Forest"},
            {503, "Horned Tyrant Below the Sands"},
            {504, "A Colossal Task"},
            {531, "Special Arena: Legiana"},
            {532, "Special Arena: Odogaron"},
            {533, "Special Arena: Rathalos"},
            {534, "Special Arena: Diablos"},
            {551, "When Desire Becomes an Obsession"},
            {552, "Redefining the \"Power Couple\""},
            {561, "Twin Spires Upon the Sands"},
            {571, "A Humid Headache"},
            {572, "Gone in a Flash"},
            {581, "Scratching the Itch"},
            {582, "Man's Best Fiend"},
            {583, "The Meat of the Matter"},
            {601, "Invader in the Waste"},
            {605, "Tickled Pink"},
            {607, "Old World Monster in the New World"},
            {631, "Special Arena: HR Pukei-Pukei"},
            {632, "Special Arena: HR Barroth"},
            {633, "Special Arena: HR Tobi-Kadachi"},
            {634, "Special Arena: HR Anjanath"},
            {635, "Special Arena: HR Rathian"},
            {636, "Special Arena: HR Paolumu"},
            {637, "Special Arena: HR Radobaan"},
            {641, "Left Quite the Impression"},
            {651, "Hard to Swallow"},
            {652, "Googly-eyed Green Monster"},
            {653, "A Hair-Raising Experience"},
            {654, "It Can't See You if You Don't Move"},
            {655, "The Sleeping Sylvan Queen"},
            {656, "Stuck in Their Ways"},
            {661, "Keep Your Hands to Yourself!"},
            {662, "A Crown of Mud and Anger"},
            {663, "Pukei-Pukei Ambush"},
            {665, "Up to Your Waist in the Waste"},
            {666, "Brown Desert, Green Queen"},
            {667, "Trespassing Troublemaker"},
            {671, "Say Cheese!"},
            {672, "Loop the Paolumu"},
            {681, "A Tingling Taste"},
            {682, "Stuck in a Rut"},
            {683, "Chef Quest! Pumped to Deliver"},
            {684, "Chef Quest! A Rotten Request"},
            {691, "A Meow for Help"},
            {692, "A Scalding Scoop"},
            {693, "Dodogama Drama"},
            {694, "Chef Quest! Gajalaka Lockdown"},
            {701, "A Wound and a Thirst"},
            {731, "Special Arena: HR Pink Rathian"},
            {732, "Special Arena: HR Legiana"},
            {733, "Special Arena: HR Odogaron"},
            {734, "Special Arena: HR Uragaan"},
            {735, "Special Arena: HR Rathalos"},
            {736, "Special Arena: HR Azure Rathalos"},
            {737, "Special Arena: HR Diablos"},
            {738, "Special Arena: HR Black Diablos"},
            {751, "Rathalos Rematch"},
            {752, "Rathalos in Blue"},
            {753, "The Red and Blue Crew"},
            {761, "Pretty In Pink"},
            {762, "Well, That Diablos!"},
            {763, "Two-horned Hostility"},
            {764, "RRRRRumble in the Waste!"},
            {771, "A Cherry Wind upon the Reefs"},
            {772, "Legiana: Highlands Royalty"},
            {773, "A Sore Site"},
            {774, "Talons of Ire and Ice"},
            {781, "Odogaron Unleashed"},
            {791, "Lavasioth, Monster of Magma"},
            {792, "Ore-eating Occupier"},
            {793, "Ruler of the Azure Skies"},
            {794, "Bazelgeuse in the Field of Fire"},
            {795, "A Fiery Convergence"},
            {801, "Kushala Daora, Dragon of Steel"},
            {802, "Teostra the Infernal"},
            {803, "Hellish Fiend Vaal Hazak"},
            {804, "Land of Convergence"},
            {805, "Beyond the Blasting Scales"},
            {806, "Thunderous Rumble in the Highlands"},
            {851, "A Portent of Disaster"},
            {861, "A Blaze on the Sand"},
            {871, "Lightning Strikes Twice"},
            {881, "Stirrings from the Grave"},
            {891, "The Eater of Elders"},
            {892, "Master of the Gale"},
            {893, "Hellfire's Stronghold"},
            {894, "The Eater of Elders"},
            {895, "The Winds of Wrath Bite Deep"},
            {896, "The Fires of Hell Bite Deep"},
            {961, "Beyond the Blasting Scales"},
            {971, "Thunderous Rumble in the Highlands"},
            {991, "A Light Upon the River's Gloom"},
            {992, "The White Winds of the New World"},
            {995, "The Sapphire Star's Guidance"},
            {996, "Showdown: the Muck and the Maul"},
            {997, "New World Sky, New World Flower"},
            {998, "A Summons from Below"},
            {1101, "Baptism by Ice"},
            {1102, "Banbaro Blockade"},
            {1121, "Deep Snow Diver"},
            {1122, "Taking Charge"},
            {1123, "Ice Catch!"},
            {1124, "Call of the Wild"},
            {1125, "Greetings from the Tundra"},
            {1131, "Special Arena: MR Pukei-Pukei"},
            {1132, "Special Arena: MR Barroth"},
            {1133, "Special Arena: MR Tobi-Kadachi"},
            {1134, "Special Arena: MR Banbaro"},
            {1151, "The Great Jagras Returns!"},
            {1152, "New World Problems"},
            {1153, "Beating Around the Bush"},
            {1154, "Literary Thief"},
            {1155, "Trapping The Tree Trasher"},
            {1161, "Wildspire Treasure Hunt"},
            {1162, "Dragged Through the Mud"},
            {1163, "Jyura In My Way"},
            {1164, "Taster's Tour"},
            {1171, "All the Wrong Signals"},
            {1181, "Grinding my Girros"},
            {1191, "Can't Bring Yourself To It"},
            {1192, "This Here's Big Horn Country!"},
            {1201, "Ready to Strike"},
            {1202, "No Time for Naps"},
            {1203, "Play Both Ends"},
            {1221, "Analysis Creates Paralysis"},
            {1222, "Poison and Paralysis Pinch"},
            {1223, "Boaboa Constrictor"},
            {1224, "By Our Powers Combined"},
            {1225, "You Scratch Our Backs..."},
            {1231, "Special Arena: MR Anjanath"},
            {1232, "Special Arena: MR Radobaan"},
            {1233, "Special Arena: MR Coral Pukei-Pukei"},
            {1234, "Special Arena: MR Viper Tobi-Kadachi"},
            {1235, "Special Arena: MR Rathian"},
            {1236, "Special Arena: MR Pink Rathian"},
            {1237, "Special Arena: MR Paolumu"},
            {1238, "Special Arena: MR Nightshade Paolumu"},
            {1251, "Anjanath Antics"},
            {1252, "Fool's Mate"},
            {1253, "Nighty Night Nightshade"},
            {1261, "A Queen At Heart"},
            {1262, "A Face Nightmares Are Made Of"},
            {1263, "Stick Your Nose Somewhere Else"},
            {1264, "Feisty Girl Talk"},
            {1271, "The Plight of Paolumu"},
            {1272, "Pink Power Grab"},
            {1273, "Protip: Stay Hydrated"},
            {1274, "Put That Red Cup Away"},
            {1281, "No Laughing Matter"},
            {1282, "Bugger Off Bugs!"},
            {1291, "Looking For That Glimmer"},
            {1301, "Blizzard Blitz"},
            {1302, "Ever-present Shadow"},
            {1303, "The Scorching Blade"},
            {1304, "Absolute Power"},
            {1305, "A Smashing Cross Counter"},
            {1306, "A Tale of Ice and Fire"},
            {1321, "Remember That One Time?"},
            {1322, "The Purr-fect Room: Stone"},
            {1323, "Proud White Knight"},
            {1331, "Special Arena: MR Legiana"},
            {1332, "Special Arena: MR Odogaron"},
            {1333, "Special Arena: MR Uragaan"},
            {1334, "Special Arena: MR Rathalos"},
            {1335, "Special Arena: MR Diablos"},
            {1336, "Special Arena: MR Barioth"},
            {1337, "Special Arena: MR Nargacuga"},
            {1338, "Special Arena: MR Glavenus"},
            {1339, "Special Arena: MR Brachydios"},
            {1340, "Special Arena: MR Tigrex"},
            {1351, "Swoop to a New Low"},
            {1352, "Nargacoulda, Shoulda, Woulda"},
            {1353, "The Secret to a Good Slice"},
            {1354, "Red and Black Aces"},
            {1361, "A Line in the Sand"},
            {1362, "A Flash of the Blade"},
            {1363, "Simmer and Slice!"},
            {1371, "Legiana Left Behind"},
            {1372, "The Black Wind"},
            {1373, "A Nasty Flesh Wound"},
            {1381, "Don't be a Jerk with the Jerky"},
            {1382, "A Roar that Shook the Vale"},
            {1383, "Runnin', Rollin', and Weepin'"},
            {1391, "Blast Warning In Effect!"},
            {1392, "Everyone's a Critic"},
            {1393, "Begone Uragaan"},
            {1394, "Secret of the Ooze"},
            {1395, "Festival of Explosions!"},
            {1401, "When the Mist Taketh You"},
            {1402, "The Disintegrating Blade"},
            {1403, "Bad Friends, Great Enemies"},
            {1404, "The Defense of Seliana"},
            {1405, "The Thunderous Troublemaker!"},
            {1421, "Noblefrost Hunter"},
            {1422, "Tundra Troublemaker"},
            {1423, "Duet of Rime"},
            {1424, "Treasure in the Steam"},
            {1431, "Special Arena: MR Azure Rathalos"},
            {1432, "Special Arena: MR Black Diablos"},
            {1433, "Special Arena: MR Acidic Glavenus"},
            {1434, "Special Arena: MR Ebony Odogaron"},
            {1435, "Special Arena: MR Fulgur Anjanath"},
            {1451, "These Azure Eyes See All"},
            {1452, "Misfortune in the Forest"},
            {1461, "In the Heat of the Moment"},
            {1462, "Piercing Black"},
            {1471, "A Shadowy Offender"},
            {1481, "This Corroded Blade"},
            {1482, "The Purr-fect Room: Light Iron"},
            {1483, "The Purr-fect Room: Dark Iron"},
            {1491, "Blue Rathalos Blues"},
            {1492, "Trap the Thunder Jaw"},
            {1501, "The Iceborne Wyvern"},
            {1502, "The Second Coming"},
            {1503, "Under the Veil of Death"},
            {1504, "A Light From The Abyss"},
            {1521, "Clashing Swords Upon The Rime"},
            {1551, "The Harbinger of Clear Skies"},
            {1552, "Here Comes the Deathmaker"},
            {1561, "Royal Audience on the Sand"},
            {1562, "The Tyrant's Banquet"},
            {1571, "Lightning Crashes"},
            {1572, "Memories of the Sea God"},
            {1581, "It's the Afterlife for Me"},
            {1591, "Wings of the Wind"},
            {1592, "Mark of the Sun"},
            {1593, "Seething with Anger"},
            {1594, "The Purr-fect Room: Silver"},
            {1601, "To The Guided, A Paean"},
            {1602, "Paean of Guidance"},
            {1603, "Sleep Now in the Fire"},
            {1604, "Big Burly Bash"},
            {1605, "To the Very Ends with You"},
            {1606, "Return of the Crazy One"},
            {1631, "We Run This Town"},
            {1632, "Special Arena: MR Zinogre"},
            {1633, "Special Arena: MR Yian Garuga"},
            {1634, "Special Arena: MR Brute Tigrex"},
            {1635, "Special Arena: MR Gold Rathian"},
            {1636, "Special Arena: MR Silver Rathalos"},
            {1641, "Faraway Lorelei"},
            {1642, "Hymn of Moon and Sun"},
            {1651, "The Storm Brings the Unexpected"},
            {1661, "One Hot Night in the Spire"},
            {1671, "Divine Surge"},
            {1691, "Into the Palace of Flame"},
            {1692, "Master Hunter of the New World"},
            {3001, "Arena Quest 02"},
            {3002, "Arena Quest 03"},
            {3031, "Arena Quest 04"},
            {3032, "Arena Quest 05"},
            {3033, "Arena Quest 06"},
            {3034, "Arena Quest 07"},
            {3051, "Arena Quest 08"},
            {3052, "Arena Quest 09"},
            {3071, "Arena Master Quest 02"},
            {3072, "Arena Master Quest 03"},
            {3073, "Arena Master Quest 04"},
            {3074, "Arena Master Quest 05"},
            {3091, "Arena Master Quest 06"},
            {3092, "Arena Master Quest 07"},
            {3101, "Arena Quest 01"},
            {3171, "Arena Master Quest 01"},
            {5003, "Troubled Troupers"},
            {50601, "A Visitor from Another World"},
            {50701, "The Food Chain Dominator"},
            {50751, "Today's Special: Hunter Flambé"},
            {50801, "The Blazing Sun"},
            {50802, "Pandora's Arena"},
            {50803, "No Remorse, No Surrender"},
            {50861, "Infernal Monarchy"},
            {50891, "Blue Prominence"},
            {50892, "Blue Prominence"},
            {50901, "Banquet in the Earthen Hall"},
            {50902, "The Fury of El Dorado"},
            {50905, "The Legendary Beast"},
            {50906, "He Taketh It with His Eyes"},
            {50910, "Contract: Trouble in the Ancient Forest"},
            {50991, "A Visitor from Eorzea"},
            {51601, "Reveal Thyself, Destroyer"},
            {51602, "Sterling Pride"},
            {51603, "Across the Lost Path"},
            {51604, "Point of No Return"},
            {51605, "The Red Dragon"},
            {51606, "...And My Rage for All"},
            {51607, "The Fury Remains"},
            {51611, "Blazing Black Twilight"},
            {51612, "The Black Dragon"},
            {51613, "Dawn's Triumph"},
            {51621, "Special Arena: MR Stygian Zinogre"},
            {51622, "Achy Brachy Heart"},
            {51623, "All That Glitters is Furious"},
            {61101, "Up at the Crack of Dawn"},
            {61103, "Where Sun Meets Moon"},
            {61104, "Timberland Troublemakers"},
            {61105, "Every Hunter's Dream"},
            {61601, "Midnight Mayhem"},
            {61603, "A Royal Pain"},
            {61604, "Kings Know No Fear"},
            {61605, "Mosswinin' and Dinin'"},
            {61606, "The Greatest Jagras"},
            {61607, "The Name's Lavasioth!"},
            {61801, "Pearl Snatchers"},
            {61802, "Every Hunter's Dream III"},
            {61803, "Every Hunter's Dream II"},
            {61805, "Duffel Duty"},
            {61806, "Scores of Ores"},
            {61807, "A Chilling Entrance"},
            {61808, "Muscle Monkey Madness"},
            {61809, "Paolumu Lullabies"},
            {61811, "Flora Frostbite"},
            {61812, "Beef is Never a Mi-steak"},
            {61813, "50 Shades of White"},
            {61814, "A Shocking Climax"},
            {62502, "The Deathly Quiet Curtain"},
            {62503, "A Whisper of White Mane"},
            {62504, "The Scorn of the Sun"},
            {62505, "The Eye of the Storm"},
            {62506, "The Heralds of Destruction Cry"},
            {62511, "When Blue Dust Surpasses Red Lust"},
            {62515, "Relish the Moment"},
            {62606, "Undying Alpenglow"},
            {62607, "Like a Moth to the Flame"},
            {62608, "Gaze Upon the Dawn"},
            {62609, "Keeper of the Otherworld"},
            {63001, "Challenge Quest 1: Beginner"},
            {63002, "Challenge Quest 2: Beginner"},
            {63003, "Vespoid Infestation!"},
            {63031, "Challenge Quest 1: Intermediate"},
            {63032, "Challenge Quest 2: Intermediate"},
            {63033, "Gajalaka Outbreak!"},
            {63038, "Empress in Full Bloom II"},
            {63039, "Empress in Full Bloom III"},
            {63051, "Challenge Quest 1: Expert"},
            {63052, "Challenge Quest 2: Expert"},
            {63071, "Challenge Quest 1: MR Intermediate"},
            {63072, "Challenge Quest 2: MR Intermediate"},
            {63073, "Challenge Quest 1: MR Expert"},
            {63074, "Challenge Quest 2: MR Expert"},
            {63101, "Nergigante Slay Event 1"},
            {63102, "Nergigante Slay Event 2"},
            {63103, "Nergigante Slay Event 3"},
            {63104, "Gale & Fangs Slay Event 1"},
            {63105, "Gale & Fangs Slay Event 2"},
            {63106, "Gale & Fangs Slay Event 3"},
            {63107, "Nergigante Slay Event 4"},
            {63108, "Deviljho Slay Event"},
            {63109, "The Best of the Best"},
            {63110, "What Lurks In The Forest"},
            {63130, "When Law Meets War"},
            {63131, "The Hunter and The Blue Empress"},
            {63143, "Wearer of the Iceborne Crown" },
            {63149, "Power and Technique" },
            {63150, "Farewell to the Frozen"},
            {64101, "Wicked Wildspire Warfare"},
            {64601, "Rollin' With The Uragaan"},
            {64801, "Balloon Fight"},
            {64802, "Talk About a Party Foul..."},
            {65601, "Scrapping with the Shamos"},
            {65602, "A Flash in the Pan"},
            {65603, "Egg Lovers United"},
            {65604, "Wiggle Me This"},
            {65605, "Triple Threat Throwdown"},
            {65606, "A Simple Task"},
            {65607, "Tracking the Delivery"},
            {66101, "Chew The Fat"},
            {66102, "Ya-Ku With That?"},
            {66103, "Flesh Cleaved to Bone"},
            {66104, "Kirin The Myth"},
            {66105, "The Poison Posse"},
            {66106, "Greeting the Gluttons"},
            {66601, "Deep Green Blues"},
            {66602, "Wildspire Bolero"},
            {66603, "Coral Waltz"},
            {66604, "Effluvial Opera"},
            {66605, "Rock N' Roll Recess"},
            {66606, "This is How Revolts Start"},
            {66607, "Snow & Cherry Blossoms"},
            {66608, "A Nose for an Eye"},
            {66609, "No Tomorrow for Usurpers"},
            {66610, "The Thronetaker"},
            {66801, "Trophy Fishin'"},
            {66802, "The Lord of the Underworld Beckons"},
            {66803, "A Bunch of Sticks in the Mud"},
            {66804, "Desert Desserts"},
            {66805, "A New Troublemaker in Town"},
            {66806, "Colorful Carnival"},
            {66807, "Hunter-Blunderer"},
            {66808, "A Sky & Sea of Fire"},
            {66809, "A Curious Experiment"},
            {66810, "Soaked and Shivering"},
            {66811, "Fired-Up Bruisers"},
            {66812, "The Desert Dash"},
            {66813, "In the Depths of the Forest"},
            {66814, "Servants of the Vale"},
            {66815, "We Three Kings"},
            {66816, "The Winter Blues"},
            {66817, "A Reason Behind The Hunger"},
            {66818, "Moonlit Howl"},
            {66819, "I Am Tranquil, I Am Sound"},
            {66820, "A Roar that Splinters the Sky"},
            {66821, "A Glance of Silver"},
            {66822, "The Moon is a Harsh Queen"},
            {66824, "Razzled, Frazzled, and Dazzled"},
            {66825, "Scars Tell the Whole Story"},
            {66826, "Yodeling in the Forest"},
            {66827, "Heavy Metal in the Waste"},
            {66828, "Symphony of the Coral"},
            {66829, "Rotten Canzone"},
            {66830, "Alt Rock Recess"},
            {66831, "Ballad of the Hoarfrost"},
            {66832, "Wolf Out of Hell"},
            {66834, "When the Swift Meets the Roar"},
            {66835, "The Wrath of Thunder Descends"},
            {66836, "Ode to the Destruction"},
            {67103, "A Rush of Blood"},
            {67106, "USJ: Gold Star Treatment"},
            {67604, "USJ Blazing Azure Stars!"},
            {67605, "Code: Red"},
            {67606, "A Visitor from Eorzea (Extreme)"},
            {67608, "SDF: Silent, Deadly, and Fierce"},
            {67610, "Contract: Woodland Spirit"},
            {67801, "RE: Return of the Bioweapon"},
            {61815, "Skyward Snipers"},
            {61816, "A Fish to Whet Your Appetite"},
            {67807, "The Assassin"},
            {66833, "The Eternal Gold Rush"},
            {66847, "The Distant Dark Tide"},
            {66859, "Talk About a Party Foul..."},
            {66860, "The Wrath of Thunder Descends"},
            {63148, "The Conqueror of Hoarfrost"},
            {66840, "In the Tempest's Wake"},
            {66841, "Day of Ruin"},
            {66842, "The Cold Never Bothered Me"},
            {66843, "The Evening Star"},
            {66844, "Dawn of the Death Star"},
            {66846, "The Last White Knight"},
            {66853, "Fetching Light Pearls"},
            {66854, "Camoflawed"},
            {66855, "Seeing is Believing"},
            {66856, "Don't Forget The Earplugs!"},
            {66857, "Monkey Business"},
            {66858, "The Naked Truth"},
            {66861, "Mew are Number One!"},
            {66867, "Old Dog, New Trick"},
            {66845, "Fade to Black"},
            {66850, "The Place Where Winter Sleeps"},
            {66862, "Kadachi Twins"},
            {66863, "Tears from Nirvana"},
            {66864, "Mighty Muscle Monkey Madness"},
            {67806, "USJ: Shine On Forever"},
            {67809, "USJ: Ballet of Frost"},
            {66865, "A Farewell to Zinogre"},
            {66866, "Brand New Brute"}
        };

        #region Monster Data

        public static int[,] MonsterMapSobjCount = new int[102, 43];

        public static string[] MonsterNames = { "None", "Anjanath" , "Rathalos" , "[s] Aptonoth" , "[s] Jagras" , "Zorah Magdaros" , "[s] Mosswine" , "[s] Gajau" , "Great Jagras" , "[s] Kestodon M" , "Rathian" , "Pink Rathian" , "Azure Rathalos" , "Diablos" ,
            "Black Diablos" , "Kirin" , "Behemoth" , "Kushala Daora" , "Lunastra" , "Teostra" , "Lavasioth" , "Deviljho" , "Barroth" , "Uragaan" , "Leshen" , "Pukei-Pukei" , "Nergigante" , "Xeno'jiiva" , "Kulu-Ya-Ku" , "Tzitzi-Ya-Ku" ,
            "Jyuratodus" , "Tobi-Kadachi" , "Paolumu" , "Legiana" , "Great Girros" , "Odogaron" , "Radobaan" , "Vaal Hazak" , "Dodogama" , "Kulve Taroth" , "Bazelgeuse" , "[s] Apceros" , "[s] Kelbi M" , "[s] Kelbi F" , "[s] Hornetaur" ,
            "[s] Vespoid" , "[s] Mernos" , "[s] Kestodon F" , "[s] Raphinos" , "[s] Shamos" , "[s] Barnos" , "[s] Girros" , "Ancient Leshen" , "[s] Gastodon" , "[s] Noios" , "[s] Magmacore 1" , "[s] Magmacore 2" , "[s] Gajalaka" ,
            "[s] Small Barrel" , "[s] Large Barrel" , "[s] Training Pole" , "NON-VALID" , "Tigrex" , "Nargacuga" , "Barioth" , "Savage Deviljho" , "Brachydios" , "Glavenus" , "Acidic Glavenus" , "Fulgur Anjanath" , "Coral Pukei-Pukei" , "Ruiner Nergigante" ,
            "Viper Tobi-Kadachi" , "Nightshade Paolumu" , "Shrieking Legiana" , "Ebony Odogaron" , "Blackveil Vaal Hazak" , "Seething Bazelgeuse" , "Beotodus" , "Banbaro" , "Velkhana" , "Namielle" , "Shara Ishvalda" , "[s] Popo" , "[s] Anteka" , "[s] Wulg" ,
            "[s] Cortos" , "[s] Boaboa" , "Alatreon" , "Gold Rathian" , "Silver Rathalos" , "Yian Garuga" , "Rajang" , "Furious Rajang" , "Brute Tigrex" , "Zinogre" , "Stygian Zinogre" , "Raging Brachydios" , "Safi'jiiva" , "[s] Wood Dummy" ,
            "Scarred Yian Garuga","Frostfang Barioth", "Fatalis"};

        public static bool[] IsGroundMonster = 
        { 
            false, //Rathian
            false, //Pink Rathian
            false, //Gold Rathian
            false, //Rathalos
            false, //Azure Rathalos
            false, //Silver Rathalos
            true, //Diablos
            true, //Black Diablos
            true, //Kirin
            false, //Fatalis
            false, //Yian Garuga and Scarred Yian Garuga
            true, //Rajang and Furious Rajang
            false, //Kushala Daora
            false, //Lunastra
            false, //Teostra
            true, //Tigrex
            true, //Brute Tigrex
            true, //Lavasioth
            true, //Nargacuga
            true, //Barioth
            true, //Frostfang Barioth
            true, //Deviljho
            true, //Savage Deviljho
            true, //Barroth
            true, //Uragaan
            false, //Alatreon
            true, //Zinogre
            true, //Stygian Zinogre
            true, //Brachydios
            true, //Raging Brachydios
            true, //Glavenus
            true, //Acidic Glavenus
            true, //Anjanath
            true, //Fulgur Anjanath
            true, //Great Jagras
            false, //Pukei-Pukei
            false, //Coral Pukei-Pukei
            false, //Nergigante
            false, //Ruiner Nergigante
            false, //Safi'jiiva
            false, //Xeno'jiiva
            true, //Zorah Magdaros
            true, //Kulu-Ya-Ku
            true, //Jyuratodus
            true, //Tobi-Kadachi
            true, //Viper Tobi-Kadachi
            false, //Paolumu
            false, //NightShade Paolumu
            false, //Legiana
            false, //Shreiking Legiana
            true, //Great Girros
            true, //Odogaron
            true, //Ebony Odogaron
            true, //Radobaan
            false, //Vaal Hazak
            false, //Blackveil Vaal Hazak
            true, //Dodogama
            true, //Kulve Taroth
            false, //Bazelgeuse
            false, //Seething Bazelgeuse
            true, //Tzitzi-Ya-Ku
            true, //Behemoth
            true, //Beotodus
            true, //Banbaro
            false, //Velkhana
            false, //Namielle
            false, //Shara Ishvalda
            true, //Leshen
            true //Ancient Leshen
        };

        public static string[] MonsterStageEmNumber = { "em100_00_st", "em002_00_st", "unused", "unused", "unused", "unused", "unused", "em101_00_st", "unused", "em001_00_st", "em001_01_st", "em002_01_st", "em007_00_st", "em007_01_st", "em011_00_st", "em121_00_st", "em024_00_st", "em026_00_st", "em027_00_st", "em036_00_st",
            "em043_00_st", "em044_00_st", "em045_00_st", "em127_00_st", "em102_00_st", "em103_00_st", "em105_00_st", "em107_00_st", "em120_00_st", "em108_00_st", "em109_00_st", "em110_00_st", "em111_00_st", "em112_00_st", "em113_00_st", "em114_00_st", "em115_00_st", "em116_00_st", "unused", "em118_00_st", "unused",
            "unused", "unused", "unused", "unused", "unused", "unused", "unused", "unused", "unused", "unused", "em127_01_st", "unused", "unused", "unused", "unused","unused", "unused", "unused", "unused", "unused", "em032_00_st", "em037_00_st", "em042_00_st", "em043_05_st", "em063_00_st", "em080_00_st", "em080_01_st",
            "em100_01_st", "em102_01_st", "em103_05_st", "em109_01_st", "em110_01_st", "em111_05_st", "em113_01_st", "em115_05_st", "em118_05_st", "em122_00_st", "em123_00_st", "em124_00_st", "em125_00_st", "em126_00_st", "unused", "unused", "unused", "unused", "unused", "em050_00_st", "em001_02_st", "em002_02_st",
            "em018_00_st", "em023_00_st", "em023_05_st", "em032_01_st", "em057_00_st", "em057_01_st", "em063_05_st", "em104_00_st", "unused", "em018_05_st", "em042_05_st", "em013_00_st" };
        
        public static string[] MonsterEmNumber = { "em100_00", "em002_00", "ems001_00", "ems049_00", "em106_00", "ems004_00", "ems029_00", "em101_00", "ems051_00", "em001_00", "em001_01", "em002_01", "em007_00", "em007_01", "em011_00", "em121_00", "em024_00", "em026_00", "em027_00", "em036_00",
            "em043_00", "em044_00", "em045_00", "em127_00", "em102_00", "em103_00", "em105_00", "em107_00", "em120_00", "em108_00", "em109_00", "em110_00", "em111_00", "em112_00", "em113_00", "em114_00", "em115_00", "em116_00", "em117_00", "em118_00", "ems002_00",
            "ems003_00", "ems003_05", "ems005_01", "ems006_00", "ems050_00", "ems051_05", "ems053_00", "ems054_00", "ems055_00", "ems056_00", "em127_01", "ems058_00", "ems059_00", "ems060_00", "ems060_01","ems061_00", "ems062_00", "ems062_01", "ems062_02", "ems062_03", "em032_00", "em037_00",
            "em042_00", "em043_05", "em063_00", "em080_00", "em080_01", "em100_01", "em102_01", "em103_05", "em109_01", "em110_01", "em111_05", "em113_01", "em115_05", "em118_05", "em122_00", "em123_00", "em124_00", "em125_00", "em126_00", "ems014_00", "ems016_00", "ems063_00", "ems064_00", "ems061_01",
            "em050_00", "em001_02", "em002_02", "em018_00", "em023_00", "em023_05", "em032_01", "em057_00", "em057_01", "em063_05", "em104_00", "unused", "em018_05", "em042_05", "em013_00" };

        public static string[] MonsterVariantNumber = { "00", "00", "unused", "unused", "unused", "unused", "unused", "00", "unused", "00", "01", "01", "00", "01", "00", "00", "00", "00", "00", "00",
            "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "unused", "00", "unused",
            "unused", "unused", "unused", "unused", "unused", "unused", "unused", "unused", "unused", "unused", "01", "unused", "unused", "unused", "unused","unused", "unused", "unused", "unused", "unused", "00", "00", "00", "05", "00", "00", "01",
            "01", "01", "05", "01", "01", "05", "01", "05", "05", "00", "00", "00", "00", "00", "unused", "unused", "unused", "unused", "unused", "00", "em001_02_st", "em002_02_st",
            "em018_00_st", "em023_00_st", "em023_05_st", "em032_01_st", "em057_00_st", "em057_01_st", "em063_05_st", "em104_00_st", "unused", "em018_05_st", "em042_05_st", "em013_00_st" };

        //======================
        //  Story Hunt Quests
        //======================
        //00201 is a expedtion, this is the quest if return from expedition
        //All of these quests seem fine now with randomizing the map
        /// <summary>
        /// <para>The key is the quest ID and the value data about what to change (Don't randomize map with these, probably breaks story sequences)</para>
        /// <para>00401 and 00504 zorah magdaros quest (try randomizing the second monster (first is zorah, second is nergigante))</para>
        /// </summary>
        public static Dictionary<string, StoryQuestData> StoryHuntQuest = new Dictionary<string, StoryQuestData>()
        {
            { "00102", new StoryQuestData { ChangeObjective = true, QuestObjTextIndexs = new int[] { 165 } } }, //A Kestodon Kerfuffle
            { "00103", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, QuestObjTextIndexs = new int[] { 168 } } }, //The Great Jagras Hunt
            { "00201", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, CreateFSM = false, QuestObjTextIndexs = new int[] { 143 } } }, //Bird-Brained Bandit
            { "00205", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, CanRandomizeMap = true, QuestObjTextIndexs = new int[] { 171 } } }, //Urgent: Pukei-Pukei Hunt
            { "00301", new StoryQuestData { ChangeObjective = true, QuestObjTextIndexs = new int[] { 147, 149 } } }, //The Best Kind of Quest //Broken monsters 21 //kinda broken 0
            { "00302", new StoryQuestData { ChangeObjective = true, QuestObjTextIndexs = new int[] { 175 } } }, //Sinister Shadows in the Swamp
            { "00305", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, QuestObjTextIndexs = new int[] { 178 } } }, //Flying Sparks: Tobi-Kadachi
            { "00306", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, QuestObjTextIndexs = new int[] { 181 } } }, //The Encroaching Anjanath
            { "00405", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, QuestObjTextIndexs = new int[] { 191 } } }, //Ballooning Problems
            { "00407", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, CanRandomizeMap = true, CreateFSM = false, QuestObjTextIndexs = new int[] { 199 } } }, //Radobaan Roadblock
            { "00408", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, CanRandomizeMap = true, QuestObjTextIndexs = new int[] { 202 } } }, //Legiana: Embodiment of Elegance
            { "00501", new StoryQuestData { ChangeObjective = true, QuestObjTextIndexs = new int[] { 205, 207 } } }, //Into the Bowels of the Vale
            { "00502", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, CanRandomizeMap = true, CreateFSM = false, QuestObjTextIndexs = new int[] { 213 } } }, //A Fiery Throne Atop the Forest
            { "00503", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, QuestObjTextIndexs = new int[] { 216 } } }, //Horned Tyrant Below the Sands
            //{ "00504", new StoryQuestData { QuestObjTextIndexs = new int[] { 219, 235 } } }, //A Colossal Task
            { "00601", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, CanRandomizeMap = true, CreateFSM = false, QuestObjTextIndexs = new int[] { 227 } } }, //Invader in the Waste
            { "00605", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, CanRandomizeMap = true, QuestObjTextIndexs = new int[] { 230 } } }, //Tickled Pink
            { "00607", new StoryQuestData { ChangeObjective = true, CanRandomizeMap = true, QuestObjTextIndexs = new int[] { 233 } } }, //Old World Monster in the New World
            { "00805", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, CanRandomizeMap = true, QuestObjTextIndexs = new int[] { 259 }, DuplicateMonsterHunt = true } }, //Beyond the Blasting Scales

            //Special Assignments
            { "50701", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, QuestObjTextIndexs = new int[] { 305 } } }, //The Food Chain Dominator
        };

        //May need some testing for 00804, 50601, 50802, and 50906 //May need special text case for 50802
        //Done doing first check for these (check if has cutscene if so don't randomize the map)
        public static Dictionary<string, StoryQuestData> StorySlayQuest = new Dictionary<string, StoryQuestData>()
        {
            { "00701", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, QuestObjTextIndexs = new int[] { 245 } } }, //A Wound and a Thirst
            { "00801", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, QuestObjTextIndexs = new int[] { 248 } } }, //Kushala Daora, Dragon of Steel
            { "00802", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, QuestObjTextIndexs = new int[] { 251 } } }, //Teostra the Infernal
            { "00803", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, QuestObjTextIndexs = new int[] { 254 } } }, //Hellish Fiend Vaal Hazak
            { "00804", new StoryQuestData { ChangeObjective = true } }, //Land of Convergence
            { "00806", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, CanRandomizeMap = true, QuestObjTextIndexs = new int[] { 261 } } }, //Thunderous Rumble in the Highlands

            //Special Assignments
            { "50801", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, CanRandomizeMap = true, QuestObjTextIndexs = new int[] { 307 } } }, //The Blazing Sun
            { "50802", new StoryQuestData { ChangeObjective = true, QuestObjTextIndexs = new int[] { 310 } } }, //Pandora's Arena
            { "50803", new StoryQuestData { ChangeQuestIcon = true, ChangeObjective = true, CanRandomizeMap = true, QuestObjTextIndexs = new int[] { 312, 313 }, MultiObjectiveHunt = true } }, //No Remorse, No Surrender
            { "50601", new StoryQuestData { ChangeObjective = true, QuestObjTextIndexs = new int[] { 317 } } }, //A Visitor from Another World
            { "50906", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, QuestObjTextIndexs = new int[] { 322 } } }, //He Taketh It with His Eyes
            { "50910", new StoryQuestData { ChangeObjective = true, QuestObjTextIndexs = new int[] { 338 } } }, //Contract: Trouble in the Ancient Forest
        };

        //================
        //  Hunt Quests
        //================
        public static string[] BigMonsterHuntQuests = { "00251", "00252", "00331", "00332", "00333", "00351", "00352", "00361", "00362", "00431", "00432", "00433", "00434", "00451", "00461", "00472", "00473", "00482", "00483", "00531", "00532", "00533", "00534", "00551", "00561", 
            "00571", "00581", "00631", "00632", "00633", "00634", "00635", "00636", "00637", "00651", "00652", "00653", "00654", "00655", "00661", "00662", "00663", "00665", "00666", "00667", "00671", "00672", "00681", "00682", "00693", "00731", "00732", "00733", "00734", "00735", 
            "00736", "00737", "00738", "00751", "00752", "00761", "00762", "00763", "00771", "00772", "00773", "00781", "00791", "00792", "00793", "00794", "50751", "61606", "61607", "62515", "65603", "66609" };

        //Hunt 2 monsters (998 first moster is slay)
        public static string[] HuntMultiObjective = { "00552", "00753", "00764", "00774", "00795", "00996", "00997", "00998", "61103", "61105", "61603", "61604", "66102", "66103", "66606", "66607", "66608", "67604" };

        //"00992 (multimonster (4 monsters))", 64101 (multimonster (3 monsters)), 65605 (multimonster (3 monsters)), 66105 (multimonster (3 monsters)), 66106 (multimonster (3 monsters)), 66601 (multimonster (5 monsters)), 66602 (multimonster (5 monsters)), 66603 (multimonster (4 monsters)),
        //66604 (multimonster (4 monsters)), 66605 (multimonster (4 monsters)), 67103 multimonster (2 monsters)), 67605 (multimonster (4 monsters)),  67608 (mulitmonster (3 monsters))
        public static string[] HuntMultiMonster = { "00992", "64101", "65605", "66105", "66106", "66601", "66602", "66603", "66604", "66605", "67103", "67605", "67608" };

        //05003 (hunt 2 of the same monster), 61104 (hunt 2 of the same monster), 64601 (hunt 2 of the same monster), 65602 (hunt 3 of the same monster), 66101 (2 of the same), 67106 (3 of same)
        public static string[] HuntDuplicate = { "00961", "05003", "61104", "64601", "65602", "66101", "67106" };

        //================
        //  Slay Quests
        //================
        public static string[] BigMonsterSlayQuests = { "00572", "00851", "00861", "00871", "00881", "00891", "00892", "00893", "00894", "00895", "00896", "00971", "00991", "03001", "03002", "03031", "03032", "03033", "03034", "03101", "62502", "62503", "62504", "62505",
            "62506", "62511", "62607", "62609", "63038", "63039", "63052", "63101", "63102", "63103", "63107", "63108", "63130", "63131", "67606", "67610",  };

        public static string[] SlayMultiObjective = { "03051", "03052", "50861", "50891", "50892", "50906", "50991", "63002", "63031", "63051" };

        //"00995 (Multi Monster (3 monsters))", 63001 (multimonster (2 monsters)), 63104 (multimonster (2 monsters)), 63105 (multimonster (2 monsters)), 63106 (multimonster (2 monsters)), 63109 (multimonster (3 monsters)), 63110 (multimonster (3 monsters)), 66610 (multimonster (3 monsters))
        public static string[] SlayMultiMonster = { "00995", "63001", "63104", "63105", "63106", "63109", "63110", "66610" };

        //63032 (2 of same), 66104 (2 of same)
        public static string[] SlayDuplicate = { "63032", "66104" };

        //==================
        //  Capture Quests
        //==================
        public static string[] BigMonsterCaptureQuests = { "00261", "00365", "00475", "00582", "00656", "63104" };

        public static int[] BigMonsterIDs = { 0, 1, 7, 9, 10, 11, 12, 13, 14, 16, 17, 18, 19, 20, 21, 22, 24, 25, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 39 };
        public static int[] LowRankBigMonsterIDs = { 0, 1, 7, 9, 12, 14, 21, 24, 27, 28, 29, 30, 31, 32, 33, 34, 35 };
        public static int[] ElderDragonIDs = { 14, 15, 16, 17, 18, 25, 26, 36, 70, 75, 79, 80, 81, 87, 97, 101 };

        //15 (behemoth), 23 (leshen), 26 (xeno'jjiva), 51 (ancient leshen)
        //public static int[] HighRankMonsters = { 10, 11, 13, 16, 17, 18, 19, 20, 22, 25, 36, 37, 39 };


        //==============================
        //          Iceborne
        //==============================

        //======================
        //  Story Hunt Quests
        //======================
        //may need to test 51604
        //Done doing first check for these (check if has cutscene if so don't randomize the map)
        /// <summary>
        /// <para>The key is the quest ID and the value data about what to change (Don't randomize map with these, probably breaks story sequences)</para>
        /// </summary>
        public static Dictionary<string, StoryQuestData> IBStoryHuntQuest = new Dictionary<string, StoryQuestData>()
        {
            { "01101", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, CanRandomizeMap = true, CreateFSM = false, QuestObjTextIndexs = new int[] { 494 } } }, //Baptism by Ice
            { "01102", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, CanRandomizeMap = true, CreateFSM = false, QuestObjTextIndexs = new int[] { 501 } } }, //Banbaro Blockade
            { "01201", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, CanRandomizeMap = true, CreateFSM = false, QuestObjTextIndexs = new int[] { 508 } } }, //Ready to Strike
            { "01202", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, QuestObjTextIndexs = new int[] { 512 } } }, //No Time for Naps
            { "01203", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, QuestObjTextIndexs = new int[] { 515 } } }, //Play Both Ends
            { "01301", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, QuestObjTextIndexs = new int[] { 519 } } }, //Blizzard Blitz
            { "01302", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, CanRandomizeMap = true, QuestObjTextIndexs = new int[] { 524 } } }, //Ever-present Shadow
            { "01303", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, QuestObjTextIndexs = new int[] { 526, 527 } } }, //The Scorching Blade
            { "01304", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, QuestObjTextIndexs = new int[] { 530, 531 } } }, //Absolute Power
            { "01305", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, QuestObjTextIndexs = new int[] { 533, 534 } } }, //A Smashing Cross Counter
            { "01306", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, QuestObjTextIndexs = new int[] { 537, 538 } } }, //A Tale of Ice and Fire (Need to test this quest, repel velkhana quest)
            { "01401", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, CanRandomizeMap = true, CreateFSM = false, QuestObjTextIndexs = new int[] { 545 } } }, //When the Mist Taketh You
            { "01402", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, QuestObjTextIndexs = new int[] { 552 } } }, //The Disintegrating Blade
            { "01403", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, QuestObjTextIndexs = new int[] { 555 } } }, //Bad Friends, Great Enemies
            { "01405", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, CanRandomizeMap = true, QuestObjTextIndexs = new int[] { 548 } } }, //The Thunderous Troublemaker!
            { "01502", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, CanRandomizeMap = true, CreateFSM = false, QuestObjTextIndexs = new int[] { 571 } } }, //The Second Coming

            //Special Assignments
            { "51603", new StoryQuestData { ChangeObjective = true, QuestObjTextIndexs = new int[] { 641 } } }, //Across the Lost Path
            { "51604", new StoryQuestData { ChangeObjective = true } }, //Point of No Return
            { "51607", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, CanRandomizeMap = true, QuestObjTextIndexs = new int[] { 654 } } }, //The Fury Remains
        };

        //May need some testing
        public static Dictionary<string, StoryQuestData> IBStorySlayQuest = new Dictionary<string, StoryQuestData>()
        {
            { "01404", new StoryQuestData { ChangeObjective = true, ChangeQuestIcon = true, QuestObjTextIndexs = new int[] { 558, 559 } } }, //The Defense of Seliana (Need to test)
            { "01501", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, QuestObjTextIndexs = new int[] { 563, 564 } } }, //The Iceborne Wyvern
            { "01503", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, QuestObjTextIndexs = new int[] { 574, 575 } } }, //Under the Veil of Death
            { "01504", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, QuestObjTextIndexs = new int[] { 578, 579 } } }, //A Light From The Abyss
            { "01601", new StoryQuestData { ChangeObjective = true, QuestObjTextIndexs = new int[] { 583 } } }, //To The Guided, A Paean
            { "01602", new StoryQuestData { ChangeObjective = true } }, //Paean of Guidance
            { "01605", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, CanRandomizeMap = true, QuestObjTextIndexs = new int[] { 598 } } }, //To the Very Ends with You

            //Special Assignments
            { "51601", new StoryQuestData { ChangeObjective = true, QuestObjTextIndexs = new int[] { 633 } } }, //Reveal Thyself, Destroyer
            { "51602", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, CanRandomizeMap = true, QuestObjTextIndexs = new int[] { 628 } } }, //Sterling Pride
            { "51606", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, CanRandomizeMap = true, QuestObjTextIndexs = new int[] { 651 } } }, //...And My Rage for All
            { "51611", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, QuestObjTextIndexs = new int[] { 662 } } }, //Blazing Black Twilight
            { "51612", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, QuestObjTextIndexs = new int[] { 673 } } }, //The Black Dragon (Fatalis)
            { "51613", new StoryQuestData { ChangeQuestIcon = true, ChangeQuestBookObjText = true, ChangeObjective = true, CanRandomizeMap = true, QuestObjTextIndexs = new int[] { 670 } } }, //Dawn's Triumph
        };

        //================
        //  Hunt Quests
        //================
        public static string[] IBBigMonsterHuntQuests = { "01121", "01122", "01131", "01132", "01133", "01134", "01151", "01152", "01153", "01154", "01161", "01162", "01163", "01164", "01171", "01181", "01191", "01192", "01221", "01224", "01225", "01231", "01232", "01233", "01234", "01235", 
            "01236", "01237", "01238", "01251", "01252", "01261", "01262", "01263", "01271", "01272", "01273", "01274", "01281", "01321", "01323", "01331", "01332", "01333", "01334", "01335", "01336", "01337", "01338", "01339", "01340", "01351", "01352", "01353", "01361", "01362", "01371", 
            "01372", "01373", "01381", "01382", "01391", "01392", "01393", "01421", "01422", "01431", "01432", "01433", "01434", "01435", "01451", "01452", "01461", "01462", "01471", "01481", "01491", "01562", "01593", "01606", "01632", "01633", "01634", "01635", "01636", "51621", "51623", 
            "61806", "61809", "61813", "64802", "66817", "66818", "66819", "66820", "66821", "66822", "66824", "66825", "66832", "66835", "66859", "66860", "67807", "66846", "66854", "66855", "66856", "66857", "66861", "66867", "67809", "66865", "66866" };

        public static string[] IBHuntMultiObjective = { "01264", "01354", "01363", "01383", "01395", "01423", "01483", "01603", "01604", "61802", "61812", "64801", "66805", "66807", "66808", "66809", "66810", "66811", "66812", "66813", "66814", "66862" };

        //01631 (multimonster (5 monsters)), 01642 (multimonster (2 monsters)), 61814 (multimonster (2 monsters)), 66806 (multimonster (4 monsters)), 66826 (multimonster (5 monsters)), 66827 (multimonster (5 monsters)), 66828 (multimonster (5 monsters)), 66829 (multimonster (5 monsters)),
        //66830 (multimonster (5 monsters)), 66831 (multimonster (5 monsters)), 66834 (multimonster (2 monsters)), 66864 (multimonster (2 monsters))
        public static string[] IBHuntMultiMonster = { "01631", "01642", "61814", "66806", "66826", "66827", "66828", "66829", "66830", "66831", "66834", "66864" };

        //61808 (hunt 2 of the same monster), 66801 (2 of same), 66803 (2 of the same)
        public static string[] IBHuntDuplicate = { "61808", "66801", "66803" };

        //================
        //  Slay Quests
        //================
        public static string[] IBBigMonsterSlayQuests = { "01521", "01551", "01552", "01561", "01571", "01572", "01581", "01591", "01592", "01641", "01661", "01691", "03071", "03072", "03073", "03091", "03171", "51605", "51622", "63073", "63074", "63143", "63150", "66836", "67801",
           "66847", "66840", "66841", "66842", "66843", "66844", "66845", "66850", "66858", "66863", "67806" };

        //66815 (multimonster (3 monsters)), 66816 (multimonster (2 monsters)), 01651 (multimonster (2 monsters)), 01671 (multimonster (2 monsters)), 01692 (multimonster (4 monsters)), 03092 (multimonster (2 monsters)), 61807 (multimonster (2 monsters)), 63071 (mulitmonster (2 monsters)), 63072 (multimonster (2 monsters))
        //63149 (multimonster (2 monster))
        public static string[] IBSlayMultiMonster = { "01651", "01671", "01692", "03074", "03092", "61807", "63071", "63072", "63149", "66815", "66816" };

        //==================
        //  Capture Quests
        //==================
        public static string[] IBBigMonsterCaptureQuests = { "01123", "01155", "01222", "01253", "01322", "01394", "01482", "01492", "01594", "61803" };


        public static int[] IBHighRankOnlyMonsters = { 7, 10, 15, 20, 23, 25, 26, 36, 39, 51 };

        //Removed safi'jiiva because doesn't spawn on most maps
        public static int[] BigMonsterIDsIB = { 0, 1, 9, 11, 12, 13, 14, 16, 17, 18, 19, 21, 22, 24, 27, 28, 29, 30, 31, 32, 33, 34, 35, 37, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 88, 89, 90, 91, 92, 93, 94, 95, 96, 99, 100 };

        #endregion
    }
}
