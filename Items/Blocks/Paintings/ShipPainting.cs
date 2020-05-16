using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.Paintings
{
    public class ShipPainting : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gloomy Galleon");
            Tooltip.SetDefault("'That ship...I still don't understand how it sunk.'");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.rare = ItemRarityID.Blue;
            item.createTile = mod.TileType("ShipPainting");
        }
    }
}