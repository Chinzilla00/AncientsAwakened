using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Vanity.Anarchy
{
    [AutoloadEquip(EquipType.Legs)]
	public class PristineLeggings : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Pristine Leggings");
            Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Contributors!'
For the record, Anarchy sprited this himself.");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(200, 200, 200);
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 22;
            item.rare = ItemRarityID.Cyan;
            item.vanity = true;
        }
    }
}