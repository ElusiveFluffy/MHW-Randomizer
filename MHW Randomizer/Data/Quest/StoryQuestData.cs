using System.Collections.Generic;

namespace MHW_Randomizer
{
    public class StoryQuestData
    {
        public bool ChangeQuestIcon;
        public bool ChangeObjective;
        public bool ChangeQuestBookObjText;
        public bool CanRandomizeMap;
        public bool DuplicateMonsterHunt;
        public bool MultiMonsterHunt;

        /// <summary>
        /// The text indexes for the hunt text in the story target file
        /// </summary>
        public int[] QuestObjTextIndexs;
    }
}
