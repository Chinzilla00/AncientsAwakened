using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace AAMod.Items.Vanity.Shox
{
    [AutoloadEquip(EquipType.Head)]
	public class ShoxVisor : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Shock Lord's Visor");
            Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Contributors!'");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(238, 168, 0);
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 20;
            item.rare = ItemRarityID.Cyan;
            item.vanity = true;
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = true;
        }
    }
}