using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class DoomStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Doom Rod");
		}

		public override void SetDefaults()
		{
			item.damage = 19;
			item.magic = true;
			item.mana = 6;
			item.width = 42;
			item.height = 42;
			item.useTime = 28;
			item.useAnimation = 28;
			item.useStyle = 1;
			item.noMelee = true;
			item.knockBack = 5;
			item.value = 1000;
			item.rare = 2;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("DoomProj");
			item.shootSpeed = 3f;
		}
	}
}