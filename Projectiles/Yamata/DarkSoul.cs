using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Yamata
{
    public class DarkSoul : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Soul");
        }

        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.aiStyle = 59;
            projectile.alpha = 255;
            projectile.ranged = true;
            projectile.tileCollide = false;
            projectile.extraUpdates = 3;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Moonraze"), 600);
        }

        public override void AI()
        {
            projectile.ai[1] += 1f;
            if (projectile.ai[1] >= 60f)
            {
                projectile.friendly = true;
                int num563 = (int)projectile.ai[0];
                if (!Main.npc[num563].active)
                {
                    int[] array2 = new int[200];
                    int num564 = 0;
                    for (int num565 = 0; num565 < 200; num565++)
                    {
                        if (Main.npc[num565].CanBeChasedBy(this, false))
                        {
                            float num566 = Math.Abs(Main.npc[num565].position.X + (float)(Main.npc[num565].width / 2) - projectile.position.X + (float)(projectile.width / 2)) + Math.Abs(Main.npc[num565].position.Y + (float)(Main.npc[num565].height / 2) - projectile.position.Y + (float)(projectile.height / 2));
                            if (num566 < 800f)
                            {
                                array2[num564] = num565;
                                num564++;
                            }
                        }
                    }
                    if (num564 == 0)
                    {
                        projectile.Kill();
                        return;
                    }
                    num563 = array2[Main.rand.Next(num564)];
                    projectile.ai[0] = (float)num563;
                }
                float num567 = 4f;
                Vector2 vector41 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                float num568 = Main.npc[num563].Center.X - vector41.X;
                float num569 = Main.npc[num563].Center.Y - vector41.Y;
                float num570 = (float)Math.Sqrt((double)(num568 * num568 + num569 * num569));
                num570 = num567 / num570;
                num568 *= num570;
                num569 *= num570;
                int num571 = 30;
                projectile.velocity.X = (projectile.velocity.X * (float)(num571 - 1) + num568) / (float)num571;
                projectile.velocity.Y = (projectile.velocity.Y * (float)(num571 - 1) + num569) / (float)num571;
            }
            for (int num572 = 0; num572 < 5; num572++)
            {
                float num573 = projectile.velocity.X * 0.2f * (float)num572;
                float num574 = -(projectile.velocity.Y * 0.2f) * (float)num572;
                int num575 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.YamataDustLight>(), 0f, 0f, 100, default(Color), 1.3f);
                Main.dust[num575].noGravity = true;
                Main.dust[num575].velocity *= 0f;
                Dust expr_178B4_cp_0 = Main.dust[num575];
                expr_178B4_cp_0.position.X -= num573;
                Dust expr_178D3_cp_0 = Main.dust[num575];
                expr_178D3_cp_0.position.Y -= num574;
            }
            return;
        }
    }
}
