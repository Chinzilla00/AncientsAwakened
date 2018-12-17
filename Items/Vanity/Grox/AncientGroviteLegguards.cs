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
	[AutoloadEquip(EquipType.Legs)]
	public class AncientGroviteLegguards : ModItem
    {
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Grovite Legguards");
            BaseUtility.AddTooltips(item, new string[] { "'Boots made of a plantlike material not of this world'" });
        }		

		public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.rare = 2;
            item.vanity = true;
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
	}
}