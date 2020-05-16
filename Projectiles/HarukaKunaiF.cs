using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class HarukaKunaiF : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.ThrowingKnife);
			projectile.width = 14;
			projectile.height = 34;
			projectile.friendly = true;
            projectile.hostile = false;
			projectile.timeLeft = 1200;
			projectile.penetrate = 1;
            projectile.thrown = false;
            projectile.ranged = true;
			aiType = ProjectileID.ShadowFlameKnife;
		}
		
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			// For going through platforms and such, javelins use a tad smaller size
			width = height = 10; // notice we set the width to the height, the height to 10. so both are 10
			return true;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Abyssal Kunai");
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Buffs.HydraToxin>(), 180);
            projectile.netUpdate = true;
        }

        public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
			{
				int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<Dusts.CthulhuAuraDust>(), projectile.oldVelocity.X * 0.1f, projectile.oldVelocity.Y * 0.1f);
			}
			Main.PlaySound(SoundID.Dig, (int)projectile.position.X, (int)projectile.position.Y, 0);
		}
	}
}