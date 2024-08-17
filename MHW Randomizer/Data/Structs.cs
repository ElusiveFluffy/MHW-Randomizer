using System;
using System.Runtime.InteropServices;

namespace MHW_Randomizer
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class ExpeditionRank
    {
        public uint st101Chance;
        public uint st102Chance;
        public uint st103Chance;
        public uint st104Chance;
        public uint st105Chance;
        public uint st108Chance;
        public uint st000Chance;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
        public uint[]? Transitions;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class ExpeditionSpawn
    {
        public uint MonsterID;
        public ExpeditionRank LowRank = new ExpeditionRank();
        public ExpeditionRank HighRank = new ExpeditionRank();
        public ExpeditionRank MasterRank = new ExpeditionRank();
    }

    public static class RecipeStructs
    {

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Armour
        {
            public byte Equipment_Category_Raw;
            public UInt16 Equipment_Index_Raw;
            public UInt16 Needed_Item_Id_to_Unlock;
            public int Monster_Unlock;
            public uint Story_Unlock;
            public uint Unknown_uint32_;
            public uint Item_Rank;
            public UInt16 Mat_1_Id;
            public byte Mat_1_Count;
            public UInt16 Mat_2_Id;
            public byte Mat_2_Count;
            public UInt16 Mat_3_Id;
            public byte Mat_3_Count;
            public UInt16 Mat_4_Id;
            public byte Mat_4_Count;
            public byte Unknown_uint8_1;
            public byte Unknown_uint8_2;
            public byte Unknown_uint8_3;
            public byte Unknown_uint8_4;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class ArmourDat
        {
            public uint Index;
            public ushort Order;
            public byte Variant;
            public ushort Set_Layered_Id;
            public byte Type;
            public byte Equip_Slot;
            public ushort Defense;
            public ushort Model_Id_1;
            public ushort Model_Id_2;
            public ushort Icon_Color;
            public byte Icon_Effect;
            public byte Rarity;
            public uint Cost;
            public byte Fire_Res;
            public byte Water_Res;
            public byte Ice_Res;
            public byte Thunder_Res;
            public byte Dragon_Res;
            public byte Slot_Count;
            public byte Slot_1_Size;
            public byte Slot_2_Size;
            public byte Slot_3_Size;
            public ushort Set_Skill_1;
            public byte Set_Skill_1_Level;
            public ushort Hidden_Skill;
            public byte Hidden_Skill_Level;
            public ushort Skill_1;
            public byte Skill_1_Level;
            public ushort Skill_2;
            public byte Skill_2_Level;
            public ushort Skill_3;
            public byte Skill_3_Level;
            public uint Gender;
            public ushort Set_Group;
            public ushort GMD_Name_Index;
            public ushort GMD_Description_Index;
            public byte Is_Permanent;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Weapon
        {
            public byte Equipment_Category_Raw;
            public ushort Equipment_Index_Raw;
            public ushort Needed_Item_Id_to_Unlock;
            public int Monster_Unlock;
            public uint Story_Unlock;
            public uint Item_Rank;
            public ushort Mat_1_Id;
            public byte Mat_1_Count;
            public ushort Mat_2_Id;
            public byte Mat_2_Count;
            public ushort Mat_3_Id;
            public byte Mat_3_Count;
            public ushort Mat_4_Id;
            public byte Mat_4_Count;
            public ushort Child_index_1;
            public ushort Child_index_2;
            public ushort Child_index_3;
            public ushort Child_index_4;
            public byte Unk_1;
            public byte Unk_2;
            public byte Unk_3;
            public byte Unk_4;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class WeaponTree
        {
            public uint Index;
            public short Unk1;
            public short Base_Model_Id;
            public short Part_1_Id;
            public short Part_2_Id;
            public byte Unk_1;
            public byte Color;
            public byte Tree_Id;
            public byte Is_Fixed_Upgrade;
            public uint Cost;
            public byte Rarity;
            public byte Sharpness_kire_Id;
            public byte Sharpness_Amount;
            public ushort Damage;
            public ushort Defense;
            public byte Affinity;
            public byte Element;
            /// <summary>
            /// Value * 10 = final shown value
            /// </summary>
            public ushort Element_Damage;
            public byte Element_Hidden;
            /// <summary>
            /// Value * 10 = final shown value
            /// </summary>
            public ushort Element_Hidden_Damage;
            public byte Elderseal;
            public byte Slot_Count;
            public byte Slot_1_Size;
            public byte Slot_2_Size;
            public byte Slot_3_Size;
            public ushort Special_Ability_1_ID;
            public ushort Special_Ability_2_ID;
            public uint Unk_2;
            public uint Unk_3;
            public uint Unk_4;
            public byte Tree_Position;
            public ushort Id;
            public ushort GMD_Name_Index;
            public ushort GMD_Description_Index;
            public ushort Skill;
            public ushort Unk_5;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class RangedWeaponTree
        {
            public uint Index;
            public short Unk_1;
            public short Base_Model_Id;
            public short Part_1_Id;
            public short Part_2_Id;
            public byte Unk_2;
            public byte Color;
            public byte Tree_Id;
            public byte Is_Fixed_Upgrade;
            public byte Muzzle_Type;
            public byte Barrel_Type;
            public byte Magazine_Type;
            public byte Scope_Type;
            public uint Cost;
            public byte Rarity;
            public ushort Damage;
            public ushort Defense;
            public byte Affinity;
            public byte Element;
            /// <summary>
            /// Value * 10 = final shown value
            /// </summary>
            public ushort Element_Damage;
            public byte Element_Hidden;
            /// <summary>
            /// Value * 10 = final shown value
            /// </summary>
            public ushort Element_Hidden_Damage;
            public byte Elderseal;
            public byte Shell_Type_Id;
            public byte Unk_3;
            public byte Deviation;
            public byte Slot_Count;
            public byte Slot_1_Size;
            public byte Slot_2_Size;
            public byte Slot_3_Size;
            public uint Unk_4;
            public uint Unk_5;
            public uint Unk_7;
            public byte Unk_8;
            public byte Special_Ammo_Type;
            public byte Tree_Position;
            public ushort Id;
            public ushort GMD_Name_Index;
            public ushort GMD_Description_Index;
            public ushort Skill;
            public ushort Unk_6;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class KinsectTree
        {
            public uint Index;
            public byte Attack_Type;
            public byte Id;
            public byte Tree_Position_Id;
            public ushort Base_Model_Id;
            public byte Tree_Id;
            public uint Craft_Cost;
            public byte Rarity;
            public ushort Power;
            public ushort Speed;
            public ushort Heal;
            public ushort Element;
            public ushort Dust_Effect;
            public byte Tree_Position;
            public ushort Equip_Id;
        }

    }

    public static class ShopStructs
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class ArmourShop
        {
            public uint Equip_Type;
            public uint Equip_Id;
            public uint Story_Unlock;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class ItemShop
        {
            public uint Index;
            public uint Item_Id;
            public uint Story_Unlock;
            public ushort Sort_Order;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class PalicoWeapon
    {
        public uint Index;
        public ushort Set_Id;
        public byte Element;
        public ushort Attack_Type;
        public ushort Melee_Damage;
        public ushort Ranged_Damage;
        public ushort Elemental_Damage;
        public short Affinity;
        public ushort Defense;
        public byte Elderseal;
        public byte Rarity;
        public ushort Order;
        public uint Model_Id;
        public uint Cost;
        public byte Unknown_byte_1;
        public ushort Id;
        public ushort GMD_Name_Index;
        public ushort GMD_Description_Index;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class ItemDataFile
    {
        public uint Id;
        public byte Sub_Type;
        public uint Type;
        public byte Rarity;
        public byte Carry_Limit;
        public byte Unknown;
        public ushort Sort_Order;
        public uint Flags_Raw;
        public uint Icon_Id;
        public ushort Icon_Color_Id;
        public uint Sell_Price;
        public uint Buy_Price;
    }

    public class MonsterAtkStructs
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Atk2
        {
            public uint Index;
            public uint Knock_back_Type;
            public uint Knock_back_Lvl;
            public uint Unk_1;
            public float Motion_Value;
            public uint Unk_2;
            public uint Unk_3;
            public float Guard_Stamina_Cost;
            public uint Guard_Req;
            public uint Element_Id;
            public uint Unk_4;
            public float Element_Dmg;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public float[]? Statuses;
            public byte Ele_Res_Down_Double;
            public byte Ele_Res_Down;
            public byte Def_Down_Double;
            public byte Def_Down;
            public uint Unk_6;
            public uint Unk_7;
            public float Unk_8;
            public float Unk_9;
            public float Unk_10;
            public float Unk_11;
            public float Unk_12;
            public uint Unk_13;
            public uint Unk_14;
            public uint Unk_15;
            public byte Unk_16;
            public float Unk_17;
            public uint Unk_18;
            public uint Unk_19;
            public uint Unk_20;
            public uint Unk_21;
            public uint Unk_22;
            public uint Unk_23;
            public uint Unk_24;
            public uint Unk_25;
            public uint Unk_26;
            public ushort Unk_27;
        }
    }

    /// <summary>
    /// For .rem files (header is 18 bytes)
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class QuestRewards
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public uint[]? Items;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[]? ItemCounts;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[]? ItemWeights;
    }

    /// <summary>
    /// For .itlot files (header is 10 bytes)
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class MonsterDrops
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public ushort[]? Items;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public byte[]? ItemCounts;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public byte[]? ItemWeights;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public CarveAnimations[]? CarveAnimation;
    }

    public enum CarveAnimations
    {
        Regular = 0,
        Plate = 1,
        Gem = 2
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class SuppItems
    {
        public ushort Item_Id;
        public ushort Item_Count;
    }
}
