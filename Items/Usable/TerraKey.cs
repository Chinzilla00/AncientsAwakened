namespace AAMod.Items.Usable
{
    public class TerraKey : BaseAAItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Keep Key");
			Tooltip.SetDefault("A very ornate key");
		}

        public override void SetDefaults()
        {
            item.width = item.height = 16;
            item.rare = 7;
            item.maxStack = 99;
			item.value = 0;
            item.noMelee = true;
        }
    }
}
