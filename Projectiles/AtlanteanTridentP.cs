using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class AtlanteanTridentP : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";
        public override void SetDefaults()
		{
			projectile.CloneDefaults(14);
			projectile.penetrate = 1;
			projectile.width = 16;
			projectile.height = 16;
			projectile.ranged = false;
			projectile.magic = true;
			projectile.friendly = true;
			projectile.timeLeft = 300;
			projectile.alpha = 150;
			projectile.aiStyle = 1;
			aiType = 14;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Atlantean Trident Water Sphere");
		}

		public override void AI()
		{
			projectile.alpha = 150;
			for (int index1 = 0; index1 < 2; ++index1)
			{
				int index2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 103, (float)(-projectile.velocity.X * 0.2), (float)(-projectile.velocity.Y * 0.2), 50, new Color(), 2f);
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity *= 2f;
				Main.dust[index2].scale *= 0.75f;
				int index3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 103, (float)(-projectile.velocity.X * 0.2), (float)(-projectile.velocity.Y * 0.2), 50, new Color(), 1f);
				Main.dust[index3].velocity *= 2f;
				Main.dust[index3].scale *= 0.75f;
				Main.dust[index3].noGravity = true;
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			projectile.Kill();
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.Kill();
			return true;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 54);
			for (int index1 = 0; index1 < 10; ++index1)
			{
				int index2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 103, 0.0f, 0.0f, 50, new Color(), 3.5f);
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity *= 2f;
				int index3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 103, 0.0f, 0.0f, 50, new Color(), 1.5f);
				Main.dust[index3].velocity *= 2f;
			}
		}
	}
}
