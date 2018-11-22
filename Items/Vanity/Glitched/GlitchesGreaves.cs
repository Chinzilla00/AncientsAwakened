using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Glitched
{
    [AutoloadEquip(EquipType.Legs)]
	public class GlitchesGreaves : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gl17cH's Greaves");
			Tooltip.SetDefault("Great for impersonating AA devs!");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 18;
			item.rare = 10;
			item.vanity = true;
		}
	}
}