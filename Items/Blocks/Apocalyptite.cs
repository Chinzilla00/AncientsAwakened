using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Blocks
{
    public class Apocalyptite : BaseAAItem
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Apocalyptite");
            Tooltip.SetDefault(@"");
        }

        public override void SetDefaults()
        {

            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.rare = ItemRarityID.Red;
            item.consumable = true;
            item.createTile = mod.TileType("Apocalyptite"); //put your CustomBlock Tile name
            
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
