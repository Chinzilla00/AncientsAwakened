using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.Graphics.Shaders;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Zero
{
    public class RedBullet : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("RedBullet");
            ProjectileID.Sets.Homing[projectile.type] = true;
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 12;
            projectile.timeLeft = 600;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.alpha = 0;
        }


        public override void AI()
        {
          for (int num163 = 0; num163 < 10; num163++) // Spawns 10 dust every ai update (I have projectile.extraUpdates = 1; so it may actually be 20 dust per ai update)
                    {
                        float x2 = projectile.Center.X- projectile.velocity.X / -10f * num163;
                        float y2 = projectile.Center.Y- projectile.velocity.Y / -10f * num163;
                        int num164 = Dust.NewDust(new Vector2(x2, y2), 1, 1, ModContent.DustType<Dusts.RealityDust>(), 0f, 0f, 0, default(Color), 1f);
                        Main.dust[num164].alpha = projectile.alpha;
                        Main.dust[num164].position.X = x2;
                        Main.dust[num164].position.Y = y2;
                        Main.dust[num164].velocity *= 0f;
                        Main.dust[num164].noGravity = true;
                        Main.dust[num164].fadeIn *= 1.8f;
                        Main.dust[num164].scale = 0.7f;
                    }

            float num165 = (float)Math.Sqrt(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y);
            float num166 = projectile.localAI[0];
            if (num166 == 0f)
            {
                projectile.localAI[0] = num165;
                num166 = num165;
            }
            float num167 = projectile.position.X;
            float num168 = projectile.position.Y;
            float num169 = 300f;
            bool flag4 = false;
            int num170 = 0;
            if (projectile.ai[1] == 0f)
            {
                for (int num171 = 0; num171 < 200; num171++)
                {
                    if (!Main.npc[num171].CanBeChasedBy(projectile) || projectile.ai[1] != 0f && projectile.ai[1] != num171 + 1) continue;
                    float num172 = Main.npc[num171].position.X + Main.npc[num171].width / 2f;
                    float num173 = Main.npc[num171].position.Y + Main.npc[num171].height / 2f;
                    float num174 = Math.Abs(projectile.position.X + projectile.width / 2f - num172) + Math.Abs(projectile.position.Y + projectile.height / 2f - num173);
                    if (!(num174 < num169) || !Collision.CanHit(new Vector2(projectile.position.X + projectile.width / 2f, projectile.position.Y + projectile.height / 2f), 1, 1,
                            Main.npc[num171].position, Main.npc[num171].width, Main.npc[num171].height)) continue;
                    num169 = num174;
                    num167 = num172;
                    num168 = num173;
                    flag4 = true;
                    num170 = num171;
                }

                if (flag4) projectile.ai[1] = num170 + 1;
                flag4 = false;
            }

            if (projectile.ai[1] > 0f)
            {
                int num175 = (int)(projectile.ai[1] - 1f);
                if (Main.npc[num175].active && Main.npc[num175].CanBeChasedBy(projectile, true) && !Main.npc[num175].dontTakeDamage)
                {
                    float num176 = Main.npc[num175].position.X + Main.npc[num175].width / 2;
                    float num177 = Main.npc[num175].position.Y + Main.npc[num175].height / 2;
                    float num178 = Math.Abs(projectile.position.X + projectile.width / 2 - num176) + Math.Abs(projectile.position.Y + projectile.height / 2 - num177);
                    if (num178 < 1000f)
                    {
                        flag4 = true;
                        num167 = Main.npc[num175].position.X + Main.npc[num175].width / 2;
                        num168 = Main.npc[num175].position.Y + Main.npc[num175].height / 2;
                    }
                }
                else
                {
                    projectile.ai[1] = 0f;
                }
            }

            if (!projectile.friendly) flag4 = false;
            if (flag4)
            {
                float num179 = num166;
                Vector2 vector19 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                float num180 = num167 - vector19.X;
                float num181 = num168 - vector19.Y;
                float num182 = (float)Math.Sqrt(num180 * num180 + num181 * num181);
                num182 = num179 / num182;
                num180 *= num182;
                num181 *= num182;
                int num183 = 8;
                projectile.velocity.X = (projectile.velocity.X * (num183 - 1) + num180) / num183;
                projectile.velocity.Y = (projectile.velocity.Y * (num183 - 1) + num181) / num183;
            }
        }

    }
}
