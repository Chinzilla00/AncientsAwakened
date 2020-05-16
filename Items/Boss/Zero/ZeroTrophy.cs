using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Boss.Zero
{
    public class ZeroTrophy : BaseAAItem
	{
        public static int type;

        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zero Trophy");
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
            item.rare = ItemRarityID.Cyan;
            AARarity = 13;
            item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.value = 2000;
			item.rare = 1;
			item.createTile = mod.TileType("ZeroTrophy");
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