using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Shen
{
    public class ShenStorm : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Storm");
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.width = 50;
            projectile.height = 50;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            cooldownSlot = 1;
            projectile.timeLeft = 60;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B, 120);
        }

        public override void AI()
        {
            projectile.timeLeft --;
            if (projectile.timeLeft <= 0)
            {
                projectile.Kill();
            }
            Lighting.AddLight(projectile.Center, (255 - projectile.alpha) * 0.9f / 255f, (255 - projectile.alpha) * 0f / 255f, (255 - projectile.alpha) * 0.9f / 255f);
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
            if (projectile.ai[1] == 0f)
            {
                projectile.ai[1] = 1f;
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 20);
            }

            if (Main.rand.Next(3) == 0)
            {
                for (int m = 0; m < 3; m++)
                {
                    int dustID = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType<Dusts.Discord>(), 0f, 0f, 100, Color.White, 1.6f);
                    Main.dust[dustID].velocity = -projectile.velocity * 0.5f;
                    Main.dust[dustID].noLight = false;
                    Main.dust[dustID].noGravity = true;
                }
                int dustID2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType<Dusts.Discord>(), 0f, 0f, 100, Color.Purple, 2f);
                Main.dust[dustID2].velocity = -projectile.velocity * 0.5f;
                Main.dust[dustID2].noLight = false;
                Main.dust[dustID2].noGravity = true;
            }

            if (projectile.frameCounter++ > 6)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(projectile.ai[0] == 1 ? mod.BuffType("DragonFire") : projectile.ai[0] == 2 ? mod.BuffType("HydraToxin") : mod.BuffType("DiscordInferno"), 200);
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(new LegacySoundStyle(2, 89, Terraria.Audio.SoundType.Sound));
            float spread = 12f * 0.0174f;
			double startAngle = Math.Atan2(projectile.velocity.X, projectile.velocity.Y)- spread/2;
	    	double Angle = spread/30f;
	    	double offsetAngle;
	    	int i;
	    	if (projectile.owner == Main.myPlayer)
	    	{
		    	for (i = 0; i < 10; i++ )
		    	{
		   			offsetAngle = startAngle + Angle * ( i + i * i ) / 2f  + 32f * i;
		        	Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)( Math.Sin(offsetAngle) * 6f ), (float)( Math.Cos(offsetAngle) * 6f ), mod.ProjectileType("ShenRain"), projectile.damage, projectile.knockBack, projectile.owner, projectile.ai[0], 0f);
		        	Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)( -Math.Sin(offsetAngle) * 6f ), (float)( -Math.Cos(offsetAngle) * 6f ), mod.ProjectileType("ShenRain"), projectile.damage, projectile.knockBack, projectile.owner, projectile.ai[0], 0f);
		    	}
	    	}
        	for (int dust = 0; dust <= 5; dust++)
            {
                int dustType = projectile.ai[0] == 1 ? mod.DustType<Dusts.AkumaADust>() : projectile.ai[0] == 2 ? mod.DustType<Dusts.YamataADust>() : mod.DustType<Dusts.Discord>();
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, dustType, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
        	}
        }
    }
}