using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    class OroborosWood : ModItem
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
            item.consumable = true;
            item.createTile = mod.TileType("OroborosWood"); //put your CustomBlock Tile name
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {

                    line2.overrideColor = new Color(100, 0, 10);

                    line2.overrideColor = AAColor.Zero;
//
                }
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Oroboros Wood");
            Tooltip.SetDefault("");
        }
    }
}
