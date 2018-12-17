using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Items.Vanity.Grox
{
	[AutoloadEquip(EquipType.Wings)]
	public class AncientGroviteWings : ModItem
    {
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Grovite Wings");
            BaseUtility.AddTooltips(item, new string[] { "'Wings made of a plantlike material not of this world'" });	
		}		
		
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.rare = 2;
            item.value = BaseUtility.CalcValue(0, 0, 3, 0);
            item.accessory = true;			
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(31, 77, 37);
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.wingTimeMax = 260;
		}

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			ascentWhenFalling = 1.2f;
			ascentWhenRising = 0.3f;
			maxCanAscendMultiplier = 1.2f;
			maxAscentMultiplier = 4f;
			constantAscend = 0.3f;
		}

		public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
		{
			speed = Math.Max(speed, 14f);
			acceleration *= 2.2f;
		}

        public override void UpdateEquip(Player p)
        {
			p.slowFall = true;
		}
	}
}