using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class GroxNote : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("GRealm Advertisement");
            Tooltip.SetDefault(@"'Want my dev weapon? Go play GRealm!'
-Grox The Great");
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

        public override void SetDefaults()
		{
            item.width = 22;
            item.height = 22;
            item.value = 0;
            item.rare = 0;
        }
	}
}
