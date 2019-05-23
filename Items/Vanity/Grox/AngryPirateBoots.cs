using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Grox
{
    [AutoloadEquip(EquipType.Legs)]
	public class AngryPirateBoots : ModItem
	{
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Angry Pirate's Legguards");
            Tooltip.SetDefault(@"Hatred towards fish that can't code radiates from these boots.
'Great for impersonating Ancients Awakened Devs!'");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(89, 119, 71);
                }
            }
        }

        public override bool DrawLegs()
        {
            return false;
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 20;
            item.rare = 7;
            item.vanity = true;
        }
    }
}