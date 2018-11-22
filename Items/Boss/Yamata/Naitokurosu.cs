using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Yamata
{
    [AutoloadEquip(EquipType.Neck)]
    public class Naitokurosu : ModItem
    {

        public static short customGlowMask = 0;


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Naitokurosu");
            Tooltip.SetDefault(@"Grants you the abilities of a true master ninja
Allows you to do a speedy dash
At night, you move twice as fast and your attacks inflict venom on your targes
From 11:00 PM to 1:00 AM, you move three times as fast and your ranged & throwing attacks inflict Moonraze");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 26;
            item.value = Item.sellPrice(3, 0, 0, 0);
            item.expert = true;
            item.accessory = true;
            item.glowMask = customGlowMask;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(146, 30, 68);
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.blackBelt = true;
            player.dash = 1;
            player.spikedBoots = 2;
            player.GetModPlayer<AAPlayer>().Naitokurosu = true;
            player.buffImmune[mod.BuffType("HydraToxin")] = true;
            player.buffImmune[mod.BuffType("Clueless")] = true;
            if (Main.dayTime)
            {
                player.moveSpeed += 0f;
            }
            if (!Main.dayTime && Main.time < 14400 && Main.time > 21600)
            {
                player.moveSpeed += 1f;
            }
            if (!Main.dayTime && Main.time >= 14400 && Main.time <= 21600)
            {
                player.moveSpeed += 2f;
            }
        }
    }
}