using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Boss.Anubis.Forsaken
{
    public class SoulFragment : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Fragment");
		}

        public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.maxStack = 99;
			item.value = 20000;
			item.rare = ItemRarityID.Pink;
            item.rare = ItemRarityID.Cyan;
            AARarity = 12;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Globals.AAColor.Rarity12;
                }
            }
        }
    }
}