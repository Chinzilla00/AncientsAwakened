using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories
{
    [AutoloadEquip(EquipType.Face, EquipType.Wings)]
    public class RealityStone : ModItem
    {
        

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reality Stone");
            Tooltip.SetDefault(
@"Grants you control over reality around you allowing long flight, insane speed, and uninhibited movement
'Now...reality can be whatever I want it to be...'");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 13));
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 36;
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
                    line2.overrideColor = Color.DarkRed;
                }
            }
        }

        public override void UpdateEquip(Player player)
        {
            player.accRunSpeed = 10;
            player.moveSpeed += 1f;
            player.iceSkate = true;
            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaImmune = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 500;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 1f;
            ascentWhenRising = 0.4f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 4f;
            constantAscend = 0.3f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 20f;
            acceleration *= 3f;
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
                    if (slot != i && player.armor[i].type == mod.ItemType<SoulStone>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == mod.ItemType<InfinityGauntlet>())
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