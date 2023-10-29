namespace MHW_Randomizer
{
    public class AlnkPaths
    {
        public int PathOffset;
        //Number of bytes
        public int PathLength;
        //The alnk the path is from
        public string TargetAlnk;
    }

    public class Alnks
    {
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
            true, //Vaal Hazak
            true, //Blackveil Vaal Hazak
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

        public static AlnkPaths[][] GroundOffsets =
        {
            //101
            new AlnkPaths[]
            {
                new AlnkPaths { PathOffset = 0x2C, PathLength = 1498, TargetAlnk = @"\em\em100\00\data\em100.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 1154, TargetAlnk = @"\em\em101\00\data\em101.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 1362, TargetAlnk = @"\em\em043\00\data\em043.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 1163, TargetAlnk = @"\em\em127\00\data\em127.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x350, PathLength = 505, TargetAlnk = @"\em\em107\00\data\em107.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 777, TargetAlnk = @"\em\em109\00\data\em109.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 961, TargetAlnk = @"\em\em123\00\data\em123.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 729, TargetAlnk = @"\em\em023\00\data\em023.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 809, TargetAlnk = @"\em\em057\00\data\em057.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 761, TargetAlnk = @"\em\em037\00\data\em037.dtt_alnk" }
            },
            
            //102
            new AlnkPaths[]
            {
                new AlnkPaths { PathOffset = 0x606, PathLength = 612, TargetAlnk = @"\em\em100\00\data\em100.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x57E, PathLength = 1100, TargetAlnk = @"\em\em043\00\data\em043.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x3ED, PathLength = 988, TargetAlnk = @"\em\em123\00\data\em123.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x305, PathLength = 660, TargetAlnk = @"\em\em023\00\data\em023.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 524, TargetAlnk = @"\em\em007\00\data\em007.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 1016, TargetAlnk = @"\em\em007\01\data\em007.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 676, TargetAlnk = @"\em\em044\00\data\em044.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x44D, PathLength = 676, TargetAlnk = @"\em\em032\00\data\em032.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x365, PathLength = 1028, TargetAlnk = @"\em\em080\00\data\em080.dtt_alnk" },
            },
            
            //103
            new AlnkPaths[]
            {
                new AlnkPaths { PathOffset = 0x9CA, PathLength = 852, TargetAlnk = @"\em\em043\00\data\em043.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x7C9, PathLength = 620, TargetAlnk = @"\em\em123\00\data\em123.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x599, PathLength = 676, TargetAlnk = @"\em\em023\00\data\em023.dtt_alnk" },
                new AlnkPaths { PathOffset = 0xAA8, PathLength = 740, TargetAlnk = @"\em\em057\00\data\em057.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x325, PathLength = 708, TargetAlnk = @"\em\em037\00\data\em037.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 748, TargetAlnk = @"\em\em120\00\data\em120.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x3D4, PathLength = 844, TargetAlnk = @"\em\em113\00\data\em113.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 724, TargetAlnk = @"\em\em011\00\data\em011.dtt_alnk" },
            },
            
            //104
            new AlnkPaths[]
            {
                new AlnkPaths { PathOffset = 0xD1E, PathLength = 857, TargetAlnk = @"\em\em043\00\data\em043.dtt_alnk" },
                new AlnkPaths { PathOffset = 0xA35, PathLength = 729, TargetAlnk = @"\em\em123\00\data\em123.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x83D, PathLength = 721, TargetAlnk = @"\em\em023\00\data\em023.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 649, TargetAlnk = @"\em\em112\00\data\em112.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 777, TargetAlnk = @"\em\em113\00\data\em113.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 569, TargetAlnk = @"\em\em114\00\data\em114.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 1002, TargetAlnk = @"\em\em115\00\data\em115.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x6F1, PathLength = 985, TargetAlnk = @"\em\em032\00\data\em032.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 633, TargetAlnk = @"\em\em080\01\data\em080.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x91D, PathLength = 729, TargetAlnk = @"\em\em100\01\data\em100.dtt_alnk" },
            },
            
            //105
            new AlnkPaths[]
            {
                new AlnkPaths { PathOffset = 0x1077, PathLength = 950, TargetAlnk = @"\em\em043\00\data\em043.dtt_alnk" },
                new AlnkPaths { PathOffset = 0xD0E, PathLength = 990, TargetAlnk = @"\em\em123\00\data\em123.dtt_alnk" },
                new AlnkPaths { PathOffset = 0xB0E, PathLength = 774, TargetAlnk = @"\em\em023\00\data\em023.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 830, TargetAlnk = @"\em\em116\00\data\em116.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x61, PathLength = 852, TargetAlnk = @"\em\em036\00\data\em036.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 726, TargetAlnk = @"\em\em045\00\data\em045.dtt_alnk" },
                new AlnkPaths { PathOffset = 0xACA, PathLength = 1294, TargetAlnk = @"\em\em032\00\data\em032.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 806, TargetAlnk = @"\em\em063\00\data\em063.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x769, PathLength = 1182, TargetAlnk = @"\em\em080\00\data\em080.dtt_alnk" },
                new AlnkPaths { PathOffset = 0xBF6, PathLength = 966, TargetAlnk = @"\em\em100\01\data\em100.dtt_alnk" },
            },

            //108
            new AlnkPaths[]
            {
                new AlnkPaths { PathOffset = 0x10EC, PathLength = 3564, TargetAlnk = @"\em\em123\00\data\em123.dtt_alnk" },
                new AlnkPaths { PathOffset = 0xE14, PathLength = 4371, TargetAlnk = @"\em\em023\00\data\em023.dtt_alnk" },
                new AlnkPaths { PathOffset = 0xDFB, PathLength = 1850, TargetAlnk = @"\em\em057\00\data\em057.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x43E, PathLength = 1073, TargetAlnk = @"\em\em011\00\data\em011.dtt_alnk" },
                new AlnkPaths { PathOffset = 0xBB, PathLength = 2058, TargetAlnk = @"\em\em109\01\data\em109.dtt_alnk" },
                new AlnkPaths { PathOffset = 0xFD8, PathLength = 2562, TargetAlnk = @"\em\em032\00\data\em032.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 2978, TargetAlnk = @"\em\em042\00\data\em042.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x142D, PathLength = 2186, TargetAlnk = @"\em\em043\05\data\em043.dtt_alnk" },
                new AlnkPaths { PathOffset = 0xFBC, PathLength = 2170, TargetAlnk = @"\em\em100\01\data\em100.dtt_alnk" },
            },

            //109
            new AlnkPaths[]
            {
                //new AlnkPaths { PathOffset = 0x86A, PathLength = 1619, TargetAlnk = @"\em\em100\00\data\em100.dtt_alnk" },
                //new AlnkPaths { PathOffset = 0x34D, PathLength = 1642, TargetAlnk = @"\em\em120\00\data\em120.dtt_alnk" },
                //new AlnkPaths { PathOffset = 0x3C4, PathLength = 970, TargetAlnk = @"\em\em109\00\data\em109.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x1ED8, PathLength = 4781, TargetAlnk = @"\em\em123\00\data\em123.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x1F27, PathLength = 6304, TargetAlnk = @"\em\em023\00\data\em023.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x355, PathLength = 1875, TargetAlnk = @"\em\em057\00\data\em057.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x5E9, PathLength = 1547, TargetAlnk = @"\em\em037\00\data\em037.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x720, PathLength = 1595, TargetAlnk = @"\em\em113\00\data\em113.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x397, PathLength = 1022, TargetAlnk = @"\em\em116\00\data\em116.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2A7, PathLength = 1354, TargetAlnk = @"\em\em007\00\data\em007.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x86F, PathLength = 2015, TargetAlnk = @"\em\em011\00\data\em011.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x7E3, PathLength = 978, TargetAlnk = @"\em\em044\00\data\em044.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x19DA, PathLength = 4060, TargetAlnk = @"\em\em032\00\data\em032.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x1836, PathLength = 5261, TargetAlnk = @"\em\em100\01\data\em100.dtt_alnk" },
            }
        };

        public static AlnkPaths[][] FlyingOffsets =
        {
            //101
            new AlnkPaths[]
            {
                new AlnkPaths { PathOffset = 0x58C, PathLength = 697, TargetAlnk = @"\em\em001\00\data\em001.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 1242, TargetAlnk = @"\em\em002\00\data\em002.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x5F0, PathLength = 1354, TargetAlnk = @"\em\em024\00\data\em024.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 705, TargetAlnk = @"\em\em102\00\data\em102.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2E0, PathLength = 737, TargetAlnk = @"\em\em110\01\data\em110.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 697, TargetAlnk = @"\em\em118\00\data\em118.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 729, TargetAlnk = @"\em\em124\00\data\em124.dtt_alnk" },
            },
            
            //102
            new AlnkPaths[]
            {
                new AlnkPaths { PathOffset = 0x2C, PathLength = 1376, TargetAlnk = @"\em\em001\00\data\em001.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x38C, PathLength = 708, TargetAlnk = @"\em\em102\00\data\em102.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x770, PathLength = 652, TargetAlnk = @"\em\em103\00\data\em103.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 692, TargetAlnk = @"\em\em110\01\data\em110.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2E5, PathLength = 628, TargetAlnk = @"\em\em118\00\data\em118.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x305, PathLength = 628, TargetAlnk = @"\em\em124\00\data\em124.dtt_alnk" },
            },
            
            //103
            new AlnkPaths[]
            {
                new AlnkPaths { PathOffset = 0x2C, PathLength = 492, TargetAlnk = @"\em\em110\00\data\em110.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 764, TargetAlnk = @"\em\em111\00\data\em111.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x559, PathLength = 628, TargetAlnk = @"\em\em118\00\data\em118.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 724, TargetAlnk = @"\em\em102\01\data\em102.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x579, PathLength = 732, TargetAlnk = @"\em\em124\00\data\em124.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 1216, TargetAlnk = @"\em\em125\00\data\em125.dtt_alnk" },
            },
            
            //104
            new AlnkPaths[]
            {
                new AlnkPaths { PathOffset = 0x7CD, PathLength = 633, TargetAlnk = @"\em\em118\00\data\em118.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x855, PathLength = 713, TargetAlnk = @"\em\em124\00\data\em124.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 1002, TargetAlnk = @"\em\em115\00\data\em115.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 1234, TargetAlnk = @"\em\em115\05\data\em115.dtt_alnk" },
            },
            
            //105
            new AlnkPaths[]
            {
                new AlnkPaths { PathOffset = 0x5A5, PathLength = 742, TargetAlnk = @"\em\em002\00\data\em002.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 1476, TargetAlnk = @"\em\em024\00\data\em024.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x252, PathLength = 1310, TargetAlnk = @"\em\em103\00\data\em103.dtt_alnk" },
                new AlnkPaths { PathOffset = 0xA46, PathLength = 686, TargetAlnk = @"\em\em118\00\data\em118.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2C, PathLength = 1214, TargetAlnk = @"\em\em118\05\data\em118.dtt_alnk" },
                new AlnkPaths { PathOffset = 0xB1E, PathLength = 1578, TargetAlnk = @"\em\em124\00\data\em124.dtt_alnk" },
            },
            
            //108
            new AlnkPaths[]
            {
                new AlnkPaths { PathOffset = 0x3C7, PathLength = 1025, TargetAlnk = @"\em\em111\00\data\em111.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x3C7, PathLength = 2138, TargetAlnk = @"\em\em111\05\data\em111.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x11B7, PathLength = 761, TargetAlnk = @"\em\em124\00\data\em124.dtt_alnk" },
            },
            
            //109
            new AlnkPaths[]
            {
                new AlnkPaths { PathOffset = 0x14F2, PathLength = 4410, TargetAlnk = @"\em\em124\00\data\em124.dtt_alnk" },
                new AlnkPaths { PathOffset = 0xA2C, PathLength = 2112, TargetAlnk = @"\em\em001\02\data\em001.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x2D9, PathLength = 2224, TargetAlnk = @"\em\em002\02\data\em002.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x559, PathLength = 1310, TargetAlnk = @"\em\em118\05\data\em118.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x7C8, PathLength = 1855, TargetAlnk = @"\em\em111\00\data\em111.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x5C1, PathLength = 1619, TargetAlnk = @"\em\em110\01\data\em110.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x650, PathLength = 1739, TargetAlnk = @"\em\em102\00\data\em102.dtt_alnk" },
                new AlnkPaths { PathOffset = 0xB3A, PathLength = 1493, TargetAlnk = @"\em\em024\00\data\em024.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x88B, PathLength = 1629, TargetAlnk = @"\em\em002\00\data\em002.dtt_alnk" },
                new AlnkPaths { PathOffset = 0x8E4, PathLength = 1595, TargetAlnk = @"\em\em001\00\data\em001.dtt_alnk" },
            },
        };
    }
}
