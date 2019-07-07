using Terraria.ID;

namespace AAMod.Items.Melee
{
    public class BackScratcher : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Back Scratcher");
		}

		public override void SetDefaults()
		{
			item.damage = 60;
			item.melee = true;
			item.width = 22;
			item.height = 28;
            item.scale *= 1.5f;
			item.useTime = 7;
			item.useAnimation = 7;
			item.useStyle = 1;
			item.knockBack = 0;
			item.value = 80000;
			item.rare = 5;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}
	}
}
