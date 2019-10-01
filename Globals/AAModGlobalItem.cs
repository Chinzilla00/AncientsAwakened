using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.Items.Boss.Akuma;
using AAMod.Items.Boss.Grips;
using AAMod.Items;

namespace AAMod
{
    public class AAModGlobalItem : GlobalItem
	{
        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.SoulofNight)
            {
                item.color = WorldGen.crimson ? Color.Firebrick : Color.Violet;
            }

            if (item.type == ItemID.LunarOre)
            {
                item.createTile = mod.TileType("LuminiteOre");
            }

            if (item.modItem != null && item.modItem.mod.Name == mod.Name && (item.damage > 0 || item.accessory || item.defense > 0) && item.maxStack < 2)
            {
                BaseAAItem AAitem = (BaseAAItem)item.modItem;

                if (AAitem.AARarity != 0)
                {
                    switch (AAitem.AARarity)
                    {
                        case 12:
                            item.value = Item.sellPrice(0, 30, 0, 0);
                            break;

                        case 13:
                            item.value = Item.sellPrice(0, 35, 0, 0);
                            break;

                        case 14:
                            item.value = Item.sellPrice(0, 40, 0, 0);
                            break;

                        case 15:
                            item.value = Item.sellPrice(0, 45, 0, 0);
                            break;
                    }
                }
                else
                {
                    switch (item.rare)
                    {
                        case 0:
                            item.value = Item.sellPrice(0, 0, 25, 0);
                            break;

                        case 1:
                            item.value = Item.sellPrice(0, 0, 50, 0);
                            break;

                        case 2:
                            item.value = Item.sellPrice(0, 0, 75, 0);
                            break;

                        case 3:
                            item.value = Item.sellPrice(0, 1, 0, 0);
                            break;

                        case 4:
                            item.value = Item.sellPrice(0, 2, 0, 0);
                            break;

                        case 5:
                            item.value = Item.sellPrice(0, 4, 0, 0);
                            break;

                        case 6:
                            item.value = Item.sellPrice(0, 6, 0, 0);
                            break;

                        case 7:
                            item.value = Item.sellPrice(0, 8, 0, 0);
                            break;

                        case 8:
                            item.value = Item.sellPrice(0, 10, 0, 0);
                            break;

                        case 9:
                            item.value = Item.sellPrice(0, 15, 0, 0);
                            break;

                        case 10:
                            item.value = Item.sellPrice(0, 20, 0, 0);
                            break;

                        case 11:
                            item.value = Item.sellPrice(0, 25, 0, 0);
                            break;
                    }
                }
            }
        }

        public override void GrabRange(Item item, Player player, ref int grabRange)
        {
            if (player.HeldItem.type == mod.ItemType<Items.Usable.CodeMagnetWeak>())
            {
                grabRange += 250;
            }

            if (player.HeldItem.type == mod.ItemType<Items.Usable.CodeMagnet>())
            {
                grabRange += 1920;
            }
        }

        public override bool CanEquipAccessory(Item item, Player player, int slot)
        {
            if (item.type == ItemID.AnkhShield || item.type == ItemID.ObsidianShield || item.type == mod.ItemType<TaiyangBaolei>())
            {
                if (slot < 10)
                {
                    int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                    for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                    {
                        // We need "slot != i" because we don't care what is currently in the slot we will be replacing.
                        if (slot != i && player.armor[i].type == ItemID.AnkhShield)
                        {
                            return false;
                        }

                        if (slot != i && player.armor[i].type == ItemID.ObsidianShield)
                        {
                            return false;
                        }

                        if (slot != i && player.armor[i].type == mod.ItemType<TaiyangBaolei>())
                        {
                            return false;
                        }
                    }
                }
            }

            if (item.type == ItemID.EoCShield ||  item.type == mod.ItemType<BulwarkOfChaos>())
            {
                if (slot < 10)
                {
                    int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                    for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                    {
                        // We need "slot != i" because we don't care what is currently in the slot we will be replacing.
                        if (slot != i && player.armor[i].type == ItemID.EoCShield)
                        {
                            return false;
                        }

                        if (slot != i && player.armor[i].type == mod.ItemType<BulwarkOfChaos>())
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public override void OpenVanillaBag(string context, Player player, int arg)
        {

        }

        public override bool OnPickup(Item item, Player player)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            if (item.ammo == AmmoID.Coin)
            {
                if (modPlayer.GreedCharm)
                {
                    player.AddBuff(mod.BuffType<Items.Boss.Greed.CharmBuff>(), 240);
                    if (modPlayer.GreedyDamage < 20)
                    {
                        modPlayer.GreedyDamage += 1;
                    }
                }
                else if (modPlayer.GreedTalisman)
                {
                    player.AddBuff(mod.BuffType<Items.Boss.Greed.TalismanBuff>(), 240);
                    if (modPlayer.GreedyDamage < 40)
                    {
                        modPlayer.GreedyDamage += 1;
                    }
                }
            }
            return base.OnPickup(item, player);
        }

        public static void OpenAACrate(Player player, int CrateType)
        {
            Mod mod = AAMod.instance;

            bool noRareItem = true;
            while (noRareItem)
            {
                if (Main.rand.Next(4) == 0)
                {
                    player.QuickSpawnItem(ItemID.GoldCoin, Main.rand.Next(5, 13));
                    noRareItem = false;
                }

                if (Main.rand.Next(4) == 0)
                {
                    int item = 0;
                    int amount = 0;

                    if (!Main.hardMode || Main.rand.Next(3) == 0)
                    {
                        int[] items = new int[]
                        {
                            ItemID.IronBar, ItemID.SilverBar, ItemID.GoldBar,
                            ItemID.LeadBar, ItemID.TungstenBar, ItemID.PlatinumBar
                        };
                        item = Main.rand.Next(items);
                        amount = Main.rand.Next(10, 21);
                    }
                    else if (Main.hardMode)
                    {
                        int[] items = new int[]
                        {
                            ItemID.CobaltBar,
                            ItemID.PalladiumBar,
                            ItemID.MythrilBar,
                            ItemID.OrichalcumBar,
                            ItemID.AdamantiteBar,
                            ItemID.TitaniumBar,
                        };
                        item = Main.rand.Next(items);
                        amount = Main.rand.Next(7, 18);
                    }

                    player.QuickSpawnItem(item, amount);
                    noRareItem = false;
                }
            }

            if (Main.rand.Next(4) == 0)
            {
                int[] items = new int[]
                {
                    ItemID.ObsidianSkinPotion, ItemID.SpelunkerPotion,
                    ItemID.HunterPotion, ItemID.GravitationPotion,
                    ItemID.MiningPotion, ItemID.HeartreachPotion
                };
                player.QuickSpawnItem(Main.rand.Next(items), Main.rand.Next(2, 5));
            }

            if (Main.rand.Next(2) == 0)
            {
                player.QuickSpawnItem(Main.rand.Next(188, 190), Main.rand.Next(5, 18));
            }

            if (Main.rand.Next(2) == 0)
            {
                int item = Main.rand.Next(2) == 0 ? ItemID.MasterBait : ItemID.JourneymanBait;
                player.QuickSpawnItem(item, Main.rand.Next(2, 7));
            }

            if (CrateType < 2)
            {
                if (Main.hardMode && Main.rand.Next(2) == 0)
                {
                    int item = CrateType == 1 ? mod.ItemType<Items.Materials.SoulOfSpite>() : mod.ItemType<Items.Materials.SoulOfSmite>();
                    player.QuickSpawnItem(item, Main.rand.Next(2, 6));
                }

                if (Main.hardMode && Main.rand.Next(2) == 0)
                {
                    int item = CrateType == 1 ? mod.ItemType<Items.Materials.HydraToxin>() : mod.ItemType<Items.Materials.DragonFire>();
                    player.QuickSpawnItem(item, Main.rand.Next(2, 6));
                }
            }
        }
    }

    public class InvokerCaligulaItem : GlobalItem
    {
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        public override bool CloneNewInstances
        {
            get
            {
                return true;
            }
        }
        public override bool CanUseItem(Item item, Player player)
        {
            if (player.GetModPlayer<InvokerPlayer>(mod).InvokedCaligula && player.inventory[player.selectedItem].damage > 0 && !(player.GetModPlayer<InvokerPlayer>(mod).DarkCaligula && player.inventory[player.selectedItem].type == mod.ItemType("InvokerStaff") && player.altFunctionUse == 2))
            {
                return false;
            }
            return true;
        }
    }
}
