using System.Collections.Generic;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class DracoAltar : ModItem
	{

		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Draconian Sun Pedestal");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
            item.rare = 5;
            item.useStyle = 1;
			item.consumable = true;
			item.value = 500;
			item.createTile = mod.TileType("DracoAltar");
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity13;
                }
            }
        }
    }
}