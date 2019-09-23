namespace AAMod.Items.Blocks
{
    public class Uranium : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.useStyle = 1;
            item.useTurn = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.autoReuse = true;
            item.maxStack = 999;
            item.consumable = true;
            item.width = 12;
            item.height = 12;
            item.value = 5500;
            item.rare = 8;
            item.createTile = mod.TileType("UraniumOre");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Uranium Ore");
        }
    }
}
