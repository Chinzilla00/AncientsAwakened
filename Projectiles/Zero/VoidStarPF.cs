using System;
using Microsoft.Xna.Framework;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Zero
{
    class VoidStarPF : ModProjectile
    {
        public short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            if (Main.netMode != 2)
            {
                Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
            projectile.glowMask = customGlowMask;
        }

        public override void SetDefaults()
        {
            projectile.width = 60;
            projectile.height = 60;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 300;
            projectile.hide = true;
            projectile.aiStyle = -1;
            projectile.alpha = 100;
            projectile.ignoreWater = true;

        }

        public override void AI()
        {
            float num472 = projectile.Center.X;
            float num473 = projectile.Center.Y;
            float num474 = 400f;
            bool flag17 = false;
            for (int num475 = 0; num475 < 200; num475++)
            {
                if (Main.npc[num475].CanBeChasedBy(projectile, false) && Collision.CanHit(projectile.Center, 1, 1, Main.npc[num475].Center, 1, 1))
                {
                    float num476 = Main.npc[num475].position.X + (float)(Main.npc[num475].width / 2);
                    float num477 = Main.npc[num475].position.Y + (float)(Main.npc[num475].height / 2);
                    float num478 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num476) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num477);
                    if (num478 < num474)
                    {
                        num474 = num478;
                        num472 = num476;
                        num473 = num477;
                        flag17 = true;
                    }
                }
            }
            if (flag17)
            {
                float num483 = 20f;
                Vector2 vector35 = new Vector2(projectile.position.X + ((float)projectile.width * 0.5f), projectile.position.Y + ((float)projectile.height * 0.5f));
                float num484 = num472 - vector35.X;
                float num485 = num473 - vector35.Y;
                float num486 = (float)Math.Sqrt((double)((num484 * num484) + (num485 * num485)));
                num486 = num483 / num486;
                num484 *= num486;
                num485 *= num486;
                projectile.velocity.X = ((projectile.velocity.X * 20f) + num484) / 21f;
                projectile.velocity.Y = ((projectile.velocity.Y * 20f) + num485) / 21f;
                projectile.rotation += projectile.direction * 0.8f;
                projectile.ai[0] += 1f;
                if (projectile.ai[0] >= 30f)
                {
                    if (projectile.ai[0] < 100f)
                    {
                        projectile.velocity *= 1.00f;
                    }
                    else
                    {
                        projectile.ai[0] = 200f;
                    }
                }
                for (int num257 = 0; num257 < 2; num257++)
                {
                    int num258 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.VoidDust>(), 0f, 0f, 100, new Color(120, 0, 30), 1f);
                    Main.dust[num258].noGravity = true;
                }
                return;
            }
            projectile.rotation += projectile.direction * 0.8f;
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 30f)
            {
                if (projectile.ai[0] < 100f)
                {
                    projectile.velocity *= 1.00f;
                }
                else
                {
                    projectile.ai[0] = 200f;
                }
            }
            for (int num257 = 0; num257 < 2; num257++)
            {
                int num258 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.VoidDust>(), 0f, 0f, 100, new Color(120, 0, 30), 1f);
                Main.dust[num258].noGravity = true;
            }
            return;
        }
    }
}
