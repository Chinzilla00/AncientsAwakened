using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Blocks
{
    public class Doomstone : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.rare = 9;
            AARarity = 13;
            item.consumable = true;
            item.createTile = mod.TileType("Doomstone"); //put your CustomBlock Tile name
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

        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("Charged Doomstone");
            Tooltip.SetDefault("");

        }
    }
}
