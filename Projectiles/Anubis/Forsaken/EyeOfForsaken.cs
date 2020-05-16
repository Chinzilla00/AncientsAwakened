using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AAMod.Projectiles.Anubis.Forsaken
{
    public class EyeOfForsaken : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 56;
            projectile.height = 42;
			projectile.tileCollide = false;
            projectile.timeLeft = 900;
            projectile.ignoreWater = true;
            projectile.sentry = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
        }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Eye of the Forsaken");
		}
	
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, Color.DarkSeaGreen.R / 255, Color.DarkSeaGreen.G / 255, Color.DarkSeaGreen.B / 255);
            Player player = Main.player[projectile.owner];
			projectile.Center = player.Center;
			projectile.position.Y = player.Center.Y-90;
			projectile.spriteDirection = player.direction;
			if (player.dead || !player.HasBuff(mod.BuffType("EyeOfForsaken")))
			{
				projectile.Kill();
			}
			for (int i = 0; i < 200; i++)
            {
                NPC target = Main.npc[i];
 
                float shootToX = target.position.X + target.width * 0.5f - projectile.Center.X;
                float shootToY = target.position.Y + target.height * 0.5f - projectile.Center.Y;
                float distance = (float)Math.Sqrt(shootToX * shootToX + shootToY * shootToY);

                if (distance < 600f && target.catchItem == 0 && !target.friendly && target.active && target.type != NPCID.TargetDummy)
                {
                    if (projectile.ai[0] > 30f) // Time in (60 = 1 second) 
                    {
                        distance = 1.6f / distance;

                        shootToX *= distance * 3;
                        shootToY *= distance * 3;
                        int id = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootToX*4, shootToY*4, mod.ProjectileType("ForsakenFrag"), projectile.damage, projectile.knockBack, Main.myPlayer, 0f, 0f);
                        Main.projectile[id].magic = false;
                        Main.projectile[id].minion = true;
                        projectile.ai[0] = 0f;
                    }
                }
            }
            projectile.ai[0] += 1f;
		}
	}
}