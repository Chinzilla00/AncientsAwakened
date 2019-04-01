using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories
{
    [AutoloadEquip(EquipType.Face)]
    public class SoulStone : ModItem
    {
        
        public static ModItem _ref;
        public static Texture2D _glow;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Stone");
            Tooltip.SetDefault(
@"+6 max minions
Allows you to detect the souls of creatures, detect valuable resources, and see traps
'I have lost more than you can imagine'");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 18;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.rare = 11;
            item.accessory = true;
            

        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Color.Orange;
                }
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void UpdateEquip(Player player)
        {
            player.AddBuff(BuffID.Spelunker, 2);
            player.AddBuff(BuffID.Hunter, 2);
            player.AddBuff(BuffID.Dangersense, 2);
            player.maxMinions += 6;
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
                    if (slot != i && player.armor[i].type == mod.ItemType<MindStone>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == mod.ItemType<InfinityGauntlet>())
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