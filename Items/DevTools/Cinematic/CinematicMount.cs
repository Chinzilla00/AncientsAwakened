using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.DevTools.Cinematic
{
	public class CinematicMount : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cinematic Mount");
		}

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 28;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.value = Item.sellPrice(0, 0, 0, 0);
			item.rare = -12;
			item.UseSound = SoundID.Item25;
			item.noMelee = true;
			item.mountType = mod.MountType("CinematicThing");
		}
	}
}