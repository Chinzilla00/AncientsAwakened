using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using BaseMod;
namespace AAMod.Items.Boss.Shen
{
    public class ShenTerratool_Axe : ModItem
    {

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Shen;
                }
            }
        }
        public override void SetDefaults()
        {
            item.melee = true;
            item.width = 60;
            item.height = 54;
			item.useStyle = 1;
            item.useTime = 4;
            item.useAnimation = 15;
            item.tileBoost += 25;
            item.knockBack = 3;
            item.value = 1000000;
            item.rare = 11;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.damage = 120;
            item.axe = 270;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terratool");
            Tooltip.SetDefault("Right Click to change tool types");
        }

        public override bool CanRightClick()
        {
            return true;
        }


        public override void RightClick(Player player)
        {
            item.TurnToAir();
            Item.NewItem(player.Center, mod.ItemType("ShenTerratool_Hammer"), 1, false, item.prefix, true, false);
        }
    }
}
