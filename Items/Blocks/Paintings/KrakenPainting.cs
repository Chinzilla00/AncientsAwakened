using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.Paintings
{
    public class KrakenPainting : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sea of Nightmares");
            Tooltip.SetDefault("'There are things in that ship no man should see...'");
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
            item.useStyle = 1;
            item.consumable = true;
            item.rare = ItemRarityID.Blue;
            item.createTile = mod.TileType("KrakenPainting");
        }
    }
}