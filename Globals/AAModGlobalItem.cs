using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.Items.Boss.Akuma;
using AAMod.Items.Boss.Retriever;
using AAMod.Items.Boss.Grips;
using AAMod.Items.Dev;

namespace AAMod
{
    public class AAModGlobalItem : GlobalItem
	{
        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.SoulofNight)
            {
                if (WorldGen.crimson)
                {
                    item.color = Color.Firebrick;
                }
                else
                {
                    item.color = Color.Violet;
                }
            }
            if (item.type == ItemID.LunarOre)
            {
                item.createTile = mod.TileType("LuminiteOre");
            }
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.type == ItemID.AnkhShield)
            {
                player.meleeSpeed += 0.07f;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ItemID.AnkhShield)
            {
                int index = 1;
                for (int m = 0; m < tooltips.Count; m++)
                {
                    TooltipLine line = tooltips[m];
                    if (line.mod == "Terraria" && line.text == "Grants immunity to most debuffs")
                    {
                        index = m;
                        break;
                    }
                }
                tooltips.Insert(index + 1, new TooltipLine(mod, "AnkhShield", "7% melee speed"));
            }
        }

        public override bool CanEquipAccessory(Item item, Player player, int slot)
        {
            if (item.type == ItemID.AnkhShield || item.type == ItemID.ObsidianShield|| item.type == mod.ItemType<TaiyangBaolei>())
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
            if (item.type == ItemID.EoCShield || item.type == mod.ItemType<StormRiot>() || item.type == mod.ItemType<BulwarkOfChaos>())
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
                        if (slot != i && player.armor[i].type == mod.ItemType<StormRiot>())
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
    }
}
