using System;
using System.IO;
using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
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
            projectile.alpha = 120;
            cooldownSlot = 1;
            projectile.timeLeft = 60;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(250, 250, 250, 0);
        }

        public override void AI()
        {
            projectile.timeLeft --;
            if (projectile.timeLeft <= 0)
            {
                projectile.Kill();
            }
            Lighting.AddLight(projectile.Center, ((255 - projectile.alpha) * 0.9f) / 255f, ((255 - projectile.alpha) * 0f) / 255f, ((255 - projectile.alpha) * 0.9f) / 255f);
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (projectile.ai[1] == 0f)
            {
                projectile.ai[1] = 1f;
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 20);
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
            target.AddBuff(projectile.ai[0] == 1 ? mod.BuffType("DiscordInferno") : projectile.ai[0] == 2 ? mod.BuffType("HydraToxin") : mod.BuffType("DiscordInferno"), 300);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            int ShaderType = projectile.ai[0] == 1 ? mod.ItemType<Items.Dyes.BlazingDye>() : projectile.ai[0] == 2 ? mod.ItemType<Items.Dyes.AbyssalDye>() : mod.ItemType<Items.Dyes.DiscordianDye>();
            int shader = GameShaders.Armor.GetShaderIdFromItemId(ShaderType);
            Rectangle frame = BaseDrawing.GetFrame(projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type], 0, 2);
            BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], shader, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, Main.projFrames[projectile.type], frame, Color.White, true);
            return false;
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
		   			offsetAngle = (startAngle + Angle * ( i + i * i ) / 2f ) + 32f * i;
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