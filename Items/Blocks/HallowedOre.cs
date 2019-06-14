using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class HallowedOre : ModItem
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
            item.rare = 4;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("HallowedOre");
            item.value = 10000;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hallowed Ore");
            Tooltip.SetDefault("It's super bright");
        }

    }
}
