using System.Collections.Generic;

namespace MHW_Randomizer
{
    public class ExpeditionData
    {
        //Kirin (14) only shows up in master rank expeditions
        public static int[] UsualLowRankMonster = { 0, 1, 7, 9, 12, 14, 21, 24, 27, 28, 29, 30, 31, 32, 33, 34, 35 };
        public static int[] UsualHighRankMonster = { 0, 1, 7, 9, 10, 11, 12, 13, 14, 16, 17, 18, 19, 20, 21, 22, 24, 27, 28, 29, 30, 31, 32, 33, 34, 35, 37, 39 };
        public static int[] HighRankOnlyMonster = { 10, 11, 13, 16, 17, 18, 19, 20, 22, 37, 39 };
        public static int[] UsualMasterRankMonster = { 0, 1, 7, 9, 10, 11, 12, 13, 14, 16, 17, 18, 19, 20, 21, 22, 24, 27, 28, 29, 30, 31, 32, 33, 34, 35, 37, 61, 62, 63, 64, 65, 66, 67, 68, 69, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80 };
        public static int[] MasterRankOnlyMonster = { 61, 62, 63, 64, 65, 66, 67, 68, 69, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80 };

        //Unused monsters in expeditions
        public static int[] UnusedHighRankMonster = { 25, 36 };
        //Shara, furious rajang, and alatreon to add based on options (remember to make sobj if they don't have one!!)
        public static int[] UnusedMasterRankMonster = { 70, 87, 88, 89, 90, 91, 93, 94, 95, 96, 99, 100 };


        //Required Monsters
        /// <summary>
        /// Key is the monster ID, and the value is the map they appear in for the expidition
        /// </summary>
        public static Dictionary<uint, int> LowRankRequiredMonsters = new Dictionary<uint, int> { { 27, 101 } };
        /// <summary>
        /// Key is the monster ID, and the value is the map they appear in for the expidition
        /// </summary>
        public static Dictionary<uint, int> HighRankRequiredMonsters = new Dictionary<uint, int> { { 9, 101 }, { 24, 102 }, { 35, 104 }, { 22, 105 }, { 19, 105 }, { 37, 105 }, { 39, 105 } };
        /// <summary>
        /// Key is the monster ID, and the value is the map they appear in for the expidition
        /// </summary>
        public static Dictionary<uint, int> MasterRankRequiredMonsters = new Dictionary<uint, int> { { 77, 108 }, { 78, 108 }, { 71, 108 }, { 73, 108 }, { 76, 108 } };

    }
}
