using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace AAMod.Items.Vanity.Gibs

{
    [AutoloadEquip(EquipType.Body)]
    public class GibsPlate : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Revenant Plate");
            Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Developers!'");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(255, 128, 0);
                }
            }
        }

        public override bool DrawBody()
        {
            return false;
        }

        public override void DrawHands(ref bool drawHands, ref bool drawArms)
        {
            drawHands = false;
            drawArms = false;
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.rare = ItemRarityID.Red;
            item.vanity = true;
        }
    }
}