using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Troschuetz.Random.Generators;

namespace MHW_Randomizer
{
    public class ShopRandomizer
    {
        public static void Randomize()
        {
            if (!(IoC.Settings.RandomShopItems || IoC.Settings.RandomShopWepArmour))
                return;

            File.Create(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Shop Log.txt").Dispose();

            if (IoC.Settings.RandomShopItems)
            {
                using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Shop Log.txt"))
                {
                    file.WriteLine("---------------------------------------------------------------------------");
                    file.WriteLine("                                 Shop Items                                ");
                    file.WriteLine("---------------------------------------------------------------------------");
                }

                byte[] shopBytes = ChunkOTF.files["shopList.slt"].ChunkState.ExtractItem(ChunkOTF.files["shopList.slt"]);
                List<ShopStructs.ItemShop> shopItemList = new List<ShopStructs.ItemShop>();
                Dictionary<uint, string> itemList = GetItemData();

                int maxItems = (int)IoC.Settings.AmountOfShopItems;
                if (IoC.Settings.AmountOfShopItems > itemList.Count)
                {
                    maxItems = itemList.Count;
                    using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Shop Log.txt"))
                    {
                        file.WriteLine("[Amount of Shop Items Changed to " + maxItems + " as the Amount Entered is More Than the Amount of Items Avaliable]");
                    }
                }

                NR3Generator r = new NR3Generator(IoC.Randomizer.Seed);

                for (int i = 0; i < maxItems; i++)
                {
                    shopItemList.Add(new ShopStructs.ItemShop { Story_Unlock = 0, Sort_Order = 0, Index = (uint)i, Item_Id = 0 });
                    ShopStructs.ItemShop item = shopItemList[i];
                    int index = r.Next(itemList.Count);

                    item.Item_Id = itemList.ElementAt(index).Key;
                    itemList.Remove(itemList.ElementAt(index).Key);
                }
                //Deserialize itemData file for game defined item sorting order
                byte[] itemDataBytes = ChunkOTF.files["itemData.itm"].ChunkState.ExtractItem(ChunkOTF.files["itemData.itm"]);
                List<ItemDataFile> itemDatas = StructTools.RawDeserialize<ItemDataFile>(itemDataBytes, 10);
                //Change Survival Jewel 1 sort order from 0 to 7000 (Just before Defence Jewel 1 (The first jewel)
                itemDatas[2270].Sort_Order = 7000;
                shopItemList = shopItemList.OrderBy(o => itemDatas.FirstOrDefault(p => p.Id == o.Item_Id).Sort_Order).ToList();

                //Reset item list
                itemList = GetItemData();
                for (int i = 0; i < maxItems; i++)
                {
                    ShopStructs.ItemShop item = shopItemList[i];
                    item.Sort_Order = (ushort)i;
                    using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Shop Log.txt"))
                    {
                        string text = "Item " + (item.Sort_Order + 1) + ": " + itemList[item.Item_Id];
                        file.WriteLine(text);
                    }
                }

                byte[] randomizedBytes = StructTools.RawSerialize(shopItemList);
                byte[] header = new byte[10];
                Array.Copy(shopBytes, header, 10);
                //Change the amount of entries in the header
                uint entryCount = BitConverter.ToUInt32(header, 6);
                entryCount = (uint)shopItemList.Count;
                Array.Copy(BitConverter.GetBytes(entryCount), 0, header, 6, 4);
                shopBytes = header.Concat(randomizedBytes).ToArray();

                Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\facility\");
                File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\facility\shopList.slt", shopBytes);
            }

            if (IoC.Settings.RandomShopWepArmour)
            {
                using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Shop Log.txt"))
                {
                    file.WriteLine("---------------------------------------------------------------------------");
                    file.WriteLine("                       Weapon and Armour Shop Items                        ");
                    file.WriteLine("---------------------------------------------------------------------------");
                }

                byte[] shopBytes = ChunkOTF.files["shop.sed"].ChunkState.ExtractItem(ChunkOTF.files["shop.sed"]);
                List<ShopStructs.ArmourShop> shopItemList = StructTools.RawDeserialize<ShopStructs.ArmourShop>(shopBytes, 10);

                NR3Generator itemIndex = new NR3Generator(IoC.Randomizer.Seed);
                int index;
                Dictionary<uint, string> currentGear = new Dictionary<uint, string>();

                byte[] armourBytes = ChunkOTF.files["armor.eq_crt"].ChunkState.ExtractItem(ChunkOTF.files["armor.eq_crt"]);
                List<RecipeStructs.Armour> armourList = StructTools.RawDeserialize<RecipeStructs.Armour>(armourBytes, 10);

                byte[] weaponBytes = ChunkOTF.files["weapon.eq_cus"].ChunkState.ExtractItem(ChunkOTF.files["weapon.eq_cus"]);
                List<RecipeStructs.Weapon> weaponList = StructTools.RawDeserialize<RecipeStructs.Weapon>(weaponBytes, 10);

                //Get all the Equipment Data
                GearData gearDatas = new GearData();
                Dictionary<uint, string> fullArmourList = JsonConvert.DeserializeObject<Dictionary<uint, string>>(Encoding.UTF8.GetString(Properties.Resources.eng_armorData));

                if (!IoC.Settings.RandomShopWepArmourType)
                {
                    for (int i = 0; i < shopItemList.Count; i++)
                    {
                        //Move switch to its own function
                        currentGear = SetCurrentGear(shopItemList[i].Equip_Type, gearDatas);
                        index = itemIndex.Next(currentGear.Count);
                        shopItemList[i].Story_Unlock = SetShopStoryUnlock(shopItemList[i].Equip_Type, currentGear.ElementAt(index).Value, index, armourList, fullArmourList, weaponList);

                        shopItemList[i].Equip_Id = currentGear.ElementAt(index).Key;

                        LogGearShop(shopItemList, currentGear.ElementAt(index).Value, i);
                        currentGear.Remove(currentGear.ElementAt(index).Key);
                    }
                }
                else
                {
                    shopItemList = new List<ShopStructs.ArmourShop>();
                    //Max amount of extras is 4259
                    //Guarantee atleast one of each type
                    for (int i = 0; i < 19; i++)
                    {
                        shopItemList.Add(new ShopStructs.ArmourShop { Equip_Id = 0, Equip_Type = 0, Story_Unlock = 0 });
                        shopItemList[i].Equip_Type = (uint)i;
                        currentGear = SetCurrentGear(shopItemList[i].Equip_Type, gearDatas);
                        index = itemIndex.Next(currentGear.Count);
                        shopItemList[i].Story_Unlock = SetShopStoryUnlock(shopItemList[i].Equip_Type, currentGear.ElementAt(index).Value, index, armourList, fullArmourList, weaponList);

                        shopItemList[i].Equip_Id = currentGear.ElementAt(index).Key;

                        currentGear.Remove(currentGear.ElementAt(index).Key);
                    }

                    NR3Generator wepType = new NR3Generator(IoC.Randomizer.Seed);
                    for (int i = 19; i < IoC.Settings.AmountOfGearShopItems; i++)
                    {
                        shopItemList.Add(new ShopStructs.ArmourShop { Equip_Id = 0, Equip_Type = 0, Story_Unlock = 0 });
                        shopItemList[i].Equip_Type = (uint)wepType.Next(19);
                        //Switch on gear to set current gear value
                        currentGear = SetCurrentGear(shopItemList[i].Equip_Type, gearDatas);
                        while (currentGear.Count == 0)
                        {
                            shopItemList[i].Equip_Type = (uint)wepType.Next(19);
                            currentGear = SetCurrentGear(shopItemList[i].Equip_Type, gearDatas);
                        }
                        index = itemIndex.Next(currentGear.Count);
                        shopItemList[i].Story_Unlock = SetShopStoryUnlock(shopItemList[i].Equip_Type, currentGear.ElementAt(index).Value, index, armourList, fullArmourList, weaponList);

                        shopItemList[i].Equip_Id = currentGear.ElementAt(index).Key;

                        currentGear.Remove(currentGear.ElementAt(index).Key);
                    }

                    shopItemList = shopItemList.OrderBy(o => o.Equip_Type).ToList();
                    gearDatas = new GearData();
                    for (int r = 0; r < shopItemList.Count; r++)
                    {
                        currentGear = SetCurrentGear(shopItemList[r].Equip_Type, gearDatas);
                        LogGearShop(shopItemList, currentGear[shopItemList[r].Equip_Id], r);
                    }
                }

                byte[] randomizedBytes = StructTools.RawSerialize(shopItemList);
                byte[] header = new byte[10];
                Array.Copy(shopBytes, header, 10);
                //Change the amount of entries in the header
                uint entryCount = BitConverter.ToUInt32(header, 6);
                entryCount = (uint)shopItemList.Count;
                Array.Copy(BitConverter.GetBytes(entryCount), 0, header, 6, 4);
                shopBytes = header.Concat(randomizedBytes).ToArray();

                Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\equip\");
                File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\common\equip\shop.sed", shopBytes);
            }
        }

        private static Dictionary<uint, string> GetItemData()
        {
            Dictionary<uint, string> itemList = JsonConvert.DeserializeObject<Dictionary<uint, string>>(Encoding.UTF8.GetString(Properties.Resources.ItemsForShopData));

            //Add relevent extra shop items
            if (IoC.Settings.ShopIncludeMaterials)
                itemList = itemList.Concat(JsonConvert.DeserializeObject<Dictionary<uint, string>>(Encoding.UTF8.GetString(Properties.Resources.MaterialsForShopData))).ToDictionary(x => x.Key, x => x.Value);
            if (IoC.Settings.ShopIncludeJewels)
                itemList = itemList.Concat(JsonConvert.DeserializeObject<Dictionary<uint, string>>(Encoding.UTF8.GetString(Properties.Resources.JewelsForShop))).ToDictionary(x => x.Key, x => x.Value);
            if (IoC.Settings.ShopIncludeSupplyItems)
                itemList = itemList.Concat(JsonConvert.DeserializeObject<Dictionary<uint, string>>(Encoding.UTF8.GetString(Properties.Resources.SupplyItems))).ToDictionary(x => x.Key, x => x.Value);
            if (IoC.Settings.ShopIncludeHousingItems)
                itemList = itemList.Concat(JsonConvert.DeserializeObject<Dictionary<uint, string>>(Encoding.UTF8.GetString(Properties.Resources.Furnature))).ToDictionary(x => x.Key, x => x.Value);

            return itemList;
        }

        private static Dictionary<uint, string> SetCurrentGear(uint equipType, GearData gearDatas)
        {
            Dictionary<uint, string> currentGear = null;
            switch (equipType)
            {
                case 0:
                    {
                        currentGear = gearDatas.GreatSwords;
                        break;
                    }
                case 1:
                    {
                        currentGear = gearDatas.SwordandShields;
                        break;
                    }
                case 2:
                    {
                        currentGear = gearDatas.DualBlades;
                        break;
                    }
                case 3:
                    {
                        currentGear = gearDatas.Longswords;
                        break;
                    }
                case 4:
                    {
                        currentGear = gearDatas.Hammers;
                        break;
                    }
                case 5:
                    {
                        currentGear = gearDatas.HuntingHorns;
                        break;
                    }
                case 6:
                    {
                        currentGear = gearDatas.Lances;
                        break;
                    }
                case 7:
                    {
                        currentGear = gearDatas.Gunlances;
                        break;
                    }
                case 8:
                    {
                        currentGear = gearDatas.SwitchAxes;
                        break;
                    }
                case 9:
                    {
                        currentGear = gearDatas.ChargeBlades;
                        break;
                    }
                case 10:
                    {
                        currentGear = gearDatas.InsectGlaives;
                        break;
                    }
                case 11:
                    {
                        currentGear = gearDatas.Bows;
                        break;
                    }
                case 12:
                    {
                        currentGear = gearDatas.HeavyBowguns;
                        break;
                    }
                case 13:
                    {
                        currentGear = gearDatas.LightBowguns;
                        break;
                    }
                case 14:
                    {
                        currentGear = gearDatas.Heads;
                        break;
                    }
                case 15:
                    {
                        currentGear = gearDatas.Chest;
                        break;
                    }
                case 16:
                    {
                        currentGear = gearDatas.Arms;
                        break;
                    }
                case 17:
                    {
                        currentGear = gearDatas.Waist;
                        break;
                    }
                case 18:
                    {
                        currentGear = gearDatas.Legs;
                        break;
                    }
            }
            return currentGear;
        }

        private static void LogGearShop(List<ShopStructs.ArmourShop> shopItemList, string currentGear, int i)
        {
            using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Shop Log.txt"))
            {
                string text = "";
                switch (shopItemList[i].Equip_Type)
                {
                    case 0:
                        {
                            text = "GreatSword \t\t";
                            break;
                        }
                    case 1:
                        {
                            text = "Sword and Shield \t";
                            break;
                        }
                    case 2:
                        {
                            text = "Dual Blades \t";
                            break;
                        }
                    case 3:
                        {
                            text = "Longsword \t\t";
                            break;
                        }
                    case 4:
                        {
                            text = "Hammer \t\t";
                            break;
                        }
                    case 5:
                        {
                            text = "Hunting Horn \t";
                            break;
                        }
                    case 6:
                        {
                            text = "Lance \t\t";
                            break;
                        }
                    case 7:
                        {
                            text = "Gunlance \t\t";
                            break;
                        }
                    case 8:
                        {
                            text = "Switch Axe \t\t";
                            break;
                        }
                    case 9:
                        {
                            text = "Charge Blade \t";
                            break;
                        }
                    case 10:
                        {
                            text = "Insect Glaive \t";
                            break;
                        }
                    case 11:
                        {
                            text = "Bow \t\t";
                            break;
                        }
                    case 12:
                        {
                            text = "Heavy Bowgun \t";
                            break;
                        }
                    case 13:
                        {
                            text = "Light Bowgun \t";
                            break;
                        }
                    case 14:
                        {
                            text = "Head Gear \t\t";
                            break;
                        }
                    case 15:
                        {
                            text = "Chest Gear \t\t";
                            break;
                        }
                    case 16:
                        {
                            text = "Arm Gear \t\t";
                            break;
                        }
                    case 17:
                        {
                            text = "Waist Gear \t\t";
                            break;
                        }
                    case 18:
                        {
                            text = "Leg Gear \t\t";
                            break;
                        }
                }
                text = "Equip Type: " + text + "Item: " + currentGear;
                file.WriteLine(text);
            }
        }

        private static uint SetShopStoryUnlock(uint equipType, string EquipName, int index, List<RecipeStructs.Armour> armourList, Dictionary<uint, string> fullArmourList, List<RecipeStructs.Weapon> weaponList)
        {
            //Set story unlock
            //Armour
            if (equipType > 13)
            {
                //Find armour item (indexes restart for each armour type so need to check the name too)
                RecipeStructs.Armour armour = armourList.FirstOrDefault(o => o.Equipment_Index_Raw == index && !string.IsNullOrEmpty(fullArmourList.Values.FirstOrDefault(p => p == EquipName)));

                if (armour == null)
                    return 50000;
                else
                    switch (armour.Item_Rank)
                    {
                        case 0:
                            {
                                return 0;
                            }
                        case 1:
                            {
                                return 610000;
                            }
                        case 2:
                            {
                                return 1100300;
                            }
                    }
            }
            //Weapons
            else
            {
                RecipeStructs.Weapon weaponData = weaponList.FirstOrDefault(o => o.Equipment_Index_Raw == index && o.Equipment_Category_Raw == equipType);
                if (weaponData == null)
                    return 50000;
                else
                {
                    switch (weaponData.Item_Rank)
                    {
                        case 0:
                            {
                                return 0;
                            }
                        case 1:
                            {
                                return 610000;
                            }
                        case 2:
                            {
                                return 1100300;
                            }
                    }
                }
            }
            return 0;
        }
    }

