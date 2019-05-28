using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Projectiles
{
    public class AsgardianIce : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Icesickle");
            Main.projFrames[projectile.type] = 5;
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.IceSpike);
            projectile.hostile = false;
            projectile.friendly = true;
			projectile.penetrate = 5;
		}
        public override void PostAI()
        {
            if (projectile.frameCounter++ > 6)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame > 4)
                {
                    projectile.frame = 0;
                }
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 5;
            target.AddBuff(BuffID.Frostburn, 120);
        }
    }
}