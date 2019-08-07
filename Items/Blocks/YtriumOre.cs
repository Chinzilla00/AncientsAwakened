namespace AAMod.Items.Blocks
{
    public class YtriumOre : BaseAAItem
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
            item.createTile = mod.TileType("YtriumOre");
            item.rare = 2;
        }

        public override void SetStaticDefaults()
        {
          DisplayName.SetDefault("Yttrium Ore");
          Tooltip.SetDefault("");
        }

    }
}
