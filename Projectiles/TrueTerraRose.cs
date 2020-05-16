using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Projectiles
{
    public class TrueTerraRose : ModProjectile
	{
		public static Color lightColor = new Color(0, 150, 50);

		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Terra Rose");
			Main.projFrames[projectile.type] = 2;
		}	

        public override void SetDefaults()
        {
            projectile.width = 34;
            projectile.height = 34;
            projectile.aiStyle = -1;
            projectile.timeLeft = 320;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.damage = 1;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.magic = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 9;
        }

		public override void AI()
		{
			BaseAI.AIVilethorn(projectile, 50, 4, 20);
			if (projectile.ai[1] == 20)
			{
				projectile.frame = 0;
			}
			else
			{
				projectile.frame = 1;
			}
		}

		public override void PostAI()
		{
			if (Main.netMode != NetmodeID.Server && projectile.alpha < 170 && projectile.alpha + 5 >= 170)
			{
				for (int j = 0; j < 4; j++)
				{
					Dust.NewDust(projectile.position, projectile.width, projectile.height, 48, projectile.velocity.X * 0.025f, projectile.velocity.Y * 0.025f, 107, Color.White, j == 0 ? 1.1f : 1.2f);
				}
			}
		}

		public override bool PreDraw(SpriteBatch sb, Color drawColor)
		{
			Rectangle frame = BaseDrawing.GetFrame(projectile.frame, 34, 34, 0, 0);

			Color newLightColor = new Color(Math.Max(0, lightColor.R + Math.Min(0, -projectile.alpha + 20)), Math.Max(0, lightColor.G + Math.Min(0, -projectile.alpha + 20)), Math.Max(0, lightColor.B + Math.Min(0, -projectile.alpha + 20)));
			BaseDrawing.AddLight(projectile.Center, newLightColor);
			BaseDrawing.DrawTexture(sb, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.direction, 2, frame, projectile.GetAlpha(Color.White), true);
			return false;
		}
	}
}