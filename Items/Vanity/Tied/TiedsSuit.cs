using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace AAMod.Items.Vanity.Tied
{
	[AutoloadEquip(EquipType.Body)]
	class TiedsSuit : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spooky Suit");
            Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Devs!'");
        }
        public override void SetDefaults()
		{
			item.width = 34;
			item.height = 22;
			item.rare = 9;
			item.vanity = true;
		}
        public override void DrawHands(ref bool drawHands, ref bool drawArms)
		{
			drawHands = false;
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(0, 105, 0);
                }
            }
        }
    }
}
