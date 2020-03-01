using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Shen
{
    public class ShenFireballFrag : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fireball");
            Main.projFrames[projectile.type] = 4;
        }

        public override void PostAI()
        {
            if (projectile.frameCounter++ > 5)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 40;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.hostile = true;
            projectile.timeLeft = 30;
            projectile.aiStyle = -1;
            cooldownSlot = 1;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 3; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.Discord>(), 0f, 0f, 100, default, 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
            if (Main.netMode != 1)
            {
                Vector2 vel = Vector2.Normalize(projectile.velocity);
                const float ai = 0.01f;
                for (int i = 0; i < 8; ++i)
                {
                    vel = vel.RotatedBy(Math.PI / 4);
                    Projectile.NewProjectile(projectile.Center, vel, mod.ProjectileType("ShenFireballAccel"), projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), ai);
                    Projectile.NewProjectile(projectile.Center, vel, mod.ProjectileType("ShenFireballAccel"), projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), -ai);
                }
            }
        }
    }
}