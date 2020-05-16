using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace AAMod.Items.Vanity.Blazen

{
    [AutoloadEquip(EquipType.Body)]
    public class BlazenPlate : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Tactical Assault Armor");
            Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Developers!'");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(0, 0, 255);
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 20;
            item.rare = ItemRarityID.Cyan;
            item.vanity = true;
        }
    }
}