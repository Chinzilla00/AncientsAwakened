using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class MushroomBlock : ModItem
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
            item.rare = 1;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("MushroomBlock"); //put your CustomBlock Tile name
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mushroom Block");
        }

    }
}
