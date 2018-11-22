using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class GladiatorsGlory : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gladiator's Glory");
		}
		public override void SetDefaults()
		{
			item.damage = 22;
			item.melee = true;
			item.width = 50;
			item.height = 52;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = 1;
			item.knockBack = 3;
			item.value = 2000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
		}
	}
}
