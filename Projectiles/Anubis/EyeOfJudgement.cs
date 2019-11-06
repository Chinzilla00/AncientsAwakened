using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace AAMod.Projectiles.Anubis
{
    public class EyeOfJudgement : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Eye of Judgement");
			projectile.light = 0.5f;
		}

		public override void SetDefaults()
		{
			projectile.width = 56;
			projectile.height = 42;
			projectile.penetrate = -1;
			projectile.timeLeft = 900;
			projectile.tileCollide = false;
			projectile.hostile = false;
			projectile.friendly = false;
			projectile.extraUpdates = 0;
			projectile.sentry = true;
		}
		
		public override void AI()
        {
            Lighting.AddLight(projectile.Center, Color.Gold.R / 255, Color.Gold.G / 255, Color.Gold.B / 255);
            Player player = Main.player[projectile.owner];
			projectile.Center = player.Center;
			projectile.position.Y = player.Center.Y-90;
			projectile.spriteDirection = player.direction;
			if (player.dead || !player.HasBuff(mod.BuffType("EyeOfJudgement")))
			{
				projectile.Kill();
			}
			
			for (int i = 0; i < 200; i++)
            {
                NPC target = Main.npc[i];
 
                float shootToX = target.position.X + target.width * 0.5f - projectile.Center.X;
                float shootToY = target.position.Y + target.height * 0.5f - projectile.Center.Y;
                float distance = (float)Math.Sqrt(shootToX * shootToX + shootToY * shootToY);

                if (distance < 600f && target.catchItem == 0 && !target.friendly && target.active && target.type != 488)
                {
                    if (projectile.ai[0] > 30f) // Time in (60 = 1 second) 
                    {
                        distance = 1.6f / distance;

                        shootToX *= distance * 3;
                        shootToY *= distance * 3;
                        Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootToX*4, shootToY*4, 668, projectile.damage, projectile.knockBack, Main.myPlayer, 0f, 0f);
                        projectile.ai[0] = 0f;
                    }
                }
            }
            projectile.ai[0] += 1f;
		}
	}
}
