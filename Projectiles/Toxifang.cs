using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Toxifang : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.CloneDefaults(14);
			projectile.penetrate = 1;
			projectile.width = 10;
			projectile.height = 12;
			projectile.ranged = true;
			projectile.magic = true;
			projectile.friendly = true;
			projectile.timeLeft = 300;
			projectile.aiStyle = 1;
			projectile.alpha = 0;
			aiType = 14;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Toxifang");
		}

		public override void AI()
		{
			projectile.alpha = 0;
			for (int index1 = 0; index1 < 2; ++index1)
			{
				int index2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 96, (float)(-projectile.velocity.X * 0.2), (float)(-projectile.velocity.Y * 0.2), 50, new Color(), 1f);
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity *= 1.5f;
				Main.dust[index2].scale *= 0.75f;
				int index3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 96, (float)(-projectile.velocity.X * 0.2), (float)(-projectile.velocity.Y * 0.2), 50, new Color(), 1f);
				Main.dust[index3].velocity *= 1.5f;
				Main.dust[index3].scale *= 0.75f;
				Main.dust[index3].noGravity = true;
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(mod.BuffType("Hydratoxin"), 600);
			projectile.Kill();
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.Kill();
			return true;
		}

		public override void Kill(int timeLeft)
        {
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1);
            for (int index1 = 0; index1 < 5; ++index1)
			{
				int index2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 96, 0.0f, 0.0f, 50, new Color(), 2.5f);
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity *= 2f;
				int index3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 96, 0.0f, 0.0f, 50, new Color(), 1.25f);
				Main.dust[index3].velocity *= 2f;
			}
		}
	}
}
