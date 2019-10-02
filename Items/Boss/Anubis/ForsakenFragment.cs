namespace AAMod.Items.Boss.Anubis
{
    public class ForsakenFragment : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Forsaken Fragment");
		}

        public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.maxStack = 99;
			item.value = 20000;
			item.rare = 5;
		}
	}
}