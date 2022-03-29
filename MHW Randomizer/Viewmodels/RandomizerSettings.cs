using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHW_Randomizer
{
    public class RandomizerSettings : BaseViewModel
    {
        public string ChunkFolderPath;
        public string UserSeed { get; set; }
        //To add:
        //Chose where to save randomized files

        //--------------------
        //  Quest Settings
        //--------------------

        //General Quest Settings
        public bool DuplicateMonster { get; set; }
        public bool IncludeLeshen { get; set; }
        public bool IncludeXenojiiva { get; set; } //Fix Xenojiiva
        public bool IncludeBehemoth { get; set; }
        public bool TwoMonsterQuests { get; set; }
        public bool RandomSobj { get; set; }
        public bool RandomMaps { get; set; }
        public bool RandomIcons { get; set; }
        public bool RandomizeMusic { get; set; }
        public bool RandomizeMultiObj { get; set; }
        public bool RandomizeMultiMon { get; set; }
        public bool RandomizeDuplicate { get; set; }
        public bool DontRandomizeSlay { get; set; }
        public bool DontRandomizeCapture { get; set; }
        //Small Monsters

        //Iceborne Specific Quest Settings
        public bool RandomizeIBQuests { get; set; }
        [DefaultValue(true)]
        public bool MonstersFoundInIB { get; set; } = true;
        public bool IceborneOnlyMonsters { get; set; }
        public bool IncludeHighRankOnly { get; set; }
        public bool IncludeFatalis { get; set; } //Fix fatalis

        [DefaultValue(100)]
        public int MonsterMinSize { get; set; } = 100;
        [DefaultValue(100)]
        public int MonsterMaxSize { get; set; } = 100;

        //-------------------
        //  Gear Settings
        //-------------------

        //Armour
        public bool ShuffleArmourRecipes { get; set; }
        public bool ShuffleCharms { get; set; }
        public bool ShuffleIronOre { get; set; }
        public bool RandomArmourIconColour { get; set; }
        public bool RandomArmourSkill { get; set; }
        public bool RandomCharmSkill { get; set; }
        public bool RandomArmourSkillLevels { get; set; }
        public bool RandomCharmSkillLevels { get; set; }
        public bool ShuffleArmourSetBonus { get; set; }
        public bool NonRankSpecificSetBonusShuffle { get; set; }
        //(scale with rank)
        public bool RandomArmourEleResist { get; set; } //
        public bool RandomArmourDecoSlots { get; set; }
        public bool GiveCharmDecoSlot { get; set; }
        public bool RandomDecoSlotSize { get; set; }

        //Weapons
        public bool ShuffleWeaponRecipes { get; set; }
        public bool ShuffleWeaponOrder { get; set; }
        public bool RandomWeaponIconColour { get; set; }
        public bool RandomWeaponDecoSlots { get; set; }
        public bool RandomWeaponDecoSlotSize { get; set; }
        public bool RandomWeaponElement { get; set; }

        //Palico
        public bool ShufflePalicoArmour { get; set; }
        public bool ShufflePalicoWeapons { get; set; }
        public bool RandomPalicoWeaponElement { get; set; }
        public bool RandomPalicoWeaponType { get; set; }
        public bool RandomPalicoWeaponColour { get; set; }

        //Kinsect
        public bool ShuffleKinsectRecipes { get; set; }
        public bool ShuffleKinsectOrder { get; set; }
        public bool RandomKinsectIconColour { get; set; }
        public bool RandomKinsectType { get; set; }
        public bool RandomKinsectDust { get; set; }

        //Extras for later maybe
        //Completely random (maybe with some logic so low rank stuff only have low rank material, high rank with high rank materials, master rank with master rank materials)
        public bool RandomArmourRecipes { get; set; }
        public bool RandomWeaponRecipes { get; set; }
        public bool RandomKinsectRecipes { get; set; }
        //Using the pre-selected gear in the quest settings (Need to figure out how this works)
        public bool RandomGearEachQuest { get; set; }

        //-----------------
        //  Shop Settings
        //-----------------

        //Gear Shop
        public bool RandomShopWepArmour { get; set; }
        public bool RandomShopWepArmourType { get; set; }
        [DefaultValue(97)]
        public uint AmountOfGearShopItems { get; set; } = 97;

        //Item Shop
        public bool RandomShopItems { get; set; }
        public bool ShopIncludeMaterials { get; set; }
        public bool ShopIncludeJewels { get; set; }
        public bool ShopIncludeSupplyItems { get; set; }
        public bool ShopIncludeHousingItems { get; set; }
        [DefaultValue(56)]
        public uint AmountOfShopItems { get; set; } = 56;

        //-----------------
        //  Misc Settings
        //-----------------
        //Add some set colours it randomly picks and make the trace colours the same as the tracking colour
        public bool RandomScoutflyColour { get; set; }
        public bool CompletelyRandomScoutflyColour { get; set; }
        public bool DifferentTrackScoutflyColour { get; set; }

        //---------------------------
        public bool ShuffleItemCraftingRecipes { get; set; }
        //Extras for later maybe
        public bool RandomizeSmallMonsters { get; set; }
        public bool RandomizeMissions { get; set; }
        public bool ShuffleGatheringSpotLocation { get; set; }
        //Completely random
        public bool RandomItemCraftingRecipes { get; set; }
        public bool RandomStartingItems { get; set; }
        //eg a great jagras could have a rathian traces
        public bool RandomMonsterTrace { get; set; }
        ///RandomizeSmallMonsters
        //Need to figure out how to do this
        public bool RandomMonsterAttacks { get; set; }
        //I think giving palicos monster moves damages the player, could have a option for it, not sure if it damages monsters, could be chaotic
        public bool RandomPalicoAttacks { get; set; }

        public bool RandomMonsterDrops { get; set; }
        public bool RandomQuestRewards { get; set; }
        //Not sure if possible
        public bool RandomStartingWeapons { get; set; }
        ///Change the story monster spawns

    }
}
