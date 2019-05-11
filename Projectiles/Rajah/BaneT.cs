using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Rajah
{
    public class BaneT : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bane of the Bunny");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.hide = true;
            projectile.MaxUpdates = 2;
        }

        public override void AI()
        {
            int num972 = 63;
            if (projectile.alpha > 0)
            {
                projectile.alpha -= num972;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            if (projectile.ai[0] == 0f)
            {
                int num973 = (int)projectile.ai[1];
                if (!Main.npc[num973].CanBeChasedBy(this, true))
                {
                    projectile.Kill();
                    return;
                }
                projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 0.785f;
                Vector2 vector122 = Main.npc[num973].Center - projectile.Center;
                if (vector122 != Vector2.Zero)
                {
                    vector122.Normalize();
                    vector122 *= 14f;
                }
                float num974 = 5f;
                projectile.velocity = (projectile.velocity * (num974 - 1f) + vector122) / num974;
            }
            if (projectile.ai[0] == 1f)
            {
                projectile.ignoreWater = true;
                projectile.tileCollide = false;
                int num977 = 5 * projectile.MaxUpdates;
                bool flag53 = false;
                bool flag54 = false;
                projectile.localAI[0] += 1f;
                if (projectile.localAI[0] % 30f == 0f)
                {
                    flag54 = true;
                }
                int num978 = (int)projectile.ai[1];
                if (projectile.localAI[0] >= (float)(60 * num977))
                {
                    flag53 = true;
                }
                else if (num978 < 0 || num978 >= 200)
                {
                    flag53 = true;
                }
                else if (Main.npc[num978].active && !Main.npc[num978].dontTakeDamage)
                {
                    projectile.Center = Main.npc[num978].Center - projectile.velocity * 2f;
                    projectile.gfxOffY = Main.npc[num978].gfxOffY;
                    if (flag54)
                    {
                        Main.npc[num978].HitEffect(0, 1.0);
                    }
                }
                else
                {
                    flag53 = true;
                }
                if (flag53)
                {
                    projectile.Kill();
                }
            }
        }
    }
}
