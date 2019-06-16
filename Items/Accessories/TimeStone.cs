using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories
{
    [AutoloadEquip(EquipType.Face)]
    public class TimeStone : ModItem
    {
        

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Time Stone");
            Tooltip.SetDefault(
@"Respawn time cut by 80%
Pressing the Time Stone hotkey will allow you to speed up and resume time.
'Dread it. Run from it. Destiny still arives.'");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 16));
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.width = 54;
            item.height = 52;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.rare = 11;
            item.accessory = true;
            item.consumable = false;
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
                    line2.overrideColor = Color.Green;
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<AAPlayer>().Time = true;
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
                    if (slot != i && player.armor[i].type == mod.ItemType<RealityStone>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == mod.ItemType<InfinityGauntlet>())
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