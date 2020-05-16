using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.NPCs.Bosses.Core.Projectiles
{
    public class Rainbow : ModProjectile
    {
        public override void SetDefaults()
		{
			projectile.width = 12;
			projectile.height = 12;
			projectile.penetrate = -1;
			projectile.alpha = 255;
			projectile.ignoreWater = true;
			projectile.scale = 1.25f;
			projectile.hostile = true;
			projectile.friendly = false;
		}

        public override void AI()
        {
			float num = Main.DiscoR / 255f;
			float num2 = Main.DiscoG / 255f;
			float num3 = Main.DiscoB / 255f;
			num = (num + 1f) / 2f;
			num2 = (num2 + 1f) / 2f;
			num3 = (num3 + 1f) / 2f;
			num *= projectile.light;
			num2 *= projectile.light;
			num3 *= projectile.light;

			Lighting.AddLight((int)((projectile.position.X + (projectile.width / 2)) / 16f), (int)((projectile.position.Y + (projectile.height / 2)) / 16f), num, num2, num3);
			
			int num421 = 40;
			if (projectile.ai[1] == 0)
			{
				if (projectile.owner == Main.myPlayer)
				{
					projectile.localAI[0] += 1f;
					if (projectile.localAI[0] > 4f)
					{
						projectile.localAI[0] = 3f;
						Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X * 0.001f, projectile.velocity.Y * 0.001f, ProjectileID.RainbowBack, projectile.damage, projectile.knockBack, projectile.owner, 0f, 1f);
					}
					if (projectile.timeLeft > num421)
					{
						projectile.timeLeft = num421;
					}
				}
				float num422 = 1f;
				if (projectile.velocity.Y < 0f)
				{
					num422 -= projectile.velocity.Y / 3f;
				}
				projectile.ai[0] += num422;
				if (projectile.ai[0] > 30f)
				{
					projectile.velocity.Y = projectile.velocity.Y + 0.5f;
					if (projectile.velocity.Y > 0f)
					{
						projectile.velocity.X = projectile.velocity.X * 0.95f;
					}
					else
					{
						projectile.velocity.X = projectile.velocity.X * 1.05f;
					}
				}
				float num423 = projectile.velocity.X;
				float num424 = projectile.velocity.Y;
				float num425 = (float)Math.Sqrt(num423 * num423 + num424 * num424);
				num425 = 15.95f * projectile.scale / num425;
				num423 *= num425;
				num424 *= num425;
				projectile.velocity.X = num423;
				projectile.velocity.Y = num424;
				projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) - 1.57f;
				return;
			}
			if (projectile.localAI[0] == 0f)
			{
				if (projectile.velocity.X > 0f)
				{
					projectile.spriteDirection = -1;
					projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) - 1.57f;
				}
				else
				{
					projectile.spriteDirection = 1;
					projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) - 1.57f;
				}
				projectile.localAI[0] = 1f;
				projectile.timeLeft = num421;
			}
			projectile.velocity.X = projectile.velocity.X * 0.98f;
			projectile.velocity.Y = projectile.velocity.Y * 0.98f;
			if (projectile.rotation == 0f)
			{
				projectile.alpha = 255;
				return;
			}
			if (projectile.timeLeft < 10)
			{
				projectile.alpha = 255 - (int)(255f * projectile.timeLeft / 10f);
				return;
			}
			if (projectile.timeLeft > num421 - 10)
			{
				int num426 = num421 - projectile.timeLeft;
				projectile.alpha = 255 - (int)(255f * num426 / 10f);
				return;
			}
			projectile.alpha = 0;
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = 6;
			height = 6;
			return true;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			if (projectile.ai[1] == 0)
			{
				return Color.Transparent;
			}
			else
			{
				int num = 255 - projectile.alpha;
				int num2 = 255 - projectile.alpha;
				int num3 = 255 - projectile.alpha;
				return new Color(num, num2, num3, 0);
			}
		}

        public override bool PreDraw(SpriteBatch sb, Color lightColor)
		{
			int num147 = 18;
			int num148 = -10;
			float num149 = Main.projectileTexture[projectile.type].Width - projectile.width * 0.5f + projectile.width * 0.5f;
			Main.spriteBatch.Draw(Main.projectileTexture[projectile.type], new Vector2(projectile.position.X - Main.screenPosition.X + num149 + (float)num148, projectile.position.Y - Main.screenPosition.Y + (float)(projectile.height / 2) + projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height)), projectile.GetAlpha(lightColor), projectile.rotation, new Vector2(num149, (float)(projectile.height / 2 + num147)), projectile.scale, SpriteEffects.None, 0f);
			return false;
        }
    }
}