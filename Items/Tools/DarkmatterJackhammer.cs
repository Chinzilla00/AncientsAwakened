using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class DarkmatterJackhammer : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Darkmatter Jackhammer");
        }

		public override void SetDefaults()
		{
			item.damage = 60;
			item.melee = true;
			item.width = 52;
            item.height = 22;
			item.useTime = 7;
			item.useAnimation = 15;
            item.channel = true;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.hammer = 120;
			item.useStyle = 5;
			item.knockBack = 6;
			item.value = 550000;
			item.rare = 10;
            item.UseSound = SoundID.Item23;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("DarkmatterJackhammerPro");
            item.shootSpeed = 40f;
            item.tileBoost += 1;
        }
    }
}
