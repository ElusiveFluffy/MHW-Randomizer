using System.ComponentModel;

namespace MHW_Randomizer
{
    public class RandomizerSettings : BaseViewModel
    {
        public string? ChunkFolderPath;
        public string? SaveFolderPath;
        public string? UserSeed { get; set; }
        //To do
        //Fix traces to match the monster

        //--------------------
        //  Quest Settings
        //--------------------

        //General Quest Settings
        public bool RandomizeQuests { get; set; }
        public bool DuplicateMonster { get; set; }
        public bool HighRankMonInLowRank { get; set; }
        public bool IncludeLeshen { get; set; }
        public bool IncludeXenojiiva { get; set; } //Fix Xenojiiva (maybe can't include, too big)
        public bool IncludeBehemoth { get; set; }
        public bool TwoMonsterQuests { get; set; }
        public bool RandomSobj { get; set; }
        public bool RandomMaps { get; set; }
        public bool IncludeArenaMap { get; set; } //Maybe including the shara stage //For if random maps is checked
        public bool IncludeXenoArena { get; set; }
        public bool IncludeIBArenaMaps { get; set; }
        public bool AllMonstersInArena { get; set; }
        public bool RandomIcons { get; set; }
        public bool RandomizeMusic { get; set; }
        public bool RandomizeMultiObj { get; set; }
        public bool RandomizeMultiMon { get; set; }
        public bool RandomizeDuplicate { get; set; }
        public bool DontRandomizeSlay { get; set; }
        public bool DontRandomizeCapture { get; set; }
        //Small Monsters

        //Supply Box
        public bool RandomSupplyBox { get; set; } //Random supply box rem ID
        public bool RandomSupplyBoxItems { get; set; } //Random supply box items
        public uint ExtraSupplyBoxes { get; set; } //Extra supply box files

        //Monsters
        public bool IncludeSmallMonsterDebuffs { get; set; }
        public bool RandomMonsterElement { get; set; } //Adds extra damage and also gives the element blights, eg frostblight, and burns
        public bool OnlyChangeExistingElement { get; set; }
        public bool IncreaseElementPower { get; set; }
        public bool EachAttackDifferentElement { get; set; }
        public bool RandomMonsterAttackStatus { get; set; } //eg like bleed, paralysis
        public bool OnlyChangeExistingStatus { get; set; }
        public bool EachAttackDifferentStatus { get; set; }
        public bool MultipleStatusesPerAttack { get; set; }

        //Iceborne Specific Quest Settings
        public bool RandomizeIBQuests { get; set; }
        [DefaultValue(true)]
        public bool MonstersFoundInIB { get; set; } = true;
        public bool IceborneOnlyMonsters { get; set; }
        public bool IncludeHighRankOnly { get; set; }
        public bool IncludeShara { get; set; }
        public bool IncludeFuriousRajang { get; set; }
        public bool IncludeAlatreon { get; set; }
        public bool IncludeFatalis { get; set; } //Fix fatalis

        [DefaultValue(100)]
        public int MonsterMinSize { get; set; } = 100;
        [DefaultValue(100)]
        public int MonsterMaxSize { get; set; } = 100;

        //-------------------------
        //  Expedition Settings
        //-------------------------

        public bool RandomizeExpeditions { get; set; }
        public bool RandomizeIceborneExpeditions { get; set; }
        public bool ExpeditionRandomSobj { get; set; }
        public bool ExpeditionRandomIBSobj { get; set; }
        public bool ExpeditionHighRankInLow { get; set; }
        public bool ExpeditionHighRankOnlyInHigh { get; set; }
        public bool ExpeditionMasterRankOnlyInMaster { get; set; }
        public bool ExpeditionIncludeNonUsualMonsters { get; set; }
        public bool ExpeditionIncludeIBNonUsualMonsters { get; set; }
        public bool ExpeditionIncludeLeshen { get; set; }
        public bool ExpeditionIncludeBehemoth { get; set; }
        public bool ExpeditionIncludeFuriousRajang { get; set; }
        public bool ExpeditionIncludeAlatreon { get; set; }
        public bool ExpeditionIncludeShara { get; set; }

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
        public bool RandomBowgunElement { get; set; }

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

        //Tweaks
        public bool OnePlayerQuests { get; set; }
        public bool MakePandorasArena30Minutes { get; set; }
        public bool FasterKinsects { get; set; }

        //Icons
        public bool UnknownMonsterIcons { get; set; }

        //Experimental
        public bool RandomizeZorahStoryQuests { get; set; }
        public bool IBMonstersInLowRank { get; set; }
        public bool IBMonstersInHighRank { get; set; }
        public bool ExpeditionIBMonstersInLowRank { get; set; }
        public bool ExpeditionIBMonstersInHighRank { get; set; }
        public bool IBMapsInBaseGame { get; set; }

        //---------------------------

        //Extras for later maybe

        //Quests
        public bool RandomGearEachQuest { get; set; } //Using the pre-selected gear in the quest settings (Need to figure out how this works)
        public bool RandomSupplyBoxItemAmount { get; set; } //Random amount for each supply box item
        public bool RandomQuestRewards { get; set; }

        public bool RandomizeQuestObjective { get; set; } //Like could change it to a hunt/capture/slay quest

        public bool RandomizeMissions { get; set; }

        //Monsters
        public bool RandomizeSmallMonsters { get; set; }
        public bool RandomMonsterProjectiles { get; set; }
        public bool RandomMonsterTrace { get; set; } //eg a great jagras could have a rathian traces
        public bool RandomMonsterDrops { get; set; }
        //Random monster slinger ammo drops

        //Completely random (maybe with some logic or only if high rank monsters in low rank so low rank stuff only have low rank material, high rank with high rank materials, master rank with master rank materials)
        public bool RandomArmourRecipes { get; set; }
        public bool RandomWeaponRecipes { get; set; }
        public bool RandomKinsectRecipes { get; set; }

        public bool RandomKinsectStats { get; set; }

        public bool RandomItemCraftingRecipes { get; set; }
        public bool ShuffleItemCraftingRecipes { get; set; }

        //Random slinger ammo? Random slinger ammo pickups
        public bool ShuffleGatheringSpots { get; set; }

        public bool RandomStartingPouchItems { get; set; } //Completely random

        //Need to figure out how to do this
        //I think giving palicos monster moves damages the player, could have a option for it, not sure if it damages monsters, could be chaotic
        public bool RandomPalicoAttacks { get; set; }

        //Mantles that get given are shuffled around

    }
}
