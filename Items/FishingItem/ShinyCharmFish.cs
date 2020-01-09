namespace AAMod.Items.FishingItem
{
    public class ShinyCharmFish : BaseAAItem
	{
		public override void SetDefaults()
		{
			item.width = 34;
            item.height = 36;
            item.maxStack = 99;
            item.rare = 9;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shiny Charm Fish");
			Tooltip.SetDefault("A kind of rare fish");
		}
    }
}
