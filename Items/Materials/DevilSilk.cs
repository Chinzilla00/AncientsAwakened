namespace AAMod.Items.Materials
{
    public class DevilSilk : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Devil Silk");
            Tooltip.SetDefault("Physical Sin; feels good, but it isn't a good long-lasting material");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 22;
			item.maxStack = 99;
            item.rare = 3;
        }
    }
}
