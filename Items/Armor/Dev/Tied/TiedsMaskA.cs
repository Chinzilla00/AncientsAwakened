using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Dev.Tied
{ 
    [AutoloadEquip(EquipType.Head)]
    public class TiedsMaskA : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spooky Skull");
            Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Devs!'");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;
            item.rare = 9;
            item.vanity = true;
        }

        public override bool DrawHead()
        {
            return false; 
        }
        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = drawAltHair = false;
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