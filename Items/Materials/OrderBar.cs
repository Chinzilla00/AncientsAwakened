namespace AAMod.Items.Materials
{
    public class OrderBar : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Order Bar");
            Tooltip.SetDefault("Glows with the power of unity");
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
			item.maxStack = 99;
            item.rare = 2;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.rare = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("OrderBar");
        }
    }
}