    class GearData
    {
        public Dictionary<uint, string> GreatSwords = JsonConvert.DeserializeObject<Dictionary<uint, string>>(Encoding.UTF8.GetString(Properties.Resources.ShopGreatsword));
        public Dictionary<uint, string> SwordandShields = JsonConvert.DeserializeObject<Dictionary<uint, string>>(Encoding.UTF8.GetString(Properties.Resources.ShopSword_and_Shield));
        public Dictionary<uint, string> DualBlades = JsonConvert.DeserializeObject<Dictionary<uint, string>>(Encoding.UTF8.GetString(Properties.Resources.ShopDual_Blades));
        public Dictionary<uint, string> Longswords = JsonConvert.DeserializeObject<Dictionary<uint, string>>(Encoding.UTF8.GetString(Properties.Resources.ShopLongsword));
        public Dictionary<uint, string> Hammers = JsonConvert.DeserializeObject<Dictionary<uint, string>>(Encoding.UTF8.GetString(Properties.Resources.ShopHammer));
        public Dictionary<uint, string> HuntingHorns = JsonConvert.DeserializeObject<Dictionary<uint, string>>(Encoding.UTF8.GetString(Properties.Resources.ShopHunting_Horn));
        public Dictionary<uint, string> Lances = JsonConvert.DeserializeObject<Dictionary<uint, string>>(Encoding.UTF8.GetString(Properties.Resources.ShopLance));
        public Dictionary<uint, string> Gunlances = JsonConvert.DeserializeObject<Dictionary<uint, string>>(Encoding.UTF8.GetString(Properties.Resources.ShopGunlance));
        public Dictionary<uint, string> SwitchAxes = JsonConvert.DeserializeObject<Dictionary<uint, string>>(Encoding.UTF8.GetString(Properties.Resources.ShopSwitch_Axe));
        public Dictionary<uint, string> ChargeBlades = JsonConvert.DeserializeObject<Dictionary<uint, string>>(Encoding.UTF8.GetString(Properties.Resources.ShopCharge_Blade));
        public Dictionary<uint, string> InsectGlaives = JsonConvert.DeserializeObject<Dictionary<uint, string>>(Encoding.UTF8.GetString(Properties.Resources.ShopInsect_Glaive));
        public Dictionary<uint, string> Bows = JsonConvert.DeserializeObject<Dictionary<uint, string>>(Encoding.UTF8.GetString(Properties.Resources.ShopBow));
        public Dictionary<uint, string> HeavyBowguns = JsonConvert.DeserializeObject<Dictionary<uint, string>>(Encoding.UTF8.GetString(Properties.Resources.ShopHeavy_Bowgun));
        public Dictionary<uint, string> LightBowguns = JsonConvert.DeserializeObject<Dictionary<uint, string>>(Encoding.UTF8.GetString(Properties.Resources.ShopLight_Bowgun));
        public Dictionary<uint, string> Heads = JsonConvert.DeserializeObject<Dictionary<uint, string>>(Encoding.UTF8.GetString(Properties.Resources.Head)).Where(o => o.Value != "Unavailable" && o.Value != "HARDUMMY" && !o.Value.Contains("Layered") &&
                                                                                                                                             !(o.Value == "Leather Headgear" && o.Key > 100)).ToDictionary(x => x.Key, x => x.Value);
        public Dictionary<uint, string> Chest = JsonConvert.DeserializeObject<Dictionary<uint, string>>(Encoding.UTF8.GetString(Properties.Resources.Chest)).Where(o => o.Value != "Unavailable" && o.Value != "HARDUMMY" && !o.Value.Contains("Layered") && o.Value != "Leather Headgear").ToDictionary(x => x.Key, x => x.Value);
        public Dictionary<uint, string> Arms = JsonConvert.DeserializeObject<Dictionary<uint, string>>(Encoding.UTF8.GetString(Properties.Resources.Arms)).Where(o => o.Value != "Unavailable" && o.Value != "HARDUMMY" && !o.Value.Contains("Layered") && o.Value != "Leather Headgear").ToDictionary(x => x.Key, x => x.Value);
        public Dictionary<uint, string> Waist = JsonConvert.DeserializeObject<Dictionary<uint, string>>(Encoding.UTF8.GetString(Properties.Resources.Waist)).Where(o => o.Value != "Unavailable" && o.Value != "HARDUMMY" && !o.Value.Contains("Layered") && o.Value != "Leather Headgear").ToDictionary(x => x.Key, x => x.Value);
        public Dictionary<uint, string> Legs = JsonConvert.DeserializeObject<Dictionary<uint, string>>(Encoding.UTF8.GetString(Properties.Resources.Legs)).Where(o => o.Value != "Unavailable" && o.Value != "HARDUMMY" && !o.Value.Contains("Layered") && o.Value != "Leather Headgear").ToDictionary(x => x.Key, x => x.Value);
    }
}