using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Anubis.Forsaken
{
    public class EnchancedMummyArrowD : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Enchanced Mummy Arrow");
		}

		public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.height = 14;
			projectile.aiStyle = 1;
			projectile.friendly = true;
            projectile.ranged = true;
			projectile.penetrate = 1;
			projectile.ignoreWater = false;
			projectile.tileCollide = true;
            projectile.arrow = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 0;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
			target.buffImmune[mod.BuffType("Forsaken")] = false;
			target.AddBuff(mod.BuffType("Forsaken"), 10);
		}

        public override void Kill(int timeleft)
        {
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1);
            for (int num468 = 0; num468 < 4; num468++)
            {
                num468 = Dust.NewDust(projectile.Center, projectile.width, projectile.height, 32, -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default);
            }
        }
    }
}
