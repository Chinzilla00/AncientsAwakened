using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.Paintings
{
    public class DaedalusPainting : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lord of Light");
            Tooltip.SetDefault("'Does what's right...even if it's not the best thing to do.'");
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
            item.createTile = mod.TileType("DaedalusPainting");
        }
    }
}