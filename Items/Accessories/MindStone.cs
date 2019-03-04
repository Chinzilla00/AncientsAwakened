using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace AAMod.Items.Accessories
{
    [AutoloadEquip(EquipType.Face)]
    public class MindStone : ModItem
    {
        

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mind Stone");
            Tooltip.SetDefault(
@"Gives you infinite mana
'It's simple Calculus'");
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 24;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.rare = 11;
            item.accessory = true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Color.Yellow;
                }
            }
        }

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 1000;
            player.manaCost *= 0.0f;
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == mod.ItemType<PowerStone>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == mod.ItemType<InfinityGauntlet>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == mod.ItemType<SoulStone>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == mod.ItemType<RealityStone>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == mod.ItemType<TimeStone>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == mod.ItemType<SpaceStone>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}