using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Glitched
{
    [AutoloadEquip(EquipType.Body)]
	public class GlitchesBreastplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Gl17cH's Breastplate");
			Tooltip.SetDefault("Great for impersonating AA devs!");
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 20;
			item.rare = 10;
			item.vanity = true;
		}
	}
}