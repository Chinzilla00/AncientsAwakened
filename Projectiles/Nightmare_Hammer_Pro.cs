using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Nightmare_Hammer_Pro : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.CloneDefaults(106);
			aiType = 106;
			projectile.light = 0f;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nightmare Hammer");

		}
	}
}