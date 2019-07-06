using System;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Akuma
{
    public class SunSpearShot : ModProjectile
    {


        public short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            if (Main.netMode != 2)
            {
                Texture2D[] glowMasks = new Microsoft.Xna.Framework.Graphics.Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
            projectile.glowMask = customGlowMask;
            DisplayName.SetDefault("Sun Ray");
        }

        public override void SetDefaults()
        {
            projectile.alpha = 255;
            projectile.width = 10;
            projectile.height = 10;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.scale = 1.1f;
            projectile.penetrate = 3;
        }
		
		public override void AI()
		{
            projectile.ai[0] += 1f;
            if (projectile.ai[0] > 30f)
            {
                projectile.ai[0] = 30f;
                projectile.velocity.Y = projectile.velocity.Y + 0.25f;
                if (projectile.velocity.Y > 16f)
                {
                    projectile.velocity.Y = 16f;
                }
                projectile.velocity.X = projectile.velocity.X * 0.995f;
            }
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
            projectile.alpha -= 50;
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            if (projectile.owner == Main.myPlayer)
            {
                projectile.localAI[0] += 1f;
                if (projectile.localAI[0] >= 4f)
                {
                    projectile.localAI[0] = 0f;
                    int num559 = 0;
                    for (int num560 = 0; num560 < 1000; num560++)
                    {
                        if (Main.projectile[num560].active && Main.projectile[num560].owner == projectile.owner && Main.projectile[num560].type == 344)
                        {
                            num559++;
                        }
                    }
                    float num561 = projectile.damage * 0.8f;
                    if (num559 > 100)
                    {
                        float num562 = num559 - 100;
                        num562 = 1f - num562 / 100f;
                        num561 *= num562;
                    }
                    if (num559 > 100)
                    {
                        projectile.localAI[0] -= 1f;
                    }
                    if (num559 > 120)
                    {
                        projectile.localAI[0] -= 1f;
                    }
                    if (num559 > 140)
                    {
                        projectile.localAI[0] -= 1f;
                    }
                    if (num559 > 150)
                    {
                        projectile.localAI[0] -= 1f;
                    }
                    if (num559 > 160)
                    {
                        projectile.localAI[0] -= 1f;
                    }
                    if (num559 > 165)
                    {
                        projectile.localAI[0] -= 1f;
                    }
                    if (num559 > 170)
                    {
                        projectile.localAI[0] -= 2f;
                    }
                    if (num559 > 175)
                    {
                        projectile.localAI[0] -= 3f;
                    }
                    if (num559 > 180)
                    {
                        projectile.localAI[0] -= 4f;
                    }
                    if (num559 > 185)
                    {
                        projectile.localAI[0] -= 5f;
                    }
                    if (num559 > 190)
                    {
                        projectile.localAI[0] -= 6f;
                    }
                    if (num559 > 195)
                    {
                        projectile.localAI[0] -= 7f;
                    }
                    if (num561 > projectile.damage * 0.1f)
                    {
                        Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("SunSpearRain"), (int)num561, projectile.knockBack * 0.55f, projectile.owner, 0f, Main.rand.Next(3));
                        return;
                    }
                }
            }
        }
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Daybreak, 600);
        }
    }
}