using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Core.Projectiles
{
    public class SnowCloud : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Snowstorm");
			Main.projFrames[projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			projectile.netImportant = true;
			projectile.tileCollide = false;
			projectile.width = 28;
			projectile.height = 28;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
		}

        public override void AI()
        {

			Move(new Vector2(projectile.ai[0], projectile.ai[1]));
			if (Vector2.Distance(projectile.Center, new Vector2(projectile.ai[0], projectile.ai[1])) < 10)
			{
				Kill(projectile.timeLeft);
			}

			projectile.rotation += projectile.velocity.X * 0.02f;
			projectile.frameCounter++;
			if (projectile.frameCounter > 4)
			{
				projectile.frameCounter = 0;
				projectile.frame++;
				if (projectile.frame > 3)
				{
					projectile.frame = 0;
					return;
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<SnowCloud1>(), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
		}

		public void Move(Vector2 point)
		{
			float Speed = 16;

			float velMultiplier = 1f;
			Vector2 dist = point - projectile.Center;

			projectile.velocity = Vector2.Normalize(dist);
			projectile.velocity *= Speed;
			projectile.velocity *= velMultiplier;
		}
	}

	public class SnowCloud1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Snowstorm");
			Main.projFrames[projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			projectile.netImportant = true;
			projectile.width = 28;
			projectile.height = 28;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
		}

		public override void AI()
		{
			projectile.frameCounter++;
			if (projectile.frameCounter > 8)
			{
				projectile.frameCounter = 0;
				projectile.frame++;
				if (projectile.frame > 5)
				{
					projectile.frame = 0;
				}
			}
			projectile.ai[1] += 1f;
			if (projectile.ai[1] >= 7200f)
			{
				projectile.alpha += 5;
				if (projectile.alpha > 255)
				{
					projectile.alpha = 255;
					projectile.Kill();
				}
			}
			else
			{
				projectile.ai[0] += 1f;
				if (projectile.ai[0] > 8f)
				{
					projectile.ai[0] = 0f;
					if (projectile.owner == Main.myPlayer)
					{
						int X = (int)(projectile.position.X + 14f + Main.rand.Next(projectile.width - 28));
						int Y = (int)(projectile.position.Y + projectile.height + 4f);
						Projectile.NewProjectile(X, Y, 0f, 5f, ModContent.ProjectileType<Snowflakes>(), projectile.damage, 0f, projectile.owner, 0f, 0f);
					}
				}
			}
		}
	}
}