using Terraria.ID;

namespace AAMod.Items.Boss.Anubis
{
    public class JackalsWrath : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Jackal's Wrath");
            Tooltip.SetDefault("Shoots out a wall-piercing returning phantom blade on swing");
        }

		public override void SetDefaults()
		{
			item.autoReuse = true;
			item.useStyle = 1;
			item.useAnimation = 20;
			item.useTime = 20;
			item.knockBack = 5f;
			item.width = 24;
			item.height = 28;
			item.damage = 30;
			item.UseSound = SoundID.Item71;
			item.rare = 6;
			item.shoot = mod.ProjectileType("PhantomBlade");
			item.shootSpeed = 14f;
			item.value = 10000;
			item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
		}
	}
}
