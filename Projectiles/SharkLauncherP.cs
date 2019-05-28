using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class SharkLauncherP : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shark");
			Main.projFrames[projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(190);
			projectile.aiStyle = 39;
			aiType = 190;
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[projectile.owner] = 4;
		}
	}
}
