using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Toad
{
    public class MushrockStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Mushrock Staff");
		}

		public override void SetDefaults()
		{
			item.damage = 40;
			item.magic = true;
			item.mana = 6;
			item.width = 58;
			item.height = 58;
			item.useStyle = 1;
			item.noMelee = true;
			item.knockBack = 5;
			item.value = 1000000;
			item.rare = 4;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("ToadRock");
            item.useTime = 30;
            item.useAnimation = 30;
            item.shootSpeed = 15f;
        }
	}
}